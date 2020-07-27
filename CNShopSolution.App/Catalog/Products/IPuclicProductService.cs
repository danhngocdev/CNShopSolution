using CNShopSolution.App.Catalog.Products.DTOS;
using CNShopSolution.App.Catalog.Products.DTOS.Mangager;
using CNShopSolution.App.Catalog.Products.DTOS.Public;
using CNShopSolution.App.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CNShopSolution.App.Catalog.Products
{
    public interface IPuclicProductService
    {
       Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetProductPaingRequest request);
    }
}
