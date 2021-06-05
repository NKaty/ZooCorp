using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ZooCorp.BusinessLogic;
using ZooCorp.BusinessLogic.Animals;
using ZooCorp.BusinessLogic.Animals.Birds;
using ZooCorp.BusinessLogic.Animals.Mammals;
using ZooCorp.BusinessLogic.Animals.Reptiles;
using ZooCorp.BusinessLogic.Exceptions;

namespace ZooCorp.Tests
{
   public  class EnclosureTest
    {
        [Fact]
        public void ShouldBeAbleToCreateEnclosure()
        {
            var zoo = new Zoo("London");
            var animal = new Parrot(1);
            Enclosure enclosure = new Enclosure("Enclosure1", zoo, 10000, new List<Animal>() { animal });
            
            Assert.Equal("Enclosure1", enclosure.Name);
            Assert.Equal(zoo, enclosure.ParentZoo);
            Assert.Equal(10000, enclosure.SqureFeet);
            Assert.Equal(animal, enclosure.Animals[0]);
        }

        public static IEnumerable<object[]> DataEnclosureForAddAnimal =>
        new List<object[]>
        {
           new object [] { 2010, new List<Animal>() { new Elephant(1), new Bison(2) } },
           new object [] { 1015, new List<Animal>() { new Turtle(1), new Bison(2), new Parrot(3) } }
        };

        [Theory]
        [MemberData(nameof(DataEnclosureForAddAnimal))]
        public void ShouldAddAnimalToEnclosure(int space, List<Animal> animals)
        {
            var zoo = new Zoo("London");
            var animal = new Parrot(10);
            Enclosure enclosure = new Enclosure("Enclosure1", zoo, space, animals);
            enclosure.AddAnimals(animal);

            Assert.Equal(animal, enclosure.Animals.Last());
        }

        public static IEnumerable<object[]> DataEnclosureForThrowNoAvailableSpaceException =>
        new List<object[]>
        {
           new object [] { 2004, new List<Animal>() { new Elephant(1), new Bison(2) } },
           new object [] { 1014, new List<Animal>() { new Turtle(1), new Bison(2), new Parrot(3) } }
        };

        [Theory]
        [MemberData(nameof(DataEnclosureForThrowNoAvailableSpaceException))]
        public void ShouldThrowNoAvailableSpaceException(int space, List<Animal> animals)
        {
            var zoo = new Zoo("London");
            var animal = new Parrot(10);
            Enclosure enclosure = new Enclosure("Enclosure1", zoo, space, animals);
            

            Assert.Throws<NoAvailableSpaceException>(() => enclosure.AddAnimals(animal));
        }

        public static IEnumerable<object[]> DataEnclosureForThrowNotFriendlyAnimalException =>
        new List<object[]>
        {
           new object [] { new Penguin(1), new List<Animal>() { new Elephant(1) } },
           new object [] { new Lion(1), new List<Animal>() { new Turtle(1), new Bison(2), new Parrot(3) } }
        };

        [Theory]
        [MemberData(nameof(DataEnclosureForThrowNotFriendlyAnimalException))]
        public void ShouldThrowNotFriendlyAnimalException(Animal animal, List<Animal> animals)
        {
            var zoo = new Zoo("London");
            Enclosure enclosure = new Enclosure("Enclosure1", zoo, 5000, animals);


            Assert.Throws<NotFriendlyAnimalException>(() => enclosure.AddAnimals(animal));
        }
    }
}
