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

namespace ZooCorp.Tests.Animals.Mammals
{
    public class ElephantTest
    {
        [Fact]
        public void ShouldBeAbleToCreateElephant()
        {
            Elephant elephant = new Elephant(1, new List<int>() { 5, 10 });
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
           new [] { new Elephant(2) },
           new [] { new Bison(2) },
           new [] { new Turtle(2) },
           new [] { new Parrot(2) }
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
           new [] { new Penguin(2) },
           new [] { new Lion(2) },
           new [] { new Snake(2) }
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
            Elephant elephant = new Elephant(1);
            var zooKeeper = new ZooKeeper("Bob", "Smith");
            elephant.Feed(new Grass(), zooKeeper);

            Assert.Single(elephant.FeedTimes);
            Assert.Equal(zooKeeper, elephant.FeedTimes[0].FeedByZooKeeper);
        }

        [Fact]
        public void ShouldThrowExceptionIfFoodIsNotFavorite()
        {
            Elephant elephant = new Elephant(1);
            var zooKeeper = new ZooKeeper("Bob", "Smith");

            Assert.Throws<NotFavoriteFoodException>(() => elephant.Feed(new Meat(), zooKeeper));
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
