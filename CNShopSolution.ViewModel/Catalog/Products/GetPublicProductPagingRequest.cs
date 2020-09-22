using CNShopSolution.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CNShopSolution.ViewModel.Catalog.Products.Public
{
    public class GetPublicProductPagingRequest : PaingRequestBase
    {
        
        public int? Category_Id { get; set; }
    }
}
