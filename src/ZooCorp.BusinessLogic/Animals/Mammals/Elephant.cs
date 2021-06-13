using System.Collections.Generic;
using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.BusinessLogic.Animals.Mammals
{
    public class Elephant : Mammal
    {
        public override int RequiredSpaceSfFt { get; } = 1000;

        public override string[] FavoriteFood { get; } = new[] {"Grass"};

        public override List<string> FriendlyWith { get; } = new List<string>()
        {
            "Elephant",
            "Bison",
            "Parrot",
            "Turtle"
        };

        public Elephant(int id, List<int> feedSchedule = null, IConsole console = null) : base(console)
        {
            ID = id;
            FeedSchedule = feedSchedule ?? new List<int>() {16};
        }

        public override bool IsFriendlyWith(Animal animal)
        {
            return FriendlyWith.Contains(animal.GetType().Name);
        }
    }
}