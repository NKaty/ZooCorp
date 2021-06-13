using Xunit;
using ZooCorp.BusinessLogic.Foods;

namespace ZooCorp.Tests.Foods
{
    public class FoodTest
    {
        [Fact]
        public void ShouldBeAbleToCreateGrass()
        {
            Grass grass = new Grass();
        }

        [Fact]
        public void ShouldBeAbleToCreateMeat()
        {
            Meat meat = new Meat();
        }

        [Fact]
        public void ShouldBeAbleToCreateVegetable()
        {
            Vegetable vegetable = new Vegetable();
        }
    }
}