
using CNShopSolution.App.Catalog.Products.DTOS;
using CNShopSolution.App.Catalog.Products.DTOS.Mangager;
using CNShopSolution.App.Dtos;
using CNShopSolution.App.Exceptions;
using CNShopSolution.Data.EF;
using CNShopSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CNShopSolution.App.Catalog.Products
{
    public class ManagedProductService : IManagedproductService
    {

        private readonly CNShopDbContext _context;
        public ManagedProductService(CNShopDbContext context)
        {
            _context = context;
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
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();

        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new CNShopException($"Cannot find a product : {productId}");
            _context.Products.Remove(product);
          return  await _context.SaveChangesAsync();
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetPaingProductRequest request)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.Name.Contains(request.KeyWord)
                        select new { p,pt,pic};
            if (!string.IsNullOrEmpty(request.KeyWord))
                query = query.Where(x => x.pt.Name.Contains(request.KeyWord));

            if (request.Category_IDs.Count >0)
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
                    SeoTitle= x.pt.SeoTitle,
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

        public async Task<int> Update(ProductEditRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            var productTran = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id);

            if(product == null || productTran == null) throw new CNShopException($"Cannot find a product with id  : {request.Id}");

            productTran.Name = request.Name;
            productTran.SeoAlias = request.SeoAlias;
            productTran.SeoDescription = request.SeoDesc;
            productTran.Details = request.Details;
            productTran.Description = request.Description;
            productTran.SeoTitle = request.SaoTitle;

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newprice)
        {
            var product = await _context.Products.FindAsync(productId);
            if(product == null) throw new CNShopException($"Cannot find a product with id  : {productId}");
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
    }
}
