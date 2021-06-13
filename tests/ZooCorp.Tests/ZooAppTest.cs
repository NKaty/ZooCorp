using Xunit;
using ZooCorp.BusinessLogic;
using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.Tests
{
    public class ZooAppTest
    {
        [Fact]
        public void ShouldBeAbleToCreateZooApp()
        {
            IConsole console = new ZooConsole();
            ZooApp zooApp = new ZooApp(console);
        }

        [Fact]
        public void ShouldBeAbleToAddNewZoo()
        {
            ZooConsole console = new ZooConsole();
            ZooApp zooApp = new ZooApp(console);
            Zoo zoo = new Zoo("London");
            zooApp.AddZoo(zoo);
            Assert.Equal(zoo, zooApp.GetZoos()[0]);
            Assert.Equal("ZooApp: Added zoo in London.", console.Messages[0]);
        }

        [Fact]
        public void ShouldBeAbleToCreateAndAddNewZoo()
        {
            ZooConsole console = new ZooConsole();
            ZooApp zooApp = new ZooApp(console);
            var zoo = zooApp.CreateZoo("London");
            Assert.Equal("London", zoo.Location);
            Assert.Equal("ZooApp: Created zoo in London.", console.Messages[1]);
            Assert.Equal("ZooApp: Added zoo in London.", console.Messages[2]);
        }
    }
}