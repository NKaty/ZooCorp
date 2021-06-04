using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ZooCorp.BusinessLogic.Foods;
using ZooCorp.BusinessLogic.Employees;

namespace ZooCorp.Tests.Foods
{
    public class FeedTimeTest
    {
        [Fact]
        public void ShouldBeAbleToCreateFeedTime()
        {
            var dateTime = new DateTime();
            var zooKeeper = new ZooKeeper("Bob", "Smith");
            FeedTime feedTime = new FeedTime(dateTime, zooKeeper);

            Assert.Equal(dateTime, feedTime.FeedAnimalTime);
            Assert.Equal(zooKeeper, feedTime.FeedByZooKeeper);
        }
    }
}
