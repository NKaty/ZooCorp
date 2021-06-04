using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
