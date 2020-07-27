using System;
using System.Collections.Generic;
using System.Text;

namespace CNShopSolution.App.Catalog.Products.DTOS.Mangager
{
    public class ProductEditRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string SeoDesc { get; set; }
        public string SaoTitle { get; set; }
        public string SeoAlias { get; set; }
        public string LanguageId { get; set; }
    }
}
