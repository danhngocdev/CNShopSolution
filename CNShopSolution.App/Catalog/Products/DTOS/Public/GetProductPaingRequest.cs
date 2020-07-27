using CNShopSolution.App.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CNShopSolution.App.Catalog.Products.DTOS.Public
{
    public class GetProductPaingRequest : PaingRequestBase
    {
        public int? Category_Id { get; set; }
    }
}
