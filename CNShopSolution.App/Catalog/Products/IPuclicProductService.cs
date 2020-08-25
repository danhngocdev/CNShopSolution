using CNShopSolution.ViewModel.Catalog.Products;
using CNShopSolution.ViewModel.Catalog.Products.Public;
using CNShopSolution.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CNShopSolution.App.Catalog.Products
{
     public interface IPuclicProductService
    {
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);

        Task<List<ProductViewModel>> GetAll();
    }
}
