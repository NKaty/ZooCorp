using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Animals.Mammals;
using ZooCorp.BusinessLogic.Animals.Reptiles;
using ZooCorp.BusinessLogic.Foods;

namespace ZooCorp.BusinessLogic.Animals.Birds
{
    public class Parrot : Bird
    {
        public override int RequiredSpaceSfFt { get; } = 5;

        public override string[] FavoriteFood { get; } = new[] { "Vegetable" };

        public override List<string> FrendlyWith { get; } = new List<string>() { 
            "Parrot",
            "Bison",
            "Elephant",
            "Turtle"
        };

        public Parrot(int id, List<int> feedSchedule = null) {
            ID = id;
            FeedSchedule = feedSchedule ?? new List<int>() { 6, 17 };
        }

        public override bool IsFriendlyWith(Animal animal)
        {
            return FrendlyWith.Contains(animal.GetType().Name);
        }
    }
}
