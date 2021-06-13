using System.Collections.Generic;
using System.Linq;
using Xunit;
using ZooCorp.BusinessLogic;
using ZooCorp.BusinessLogic.Animals;
using ZooCorp.BusinessLogic.Animals.Birds;
using ZooCorp.BusinessLogic.Animals.Mammals;
using ZooCorp.BusinessLogic.Animals.Reptiles;
using ZooCorp.BusinessLogic.Exceptions;
using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.Tests
{
    public class EnclosureTest
    {
        [Fact]
        public void ShouldBeAbleToCreateEnclosure()
        {
            var zoo = new Zoo("London");
            var animal = new Parrot(1);
            Enclosure enclosure = new Enclosure("Enclosure1", zoo, 10000, new List<Animal>() {animal});

            Assert.Equal("Enclosure1", enclosure.Name);
            Assert.Equal(zoo, enclosure.ParentZoo);
            Assert.Equal(10000, enclosure.SquareFeet);
            Assert.Equal(animal, enclosure.Animals[0]);
        }

        public static IEnumerable<object[]> DataEnclosureForAddAnimal =>
            new List<object[]>
            {
                new object[] {2010, new List<Animal>() {new Elephant(1), new Bison(2)}},
                new object[] {1015, new List<Animal>() {new Turtle(1), new Bison(2), new Parrot(3)}}
            };

        [Theory]
        [MemberData(nameof(DataEnclosureForAddAnimal))]
        public void ShouldAddAnimalToEnclosure(int space, List<Animal> animals)
        {
            ZooConsole console = new ZooConsole();
            var zoo = new Zoo("London");
            var animal = new Parrot(10);
            Enclosure enclosure = new Enclosure("Enclosure1", zoo, space, animals, console);
            enclosure.AddAnimals(animal);

            Assert.Equal(animal, enclosure.Animals.Last());
            Assert.Equal("Enclosure: Added Parrot ID 10 in zoo in Enclosure1.", console.Messages[0]);
        }

        [Theory]
        [MemberData(nameof(DataEnclosureForAddAnimal))]
        public void ShouldReturnTrueIfThereIsAvailableSpace(int space, List<Animal> animals)
        {
            var zoo = new Zoo("London");
            var animal = new Parrot(10);
            Enclosure enclosure = new Enclosure("Enclosure1", zoo, space, animals);

            Assert.True(enclosure.IsAvailableSpace(animal));
        }

        [Theory]
        [MemberData(nameof(DataEnclosureForAddAnimal))]
        public void ShouldReturnTrueIfAnimalsAreFriendly(int space, List<Animal> animals)
        {
            var zoo = new Zoo("London");
            var animal = new Parrot(10);
            Enclosure enclosure = new Enclosure("Enclosure1", zoo, space, animals);

            Assert.True(enclosure.IsAnimalFriendly(animal));
        }

        public static IEnumerable<object[]> DataEnclosureForThrowNoAvailableSpaceException =>
            new List<object[]>
            {
                new object[] {2004, new List<Animal>() {new Elephant(1), new Bison(2)}},
                new object[] {1014, new List<Animal>() {new Turtle(1), new Bison(2), new Parrot(3)}}
            };

        [Theory]
        [MemberData(nameof(DataEnclosureForThrowNoAvailableSpaceException))]
        public void ShouldThrowNoAvailableSpaceException(int space, List<Animal> animals)
        {
            ZooConsole console = new ZooConsole();
            var zoo = new Zoo("London");
            var animal = new Parrot(10);
            Enclosure enclosure = new Enclosure("Enclosure1", zoo, space, animals, console);


            Assert.Throws<NoAvailableSpaceException>(() => enclosure.AddAnimals(animal));
            Assert.Equal("Enclosure: The enclosure Enclosure1 doesn't have available space.", console.Messages[0]);
        }

        [Theory]
        [MemberData(nameof(DataEnclosureForThrowNoAvailableSpaceException))]
        public void ShouldReturnFalseIfNoAvailableSpace(int space, List<Animal> animals)
        {
            var zoo = new Zoo("London");
            var animal = new Parrot(10);
            Enclosure enclosure = new Enclosure("Enclosure1", zoo, space, animals);


            Assert.False(enclosure.IsAvailableSpace(animal));
        }

        public static IEnumerable<object[]> DataEnclosureForThrowNotFriendlyAnimalException =>
            new List<object[]>
            {
                new object[] {new Penguin(1), new List<Animal>() {new Elephant(1)}},
                new object[] {new Lion(1), new List<Animal>() {new Turtle(1), new Bison(2), new Parrot(3)}}
            };

        [Theory]
        [MemberData(nameof(DataEnclosureForThrowNotFriendlyAnimalException))]
        public void ShouldThrowNotFriendlyAnimalException(Animal animal, List<Animal> animals)
        {
            ZooConsole console = new ZooConsole();
            var zoo = new Zoo("London");
            Enclosure enclosure = new Enclosure("Enclosure1", zoo, 5000, animals, console);


            Assert.Throws<NotFriendlyAnimalException>(() => enclosure.AddAnimals(animal));
            Assert.Equal("Enclosure: The enclosure Enclosure1 has unfriendly animals.", console.Messages[0]);
        }

        [Theory]
        [MemberData(nameof(DataEnclosureForThrowNotFriendlyAnimalException))]
        public void ShouldReturnFalseIfAnimalsAreUnfriendly(Animal animal, List<Animal> animals)
        {
            var zoo = new Zoo("London");
            Enclosure enclosure = new Enclosure("Enclosure1", zoo, 5000, animals);


            Assert.False(enclosure.IsAnimalFriendly(animal));
        }
    }
}