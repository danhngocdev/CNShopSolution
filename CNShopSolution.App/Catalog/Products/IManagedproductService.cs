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

        Task<bool> UpdatePrice(int productId, decimal newprice);
        Task<bool> UpdateStock(int productId, decimal addQuantity);

        Task AddViewCount(int productId);
        Task<int> Delete(int productId);

        Task<List<ProductViewModel>> GetAll();
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPaingRequest request);
        Task<int> AddImages(int productId,List<IFormFile> files);
        Task<int> RemoveImages(int imageId);
        Task<int> UpdateImage(int imageId, string Caption, bool isdefault);

        Task<List<ProductImageViewModel>> GetListImage(int productId);
    }
}
