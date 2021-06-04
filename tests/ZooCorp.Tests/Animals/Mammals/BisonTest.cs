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
    public class BisonTest
    {
        [Fact]
        public void ShouldBeAbleToCreateBison()
        {
            Bison bison = new Bison(1, new List<int>() { 5, 10 });
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
           new [] { new Bison(2) },
           new [] { new Elephant(2) }
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
           new [] { new Penguin(2) },
           new [] { new Snake(2) },
           new [] { new Lion(2) },
           new [] { new Turtle(2) },
           new [] { new Parrot(2) }
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
            Bison bison = new Bison(1);
            var zooKeeper = new ZooKeeper("Bob", "Smith");
            bison.Feed(new Grass(), zooKeeper);

            Assert.Single(bison.FeedTimes);
            Assert.Equal(zooKeeper, bison.FeedTimes[0].FeedByZooKeeper);
        }

        [Fact]
        public void ShouldThrowExceptionIfFoodIsNotFavorite()
        {
            Bison bison = new Bison(1);
            var zooKeeper = new ZooKeeper("Bob", "Smith");


            Assert.Throws<NotFavoriteFoodException>(() => bison.Feed(new Meat(), zooKeeper));
        }
    }
}
