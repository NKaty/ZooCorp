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
    public class ParrotTest
    {
        [Fact]
        public void ShouldBeAbleToCreateParrot()
        {
            Parrot parrot = new Parrot(1, new List<int>() {5, 10});
            Assert.Equal(1, parrot.ID);
            Assert.Equal(5, parrot.FeedSchedule[0]);
        }

        [Fact]
        public void ShouldRequireSpace5()
        {
            Parrot parrot = new Parrot(1);
            Assert.Equal(5, parrot.RequiredSpaceSfFt);
        }

        [Fact]
        public void ShouldHaveFavoriteFoodAsVegetable()
        {
            Parrot parrot = new Parrot(1);
            Assert.Equal("Vegetable", parrot.FavoriteFood[0]);
        }

        public static IEnumerable<object[]> DataFriendlyWith =>
            new List<object[]>
            {
                new object[] {new Parrot(2)},
                new object[] {new Bison(2)},
                new object[] {new Elephant(2)},
                new object[] {new Turtle(2)}
            };

        [Theory]
        [MemberData(nameof(DataFriendlyWith))]
        public void ShouldBeFriendly(Animal animal)
        {
            Parrot parrot = new Parrot(1);
            Assert.True(parrot.IsFriendlyWith(animal));
        }

        public static IEnumerable<object[]> DataNotFriendlyWith =>
            new List<object[]>
            {
                new object[] {new Penguin(2)},
                new object[] {new Lion(2)},
                new object[] {new Snake(2)}
            };

        [Theory]
        [MemberData(nameof(DataNotFriendlyWith))]
        public void ShouldNotBeFriendly(Animal animal)
        {
            Parrot parrot = new Parrot(1);
            Assert.False(parrot.IsFriendlyWith(animal));
        }

        [Fact]
        public void ShouldTrackFeeding()
        {
            ZooConsole console = new ZooConsole();
            Parrot parrot = new Parrot(1, null, console);
            var zooKeeper = new ZooKeeper("Bob", "Smith");
            parrot.Feed(new Vegetable(), zooKeeper);

            Assert.Single(parrot.FeedTimes);
            Assert.Equal(zooKeeper, parrot.FeedTimes[0].FeedByZooKeeper);
            Assert.Equal("Parrot: Parrot ID 1 was fed by Bob Smith.", console.Messages[0]);
        }

        [Fact]
        public void ShouldThrowExceptionIfFoodIsNotFavorite()
        {
            ZooConsole console = new ZooConsole();
            Parrot parrot = new Parrot(1, null, console);
            var zooKeeper = new ZooKeeper("Bob", "Smith");

            Assert.Throws<NotFavoriteFoodException>(() => parrot.Feed(new Meat(), zooKeeper));
            Assert.Equal("Parrot: Trying to feed Parrot ID 1 with not its favorite food.", console.Messages[0]);
        }

        [Fact]
        public void ShouldBeHungryUntilAteTwicePerDay()
        {
            Parrot parrot = new Parrot(1);
            Assert.True(parrot.IsHungry(DateTime.Now));
            parrot.Feed(new Vegetable(), new ZooKeeper("Bob", "Smith"));
            Assert.True(parrot.IsHungry(DateTime.Now));
        }

        [Fact]
        public void ShouldNotBeHungryAfterAteTwicePerDay()
        {
            Parrot parrot = new Parrot(1);
            parrot.Feed(new Vegetable(), new ZooKeeper("Bob", "Smith"));
            parrot.Feed(new Vegetable(), new ZooKeeper("Bob", "Smith"));
            Assert.False(parrot.IsHungry(DateTime.Now));
        }
    }
}