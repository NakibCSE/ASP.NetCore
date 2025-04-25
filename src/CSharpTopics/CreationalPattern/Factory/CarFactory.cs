using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreationalPattern.Factory
{
    public class CarFactory
    {
        public Car CreateCar(string type, string model, string color, double speed)
        {
            if (type == "Toyota")
            {
                if (color == "red")
                {
                    color = "Red-Ultra-Toyota";
                }
                return new Toyota() { Color = color, Model = model, Speed = speed };
            }
            else if (type == "Nissan")
            {
                return new Nissan() { Color = color, Model = model, Speed = speed };
            }

            return null;
        }
    }
}
