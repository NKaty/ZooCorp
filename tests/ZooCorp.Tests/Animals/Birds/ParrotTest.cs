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

namespace ZooCorp.Tests.Animals.Birds
{
    public class ParrotTest
    {
        [Fact]
        public void ShouldBeAbleToCreateParrot()
        {
            Parrot parrot = new Parrot(1, new List<int>() { 5, 10 });
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
           new [] { new Parrot(2) },
           new [] { new Bison(2) },
           new [] { new Elephant(2) },
           new [] { new Turtle(2) }
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
           new [] { new Penguin(2) },
           new [] { new Lion(2) },
           new [] { new Snake(2) }
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
            Parrot parrot = new Parrot(1);
            var zooKeeper = new ZooKeeper("Bob", "Smith");
            parrot.Feed(new Vegetable(), zooKeeper);

            Assert.Single(parrot.FeedTimes);
            Assert.Equal(zooKeeper, parrot.FeedTimes[0].FeedByZooKeeper);
        }

        [Fact]
        public void ShouldThrowExceptionIfFoodIsNotFavorite()
        {
            Parrot parrot = new Parrot(1);
            var zooKeeper = new ZooKeeper("Bob", "Smith");

            Assert.Throws<NotFavoriteFoodException>(() => parrot.Feed(new Meat(), zooKeeper));
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
