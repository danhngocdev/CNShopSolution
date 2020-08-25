using CNShopSolution.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CNShopSolution.ViewModel.Catalog.Products
{
  public  class GetManageProductPaingRequest : PaingRequestBase
    {
        public string KeyWord { get; set; }
        public List<int> Category_IDs { get; set; }
    }
}
