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
    public class BisonTest
    {
        [Fact]
        public void ShouldBeAbleToCreateBison()
        {
            Bison bison = new Bison(1, new List<int>() {5, 10});
            Assert.Equal(1, bison.ID);
            Assert.Equal(5, bison.FeedSchedule[0]);
        }

        [Fact]
        public void ShouldRequireSpace1000()
        {
            Bison bison = new Bison(1);
            Assert.Equal(1000, bison.RequiredSpaceSfFt);
        }

        [Fact]
        public void ShouldHaveFavoriteFoodAsGrass()
        {
            Bison bison = new Bison(1);
            Assert.Equal("Grass", bison.FavoriteFood[0]);
        }

        public static IEnumerable<object[]> DataFriendlyWith =>
            new List<object[]>
            {
                new object[] {new Bison(2)},
                new object[] {new Elephant(2)}
            };

        [Theory]
        [MemberData(nameof(DataFriendlyWith))]
        public void ShouldBeFriendly(Animal animal)
        {
            Bison bison = new Bison(1);
            Assert.True(bison.IsFriendlyWith(animal));
        }

        public static IEnumerable<object[]> DataNotFriendlyWith =>
            new List<object[]>
            {
                new object[] {new Penguin(2)},
                new object[] {new Snake(2)},
                new object[] {new Lion(2)},
                new object[] {new Turtle(2)},
                new object[] {new Parrot(2)}
            };

        [Theory]
        [MemberData(nameof(DataNotFriendlyWith))]
        public void ShouldNotBeFriendly(Animal animal)
        {
            Bison bison = new Bison(1);
            Assert.False(bison.IsFriendlyWith(animal));
        }

        [Fact]
        public void ShouldTrackFeeding()
        {
            ZooConsole console = new ZooConsole();
            Bison bison = new Bison(1, null, console);
            var zooKeeper = new ZooKeeper("Bob", "Smith");
            bison.Feed(new Grass(), zooKeeper);

            Assert.Single(bison.FeedTimes);
            Assert.Equal(zooKeeper, bison.FeedTimes[0].FeedByZooKeeper);
            Assert.Equal("Bison: Bison ID 1 was fed by Bob Smith.", console.Messages[0]);
        }

        [Fact]
        public void ShouldThrowExceptionIfFoodIsNotFavorite()
        {
            ZooConsole console = new ZooConsole();
            Bison bison = new Bison(1, null, console);
            var zooKeeper = new ZooKeeper("Bob", "Smith");

            Assert.Throws<NotFavoriteFoodException>(() => bison.Feed(new Meat(), zooKeeper));
            Assert.Equal("Bison: Trying to feed Bison ID 1 with not its favorite food.", console.Messages[0]);
        }

        [Fact]
        public void ShouldBeHungryUntilAteTwicePerDay()
        {
            Bison bison = new Bison(1);
            Assert.True(bison.IsHungry(DateTime.Now));
            bison.Feed(new Grass(), new ZooKeeper("Bob", "Smith"));
            Assert.True(bison.IsHungry(DateTime.Now));
        }

        [Fact]
        public void ShouldNotBeHungryAfterAteTwicePerDay()
        {
            Bison bison = new Bison(1);
            bison.Feed(new Grass(), new ZooKeeper("Bob", "Smith"));
            bison.Feed(new Grass(), new ZooKeeper("Bob", "Smith"));
            Assert.False(bison.IsHungry(DateTime.Now));
        }
    }
}