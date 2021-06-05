using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using ZooCorp.BusinessLogic.Animals;
using ZooCorp.BusinessLogic.Medicines;
using ZooCorp.BusinessLogic.Exceptions;

namespace ZooCorp.Tests.Animals
{
    public class AnimalTest
    {
        [Fact]
        public void ShouldBeNotSickFromBeginning()
        {
            var mock = new Mock<Animal>();

            Assert.False(mock.Object.IsSick());
        }

        [Fact]
        public void ShouldBeSickAfterMarkedAsSick()
        {
            var mock = new Mock<Animal>();
            mock.Object.MarkSick();

            Assert.True(mock.Object.IsSick());
        }

        [Fact]
        public void ShouldBeSickAfterMarkedAsSickWithNeededMedicine()
        {
            var mock = new Mock<Animal>();
            mock.Object.MarkSick(new Antibiotics());

            Assert.True(mock.Object.IsSick());
            Assert.Equal("Antibiotics", mock.Object.neededMedicine);
        }

        [Fact]
        public void ShouldBeNotSickAfterHealingWithNeededMedicine()
        {
            var mock = new Mock<Animal>();
            mock.Object.MarkSick(new Antibiotics());
            mock.Object.Heal(new Antibiotics());

            Assert.False(mock.Object.IsSick());
        }

        [Fact]
        public void ShouldThrowExceptionAfterHealingWithWrongMedicine()
        {
            var mock = new Mock<Animal>();
            mock.Object.MarkSick(new Antibiotics());

            Assert.Throws<NotNeededMedicineException>(() => mock.Object.Heal(new Antidepression()));
            Assert.True(mock.Object.IsSick());
        }

        [Fact]
        public void ShouldHaveFeedSchedule()
        {
            var mock = new Mock<Animal>();
            mock.Object.FeedSchedule = new List<int>() { 5, 10 };

            Assert.Equal(2, mock.Object.FeedSchedule.Count);
        }

        [Fact]
        public void ShouldAddFeedSchedule()
        {
            var mock = new Mock<Animal>();
            mock.Object.FeedSchedule = new List<int>() { 5, 10 };
            mock.Object.AddFeedSchedule(new List<int>() { 12, 14 });

            Assert.Equal(4, mock.Object.FeedSchedule.Count);
            Assert.Equal(14, mock.Object.FeedSchedule[3]);
        }
    }
}
