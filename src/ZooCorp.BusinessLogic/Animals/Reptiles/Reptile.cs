using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.BusinessLogic.Animals.Reptiles
{
    public abstract class Reptile : Animal
    {
        protected Reptile(IConsole console = null) : base(console)
        {
        }
    }
}