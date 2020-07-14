using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CNShopSolution.Data.Entities
{
   public class AppRole :IdentityRole<Guid>
    {
        public string Desc { get; set; }
    }
}
