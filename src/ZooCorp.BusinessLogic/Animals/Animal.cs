using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Foods;
using ZooCorp.BusinessLogic.Medicines;
using ZooCorp.BusinessLogic.Exceptions;
using ZooCorp.BusinessLogic.Employees;
using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.BusinessLogic.Animals
{
    public abstract class Animal
    {
        private bool _isSick = false;

        private readonly IConsole _console;

        public int ID { get; protected set; }

        public abstract int RequiredSpaceSfFt { get; }

        public abstract string[] FavoriteFood { get; }

        public abstract List<string> FriendlyWith { get; }

        public List<FeedTime> FeedTimes { get; } = new List<FeedTime>();

        public List<int> FeedSchedule { get; set; }

        public string NeededMedicine { get; set; } = null;

        public Animal() { }

        public Animal(IConsole console = null)
        {
            _console = console;
        }

        public bool IsSick()
        {
            return _isSick;
        }

        public bool IsHungry(DateTime dateTime)
        {
            var dayBeginning = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            if (FeedTimes.Count == 0 || FeedTimes.Count == 1)
            {
                return true;
            }

            return !(FeedTimes[^1].FeedAnimalTime >= dayBeginning &&
            FeedTimes[^2].FeedAnimalTime >= dayBeginning);
        }

        public abstract bool IsFriendlyWith(Animal animal);

        public void Feed(Food food, ZooKeeper zooKeeper)
        {
            if (FavoriteFood.Contains(food.GetType().Name))
            {
                FeedTimes.Add(new FeedTime(DateTime.Now, zooKeeper));
                _console?.WriteLine($"{GetType().Name}: {GetType().Name} ID {ID} was fed by {zooKeeper.FirstName} {zooKeeper.LastName}.");
            } else
            {
                _console?.WriteLine($"{GetType().Name}: Trying to feed {GetType().Name} ID {ID} with not its favorite food.");
                throw new NotFavoriteFoodException("The animal does not eat this type of food.");
            }
        }

        public void AddFeedSchedule(List<int> hours)
        {
            _console?.WriteLine($"{GetType().Name}: The feed schedule of {GetType().Name} ID {ID} was changed.");
            FeedSchedule.AddRange(hours);
        }

        public void MarkSick(Medicine medicine = null)
        {
            _isSick = true;
            NeededMedicine = medicine?.GetType().Name;
            _console?.WriteLine($"{GetType().Name}: {GetType().Name} ID {ID} got sick.");
        }

        public void Heal(Medicine medicine = null)
        {
            if (NeededMedicine == medicine?.GetType().Name)
            {
                NeededMedicine = null;
                _isSick = false;
                _console?.WriteLine($"{GetType().Name}: {GetType().Name} ID {ID} was healed.");
            } else
            {
                _console?.WriteLine($"{GetType().Name}: Trying to heal {GetType().Name} ID {ID} with incorrect type of medicine.");
                throw new NotNeededMedicineException("The animal does need another type of medicine.");
            }
            
        }
    }
}
