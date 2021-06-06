using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Animals;
using ZooCorp.BusinessLogic.Common;
using ZooCorp.BusinessLogic.Medicines;

namespace ZooCorp.BusinessLogic.Employees
{
    public class Veterinarian : IEmployee
    {
        private IConsole _console;

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public List<string> AnimalExperiences { get; private set; }

        public Veterinarian(string firstName, string lastName, List<string> animalExperiences = null, IConsole console = null)
        {
            FirstName = firstName;
            LastName = lastName;
            AnimalExperiences = animalExperiences ?? new List<string>();
            _console = console;
        }

        public void AddAnimalExperience(Animal animal)
        {
            AnimalExperiences.Add(animal.GetType().Name);
            _console?.WriteLine($"Added experience with {animal.GetType().Name} to Veterinarian <<{FirstName} {LastName}>>.");
        }

        public bool HasAnimalExperience(Animal animal)
        {
            return AnimalExperiences.Contains(animal.GetType().Name);
        }

        public bool HealAnimal(Animal animal)
        {
            if (!HasAnimalExperience(animal))
            {
                return false;
            }
            if (!animal.IsSick())
            {
                return false;
            }
            Medicine medicine = null;
            if (animal.NeededMedicine != null)
            {
                string medicineClassName = $"ZooCorp.BusinessLogic.Medicines.{animal.NeededMedicine}";
                Type type = Type.GetType(medicineClassName);
                medicine = Activator.CreateInstance(type) as Medicine;
            }

            animal.Heal(medicine);
            _console?.WriteLine($"Veterinarian <<{FirstName} {LastName}>> healed {animal.GetType().Name} <<{animal.ID}>>.");

            return true;
        }
    }
}
