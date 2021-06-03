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

namespace ZooCorp.Tests.Animals.Birds
{
    public class ParrotTest
    {
        [Fact]
        public void ShouldBeAbleToCreateParrot()
        {
            Parrot parrot = new Parrot();
        }

        [Fact]
        public void ShouldRequireSpace5()
        {
            Parrot parrot = new Parrot();
            Assert.Equal(5, parrot.RequiredSpaceSfFt);
        }

        [Fact]
        public void ShouldHaveFavoriteFoodAsSeedAndFruit()
        {
            Parrot parrot = new Parrot();
            Assert.Equal("seed", parrot.FavoriteFood[0]);
            Assert.Equal("fruit", parrot.FavoriteFood[1]);
        }

        public static IEnumerable<object[]> DataFriendlyWith =>
        new List<object[]>
        {
           new [] { new Parrot() },
           new [] { new Bison() },
           new [] { new Elephant() },
           new [] { new Turtle() }
        };

        [Theory]
        [MemberData(nameof(DataFriendlyWith))]
        public void ShouldBeFriendly(Animal animal)
        {
            Parrot parrot = new Parrot();
            Assert.True(parrot.IsFriendlyWith(animal));
        }

        public static IEnumerable<object[]> DataNotFriendlyWith =>
        new List<object[]>
        {
           new [] { new Penguin() },
           new [] { new Lion() },
           new [] { new Snake() }
        };

        [Theory]
        [MemberData(nameof(DataNotFriendlyWith))]
        public void ShouldNotBeFriendly(Animal animal)
        {
            Parrot parrot = new Parrot();
            Assert.False(parrot.IsFriendlyWith(animal));
        }
    }
}
