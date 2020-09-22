using CNShopSolution.ViewModel.Catalog.ProductImages;
using CNShopSolution.ViewModel.Catalog.Products;

using CNShopSolution.ViewModel.Catalog.Products.Public;
using CNShopSolution.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CNShopSolution.App.Catalog.Products
{
    public interface IManagedproductService
    {
        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductEditRequest request);

        Task<ProductViewModel> GetById(int productId,string languageId);

        Task<bool> UpdatePrice(int productId, decimal newprice);
        Task<bool> UpdateStock(int productId, decimal addQuantity);

        Task AddViewCount(int productId);
        Task<int> Delete(int productId);

        Task<List<ProductViewModel>> GetAll();
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPaingRequest request);
        Task<int> AddImage(int productId, ProductImageCreateRequest productImage);
        Task<int> RemoveImage(int imageId);
        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest productImage);

        Task<ProductImageViewModel> GetImageById(int iamgeId);

        Task<List<ProductImageViewModel>> GetListImages(int productId);
    }
}
