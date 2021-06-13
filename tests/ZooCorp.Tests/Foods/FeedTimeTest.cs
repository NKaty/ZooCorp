using System;
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