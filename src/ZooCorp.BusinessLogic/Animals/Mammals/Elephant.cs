using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Animals.Reptiles;
using ZooCorp.BusinessLogic.Animals.Birds;

namespace ZooCorp.BusinessLogic.Animals.Mammals
{
    public class Elephant : Mammal
    {
        public override int RequiredSpaceSfFt { get; } = 1000;

        public override string[] FavoriteFood { get; } = new[] { "banana", "grass" };

        public override List<string> FrendlyWith { get; } = new List<string>() { 
            typeof(Elephant).ToString(),
            typeof(Bison).ToString(),
            typeof(Parrot).ToString(),
            typeof(Turtle).ToString()
        };

        public override bool IsFriendlyWith(Animal animal)
        {
            return FrendlyWith.Contains(animal.GetType().ToString());
        }
    }
}
