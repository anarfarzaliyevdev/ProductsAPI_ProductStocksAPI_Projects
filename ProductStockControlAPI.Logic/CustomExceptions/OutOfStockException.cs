using System;
using System.Collections.Generic;
using System.Text;

namespace ProductStockControlAPI.Logic.CustomExceptions
{
    public class OutOfStockException:Exception
    {
      

        public OutOfStockException()
            : base(String.Format("This product is out of stock"))
        {

        }
    }
}
