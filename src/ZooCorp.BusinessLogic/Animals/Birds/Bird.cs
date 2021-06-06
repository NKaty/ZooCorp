using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.BusinessLogic.Animals.Birds
{
    public abstract class Bird : Animal {
        public Bird(IConsole console = null) : base(console) { }
    }
}
