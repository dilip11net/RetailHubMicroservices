using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Exceptions
{
    public class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string message) : base("basket", message)
        {
        }
    }
}
