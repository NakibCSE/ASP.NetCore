using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreationalPattern.AbstractFactory
{
    public class ToyotaEngineFactory : EngineFactory
    {
        public override Engine CreateEngine()
        {
            return new ToyotaEngine();
        }
    }
}
