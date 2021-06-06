using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Animals.Reptiles;
using ZooCorp.BusinessLogic.Animals.Birds;
using ZooCorp.BusinessLogic.Foods;

namespace ZooCorp.BusinessLogic.Animals.Mammals
{
    public class Bison : Mammal
    {
        public override int RequiredSpaceSfFt { get; } = 1000;

        public override string[] FavoriteFood { get; } = new[] { "Grass" };

        public override List<string> FrendlyWith { get; } = new List<string>() { 
            "Bison",
            "Elephant"
        };

        public Bison(int id, List<int> feedSchedule = null) {
            ID = id;
            FeedSchedule = feedSchedule ?? new List<int>() { 15 };
        }

        public override bool IsFriendlyWith(Animal animal)
        {
            return FrendlyWith.Contains(animal.GetType().Name);
        }
    }
}
