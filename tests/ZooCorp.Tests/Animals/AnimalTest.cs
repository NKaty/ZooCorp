using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using ZooCorp.BusinessLogic.Animals;
using ZooCorp.BusinessLogic.Medicines;

namespace ZooCorp.Tests.Animals
{
    public class AnimalTest
    {
        [Fact]
        public void ShouldBeNotSickFromBeginning()
        {
            var mock = new Mock<Animal>()
            {
                CallBase = true
            };
           
            Assert.False(mock.Object.IsSick());
        }

        [Fact]
        public void ShouldBeSickAfterMarkedAsSick()
        {
            var mock = new Mock<Animal>()
            {
                CallBase = true
            };
            mock.Object.MarkSick();

            Assert.True(mock.Object.IsSick());
        }

        [Fact]
        public void ShouldBeNotSickAfterHealing()
        {
            var mock = new Mock<Animal>()
            {
                CallBase = true
            };
            mock.Object.Heal(new Antibiotics());

            Assert.False(mock.Object.IsSick());
        }
    }
}
