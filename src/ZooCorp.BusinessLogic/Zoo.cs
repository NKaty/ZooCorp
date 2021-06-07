using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Employees;
using ZooCorp.BusinessLogic.Employees.HireValidators;
using ZooCorp.BusinessLogic.Employees.HireValidators.ValidationErrors;
using ZooCorp.BusinessLogic.Animals;
using ZooCorp.BusinessLogic.Exceptions;
using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.BusinessLogic
{
    public class Zoo
    {
        private int _idCounter = 0;

        private readonly IConsole _console;

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

        public Zoo(string location, IConsole console = null)
        {
            Location = location;
            _console = console;
            _console?.WriteLine($"Zoo: Created zoo in {location}.");
        }

        public Enclosure AddEnclosure(string name, int squreFeet)
        {
            var enclosure = new Enclosure(name, this, squreFeet, null, _console);
            Enclosures.Add(enclosure);
            _console?.WriteLine($"Zoo: Created enclosure {name} in zoo in {Location}.");
            return enclosure;
        }

        public Enclosure FindAvailableEnclosure(Animal animal)
        {
            foreach (var enclosure in Enclosures)
            {
                if (enclosure.IsAvailableSpace(animal) && enclosure.IsAnimalFriendly(animal))
                {
                    _console?.WriteLine($"Zoo: Found an available enclosure {enclosure.Name} in zoo in {Location}.");
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
                _console?.WriteLine($"Zoo: Trying to add unknown type of animal to the zoo in {Location}.");
                throw new UnknownAnimalException($"The zoo does not keep this type of animals - {animalType}.");
            }

            var zooAnimalType = zooAnimalTypes.First();
            string animalClassName = $"ZooCorp.BusinessLogic.Animals.{zooAnimalType.Item1}.{zooAnimalType.Item2}";
            Type classType = Type.GetType(animalClassName);
            var animal = Activator.CreateInstance(classType, new object[] { ++_idCounter, feedSchedule, _console }) as Animal;

            var enclosure = FindAvailableEnclosure(animal);
            enclosure.AddAnimals(animal);
            _console?.WriteLine($"Zoo: Added {animalType} ID {animal.ID} to {enclosure.Name} in zoo in {Location}.");

            return animal;
        }

        public void CreateEmployee(string employeeType, string firstName, string lastName, List<string> animalExperiences = null)
        {
            if (employeeType == "Veterinarian")
            {
                HireEmployee(new Veterinarian(firstName, lastName, animalExperiences, _console));
                _console?.WriteLine($"Zoo: Created Veterinarian {firstName} {lastName} in zoo in {Location}.");
                return;
            }

            if (employeeType == "ZooKeeper")
            {
                HireEmployee(new ZooKeeper(firstName, lastName, animalExperiences, _console));
                _console?.WriteLine($"Zoo: Created ZooKeeper {firstName} {lastName} in zoo in {Location}.");
                return;
            }

            _console?.WriteLine($"Zoo: Trying to hire unknown type of employee.");
            throw new UnknownEmployeeException($"The zoo does not have this type of employee in the zoo in {Location}.");
        }

        public void HireEmployee(IEmployee employee)
        {
            IHireValidator hireValidator = HireValidatorProvider.GetHireValidator(employee);
            var errors = hireValidator.ValidateEmployee(employee, GetAnimalsOfEachType());

            if (errors.Count() != 0)
            {
                _console?.WriteLine($"Zoo: {employee.GetType().Name} {employee.FirstName} {employee.LastName} does not have needed experience.");
                foreach (var error in errors)
                {
                    _console?.WriteLine($"{(error as AnimalExperiencesValidationError).Animal}: {(error as AnimalExperiencesValidationError).Message}");
                }
                throw new NoNeededExperienceException($"The {employee.GetType().Name} does not have excperience with all the zoo animals");
            }

            _console?.WriteLine($"Zoo: Hired {employee.GetType().Name} {employee.FirstName} {employee.LastName} in zoo in {Location}.");
            Employees.Add(employee);
        }

        public void FeedAnimals(DateTime dateTime)
        {
            var dividedAnimals = DivideAnimalsBetweenEmployees("ZooKeeper", (Animal animal) => animal.IsHungry(dateTime));

            foreach (var group in dividedAnimals)
            {
                foreach (var animal in group.Item2)
                {
                    (group.Item1 as ZooKeeper).FeedAnimal(animal);
                    _console?.WriteLine($"Zoo: {animal.GetType().Name} ID {animal.ID} was fed by {group.Item1.FirstName} {group.Item1.LastName} in zoo in {Location}.");
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
                    _console?.WriteLine($"Zoo: {animal.GetType().Name} ID {animal.ID} was healed by {group.Item1.FirstName} {group.Item1.LastName} in zoo in {Location}.");
                }
            }
        }

        public List<(IEmployee, List<Animal>)> DivideAnimalsBetweenEmployees(string employeeType, Predicate<Animal> checkAnimal)
        {
            var employees = Employees.Where(e => e.GetType().Name == employeeType);

            var dividedAnimals = employees.Select((e) => (employee: e, animals: new List<Animal>())).ToList();

            bool noEmployeeWithAnimalExperience = false;

            foreach (var enclosure in Enclosures)
            {
                foreach (var animal in enclosure.Animals)
                {
                    if (checkAnimal(animal))
                    {
                        dividedAnimals = dividedAnimals.OrderBy(d => d.animals.Count()).ToList();
                        foreach (var item in dividedAnimals)
                        {
                            if (item.employee.HasAnimalExperience(animal))
                            {
                                item.animals.Add(animal);
                                break;
                            }
                            else
                            {
                                noEmployeeWithAnimalExperience = true;
                            }
                        }
                        if (noEmployeeWithAnimalExperience)
                        {
                            throw new UnknownAnimalException($"The zoo does not keep this type of animals - {animal.GetType().Name}.");
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
