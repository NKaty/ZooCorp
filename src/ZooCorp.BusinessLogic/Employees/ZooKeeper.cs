using System;
using System.Collections.Generic;
using ZooCorp.BusinessLogic.Animals;
using ZooCorp.BusinessLogic.Common;
using ZooCorp.BusinessLogic.Foods;

namespace ZooCorp.BusinessLogic.Employees
{
    public class ZooKeeper : IEmployee
    {
        private readonly IConsole _console;

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public List<string> AnimalExperiences { get; private set; }

        public ZooKeeper(string firstName, string lastName, List<string> animalExperiences = null,
            IConsole console = null)
        {
            FirstName = firstName;
            LastName = lastName;
            AnimalExperiences = animalExperiences ?? new List<string>();
            _console = console;
        }

        public void AddAnimalExperience(Animal animal)
        {
            AnimalExperiences.Add(animal.GetType().Name);
            _console?.WriteLine(
                $"ZooKeeper: Added experience with {animal.GetType().Name} to ZooKeeper {FirstName} {LastName}.");
        }

        public bool HasAnimalExperience(Animal animal)
        {
            return AnimalExperiences.Contains(animal.GetType().Name);
        }

        public bool FeedAnimal(Animal animal)
        {
            if (!HasAnimalExperience(animal))
            {
                return false;
            }

            Food food = null;
            string foodClassName = $"ZooCorp.BusinessLogic.Foods.{animal.FavoriteFood[0]}";
            Type type = Type.GetType(foodClassName);
            if (type is not null)
            {
                food = Activator.CreateInstance(type) as Food;
            }

            animal.Feed(food, this);
            _console?.WriteLine(
                $"ZooKeeper: ZooKeeper {FirstName} {LastName} fed {animal.GetType().Name} ID {animal.ID}.");

            return true;
        }
    }
}