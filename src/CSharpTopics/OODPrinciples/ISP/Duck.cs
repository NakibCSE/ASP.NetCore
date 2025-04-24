using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODPrinciples.ISP
{
    public class Duck : IBird
    {
        public void Fly()
        {
            throw new NotImplementedException();
        }
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
