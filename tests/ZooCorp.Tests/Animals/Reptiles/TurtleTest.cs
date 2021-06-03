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


namespace ZooCorp.Tests.Animals.Reptiles
{
    public class TurtleTest
    {
        [Fact]
        public void ShouldBeAbleToCreateTurtle()
        {
            Turtle turtle = new Turtle();
        }

        [Fact]
        public void ShouldRequireSpace5()
        {
            Turtle turtle = new Turtle();
            Assert.Equal(5, turtle.RequiredSpaceSfFt);
        }

        [Fact]
        public void ShouldHaveFavoriteFoodAsFruitAndVegetable()
        {
            Turtle turtle = new Turtle();
            Assert.Equal("fruit", turtle.FavoriteFood[0]);
            Assert.Equal("vegetable", turtle.FavoriteFood[1]);
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
            Turtle turtle = new Turtle();
            Assert.True(turtle.IsFriendlyWith(animal));
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
            Turtle turtle = new Turtle();
            Assert.False(turtle.IsFriendlyWith(animal));
        }
    }
}
