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
using ZooCorp.BusinessLogic.Employees;

namespace ZooCorp.Tests
{
    public class ZooTest
    {
        [Fact]
        public void ShouldBeAbleToCreateZoo()
        {
            Zoo zoo = new Zoo("London");
            Assert.Equal("London", zoo.Location);
        }

        [Fact]
        public void ShouldBeAbleToCreateEnclosureInZoo()
        {
            Zoo zoo = new Zoo("London");
            zoo.AddEnclosure("Enclosure1", 10000);
            Assert.Equal("Enclosure1", zoo.Enclosures[0].Name);
            Assert.Equal(10000, zoo.Enclosures[0].SqureFeet);
            Assert.Equal(zoo, zoo.Enclosures[0].ParentZoo);
        }

        [Fact]
        public void ShouldFindAvailableEnclosure()
        {
            Zoo zoo = new Zoo("London");
            zoo.AddEnclosure("Enclosure1", 10000);
            Animal animal = new Parrot(1);
            Enclosure enclosure1 = zoo.FindAvailableEnclosure(animal);
            Assert.Equal("Enclosure1", enclosure1.Name);
            Assert.Equal(10000, enclosure1.SqureFeet);
            Assert.Equal(zoo, enclosure1.ParentZoo);
            enclosure1.AddAnimals(animal);
            Enclosure enclosure2 = zoo.FindAvailableEnclosure(new Elephant(2));
            Assert.Equal("Enclosure1", enclosure2.Name);
            Assert.Equal(10000, enclosure2.SqureFeet);
            Assert.Equal(zoo, enclosure2.ParentZoo);
        }

        public static IEnumerable<object[]> DataEnclosureForNoAvailableEnclosureException =>
        new List<object[]>
        {
            new object [] { new Animal[] { new Bison(1), new Elephant(2) } },
            new object [] { new Animal[] { new Lion(1), new Snake(2) } }
        };

        [Theory]
        [MemberData(nameof(DataEnclosureForNoAvailableEnclosureException))]
        public void ShouldThrowNoAvailableEnclosureExceptionIfTheseIsNoAvailableEnclosure(Animal[] animals)
        {
            Zoo zoo = new Zoo("London");
            Enclosure enclosure1 = zoo.AddEnclosure("Enclosure1", 1000);
            Enclosure enclosure2 = zoo.AddEnclosure("Enclosure2", 1000);
            enclosure1.AddAnimals(animals[0]);
            enclosure2.AddAnimals(animals[1]);

            Assert.Throws<NoAvailableEnclosureException>(() => zoo.FindAvailableEnclosure(new Parrot(3)));
        }

        [Theory]
        [InlineData("Parrot")]
        [InlineData("Penguin")]
        [InlineData("Lion")]
        [InlineData("Bison")]
        [InlineData("Elephant")]
        [InlineData("Snake")]
        [InlineData("Turtle")]
        public void ShouldCreateAnimalAndAddItToEnclosure(string animal)
        {
            Zoo zoo = new Zoo("London");
            zoo.AddEnclosure("Enclosure1", 10000);
            zoo.AddAnimals(animal, new List<int>() { 8, 18 });

            Assert.Equal(animal, zoo.Enclosures[0].Animals[0].GetType().Name);
        }

        [Fact]
        public void ShouldAddAnimalsThrowUnknownAnimalException()
        {
            Zoo zoo = new Zoo("London");
            zoo.AddEnclosure("Enclosure1", 10000);

            Assert.Throws<UnknownAnimalException>(() => zoo.AddAnimals("Horse", new List<int>() { 8, 18 }));
        }

        [Fact]
        public void ShouldAnimalsHaveUniqueID()
        {
            Zoo zoo = new Zoo("London");
            Enclosure enclosure = zoo.AddEnclosure("Enclosure1", 10000);
            zoo.AddAnimals("parrot");
            zoo.AddAnimals("parrot");
            zoo.AddAnimals("elephant");

            Assert.NotEqual(enclosure.Animals[0].ID, enclosure.Animals[1].ID);
            Assert.NotEqual(enclosure.Animals[1].ID, enclosure.Animals[2].ID);
        }

        public static IEnumerable<object[]> DataEmployeeToCheckExperience =>
        new List<object[]>
        {
            new object [] { new Veterinarian("Bob", "Smith", new List<string>() { "Elephant", "Parrot", "Lion" }) },
            new object [] { new ZooKeeper("Bob", "Smith", new List<string>() { "Elephant", "Parrot", "Lion" }) }
        };

        [Theory]
        [MemberData(nameof(DataEmployeeToCheckExperience))]
        public void ShouldHireExperiencedEmployee(IEmployee employee)
        {
            Zoo zoo = new Zoo("London");
            zoo.AddEnclosure("Enclosure1", 10000);
            zoo.AddEnclosure("Enclosure2", 10000);
            zoo.AddAnimals("parrot");
            zoo.AddAnimals("lion");
            zoo.AddAnimals("elephant");
            zoo.HireEmployee(employee);

            Assert.Equal("Bob", zoo.Employees[0].FirstName);
            Assert.Equal("Smith", zoo.Employees[0].LastName);
        }

        [Theory]
        [MemberData(nameof(DataEmployeeToCheckExperience))]
        public void ShouldTrowNoNeededExperienceException(IEmployee employee)
        {
            Zoo zoo = new Zoo("London");
            zoo.AddEnclosure("Enclosure1", 10000);
            zoo.AddEnclosure("Enclosure2", 10000);
            zoo.AddAnimals("parrot");
            zoo.AddAnimals("snake");
            zoo.AddAnimals("elephant");

            Assert.Throws<NoNeededExperienceException>(() => zoo.HireEmployee(employee));
        }

        [Fact]
        public void ShouldDivideAnimalsBetweenVeterinarians()
        {
            Zoo zoo = new Zoo("London");
            zoo.AddEnclosure("Enclosure1", 10000);
            zoo.AddEnclosure("Enclosure2", 10000);
            zoo.AddEnclosure("Enclosure3", 10000);
            zoo.AddAnimals("parrot");
            zoo.AddAnimals("lion");
            zoo.AddAnimals("elephant");
            zoo.AddAnimals("penguin");
            var veterinarian1 = new Veterinarian("Bob", "Smith", new List<string>() { "Elephant", "Parrot", "Lion", "Penguin" });
            var veterinarian2 = new Veterinarian("Tom", "Ford", new List<string>() { "Elephant", "Parrot", "Lion", "Penguin" });
            zoo.HireEmployee(veterinarian1);
            zoo.HireEmployee(veterinarian2);
            var dividedAnimals = zoo.DivideAnimalsBetweenEmployees("Veterinarian", animal => true);

            Assert.Equal(2, dividedAnimals[0].Item2.Count());
            Assert.Equal(2, dividedAnimals[1].Item2.Count());
        }

        [Fact]
        public void ShouldDivideAnimalsBetweenZooKeepers()
        {
            Zoo zoo = new Zoo("London");
            zoo.AddEnclosure("Enclosure1", 10000);
            zoo.AddEnclosure("Enclosure2", 10000);
            zoo.AddEnclosure("Enclosure3", 10000);
            zoo.AddAnimals("parrot");
            zoo.AddAnimals("lion");
            zoo.AddAnimals("elephant");
            zoo.AddAnimals("penguin");
            var zooKeeper1 = new ZooKeeper("Bob", "Smith", new List<string>() { "Elephant", "Parrot", "Lion", "Penguin" });
            var zooKeeper2 = new ZooKeeper("Tom", "Ford", new List<string>() { "Elephant", "Parrot", "Lion", "Penguin" });
            zoo.HireEmployee(zooKeeper1);
            zoo.HireEmployee(zooKeeper2);
            var dividedAnimals = zoo.DivideAnimalsBetweenEmployees("ZooKeeper", animal => true);

            Assert.Equal(2, dividedAnimals[0].Item2.Count());
            Assert.Equal(2, dividedAnimals[1].Item2.Count());
        }

        [Fact]
        public void ShouldKeepersFeedAnimals()
        {
            Zoo zoo = new Zoo("London");
            zoo.AddEnclosure("Enclosure1", 10000);
            zoo.AddEnclosure("Enclosure2", 10000);
            zoo.AddEnclosure("Enclosure3", 10000);
            var parrot = zoo.AddAnimals("parrot");
            var lion = zoo.AddAnimals("lion");
            var penguin = zoo.AddAnimals("penguin");
            var penguin2 = zoo.AddAnimals("penguin");
            var zooKeeper1 = new ZooKeeper("Bob", "Smith", new List<string>() { "Elephant", "Parrot", "Lion", "Penguin" });
            var zooKeeper2 = new ZooKeeper("Tom", "Ford", new List<string>() { "Elephant", "Parrot", "Lion", "Penguin" });
            zoo.HireEmployee(zooKeeper1);
            zoo.HireEmployee(zooKeeper2);
            zooKeeper1.FeedAnimal(parrot);
            zooKeeper2.FeedAnimal(penguin2);
            zooKeeper1.FeedAnimal(penguin2);
            DateTime dateTime = DateTime.Now;
            zoo.FeedAnimals(dateTime);

            Assert.Equal(2, parrot.FeedTimes.Count);
            Assert.Single(lion.FeedTimes);
            Assert.Single(penguin.FeedTimes);
            Assert.Equal(2, penguin2.FeedTimes.Count);
        }

        [Fact]
        public void ShouldVeterinarianHealAnimals()
        {
            Zoo zoo = new Zoo("London");
            zoo.AddEnclosure("Enclosure1", 10000);
            zoo.AddEnclosure("Enclosure2", 10000);
            zoo.AddEnclosure("Enclosure3", 10000);
            var parrot = zoo.AddAnimals("parrot");
            parrot.MarkSick();
            var lion = zoo.AddAnimals("lion");
            lion.MarkSick();
            var elephant = zoo.AddAnimals("elephant");
            elephant.MarkSick();
            var penguin = zoo.AddAnimals("penguin");
            penguin.MarkSick();
            var veterinarian1 = new Veterinarian("Bob", "Smith", new List<string>() { "Elephant", "Parrot", "Lion", "Penguin" });
            var veterinarian2 = new Veterinarian("Tom", "Ford", new List<string>() { "Elephant", "Parrot", "Lion", "Penguin" });
            zoo.HireEmployee(veterinarian1);
            zoo.HireEmployee(veterinarian2);
            Assert.True(parrot.IsSick());
            Assert.True(lion.IsSick());
            Assert.True(elephant.IsSick());
            Assert.True(penguin.IsSick());
            zoo.HealAnimals();
            Assert.False(parrot.IsSick());
            Assert.False(lion.IsSick());
            Assert.False(elephant.IsSick());
            Assert.False(penguin.IsSick());
        }
    }
}
