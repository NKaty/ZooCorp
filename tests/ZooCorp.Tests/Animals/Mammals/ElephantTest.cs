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
    public class ElephantTest
    {
        [Fact]
        public void ShouldBeAbleToCreateElephant()
        {
            Elephant elephant = new Elephant();
        }

        [Fact]
        public void ShouldRequireSpace1000()
        {
            Elephant elephant = new Elephant();
            Assert.Equal(1000, elephant.RequiredSpaceSfFt);
        }

        [Fact]
        public void ShouldHaveFavoriteFoodAsBananaAndGrass()
        {
            Elephant elephant = new Elephant();
            Assert.Equal("banana", elephant.FavoriteFood[0]);
            Assert.Equal("grass", elephant.FavoriteFood[1]);
        }

        public static IEnumerable<object[]> DataFriendlyWith =>
        new List<object[]>
        {
           new [] { new Elephant() },
           new [] { new Bison() },
           new [] { new Turtle() },
           new [] { new Parrot() }
        };

        [Theory]
        [MemberData(nameof(DataFriendlyWith))]
        public void ShouldBeFriendly(Animal animal)
        {
            Elephant elephant = new Elephant();
            Assert.True(elephant.IsFriendlyWith(animal));
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
            Elephant elephant = new Elephant();
            Assert.False(elephant.IsFriendlyWith(animal));
        }
    }
}
