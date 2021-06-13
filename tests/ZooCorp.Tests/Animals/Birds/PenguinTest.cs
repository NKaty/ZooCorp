using System;
using System.Collections.Generic;
using Xunit;
using ZooCorp.BusinessLogic.Animals;
using ZooCorp.BusinessLogic.Animals.Birds;
using ZooCorp.BusinessLogic.Animals.Mammals;
using ZooCorp.BusinessLogic.Animals.Reptiles;
using ZooCorp.BusinessLogic.Foods;
using ZooCorp.BusinessLogic.Employees;
using ZooCorp.BusinessLogic.Exceptions;
using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.Tests.Animals.Birds
{
    public class PenguinTest
    {
        [Fact]
        public void ShouldBeAbleToCreatePenguin()
        {
            Penguin penguin = new Penguin(1, new List<int>() {5, 10});
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
                new object[] {new Penguin(2)}
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
                new object[] {new Parrot(2)},
                new object[] {new Bison(2)},
                new object[] {new Elephant(2)},
                new object[] {new Turtle(2)},
                new object[] {new Lion(2)},
                new object[] {new Snake(2)}
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
            ZooConsole console = new ZooConsole();
            Penguin penguin = new Penguin(1, null, console);
            var zooKeeper = new ZooKeeper("Bob", "Smith");
            penguin.Feed(new Meat(), zooKeeper);

            Assert.Single(penguin.FeedTimes);
            Assert.Equal(zooKeeper, penguin.FeedTimes[0].FeedByZooKeeper);
            Assert.Equal("Penguin: Penguin ID 1 was fed by Bob Smith.", console.Messages[0]);
        }

        [Fact]
        public void ShouldThrowExceptionIfFoodIsNotFavorite()
        {
            ZooConsole console = new ZooConsole();
            Penguin penguin = new Penguin(1, null, console);
            var zooKeeper = new ZooKeeper("Bob", "Smith");

            Assert.Throws<NotFavoriteFoodException>(() => penguin.Feed(new Grass(), zooKeeper));
            Assert.Equal("Penguin: Trying to feed Penguin ID 1 with not its favorite food.", console.Messages[0]);
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