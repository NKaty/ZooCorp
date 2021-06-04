using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ZooCorp.BusinessLogic.Employees;
using ZooCorp.BusinessLogic.Animals.Birds;

namespace ZooCorp.Tests.Employees
{
    public class ZooKeeperTest
    {
        [Fact]
        public void ShouldBeAbleToCreateZooKeeper()
        {
            ZooKeeper zooKeeper = new ZooKeeper("Bob", "Smith", new List<string>() { "Bison" });
            Assert.Equal("Bob", zooKeeper.FirstName);
            Assert.Equal("Smith", zooKeeper.LastName);
            Assert.Equal("Bison", zooKeeper.AnimalExperiences[0]);
        }

        [Fact]
        public void ShouldBeAbleToAddAnimalExperience()
        {
            ZooKeeper zooKeeper = new ZooKeeper("Bob", "Smith");
            zooKeeper.AddAnimalExperience(new Parrot(1));
            Assert.Equal("Parrot", zooKeeper.AnimalExperiences[0]);
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
            ZooKeeper zooKeeper = new ZooKeeper("Bob", "Smith", new List<string>() { "Parrot" });
            Assert.True(zooKeeper.FeedAnimal(new Parrot(1)));
        }

        [Fact]
        public void ShouldNotFeedAnimalIfDoesNotHaveAnimalExperience()
        {
            ZooKeeper zooKeeper = new ZooKeeper("Bob", "Smith", new List<string>() { "Parrot" });
            Assert.False(zooKeeper.FeedAnimal(new Penguin(1)));
        }
    }
}
