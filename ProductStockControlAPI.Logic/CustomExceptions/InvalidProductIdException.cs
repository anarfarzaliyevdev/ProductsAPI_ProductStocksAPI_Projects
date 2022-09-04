using System;
using System.Collections.Generic;
using System.Text;

namespace ProductStockControlAPI.Logic.CustomExceptions
{
    public class InvalidProductIdException: Exception
    {

        public InvalidProductIdException(int productId)
            : base(String.Format("Invalid product id: {0}",productId))
        {

        }
    }
}
