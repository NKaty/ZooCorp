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

namespace ZooCorp.Tests.Animals.Mammals
{
    public class LionTest
    {
        [Fact]
        public void ShouldBeAbleToCreateLion()
        {
            Lion lion = new Lion(1, new List<int>() {5, 10});
            Assert.Equal(1, lion.ID);
            Assert.Equal(5, lion.FeedSchedule[0]);
        }

        [Fact]
        public void ShouldRequireSpace1000()
        {
            Lion lion = new Lion(1);
            Assert.Equal(1000, lion.RequiredSpaceSfFt);
        }

        [Fact]
        public void ShouldHaveFavoriteFoodAsMeat()
        {
            Lion lion = new Lion(1);
            Assert.Equal("Meat", lion.FavoriteFood[0]);
        }

        public static IEnumerable<object[]> DataFriendlyWith =>
            new List<object[]>
            {
                new object[] {new Lion(2)}
            };

        [Theory]
        [MemberData(nameof(DataFriendlyWith))]
        public void ShouldBeFriendly(Animal animal)
        {
            Lion lion = new Lion(1);
            Assert.True(lion.IsFriendlyWith(animal));
        }

        public static IEnumerable<object[]> DataNotFriendlyWith =>
            new List<object[]>
            {
                new object[] {new Penguin(2)},
                new object[] {new Elephant(2)},
                new object[] {new Snake(2)},
                new object[] {new Bison(2)},
                new object[] {new Turtle(2)},
                new object[] {new Parrot(2)}
            };

        [Theory]
        [MemberData(nameof(DataNotFriendlyWith))]
        public void ShouldNotBeFriendly(Animal animal)
        {
            Lion lion = new Lion(1);
            Assert.False(lion.IsFriendlyWith(animal));
        }

        [Fact]
        public void ShouldTrackFeeding()
        {
            ZooConsole console = new ZooConsole();
            Lion lion = new Lion(1, null, console);
            var zooKeeper = new ZooKeeper("Bob", "Smith");
            lion.Feed(new Meat(), zooKeeper);

            Assert.Single(lion.FeedTimes);
            Assert.Equal(zooKeeper, lion.FeedTimes[0].FeedByZooKeeper);
            Assert.Equal("Lion: Lion ID 1 was fed by Bob Smith.", console.Messages[0]);
        }

        [Fact]
        public void ShouldThrowExceptionIfFoodIsNotFavorite()
        {
            ZooConsole console = new ZooConsole();
            Lion lion = new Lion(1, null, console);
            var zooKeeper = new ZooKeeper("Bob", "Smith");

            Assert.Throws<NotFavoriteFoodException>(() => lion.Feed(new Vegetable(), zooKeeper));
            Assert.Equal("Lion: Trying to feed Lion ID 1 with not its favorite food.", console.Messages[0]);
        }

        [Fact]
        public void ShouldBeHungryUntilAteTwicePerDay()
        {
            Lion lion = new Lion(1);
            Assert.True(lion.IsHungry(DateTime.Now));
            lion.Feed(new Meat(), new ZooKeeper("Bob", "Smith"));
            Assert.True(lion.IsHungry(DateTime.Now));
        }

        [Fact]
        public void ShouldNotBeHungryAfterAteTwicePerDay()
        {
            Lion lion = new Lion(1);
            lion.Feed(new Meat(), new ZooKeeper("Bob", "Smith"));
            lion.Feed(new Meat(), new ZooKeeper("Bob", "Smith"));
            Assert.False(lion.IsHungry(DateTime.Now));
        }
    }
}