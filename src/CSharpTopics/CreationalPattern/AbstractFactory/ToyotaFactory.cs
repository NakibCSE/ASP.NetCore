using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreationalPattern.AbstractFactory
{
    public class ToyotaFactory : CarFactory
    {
        public ToyotaFactory()
        {
            EngineFactory = new ToyotaEngineFactory();
            HeadLightFactory = new ToyotaHeadLightFactory();
        }
    }
}
