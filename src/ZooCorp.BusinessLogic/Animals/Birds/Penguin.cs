using System.Collections.Generic;
using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.BusinessLogic.Animals.Birds
{
    public class Penguin : Bird
    {
        public override int RequiredSpaceSfFt { get; } = 10;

        public override string[] FavoriteFood { get; } = new[] {"Meat"};

        public override List<string> FriendlyWith { get; } = new List<string>() {"Penguin"};

        public Penguin(int id, List<int> feedSchedule = null, IConsole console = null) : base(console)
        {
            ID = id;
            FeedSchedule = feedSchedule ?? new List<int>() {10, 20};
        }

        public override bool IsFriendlyWith(Animal animal)
        {
            return FriendlyWith.Contains(animal.GetType().Name);
        }
    }
}