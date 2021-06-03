using System;
using System.Collections;
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
    public class PenguinTest
    {
        [Fact]
        public void ShouldBeAbleToCreatePenguin()
        {
            Penguin penguin = new Penguin();
        }

        [Fact]
        public void ShouldRequireSpace10()
        {
            Penguin penguin = new Penguin();
            Assert.Equal(10, penguin.RequiredSpaceSfFt);
        }

        [Fact]
        public void ShouldHaveFavoriteFoodAsFishAndShellfish()
        {
            Penguin penguin = new Penguin();
            Assert.Equal("fish", penguin.FavoriteFood[0]);
            Assert.Equal("shellfish", penguin.FavoriteFood[1]);
        }

        public static IEnumerable<object[]> DataFriendlyWith =>
        new List<object[]>
        {
           new [] { new Penguin() }
        };

        [Theory]
        [MemberData(nameof(DataFriendlyWith))]
        public void ShouldBeFriendly(Animal animal)
        {
            Penguin penguin = new Penguin();
            Assert.True(penguin.IsFriendlyWith(animal));
        }

        public static IEnumerable<object[]> DataNotFriendlyWith =>
        new List<object[]>
        {
           new [] { new Parrot() },
           new [] { new Bison() },
           new [] { new Elephant() },
           new [] { new Turtle() },
           new [] { new Lion() },
           new [] { new Snake() }
        };

        [Theory]
        [MemberData(nameof(DataNotFriendlyWith))]
        public void ShouldNotBeFriendly(Animal animal)
        {
            Penguin penguin = new Penguin();
            Assert.False(penguin.IsFriendlyWith(animal));
        }
    }
}
