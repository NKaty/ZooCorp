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
    public class SnakeTest
    {
        [Fact]
        public void ShouldBeAbleToCreateSnake()
        {
            Snake snake = new Snake(1, new List<int>() {5, 10});
            Assert.Equal(1, snake.ID);
            Assert.Equal(5, snake.FeedSchedule[0]);
        }

        [Fact]
        public void ShouldRequireSpace2()
        {
            Snake snake = new Snake(1);
            Assert.Equal(2, snake.RequiredSpaceSfFt);
        }

        [Fact]
        public void ShouldHaveFavoriteFoodAsMeat()
        {
            Snake snake = new Snake(1);
            Assert.Equal("Meat", snake.FavoriteFood[0]);
        }

        public static IEnumerable<object[]> DataFriendlyWith =>
            new List<object[]>
            {
                new object[] {new Snake(2)},
            };

        [Theory]
        [MemberData(nameof(DataFriendlyWith))]
        public void ShouldBeFriendly(Animal animal)
        {
            Snake snake = new Snake(1);
            Assert.True(snake.IsFriendlyWith(animal));
        }

        public static IEnumerable<object[]> DataNotFriendlyWith =>
            new List<object[]>
            {
                new object[] {new Penguin(2)},
                new object[] {new Lion(2)},
                new object[] {new Parrot(2)},
                new object[] {new Bison(2)},
                new object[] {new Elephant(2)},
                new object[] {new Turtle(2)}
            };

        [Theory]
        [MemberData(nameof(DataNotFriendlyWith))]
        public void ShouldNotBeFriendly(Animal animal)
        {
            Snake snake = new Snake(1);
            Assert.False(snake.IsFriendlyWith(animal));
        }

        [Fact]
        public void ShouldTrackFeeding()
        {
            ZooConsole console = new ZooConsole();
            Snake snake = new Snake(1, null, console);
            var zooKeeper = new ZooKeeper("Bob", "Smith");
            snake.Feed(new Meat(), zooKeeper);

            Assert.Single(snake.FeedTimes);
            Assert.Equal(zooKeeper, snake.FeedTimes[0].FeedByZooKeeper);
            Assert.Equal("Snake: Snake ID 1 was fed by Bob Smith.", console.Messages[0]);
        }

        [Fact]
        public void ShouldThrowExceptionIfFoodIsNotFavorite()
        {
            ZooConsole console = new ZooConsole();
            Snake snake = new Snake(1, null, console);
            var zooKeeper = new ZooKeeper("Bob", "Smith");

            Assert.Throws<NotFavoriteFoodException>(() => snake.Feed(new Grass(), zooKeeper));
            Assert.Equal("Snake: Trying to feed Snake ID 1 with not its favorite food.", console.Messages[0]);
        }

        [Fact]
        public void ShouldBeHungryUntilAteTwicePerDay()
        {
            Snake snake = new Snake(1);
            Assert.True(snake.IsHungry(DateTime.Now));
            snake.Feed(new Meat(), new ZooKeeper("Bob", "Smith"));
            Assert.True(snake.IsHungry(DateTime.Now));
        }

        [Fact]
        public void ShouldNotBeHungryAfterAteTwicePerDay()
        {
            Snake snake = new Snake(1);
            snake.Feed(new Meat(), new ZooKeeper("Bob", "Smith"));
            snake.Feed(new Meat(), new ZooKeeper("Bob", "Smith"));
            Assert.False(snake.IsHungry(DateTime.Now));
        }
    }
}