using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Foods;
using ZooCorp.BusinessLogic.Medicines;
using ZooCorp.BusinessLogic.Exceptions;
using ZooCorp.BusinessLogic.Employees;

namespace ZooCorp.BusinessLogic.Animals
{
    public abstract class Animal
    {
        private bool _isSick = false;

        public int ID { get; set; }

        public abstract int RequiredSpaceSfFt { get; }

        public abstract string[] FavoriteFood { get; }

        public abstract List<string> FrendlyWith { get; }

        public List<FeedTime> FeedTimes { get; } = new List<FeedTime>();

        public List<int> FeedSchedule { get; set; }

        public string neededMedicine { get; set; } = null;

        public bool IsSick()
        {
            return _isSick;
        }

        public abstract bool IsFriendlyWith(Animal animal);

        public void Feed(Food food, ZooKeeper zooKeeper)
        {
            if (FavoriteFood.Contains(food.GetType().Name))
            {
                FeedTimes.Add(new FeedTime(new DateTime(), zooKeeper));
            } else
            {
                throw new NotFavoriteFoodException("The animal does not eat this type of food.");
            }
        }

        public void AddFeedSchedule(List<int> hours)
        {
            FeedSchedule.AddRange(hours);
        }

        public void MarkSick(Medicine medicine = null)
        {
            _isSick = true;
            neededMedicine = medicine?.GetType().Name;
        }

        public void Heal(Medicine medicine = null)
        {
            if (neededMedicine == medicine?.GetType().Name)
            {
                neededMedicine = null;
                _isSick = false;
            } else
            {
                throw new NotNeededMedicineException("The animal does need another type of medicine.");
            }
            
        }
    }
}
