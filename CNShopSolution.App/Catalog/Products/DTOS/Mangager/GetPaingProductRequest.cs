using CNShopSolution.App.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CNShopSolution.App.Catalog.Products.DTOS.Mangager
{
    public class GetPaingProductRequest : PaingRequestBase
    {
        public string KeyWord { get; set; }
        public List<int> Category_IDs { get; set; }
    }
}
