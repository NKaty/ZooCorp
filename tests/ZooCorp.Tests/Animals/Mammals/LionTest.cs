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
    public class LionTest
    {
        [Fact]
        public void ShouldBeAbleToCreateLion()
        {
            Lion lion = new Lion(1, new List<int>() { 5, 10 });
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
           new [] { new Lion(2) }
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
           new [] { new Penguin(2) },
           new [] { new Elephant(2) },
           new [] { new Snake(2) },
           new [] { new Bison(2) },
           new [] { new Turtle(2) },
           new [] { new Parrot(2) }
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
            Lion lion = new Lion(1);
            var zooKeeper = new ZooKeeper("Bob", "Smith");
            lion.Feed(new Meat(), zooKeeper);

            Assert.Single(lion.FeedTimes);
            Assert.Equal(zooKeeper, lion.FeedTimes[0].FeedByZooKeeper);
        }

        [Fact]
        public void ShouldThrowExceptionIfFoodIsNotFavorite()
        {
            Lion lion = new Lion(1);
            var zooKeeper = new ZooKeeper("Bob", "Smith");


            Assert.Throws<NotFavoriteFoodException>(() => lion.Feed(new Vegetable(), zooKeeper));
        }
    }
}
