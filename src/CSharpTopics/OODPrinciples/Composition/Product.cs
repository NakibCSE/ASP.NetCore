using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODPrinciples.Composition
{
    public class Product
    {
        public double Price { get; set; }

        public double CalculatePriceAfterDiscount(double discount)
        {
            return Price - (Price * discount / 100);
        }
    }
}
