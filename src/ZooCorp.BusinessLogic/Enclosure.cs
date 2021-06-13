using System.Collections.Generic;
using ZooCorp.BusinessLogic.Animals;
using ZooCorp.BusinessLogic.Common;
using ZooCorp.BusinessLogic.Exceptions;

namespace ZooCorp.BusinessLogic
{
    public class Enclosure
    {
        private readonly IConsole _console;
        public string Name { get; private set; }

        public List<Animal> Animals { get; private set; }

        public Zoo ParentZoo { get; private set; }

        public int SquareFeet { get; private set; }

        public Enclosure(string name, Zoo parentZoo, int squareFeet, List<Animal> animals = null,
            IConsole console = null)
        {
            Name = name;
            ParentZoo = parentZoo;
            SquareFeet = squareFeet;
            Animals = animals ?? new List<Animal>();
            _console = console;
        }

        public void AddAnimals(Animal animal)
        {
            if (!IsAvailableSpace(animal))
            {
                _console?.WriteLine($"Enclosure: The enclosure {Name} doesn't have available space.");
                throw new NoAvailableSpaceException($"The enclosure {Name} doesn't have available space.");
            }

            if (!IsAnimalFriendly(animal))
            {
                _console?.WriteLine($"Enclosure: The enclosure {Name} has unfriendly animals.");
                throw new NotFriendlyAnimalException($"The enclosure {Name} has unfriendly animals.");
            }

            _console?.WriteLine($"Enclosure: Added {animal.GetType().Name} ID {animal.ID} in zoo in {Name}.");
            Animals.Add(animal);
        }

        public bool IsAvailableSpace(Animal newAnimal)
        {
            int availableSpace = SquareFeet;

            foreach (var animal in Animals)
            {
                availableSpace -= animal.RequiredSpaceSfFt;
            }

            return newAnimal.RequiredSpaceSfFt <= availableSpace;
        }

        public bool IsAnimalFriendly(Animal newAnimal)
        {
            foreach (var animal in Animals)
            {
                if (!newAnimal.IsFriendlyWith(animal)) return false;
            }

            return true;
        }
    }
}