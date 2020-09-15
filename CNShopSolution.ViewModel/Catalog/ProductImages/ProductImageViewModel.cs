using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CNShopSolution.ViewModel.Catalog.ProductImages
{
   public class ProductImageViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Caption { get; set; }

        public DateTime CreateDate { get; set; }

        public int SortOrder { get; set; }

        public string FilePath { get; set; }

        public bool IsDefault { get; set; }

        public long FileSize  { get; set; }

    }
}
