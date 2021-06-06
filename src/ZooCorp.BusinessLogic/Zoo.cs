using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Employees;
using ZooCorp.BusinessLogic.Employees.HireValidators;
using ZooCorp.BusinessLogic.Animals;
using ZooCorp.BusinessLogic.Exceptions;

namespace ZooCorp.BusinessLogic
{
    public class Zoo
    {
        private static int _idCounter = 0;

        private List<(string, string)> _animalTypes = new List<(string, string)>()
        {
            ("Birds", "Parrot"),
            ("Birds", "Penguin"),
            ("Mammals", "Bison"),
            ("Mammals", "Elephant"),
            ("Mammals", "Lion"),
            ("Reptiles", "Snake"),
            ("Reptiles", "Turtle")
        };

        public List<Enclosure> Enclosures { get; private set; } = new List<Enclosure>();

        public List<IEmployee> Employees { get; private set; } = new List<IEmployee>();

        public string Location { get; private set; }

        public Zoo(string location)
        {
            Location = location;
        }

        public Enclosure AddEnclosure(string name, int squreFeet)
        {
            var enclosure = new Enclosure(name, this, squreFeet);
            Enclosures.Add(enclosure);
            return enclosure;
        }

        public Enclosure FindAvailableEnclosure(Animal animal)
        {
            foreach (var enclosure in Enclosures)
            {
                if (enclosure.IsAvailableSpace(animal) && enclosure.IsAnimalFriendly(animal))
                {
                    return enclosure;
                }
            }

            throw new NoAvailableEnclosureException($"There is no available enclosure for this type of animal - {animal.GetType().Name}.");
        }

        public Animal AddAnimals(string animalType, List<int> feedSchedule = null)
        {
            var zooAnimalTypes = _animalTypes.Where(type => type.Item2.ToLower() == animalType.ToLower());
            if (zooAnimalTypes.Count() == 0)
            {
                throw new UnknownAnimalException($"The zoo does not keep this type of animals - {animalType}.");
            }

            var zooAnimalType = zooAnimalTypes.First();
            string animalClassName = $"ZooCorp.BusinessLogic.Animals.{zooAnimalType.Item1}.{zooAnimalType.Item2}";
            Type classType = Type.GetType(animalClassName);
            var animal = Activator.CreateInstance(classType, new object[] { ++_idCounter, feedSchedule }) as Animal;

            var enclosure = FindAvailableEnclosure(animal);
            enclosure.AddAnimals(animal);

            return animal;
        }

        public void HireEmployee(IEmployee employee)
        {
            IHireValidator hireValidator = HireValidatorProvider.GetHireValidator(employee);
            var errors = hireValidator.ValidateEmployee(employee, GetAnimalsOfEachType());

            if (errors.Count() != 0)
            {
                throw new NoNeededExperienceException($"The {employee.GetType().Name} does not have excperience with all the zoo animals");
            }

            Employees.Add(employee);
        }

        public void FeedAnimals(DateTime dateTime)
        {
            var dividedAnimals = DivideAnimalsBetweenEmployees("ZooKeeper", (Animal animal) =>
            {
                var dayBeginning = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
                if (animal.FeedTimes.Count == 0 || animal.FeedTimes.Count == 1)
                {
                    return true;
                }

                return !(animal.FeedTimes[animal.FeedTimes.Count - 1].FeedAnimalTime >= dayBeginning &&
                animal.FeedTimes[animal.FeedTimes.Count - 2].FeedAnimalTime >= dayBeginning);
            });

            foreach (var group in dividedAnimals)
            {
                foreach (var animal in group.Item2)
                {
                    (group.Item1 as ZooKeeper).FeedAnimal(animal);
                }
            }
        }

        public void HealAnimals()
        {
            var dividedAnimals = DivideAnimalsBetweenEmployees("Veterinarian", (Animal animal) => animal.IsSick());

            foreach (var group in dividedAnimals)
            {
                foreach (var animal in group.Item2)
                {
                    (group.Item1 as Veterinarian).HealAnimal(animal);
                }
            }
        }

        public List<(IEmployee, List<Animal>)> DivideAnimalsBetweenEmployees(string employeeType, Predicate<Animal> checkCondition)
        {
            var employees = Employees.Where(e => e.GetType().Name == employeeType);

            var dividedAnimals = employees.Select((e) => (employee: e, animals: new List<Animal>() )).ToList();

            foreach (var enclosure in Enclosures)
            {
                foreach (var animal in enclosure.Animals)
                {
                    if (checkCondition(animal))
                    {
                        dividedAnimals = dividedAnimals.OrderBy(d => d.animals.Count()).ToList();
                        foreach (var item in dividedAnimals)
                        {
                            if (item.employee.HasAnimalExperience(animal))
                            {
                                item.animals.Add(animal);
                                break;
                            }
                        }
                    }
                }
            }

            return dividedAnimals.ToList();
        }

        private List<Animal> GetAnimalsOfEachType()
        {
            var animals = new List<Animal>();
            var animalTypes = new List<string>();

            foreach (var enclosure in Enclosures)
            {
                foreach (var animal in enclosure.Animals)
                {
                    if (!animalTypes.Contains(animal.GetType().Name))
                    {
                        animals.Add(animal);
                        animalTypes.Add(animal.GetType().Name);
                    }
                }
            }

            return animals;
        }
    }
}
