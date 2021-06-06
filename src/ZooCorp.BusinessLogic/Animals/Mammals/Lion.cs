using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.BusinessLogic.Animals.Mammals
{
    public class Lion : Mammal
    {
        public override int RequiredSpaceSfFt { get; } = 1000;

        public override string[] FavoriteFood { get; } = new[] { "Meat" };

        public override List<string> FrendlyWith { get; } = new List<string>() { "Lion" };

        public Lion(int id, List<int> feedSchedule = null, IConsole console = null) : base(console)
        {
            ID = id;
            FeedSchedule = feedSchedule ?? new List<int>() { 8 };
        }

        public override bool IsFriendlyWith(Animal animal)
        {
            return FrendlyWith.Contains(animal.GetType().Name);
        }
    }
}
