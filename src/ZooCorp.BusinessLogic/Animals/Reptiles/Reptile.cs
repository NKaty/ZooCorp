using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.BusinessLogic.Animals.Reptiles
{
    public abstract class Reptile : Animal {
        public Reptile(IConsole console = null) : base(console) { }
    }
}
