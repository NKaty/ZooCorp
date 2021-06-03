using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Animals.Reptiles;
using ZooCorp.BusinessLogic.Animals.Birds;

namespace ZooCorp.BusinessLogic.Animals.Mammals
{
    public class Bison : Mammal
    {
        public override int RequiredSpaceSfFt { get; } = 1000;

        public override string[] FavoriteFood { get; } = new[] { "grass" };

        public override List<string> FrendlyWith { get; } = new List<string>() { 
            typeof(Bison).ToString(),
            typeof(Elephant).ToString()
        };

        public override bool IsFriendlyWith(Animal animal)
        {
            return FrendlyWith.Contains(animal.GetType().ToString());
        }
    }
}
