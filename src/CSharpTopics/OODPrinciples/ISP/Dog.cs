using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODPrinciples.ISP
{
    public class Dog : IWalkable, ISwimable
    {
        public void Swim()
        {
            throw new NotImplementedException();
        }

        public void Walk()
        {
            throw new NotImplementedException();
        }
    }
}
