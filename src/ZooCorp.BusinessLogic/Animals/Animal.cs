using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Foods;
using ZooCorp.BusinessLogic.Medicines;



namespace ZooCorp.BusinessLogic.Animals
{
    public abstract class Animal
    {
        private bool _isSick = false;

        public int ID { get; set; }

        public abstract int RequiredSpaceSfFt { get; }

        public abstract string[] FavoriteFood { get; }

        public abstract List<string> FrendlyWith { get; }

        public List<FeedTime> FeedTimes { get; }

        public List<int> FeedSchedule { get; }

        public bool IsSick()
        {
            return _isSick;
        }

        public abstract bool IsFriendlyWith(Animal animal);

        public void Feed(Food food)
        {

        }

        public void AddFeedSchedule(List<int> hours)
        {
            FeedSchedule.AddRange(hours);
        }

        public void MarkSick()
        {
            _isSick = true;
        }

        public void Heal(Medicine medicine)
        {
            _isSick = false;
        }
    }
}
