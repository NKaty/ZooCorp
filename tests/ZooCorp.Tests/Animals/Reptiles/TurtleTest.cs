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
    public class TurtleTest
    {
        [Fact]
        public void ShouldBeAbleToCreateTurtle()
        {
            Turtle turtle = new Turtle(1, new List<int>() { 5, 10 });
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
           new [] { new Parrot(2) },
           new [] { new Bison(2) },
           new [] { new Elephant(2) },
           new [] { new Turtle(2) }
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
           new [] { new Penguin(2) },
           new [] { new Lion(2) },
           new [] { new Snake(2) }
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
            Turtle turtle = new Turtle(1);
            var zooKeeper = new ZooKeeper("Bob", "Smith");
            turtle.Feed(new Vegetable(), zooKeeper);

            Assert.Single(turtle.FeedTimes);
            Assert.Equal(zooKeeper, turtle.FeedTimes[0].FeedByZooKeeper);
        }

        [Fact]
        public void ShouldThrowExceptionIfFoodIsNotFavorite()
        {
            Turtle turtle = new Turtle(1);
            var zooKeeper = new ZooKeeper("Bob", "Smith");


            Assert.Throws<NotFavoriteFoodException>(() => turtle.Feed(new Meat(), zooKeeper));
        }
    }
}
