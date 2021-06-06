using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Common;
using ZooCorp.BusinessLogic.Foods;

namespace ZooCorp.BusinessLogic.Animals.Birds
{
    public class Penguin : Bird
    {
        public override int RequiredSpaceSfFt { get; } = 10;
       
        public override string[] FavoriteFood { get; } = new[] { "Meat" };

        public override List<string> FrendlyWith { get; } = new List<string>() { "Penguin" };

        public Penguin(int id, List<int> feedSchedule = null, IConsole console = null) : base(console)
        {
            ID = id;
            FeedSchedule = feedSchedule ?? new List<int>() { 10, 20 };
        }

        public override bool IsFriendlyWith(Animal animal)
        {
            return FrendlyWith.Contains(animal.GetType().Name);
        }
    }
}
