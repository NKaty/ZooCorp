using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.BusinessLogic.Animals.Mammals
{
    public abstract class Mammal : Animal
    {
        protected Mammal(IConsole console = null) : base(console)
        {
        }
    }
}