using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Animals.Mammals;
using ZooCorp.BusinessLogic.Animals.Reptiles;

namespace ZooCorp.BusinessLogic.Animals.Birds
{
    public class Parrot : Bird
    {
        public override int RequiredSpaceSfFt { get; } = 5;

        public override string[] FavoriteFood { get; } = new[] { "seed", "fruit" };

        public override List<string> FrendlyWith { get; } = new List<string>() { 
            typeof(Parrot).ToString(),
            typeof(Bison).ToString(),
            typeof(Elephant).ToString(),
            typeof(Turtle).ToString(),
        };

        public override bool IsFriendlyWith(Animal animal)
        {
            return FrendlyWith.Contains(animal.GetType().ToString());
        }
    }
}
