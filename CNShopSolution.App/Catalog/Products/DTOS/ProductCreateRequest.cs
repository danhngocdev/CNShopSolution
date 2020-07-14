using System;
using System.Collections.Generic;
using System.Text;

namespace CNShopSolution.App.Catalog.Products.DTOS
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
