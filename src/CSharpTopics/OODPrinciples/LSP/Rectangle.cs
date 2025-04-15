using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODPrinciples.LSP
{
    public class Rectangle
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double CalculateArea()
        {
            return Width * Height;
        }
    }
}
