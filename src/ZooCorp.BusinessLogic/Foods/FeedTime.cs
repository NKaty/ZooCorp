using System;
using ZooCorp.BusinessLogic.Employees;

namespace ZooCorp.BusinessLogic.Foods
{
    public class FeedTime
    {
        public DateTime FeedAnimalTime { get; set; }
        public ZooKeeper FeedByZooKeeper { get; set; }

        public FeedTime(DateTime feedAnimalTime, ZooKeeper feedByZooKeeper)
        {
            FeedAnimalTime = feedAnimalTime;
            FeedByZooKeeper = feedByZooKeeper;
        }
    }
}