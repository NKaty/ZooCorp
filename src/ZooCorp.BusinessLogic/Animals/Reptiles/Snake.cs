using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Animals.Mammals;
using ZooCorp.BusinessLogic.Animals.Birds;

namespace ZooCorp.BusinessLogic.Animals.Reptiles
{
    public class Snake : Reptile
    {
        public override int RequiredSpaceSfFt { get; } = 2;

        public override string[] FavoriteFood { get; } = new[] { "mouse", "insect" };

        public override List<string> FrendlyWith { get; } = new List<string>() { typeof(Snake).ToString() };

        public override bool IsFriendlyWith(Animal animal)
        {
            return FrendlyWith.Contains(animal.GetType().ToString());
        }
    }
}
