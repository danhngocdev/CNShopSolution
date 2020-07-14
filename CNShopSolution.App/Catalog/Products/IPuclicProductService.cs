using CNShopSolution.App.Catalog.Products.DTOS;
using CNShopSolution.App.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CNShopSolution.App.Catalog.Products
{
   public interface IPuclicProductService
    {
        PagedViewModel<ProductViewModel> GetAllByCategoryId(int cateId,int pageIndex,int pagesize);
    }
}
