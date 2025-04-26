using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreationalPattern.AbstractFactory
{
    public class NissanFactory : CarFactory
    {
        public NissanFactory()
        {
            EngineFactory = new NissanEngineFactory();
            HeadLightFactory = new NissanHeadLightFactory();
        }
    }
}
