using CNShopSolution.App.Common;
using CNShopSolution.App.Exceptions;
using CNShopSolution.Data.EF;
using CNShopSolution.Data.Entities;
using CNShopSolution.ViewModel.Catalog.ProductImages;
using CNShopSolution.ViewModel.Catalog.Products;
using CNShopSolution.ViewModel.Catalog.Products.Public;
using CNShopSolution.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CNShopSolution.App.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly CNShopDbContext _context;
        private readonly IStorageService _storageService;

        public ProductService(CNShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            var productImages = new ProductImage()
            {
                Caption = request.Caption,
                CreateDate = DateTime.Now,
                IsDefault = request.IsDefault,
                ProductId = productId,
                SortOrder = request.SortOrder,
            };
            if (request.ImageFile != null)
            {
                productImages.PathImages = await this.SaveFile(request.ImageFile);
                productImages.FileSize = request.ImageFile.Length;
            }

            _context.ProductImages.Add(productImages);
            await _context.SaveChangesAsync();
            return productImages.Id;
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                CreateDate = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                  new ProductTranslation()
                  {
                      Name = request.Name,
                      Description = request.Description,
                      Details = request.Details,
                      SeoDescription = request.SeoDescription,
                      SeoAlias = request.SeoAlias,
                      SeoTitle = request.SeoTitle,
                      LanguageId = request.LanguageId,
                  }
                }
            };
            //Save image
            if (request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail Image",
                        CreateDate = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        PathImages = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1,
                    }
                };
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new CNShopException($"Cannot find a product : {productId}");

            var images = _context.ProductImages.Where(x => x.ProductId == productId);

            foreach (var img in images)
            {
                await _storageService.DeleleFileAsync(img.PathImages);
            }

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public Task<List<ProductViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(string languageId, GetPublicProductPagingRequest request)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId == languageId
                        select new { p, pt, pic };


            if (request.Category_Id.HasValue && request.Category_Id.Value > 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.Category_Id);
            }
            //paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    ID = x.p.Id,
                    Name = x.pt.Name,
                    CreateDate = x.p.CreateDate,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount

                }).ToListAsync();

            var pageresult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data,
            };

            return pageresult;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPaingRequest request)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.Name.Contains(request.KeyWord)
                        select new { p, pt, pic };
            if (!string.IsNullOrEmpty(request.KeyWord))
                query = query.Where(x => x.pt.Name.Contains(request.KeyWord));

            if (request.Category_IDs.Count > 0)
            {
                query = query.Where(p => request.Category_IDs.Contains(p.pic.CategoryId));
            }
            //paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    ID = x.p.Id,
                    Name = x.pt.Name,
                    CreateDate = x.p.CreateDate,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();

            var pageresult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data,
            };

            return pageresult;
        }

        public async Task<ProductViewModel> GetById(int productId, string languageId)
        {
            var product = await _context.Products.FindAsync(productId);
            var productTran = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == productId && x.LanguageId == languageId);

            var productviewModel = new ProductViewModel()
            {
                ID = product.Id,
                CreateDate = product.CreateDate,
                Description = productTran != null ? productTran.Description : null,
                LanguageId = productTran.LanguageId,
                Details = productTran != null ? productTran.Details : null,
                Name = productTran != null ? productTran.Name : null,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoAlias = productTran != null ? productTran.SeoAlias : null,
                SeoDescription = productTran != null ? productTran.SeoDescription : null,
                SeoTitle = productTran != null ? productTran.SeoTitle : null,
                Stock = product.Stock,
                ViewCount = product.ViewCount
            };

            return productviewModel;
        }

        public async Task<ProductImageViewModel> GetImageById(int iamgeId)
        {
            var product = await _context.ProductImages.FindAsync(iamgeId);
            if (product == null)
            {
                throw new CNShopException($"Cannot find an iamge with id{iamgeId}");
            }

            var productviewModel = new ProductImageViewModel()
            {
                Caption = product.Caption,
                CreateDate = product.CreateDate,
                FilePath = product.PathImages,
                FileSize = product.FileSize,
                Id = product.Id,
                ProductId = product.ProductId,
                IsDefault = product.IsDefault,
                SortOrder = product.SortOrder
            };

            return productviewModel;
        }

        public async Task<List<ProductImageViewModel>> GetListImages(int productId)
        {
            return await _context.ProductImages.Where(x => x.ProductId == productId).Select(i => new ProductImageViewModel()
            {
                Caption = i.Caption,
                CreateDate = i.CreateDate,
                FileSize = i.FileSize,
                Id = i.Id,
                IsDefault = i.IsDefault,
                ProductId = i.ProductId,
                FilePath = i.PathImages,
                SortOrder = i.SortOrder
            }).ToListAsync();
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null)
            {
                throw new CNShopException($"Cannot find an image with id {imageId}");
            }
            _context.ProductImages.Remove(image);
            return await _context.SaveChangesAsync();
        }

        //public Task<List<ProductImageViewModel>> GetListImage(int productId)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<int> RemoveImages(int imageId)
        //{
        //    var images = await _context.ProductImages.FirstOrDefaultAsync(x => x.Id == imageId);

        //    if (images != null)
        //    {
        //        _context.ProductImages.Remove(images);
        //    }
        //    else
        //    {
        //        throw new CNShopException($"Cannot find a product with id  : {imageId}");
        //    }

        //    return  await _context.SaveChangesAsync();
        //}

        public async Task<int> Update(ProductEditRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            var productTran = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id);

            if (product == null || productTran == null) throw new CNShopException($"Cannot find a product with id  : {request.Id}");

            productTran.Name = request.Name;
            productTran.SeoAlias = request.SeoAlias;
            productTran.SeoDescription = request.SeoDesc;
            productTran.Details = request.Details;
            productTran.Description = request.Description;
            productTran.SeoTitle = request.SaoTitle;
            //Save image
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(x => x.IsDefault == true && x.ProductId == request.Id);
                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.PathImages = await this.SaveFile(request.ThumbnailImage);
                    _context.ProductImages.Update(thumbnailImage);
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null)
            {
                throw new CNShopException($"Cannot find an image with id {imageId}");
            }

            if (request.ImageFile != null)
            {
                image.PathImages = await this.SaveFile(request.ImageFile);
                image.FileSize = request.ImageFile.Length;
            }

            _context.ProductImages.Update(image);
            return await _context.SaveChangesAsync();
        }

        //public async Task<int> UpdateImage(int imageId, ProductImageViewModel productImage)
        //{
        //    var productimg = await _context.ProductImages.FirstOrDefaultAsync(x => x.Id == imageId);
        //    if (productimg != null)
        //    {
        //        productimg.Caption = Caption;
        //        productimg.IsDefault = isdefault;
        //        _context.ProductImages.Update(productimg);
        //    }
        //    else
        //    {
        //        throw new CNShopException($"Cannot find a product with id  : {imageId}");
        //    }

        //    return await _context.SaveChangesAsync();
        //}

        public async Task<bool> UpdatePrice(int productId, decimal newprice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new CNShopException($"Cannot find a product with id  : {productId}");
            product.Price = newprice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, decimal addQuantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new CNShopException($"Cannot find a product with id  : {productId}");
            product.Price += addQuantity;
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);

            return fileName;
        }
    }
}