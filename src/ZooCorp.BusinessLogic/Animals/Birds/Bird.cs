using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.BusinessLogic.Animals.Birds
{
    public abstract class Bird : Animal
    {
        protected Bird(IConsole console = null) : base(console)
        {
        }
    }
}