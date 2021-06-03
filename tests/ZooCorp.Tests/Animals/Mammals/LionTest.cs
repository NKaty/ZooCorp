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


namespace ZooCorp.Tests.Animals.Mammals
{
    public class LionTest
    {
        [Fact]
        public void ShouldBeAbleToCreateLion()
        {
            Lion lion = new Lion();
        }

        [Fact]
        public void ShouldRequireSpace1000()
        {
            Lion lion = new Lion();
            Assert.Equal(1000, lion.RequiredSpaceSfFt);
        }

        [Fact]
        public void ShouldHaveFavoriteFoodAsMeat()
        {
            Lion lion = new Lion();
            Assert.Equal("meat", lion.FavoriteFood[0]);
        }

        public static IEnumerable<object[]> DataFriendlyWith =>
        new List<object[]>
        {
           new [] { new Lion() }
        };

        [Theory]
        [MemberData(nameof(DataFriendlyWith))]
        public void ShouldBeFriendly(Animal animal)
        {
            Lion lion = new Lion();
            Assert.True(lion.IsFriendlyWith(animal));
        }

        public static IEnumerable<object[]> DataNotFriendlyWith =>
        new List<object[]>
        {
           new [] { new Penguin() },
           new [] { new Elephant() },
           new [] { new Snake() },
           new [] { new Bison() },
           new [] { new Turtle() },
           new [] { new Parrot() }
        };

        [Theory]
        [MemberData(nameof(DataNotFriendlyWith))]
        public void ShouldNotBeFriendly(Animal animal)
        {
            Lion lion = new Lion();
            Assert.False(lion.IsFriendlyWith(animal));
        }
    }
}
