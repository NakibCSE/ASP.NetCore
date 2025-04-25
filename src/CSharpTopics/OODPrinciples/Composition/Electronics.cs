using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODPrinciples.Composition
{
    public class Electronics
    {
        public string Name { get; set; }
        private Product Product { get; set; }

        public double Price
        {
            get
            {
                return Product.Price;
            }
            set
            {
                Product.Price = value;
            }
        }


        public double GetPriceAfterDiscount(double discount)
        {
            return Product.CalculatePriceAfterDiscount(discount);
        }
    }
}
