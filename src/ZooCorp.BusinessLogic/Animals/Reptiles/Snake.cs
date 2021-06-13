using System.Collections.Generic;
using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.BusinessLogic.Animals.Reptiles
{
    public class Snake : Reptile
    {
        public override int RequiredSpaceSfFt { get; } = 2;

        public override string[] FavoriteFood { get; } = new[] {"Meat"};

        public override List<string> FriendlyWith { get; } = new List<string>() {"Snake"};

        public Snake(int id, List<int> feedSchedule = null, IConsole console = null) : base(console)
        {
            ID = id;
            FeedSchedule = feedSchedule ?? new List<int>() {4};
        }

        public override bool IsFriendlyWith(Animal animal)
        {
            return FriendlyWith.Contains(animal.GetType().Name);
        }
    }
}