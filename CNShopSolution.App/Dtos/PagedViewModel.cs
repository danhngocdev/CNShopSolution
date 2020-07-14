using System;
using System.Collections.Generic;
using System.Text;

namespace CNShopSolution.App.Dtos
{
   public class PagedViewModel<T>
    {

        List<T> items { get; set; }
        public int TotalRecord { get; set; }
    }
}
