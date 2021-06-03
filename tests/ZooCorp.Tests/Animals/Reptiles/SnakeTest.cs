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
    public class SnakeTest
    {
        [Fact]
        public void ShouldBeAbleToCreateSnake()
        {
            Snake snake = new Snake();
        }

        [Fact]
        public void ShouldRequireSpace2()
        {
            Snake snake = new Snake();
            Assert.Equal(2, snake.RequiredSpaceSfFt);
        }

        [Fact]
        public void ShouldHaveFavoriteFoodAsMouseAndInsect()
        {
            Snake snake = new Snake();
            Assert.Equal("mouse", snake.FavoriteFood[0]);
            Assert.Equal("insect", snake.FavoriteFood[1]);
        }

        public static IEnumerable<object[]> DataFriendlyWith =>
        new List<object[]>
        {
           new [] { new Snake() },
        };

        [Theory]
        [MemberData(nameof(DataFriendlyWith))]
        public void ShouldBeFriendly(Animal animal)
        {
            Snake snake = new Snake();
            Assert.True(snake.IsFriendlyWith(animal));
        }

        public static IEnumerable<object[]> DataNotFriendlyWith =>
        new List<object[]>
        {
           new [] { new Penguin() },
           new [] { new Lion() },
           new [] { new Parrot() },
           new [] { new Bison() },
           new [] { new Elephant() },
           new [] { new Turtle() }
        };

        [Theory]
        [MemberData(nameof(DataNotFriendlyWith))]
        public void ShouldNotBeFriendly(Animal animal)
        {
            Snake snake = new Snake();
            Assert.False(snake.IsFriendlyWith(animal));
        }
    }
}
