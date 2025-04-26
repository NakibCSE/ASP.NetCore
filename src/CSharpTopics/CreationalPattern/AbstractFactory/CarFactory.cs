using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreationalPattern.AbstractFactory
{
    public abstract class CarFactory
    {
        public HeadLightFactory HeadLightFactory { get; protected set; }
        public EngineFactory EngineFactory { get; protected set; }
    }
}
