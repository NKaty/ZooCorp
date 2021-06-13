using System.Collections.Generic;
using Xunit;
using ZooCorp.BusinessLogic.Common;
using ZooCorp.BusinessLogic.Employees;
using ZooCorp.BusinessLogic.Animals.Birds;

namespace ZooCorp.Tests.Employees
{
    public class ZooKeeperTest
    {
        [Fact]
        public void ShouldBeAbleToCreateZooKeeper()
        {
            ZooKeeper zooKeeper = new ZooKeeper("Bob", "Smith", new List<string>() {"Bison"});
            Assert.Equal("Bob", zooKeeper.FirstName);
            Assert.Equal("Smith", zooKeeper.LastName);
            Assert.Equal("Bison", zooKeeper.AnimalExperiences[0]);
        }

        [Fact]
        public void ShouldBeAbleToAddAnimalExperience()
        {
            ZooConsole console = new ZooConsole();
            ZooKeeper zooKeeper = new ZooKeeper("Bob", "Smith", null, console);
            zooKeeper.AddAnimalExperience(new Parrot(1));
            Assert.Equal("Parrot", zooKeeper.AnimalExperiences[0]);
            Assert.Equal("ZooKeeper: Added experience with Parrot to ZooKeeper Bob Smith.", console.Messages[0]);
        }

        [Fact]
        public void ShouldHaveAnimalExperience()
        {
            ZooKeeper zooKeeper = new ZooKeeper("Bob", "Smith");
            zooKeeper.AddAnimalExperience(new Parrot(1));
            Assert.True(zooKeeper.HasAnimalExperience(new Parrot(1)));
        }

        [Fact]
        public void ShouldNotHaveAnimalExperience()
        {
            ZooKeeper zooKeeper = new ZooKeeper("Bob", "Smith");
            Assert.False(zooKeeper.HasAnimalExperience(new Parrot(1)));
        }

        [Fact]
        public void ShouldFeedAnimalIfHasAnimalExperience()
        {
            ZooConsole console = new ZooConsole();
            ZooKeeper zooKeeper = new ZooKeeper("Bob", "Smith", new List<string>() {"Parrot"}, console);
            Assert.True(zooKeeper.FeedAnimal(new Parrot(1)));
            Assert.Equal("ZooKeeper: ZooKeeper Bob Smith fed Parrot ID 1.", console.Messages[0]);
        }

        [Fact]
        public void ShouldNotFeedAnimalIfDoesNotHaveAnimalExperience()
        {
            ZooKeeper zooKeeper = new ZooKeeper("Bob", "Smith", new List<string>() {"Parrot"});
            Assert.False(zooKeeper.FeedAnimal(new Penguin(1)));
        }
    }
}