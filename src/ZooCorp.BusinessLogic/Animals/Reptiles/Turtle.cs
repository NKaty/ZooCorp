using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.BusinessLogic.Animals.Reptiles
{
    public class Turtle: Reptile
    {
        public override int RequiredSpaceSfFt { get; } = 5;

        public override string[] FavoriteFood { get; } = new[] {
            "Grass",
            "Vegetable"
        };

        public override List<string> FrendlyWith { get; } = new List<string>() { 
            "Turtle",
            "Parrot",
            "Bison",
            "Elephant"
        };

        public Turtle(int id, List<int> feedSchedule = null, IConsole console = null) : base(console)
        {
            ID = id;
            FeedSchedule = feedSchedule ?? new List<int>() { 5, 16};
        }

        public override bool IsFriendlyWith(Animal animal)
        {
            return FrendlyWith.Contains(animal.GetType().Name);
        }
    }
}
