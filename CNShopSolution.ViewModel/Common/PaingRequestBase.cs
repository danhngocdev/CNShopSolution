using System;
using System.Collections.Generic;
using System.Text;

namespace CNShopSolution.ViewModel.Common
{
    public class PaingRequestBase :RequestBase
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
