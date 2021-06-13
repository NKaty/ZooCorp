using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ZooCorp.BusinessLogic;
using ZooCorp.BusinessLogic.Animals;
using ZooCorp.BusinessLogic.Animals.Birds;
using ZooCorp.BusinessLogic.Animals.Mammals;
using ZooCorp.BusinessLogic.Animals.Reptiles;
using ZooCorp.BusinessLogic.Exceptions;
using ZooCorp.BusinessLogic.Employees;
using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.Tests
{
    public class ZooTest
    {
        [Fact]
        public void ShouldBeAbleToCreateZoo()
        {
            ZooConsole console = new ZooConsole();
            Zoo zoo = new Zoo("London", console);
            Assert.Equal("London", zoo.Location);
            Assert.Equal("Zoo: Created zoo in London.", console.Messages[0]);
        }

        [Fact]
        public void ShouldBeAbleToCreateEnclosureInZoo()
        {
            ZooConsole console = new ZooConsole();
            Zoo zoo = new Zoo("London", console);
            zoo.AddEnclosure("Enclosure1", 10000);
            Assert.Equal("Enclosure1", zoo.Enclosures[0].Name);
            Assert.Equal(10000, zoo.Enclosures[0].SquareFeet);
            Assert.Equal(zoo, zoo.Enclosures[0].ParentZoo);
            Assert.Equal("Zoo: Created enclosure Enclosure1 in zoo in London.", console.Messages[1]);
        }

        [Fact]
        public void ShouldFindAvailableEnclosure()
        {
            ZooConsole console = new ZooConsole();
            Zoo zoo = new Zoo("London", console);
            zoo.AddEnclosure("Enclosure1", 10000);
            Animal animal = new Parrot(1);
            Enclosure enclosure1 = zoo.FindAvailableEnclosure(animal);
            Assert.Equal("Enclosure1", enclosure1.Name);
            Assert.Equal(10000, enclosure1.SquareFeet);
            Assert.Equal(zoo, enclosure1.ParentZoo);
            enclosure1.AddAnimals(animal);
            Enclosure enclosure2 = zoo.FindAvailableEnclosure(new Elephant(2));
            Assert.Equal("Enclosure1", enclosure2.Name);
            Assert.Equal(10000, enclosure2.SquareFeet);
            Assert.Equal(zoo, enclosure2.ParentZoo);
            Assert.Equal("Zoo: Found an available enclosure Enclosure1 in zoo in London.", console.Messages[2]);
        }

        public static IEnumerable<object[]> DataEnclosureForNoAvailableEnclosureException =>
            new List<object[]>
            {
                new object[] {new Animal[] {new Bison(1), new Elephant(2)}},
                new object[] {new Animal[] {new Lion(1), new Snake(2)}}
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
            ZooConsole console = new ZooConsole();
            Zoo zoo = new Zoo("London", console);
            zoo.AddEnclosure("Enclosure1", 10000);
            zoo.AddAnimals(animal, new List<int>() {8, 18});

            Assert.Equal(animal, zoo.Enclosures[0].Animals[0].GetType().Name);
            Assert.Equal($"Zoo: Added {animal} ID {zoo.Enclosures[0].Animals[0].ID} to Enclosure1 in zoo in London.",
                console.Messages[4]);
        }

        [Fact]
        public void ShouldAddAnimalsThrowUnknownAnimalException()
        {
            ZooConsole console = new ZooConsole();
            Zoo zoo = new Zoo("London", console);
            zoo.AddEnclosure("Enclosure1", 10000);

            Assert.Throws<UnknownAnimalException>(() => zoo.AddAnimals("Horse", new List<int>() {8, 18}));
            Assert.Equal("Zoo: Trying to add unknown type of animal to the zoo in London.", console.Messages[2]);
        }

        [Fact]
        public void ShouldAnimalsHaveUniqueId()
        {
            Zoo zoo = new Zoo("London");
            Enclosure enclosure = zoo.AddEnclosure("Enclosure1", 10000);
            zoo.AddAnimals("parrot");
            zoo.AddAnimals("parrot");
            zoo.AddAnimals("elephant");

            Assert.NotEqual(enclosure.Animals[0].ID, enclosure.Animals[1].ID);
            Assert.NotEqual(enclosure.Animals[1].ID, enclosure.Animals[2].ID);
        }

        public static IEnumerable<object[]> DataEmployeeToCreateEmployee =>
            new List<object[]>
            {
                new object[] {"Veterinarian", "Bob", "Smith", new List<string>() {"Elephant", "Parrot", "Lion"}},
                new object[] {"ZooKeeper", "Bob", "Smith", new List<string>() {"Elephant", "Parrot", "Lion"}}
            };

        [Theory]
        [MemberData(nameof(DataEmployeeToCreateEmployee))]
        public void ShouldCreateAndHireExperiencedEmployee(string type, string firstName, string lastName,
            List<string> animals)
        {
            ZooConsole console = new ZooConsole();
            Zoo zoo = new Zoo("London", console);
            zoo.AddEnclosure("Enclosure1", 10000);
            zoo.AddEnclosure("Enclosure2", 10000);
            zoo.AddAnimals("parrot");
            zoo.AddAnimals("lion");
            zoo.AddAnimals("elephant");
            zoo.CreateEmployee(type, firstName, lastName, animals);

            Assert.Equal("Bob", zoo.Employees[0].FirstName);
            Assert.Equal("Smith", zoo.Employees[0].LastName);
            Assert.Equal($"Zoo: Created {type} Bob Smith in zoo in London.", console.Messages[13]);
        }

        [Fact]
        public void ShouldThrowUnknownEmployeeException()
        {
            ZooConsole console = new ZooConsole();
            Zoo zoo = new Zoo("London", console);

            Assert.Throws<UnknownEmployeeException>(() => zoo.CreateEmployee("Janitor", "Bob", "Smith"));
            Assert.Equal("Zoo: Trying to hire unknown type of employee.", console.Messages[1]);
        }

        public static IEnumerable<object[]> DataEmployeeToCheckExperience =>
            new List<object[]>
            {
                new object[] {new Veterinarian("Bob", "Smith", new List<string>() {"Elephant", "Parrot", "Lion"})},
                new object[] {new ZooKeeper("Bob", "Smith", new List<string>() {"Elephant", "Parrot", "Lion"})}
            };

        [Theory]
        [MemberData(nameof(DataEmployeeToCheckExperience))]
        public void ShouldHireExperiencedEmployee(IEmployee employee)
        {
            ZooConsole console = new ZooConsole();
            Zoo zoo = new Zoo("London", console);
            zoo.AddEnclosure("Enclosure1", 10000);
            zoo.AddEnclosure("Enclosure2", 10000);
            zoo.AddAnimals("parrot");
            zoo.AddAnimals("lion");
            zoo.AddAnimals("elephant");
            zoo.HireEmployee(employee);

            Assert.Equal("Bob", zoo.Employees[0].FirstName);
            Assert.Equal("Smith", zoo.Employees[0].LastName);
            Assert.Equal(
                $"Zoo: Hired {employee.GetType().Name} {employee.FirstName} {employee.LastName} in zoo in London.",
                console.Messages[12]);
        }

        [Theory]
        [MemberData(nameof(DataEmployeeToCheckExperience))]
        public void ShouldTrowNoNeededExperienceException(IEmployee employee)
        {
            ZooConsole console = new ZooConsole();
            Zoo zoo = new Zoo("London", console);
            zoo.AddEnclosure("Enclosure1", 10000);
            zoo.AddEnclosure("Enclosure2", 10000);
            zoo.AddAnimals("parrot");
            zoo.AddAnimals("snake");
            zoo.AddAnimals("elephant");

            Assert.Throws<NoNeededExperienceException>(() => zoo.HireEmployee(employee));
            Assert.Equal(
                $"Zoo: {employee.GetType().Name} {employee.FirstName} {employee.LastName} does not have needed experience.",
                console.Messages[12]);
            Assert.Equal("Snake: No experience", console.Messages[13]);
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
            var veterinarian1 = new Veterinarian("Bob", "Smith",
                new List<string>() {"Elephant", "Parrot", "Lion", "Penguin"});
            var veterinarian2 =
                new Veterinarian("Tom", "Ford", new List<string>() {"Elephant", "Parrot", "Lion", "Penguin"});
            zoo.HireEmployee(veterinarian1);
            zoo.HireEmployee(veterinarian2);
            var dividedAnimals = zoo.DivideAnimalsBetweenEmployees("Veterinarian", animal => true);

            Assert.Equal(2, dividedAnimals[0].Item2.Count());
            Assert.Equal(2, dividedAnimals[1].Item2.Count());
        }

        [Fact]
        public void ShouldThrowUnknownAnimalExceptionIfNoVeterinarianWithSuchAnimalExperience()
        {
            Zoo zoo = new Zoo("London");
            zoo.AddEnclosure("Enclosure1", 10000);
            zoo.AddEnclosure("Enclosure2", 10000);
            zoo.AddEnclosure("Enclosure3", 10000);
            zoo.AddAnimals("lion");
            zoo.AddAnimals("elephant");
            zoo.AddAnimals("penguin");
            var veterinarian1 = new Veterinarian("Bob", "Smith",
                new List<string>() {"Elephant", "Parrot", "Lion", "Penguin"});
            var veterinarian2 =
                new Veterinarian("Tom", "Ford", new List<string>() {"Elephant", "Parrot", "Lion", "Penguin"});
            zoo.HireEmployee(veterinarian1);
            zoo.HireEmployee(veterinarian2);
            zoo.AddAnimals("turtle");

            Assert.Throws<UnknownAnimalException>(() =>
                zoo.DivideAnimalsBetweenEmployees("Veterinarian", animal => true));
        }

        [Fact]
        public void ShouldThrowUnknownAnimalExceptionIfNoZooKeeperWithSuchAnimalExperience()
        {
            Zoo zoo = new Zoo("London");
            zoo.AddEnclosure("Enclosure1", 10000);
            zoo.AddEnclosure("Enclosure2", 10000);
            zoo.AddEnclosure("Enclosure3", 10000);
            zoo.AddAnimals("parrot");
            zoo.AddAnimals("lion");
            zoo.AddAnimals("elephant");
            zoo.AddAnimals("penguin");
            var zooKeeper1 =
                new ZooKeeper("Bob", "Smith", new List<string>() {"Elephant", "Parrot", "Lion", "Penguin"});
            var zooKeeper2 = new ZooKeeper("Tom", "Ford", new List<string>() {"Elephant", "Parrot", "Lion", "Penguin"});
            zoo.HireEmployee(zooKeeper1);
            zoo.HireEmployee(zooKeeper2);
            zoo.AddAnimals("turtle");

            Assert.Throws<UnknownAnimalException>(() => zoo.DivideAnimalsBetweenEmployees("ZooKeeper", animal => true));
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
            var zooKeeper1 =
                new ZooKeeper("Bob", "Smith", new List<string>() {"Elephant", "Parrot", "Lion", "Penguin"});
            var zooKeeper2 = new ZooKeeper("Tom", "Ford", new List<string>() {"Elephant", "Parrot", "Lion", "Penguin"});
            zoo.HireEmployee(zooKeeper1);
            zoo.HireEmployee(zooKeeper2);
            var dividedAnimals = zoo.DivideAnimalsBetweenEmployees("ZooKeeper", animal => true);

            Assert.Equal(2, dividedAnimals[0].Item2.Count());
            Assert.Equal(2, dividedAnimals[1].Item2.Count());
        }

        [Fact]
        public void ShouldKeepersFeedAnimals()
        {
            ZooConsole console = new ZooConsole();
            Zoo zoo = new Zoo("London", console);
            zoo.AddEnclosure("Enclosure1", 10000);
            zoo.AddEnclosure("Enclosure2", 10000);
            zoo.AddEnclosure("Enclosure3", 10000);
            var parrot = zoo.AddAnimals("parrot");
            var lion = zoo.AddAnimals("lion");
            var penguin = zoo.AddAnimals("penguin");
            var penguin2 = zoo.AddAnimals("penguin");
            var zooKeeper1 =
                new ZooKeeper("Bob", "Smith", new List<string>() {"Elephant", "Parrot", "Lion", "Penguin"});
            var zooKeeper2 = new ZooKeeper("Tom", "Ford", new List<string>() {"Elephant", "Parrot", "Lion", "Penguin"});
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
            Assert.Equal("Zoo: Parrot ID 1 was fed by Bob Smith in zoo in London.", console.Messages[26]);
        }

        [Fact]
        public void ShouldVeterinarianHealAnimals()
        {
            ZooConsole console = new ZooConsole();
            Zoo zoo = new Zoo("London", console);
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
            var veterinarian1 = new Veterinarian("Bob", "Smith",
                new List<string>() {"Elephant", "Parrot", "Lion", "Penguin"});
            var veterinarian2 =
                new Veterinarian("Tom", "Ford", new List<string>() {"Elephant", "Parrot", "Lion", "Penguin"});
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
            Assert.Equal("Zoo: Parrot ID 1 was healed by Bob Smith in zoo in London.", console.Messages[23]);
        }
    }
}