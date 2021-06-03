using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooCorp.BusinessLogic.Animals.Birds
{
    public class Penguin : Bird
    {
        public override int RequiredSpaceSfFt { get; } = 10;
       
        public override string[] FavoriteFood { get; } = new[] { "fish",  "shellfish" };

        public override List<string> FrendlyWith { get; } = new List<string>() { typeof(Penguin).ToString() };

        public override bool IsFriendlyWith(Animal animal)
        {
            return FrendlyWith.Contains(animal.GetType().ToString());
        }
    }
}
