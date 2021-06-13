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

namespace ZooCorp.Tests.Animals.Reptiles
{
    public class TurtleTest
    {
        [Fact]
        public void ShouldBeAbleToCreateTurtle()
        {
            Turtle turtle = new Turtle(1, new List<int>() {5, 10});
            Assert.Equal(1, turtle.ID);
            Assert.Equal(5, turtle.FeedSchedule[0]);
        }

        [Fact]
        public void ShouldRequireSpace5()
        {
            Turtle turtle = new Turtle(1);
            Assert.Equal(5, turtle.RequiredSpaceSfFt);
        }

        [Fact]
        public void ShouldHaveFavoriteFoodAsGrassAndVegetable()
        {
            Turtle turtle = new Turtle(1);
            Assert.Equal("Grass", turtle.FavoriteFood[0]);
            Assert.Equal("Vegetable", turtle.FavoriteFood[1]);
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
            Turtle turtle = new Turtle(1);
            Assert.True(turtle.IsFriendlyWith(animal));
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
            Turtle turtle = new Turtle(1);
            Assert.False(turtle.IsFriendlyWith(animal));
        }

        [Fact]
        public void ShouldTrackFeeding()
        {
            ZooConsole console = new ZooConsole();
            Turtle turtle = new Turtle(1, null, console);
            var zooKeeper = new ZooKeeper("Bob", "Smith");
            turtle.Feed(new Vegetable(), zooKeeper);

            Assert.Single(turtle.FeedTimes);
            Assert.Equal(zooKeeper, turtle.FeedTimes[0].FeedByZooKeeper);
            Assert.Equal("Turtle: Turtle ID 1 was fed by Bob Smith.", console.Messages[0]);
        }

        [Fact]
        public void ShouldThrowExceptionIfFoodIsNotFavorite()
        {
            ZooConsole console = new ZooConsole();
            Turtle turtle = new Turtle(1, null, console);
            var zooKeeper = new ZooKeeper("Bob", "Smith");

            Assert.Throws<NotFavoriteFoodException>(() => turtle.Feed(new Meat(), zooKeeper));
            Assert.Equal("Turtle: Trying to feed Turtle ID 1 with not its favorite food.", console.Messages[0]);
        }

        [Fact]
        public void ShouldBeHungryUntilAteTwicePerDay()
        {
            Turtle turtle = new Turtle(1);
            Assert.True(turtle.IsHungry(DateTime.Now));
            turtle.Feed(new Vegetable(), new ZooKeeper("Bob", "Smith"));
            Assert.True(turtle.IsHungry(DateTime.Now));
        }

        [Fact]
        public void ShouldNotBeHungryAfterAteTwicePerDay()
        {
            Turtle turtle = new Turtle(1);
            turtle.Feed(new Vegetable(), new ZooKeeper("Bob", "Smith"));
            turtle.Feed(new Vegetable(), new ZooKeeper("Bob", "Smith"));
            Assert.False(turtle.IsHungry(DateTime.Now));
        }
    }
}