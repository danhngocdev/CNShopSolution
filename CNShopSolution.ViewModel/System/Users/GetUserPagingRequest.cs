using CNShopSolution.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CNShopSolution.ViewModel.System.Users
{
  public  class GetUserPagingRequest : PaingRequestBase
    {
        public string Keyword { get; set; }
    }
}
