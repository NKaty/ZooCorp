using System;
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

namespace ZooCorp.Tests.Animals.Reptiles
{
    public class SnakeTest
    {
        [Fact]
        public void ShouldBeAbleToCreateSnake()
        {
            Snake snake = new Snake(1, new List<int>() { 5, 10 });
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
           new [] { new Snake(2) },
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
           new [] { new Penguin(2) },
           new [] { new Lion(2) },
           new [] { new Parrot(2) },
           new [] { new Bison(2) },
           new [] { new Elephant(2) },
           new [] { new Turtle(2) }
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
            Snake snake = new Snake(1);
            var zooKeeper = new ZooKeeper("Bob", "Smith");
            snake.Feed(new Meat(), zooKeeper);

            Assert.Single(snake.FeedTimes);
            Assert.Equal(zooKeeper, snake.FeedTimes[0].FeedByZooKeeper);
        }

        [Fact]
        public void ShouldThrowExceptionIfFoodIsNotFavorite()
        {
            Snake snake = new Snake(1);
            var zooKeeper = new ZooKeeper("Bob", "Smith");


            Assert.Throws<NotFavoriteFoodException>(() => snake.Feed(new Grass(), zooKeeper));
        }
    }
}
