using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ZooCorp.BusinessLogic;

namespace ZooCorp.Tests
{
    public class ZooAppTest
    {
        [Fact]
        public void ShouldBeAbleToCreateZooApp()
        {
            ZooApp zooApp = new ZooApp();
        }

        [Fact]
        public void ShouldBeAbleToAddNewZoo()
        {
            ZooApp zooApp = new ZooApp();
            Zoo zoo = new Zoo("London");
            zooApp.AddZoo(zoo);
            Assert.Equal(zoo, zooApp.GetZoos()[0]);
        }
    }
}
