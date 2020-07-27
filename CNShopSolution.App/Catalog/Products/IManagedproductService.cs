using CNShopSolution.App.Catalog.Products.DTOS;
using CNShopSolution.App.Catalog.Products.DTOS.Mangager;
using CNShopSolution.App.Dtos;
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
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetPaingProductRequest request);
    }
}
