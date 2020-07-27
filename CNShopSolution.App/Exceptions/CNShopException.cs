using System;
using System.Collections.Generic;
using System.Text;

namespace CNShopSolution.App.Exceptions
{
   public class CNShopException :Exception
    {
        public CNShopException()
        {

        }
        public CNShopException(string message):base(message)
        {

        }

        public CNShopException(string message,Exception inner) : base(message,inner)
        {

        }
    }
}
