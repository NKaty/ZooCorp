using System.Collections.Generic;
using Xunit;
using Moq;
using ZooCorp.BusinessLogic.Animals;
using ZooCorp.BusinessLogic.Medicines;
using ZooCorp.BusinessLogic.Exceptions;
using ZooCorp.BusinessLogic.Common;

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
            ZooConsole console = new ZooConsole();
            var mock = new Mock<Animal>(console);
            mock.Object.MarkSick();

            Assert.True(mock.Object.IsSick());
            Assert.Equal("AnimalProxy: AnimalProxy ID 0 got sick.", console.Messages[0]);
        }

        [Fact]
        public void ShouldBeSickAfterMarkedAsSickWithNeededMedicine()
        {
            var mock = new Mock<Animal>();
            mock.Object.MarkSick(new Antibiotics());

            Assert.True(mock.Object.IsSick());
            Assert.Equal("Antibiotics", mock.Object.NeededMedicine);
        }

        [Fact]
        public void ShouldBeNotSickAfterHealingWithNeededMedicine()
        {
            ZooConsole console = new ZooConsole();
            var mock = new Mock<Animal>(console);
            mock.Object.MarkSick(new Antibiotics());
            mock.Object.Heal(new Antibiotics());

            Assert.False(mock.Object.IsSick());
            Assert.Equal("AnimalProxy: AnimalProxy ID 0 was healed.", console.Messages[1]);
        }

        [Fact]
        public void ShouldThrowExceptionAfterHealingWithWrongMedicine()
        {
            ZooConsole console = new ZooConsole();
            var mock = new Mock<Animal>(console);
            mock.Object.MarkSick(new Antibiotics());

            Assert.Throws<NotNeededMedicineException>(() => mock.Object.Heal(new AntiDepression()));
            Assert.True(mock.Object.IsSick());
            Assert.Equal("AnimalProxy: Trying to heal AnimalProxy ID 0 with incorrect type of medicine.",
                console.Messages[1]);
        }

        [Fact]
        public void ShouldHaveFeedSchedule()
        {
            var mock = new Mock<Animal>();
            mock.Object.FeedSchedule = new List<int>() {5, 10};

            Assert.Equal(2, mock.Object.FeedSchedule.Count);
        }

        [Fact]
        public void ShouldAddFeedSchedule()
        {
            ZooConsole console = new ZooConsole();
            var mock = new Mock<Animal>(console);
            mock.Object.FeedSchedule = new List<int>() {5, 10};
            mock.Object.AddFeedSchedule(new List<int>() {12, 14});

            Assert.Equal(4, mock.Object.FeedSchedule.Count);
            Assert.Equal(14, mock.Object.FeedSchedule[3]);
            Assert.Equal("AnimalProxy: The feed schedule of AnimalProxy ID 0 was changed.", console.Messages[0]);
        }
    }
}