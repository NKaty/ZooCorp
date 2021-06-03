using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Animals.Mammals;
using ZooCorp.BusinessLogic.Animals.Birds;

namespace ZooCorp.BusinessLogic.Animals.Reptiles
{
    public class Turtle: Reptile
    {
        public override int RequiredSpaceSfFt { get; } = 5;

        public override string[] FavoriteFood { get; } = new[] { "fruit", "vegetable" };

        public override List<string> FrendlyWith { get; } = new List<string>() { 
            typeof(Turtle).ToString(),
            typeof(Parrot).ToString(),
            typeof(Bison).ToString(),
            typeof(Elephant).ToString()
        };

        public override bool IsFriendlyWith(Animal animal)
        {
            return FrendlyWith.Contains(animal.GetType().ToString());
        }
    }
}
