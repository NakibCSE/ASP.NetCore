using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODPrinciples.Composition
{
    public class Product
    {
        private double _price;
        public double Price
        {
            get { return _price; }
            set { if (value > 0) _price = value; }
        }

        public double CalculatePriceAfterDiscount(double discount)
        {
            return Price - (Price * discount / 100);
        }
    }
}
