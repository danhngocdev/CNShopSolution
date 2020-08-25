using System;
using System.Collections.Generic;
using System.Text;

namespace CNShopSolution.Data.Entities
{
   public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public string PathImages { get; set; }
        public string Caption { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreateDate { get; set; }
        public int SortOrder { get; set; }
        public long FileSize { get; set; }
        public Product Product { get; set; }

       
    }
}
