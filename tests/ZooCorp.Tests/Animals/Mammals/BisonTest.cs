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
    public class BisonTest
    {
        [Fact]
        public void ShouldBeAbleToCreateBison()
        {
            Bison bison = new Bison();
        }

        [Fact]
        public void ShouldRequireSpace1000()
        {
            Bison bison = new Bison();
            Assert.Equal(1000, bison.RequiredSpaceSfFt);
        }

        [Fact]
        public void ShouldHaveFavoriteFoodAsGrass()
        {
            Bison bison = new Bison();
            Assert.Equal("grass", bison.FavoriteFood[0]);
        }

        public static IEnumerable<object[]> DataFriendlyWith =>
        new List<object[]>
        {
           new [] { new Bison() },
           new [] { new Elephant() }
        };

        [Theory]
        [MemberData(nameof(DataFriendlyWith))]
        public void ShouldBeFriendly(Animal animal)
        {
            Bison bison = new Bison();
            Assert.True(bison.IsFriendlyWith(animal));
        }

        public static IEnumerable<object[]> DataNotFriendlyWith =>
        new List<object[]>
        {
           new [] { new Penguin() },
           new [] { new Snake() },
           new [] { new Lion() },
           new [] { new Turtle() },
           new [] { new Parrot() }
        };

        [Theory]
        [MemberData(nameof(DataNotFriendlyWith))]
        public void ShouldNotBeFriendly(Animal animal)
        {
            Bison bison = new Bison();
            Assert.False(bison.IsFriendlyWith(animal));
        }
    }
}
