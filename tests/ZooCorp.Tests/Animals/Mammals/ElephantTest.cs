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
    public class ElephantTest
    {
        [Fact]
        public void ShouldBeAbleToCreateElephant()
        {
            Elephant elephant = new Elephant(1, new List<int>() {5, 10});
            Assert.Equal(1, elephant.ID);
            Assert.Equal(5, elephant.FeedSchedule[0]);
        }

        [Fact]
        public void ShouldRequireSpace1000()
        {
            Elephant elephant = new Elephant(1);
            Assert.Equal(1000, elephant.RequiredSpaceSfFt);
        }

        [Fact]
        public void ShouldHaveFavoriteFoodAsGrass()
        {
            Elephant elephant = new Elephant(1);
            Assert.Equal("Grass", elephant.FavoriteFood[0]);
        }

        public static IEnumerable<object[]> DataFriendlyWith =>
            new List<object[]>
            {
                new object[] {new Elephant(2)},
                new object[] {new Bison(2)},
                new object[] {new Turtle(2)},
                new object[] {new Parrot(2)}
            };

        [Theory]
        [MemberData(nameof(DataFriendlyWith))]
        public void ShouldBeFriendly(Animal animal)
        {
            Elephant elephant = new Elephant(1);
            Assert.True(elephant.IsFriendlyWith(animal));
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
            Elephant elephant = new Elephant(1);
            Assert.False(elephant.IsFriendlyWith(animal));
        }

        [Fact]
        public void ShouldTrackFeeding()
        {
            ZooConsole console = new ZooConsole();
            Elephant elephant = new Elephant(1, null, console);
            var zooKeeper = new ZooKeeper("Bob", "Smith");
            elephant.Feed(new Grass(), zooKeeper);

            Assert.Single(elephant.FeedTimes);
            Assert.Equal(zooKeeper, elephant.FeedTimes[0].FeedByZooKeeper);
            Assert.Equal("Elephant: Elephant ID 1 was fed by Bob Smith.", console.Messages[0]);
        }

        [Fact]
        public void ShouldThrowExceptionIfFoodIsNotFavorite()
        {
            ZooConsole console = new ZooConsole();
            Elephant elephant = new Elephant(1, null, console);
            var zooKeeper = new ZooKeeper("Bob", "Smith");

            Assert.Throws<NotFavoriteFoodException>(() => elephant.Feed(new Meat(), zooKeeper));
            Assert.Equal("Elephant: Trying to feed Elephant ID 1 with not its favorite food.", console.Messages[0]);
        }

        [Fact]
        public void ShouldBeHungryUntilAteTwicePerDay()
        {
            Elephant elephant = new Elephant(1);
            Assert.True(elephant.IsHungry(DateTime.Now));
            elephant.Feed(new Grass(), new ZooKeeper("Bob", "Smith"));
            Assert.True(elephant.IsHungry(DateTime.Now));
        }

        [Fact]
        public void ShouldNotBeHungryAfterAteTwicePerDay()
        {
            Elephant elephant = new Elephant(1);
            elephant.Feed(new Grass(), new ZooKeeper("Bob", "Smith"));
            elephant.Feed(new Grass(), new ZooKeeper("Bob", "Smith"));
            Assert.False(elephant.IsHungry(DateTime.Now));
        }
    }
}