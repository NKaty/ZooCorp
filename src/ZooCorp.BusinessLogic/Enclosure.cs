using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Animals;
using ZooCorp.BusinessLogic.Exceptions;

namespace ZooCorp.BusinessLogic
{
    public class Enclosure
    {
        public string Name { get; private set; }

        public List<Animal> Animals { get; private set; }

        public Zoo ParentZoo { get; private set; }

        public int SqureFeet { get; private set; }

        public Enclosure(string name, Zoo parentZoo, int squreFeet, List<Animal> animals = null)
        {
            Name = name;
            ParentZoo = parentZoo;
            SqureFeet = squreFeet;
            Animals = animals ?? new List<Animal>();
        }

        public void AddAnimals(Animal animal)
        {
            if (!IsAvailableSpace(animal))
            {
                throw new NoAvailableSpaceException($"The enclosure { Name } doesn't have available space.");
            }

            if (!IsAnimalFriendly(animal))
            {
                throw new NotFriendlyAnimalException($"The enclosure { Name } has unfriendly animals.");
            }

            Animals.Add(animal);
        }

        public bool IsAvailableSpace(Animal newAnimal)
        {
            int availableSpace = SqureFeet;

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
