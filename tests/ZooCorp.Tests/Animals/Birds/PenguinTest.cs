using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ZooCorp.BusinessLogic.Animals;
using ZooCorp.BusinessLogic.Animals.Birds;
using ZooCorp.BusinessLogic.Animals.Mammals;
using ZooCorp.BusinessLogic.Animals.Reptiles;
using ZooCorp.BusinessLogic.Foods;
using ZooCorp.BusinessLogic.Employees;
using ZooCorp.BusinessLogic.Exceptions;

namespace ZooCorp.Tests.Animals.Birds
{
    public class PenguinTest
    {
        [Fact]
        public void ShouldBeAbleToCreatePenguin()
        {
            Penguin penguin = new Penguin(1, new List<int>() { 5, 10 });
            Assert.Equal(1, penguin.ID);
            Assert.Equal(5, penguin.FeedSchedule[0]);
        }

        [Fact]
        public void ShouldRequireSpace10()
        {
            Penguin penguin = new Penguin(1);
            Assert.Equal(10, penguin.RequiredSpaceSfFt);
        }

        [Fact]
        public void ShouldHaveFavoriteFoodAsMeat()
        {
            Penguin penguin = new Penguin(1);
            Assert.Equal("Meat", penguin.FavoriteFood[0]);
        }

        public static IEnumerable<object[]> DataFriendlyWith =>
        new List<object[]>
        {
           new [] { new Penguin(2) }
        };

        [Theory]
        [MemberData(nameof(DataFriendlyWith))]
        public void ShouldBeFriendly(Animal animal)
        {
            Penguin penguin = new Penguin(1);
            Assert.True(penguin.IsFriendlyWith(animal));
        }

        public static IEnumerable<object[]> DataNotFriendlyWith =>
        new List<object[]>
        {
           new [] { new Parrot(2) },
           new [] { new Bison(2) },
           new [] { new Elephant(2) },
           new [] { new Turtle(2) },
           new [] { new Lion(2) },
           new [] { new Snake(2) }
        };

        [Theory]
        [MemberData(nameof(DataNotFriendlyWith))]
        public void ShouldNotBeFriendly(Animal animal)
        {
            Penguin penguin = new Penguin(2);
            Assert.False(penguin.IsFriendlyWith(animal));
        }

        [Fact]
        public void ShouldTrackFeeding()
        {
            Penguin penguin = new Penguin(1);
            var zooKeeper = new ZooKeeper("Bob", "Smith");
            penguin.Feed(new Meat(), zooKeeper);

            Assert.Single(penguin.FeedTimes);
            Assert.Equal(zooKeeper, penguin.FeedTimes[0].FeedByZooKeeper);
        }

        [Fact]
        public void ShouldThrowExceptionIfFoodIsNotFavorite()
        {
            Penguin penguin = new Penguin(1);
            var zooKeeper = new ZooKeeper("Bob", "Smith");

            Assert.Throws<NotFavoriteFoodException>(() => penguin.Feed(new Grass(), zooKeeper));
        }

        [Fact]
        public void ShouldBeHungryUntilAteTwicePerDay()
        {
            Penguin penguin = new Penguin(1);
            Assert.True(penguin.IsHungry(DateTime.Now));
            penguin.Feed(new Meat(), new ZooKeeper("Bob", "Smith"));
            Assert.True(penguin.IsHungry(DateTime.Now));
        }

        [Fact]
        public void ShouldNotBeHungryAfterAteTwicePerDay()
        {
            Penguin penguin = new Penguin(1);
            penguin.Feed(new Meat(), new ZooKeeper("Bob", "Smith"));
            penguin.Feed(new Meat(), new ZooKeeper("Bob", "Smith"));
            Assert.False(penguin.IsHungry(DateTime.Now));
        }
    }
}
