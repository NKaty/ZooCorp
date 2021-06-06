using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.BusinessLogic.Animals.Mammals
{
    public abstract class Mammal : Animal {
        public Mammal(IConsole console = null) : base(console) { }
    }

}
