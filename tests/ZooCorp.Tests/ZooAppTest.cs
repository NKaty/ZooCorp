using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            ZooApp zooApp = new ZooApp();
            Zoo zoo = new Zoo("London");
            zooApp.AddZoo(zoo);
            Assert.Equal(zoo, zooApp.GetZoos()[0]);
        }

        [Fact]
        public void ShouldBeAbleToCreateAndAddNewZoo()
        {
            ZooApp zooApp = new ZooApp();
            var zoo = zooApp.CreateZoo("London");
            Assert.Equal("London", zoo.Location);
        }
    }
}
