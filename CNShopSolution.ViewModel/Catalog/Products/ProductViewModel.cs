using System;
using System.Collections.Generic;
using System.Text;

namespace CNShopSolution.ViewModel.Catalog.Products
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }

        public int ViewCount { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public int Stock { get; set; }
        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }
    }
}
