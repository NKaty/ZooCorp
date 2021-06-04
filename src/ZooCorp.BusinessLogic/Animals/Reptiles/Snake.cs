using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Animals.Mammals;
using ZooCorp.BusinessLogic.Animals.Birds;
using ZooCorp.BusinessLogic.Foods;

namespace ZooCorp.BusinessLogic.Animals.Reptiles
{
    public class Snake : Reptile
    {
        public override int RequiredSpaceSfFt { get; } = 2;

        public override string[] FavoriteFood { get; } = new[] { "Meat" };

        public override List<string> FrendlyWith { get; } = new List<string>() { "Snake" };

        public Snake(int id, List<int> feedSchedule = null) {
            ID = id;
            FeedSchedule = feedSchedule ?? new List<int>();
        }

        public override bool IsFriendlyWith(Animal animal)
        {
            return FrendlyWith.Contains(animal.GetType().Name);
        }
    }
}
