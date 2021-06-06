using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.Tests.Common
{
    public class ZooConsoleTest
    {
        [Fact]
        public void ShouldBeAbleToCreateZooConsole()
        {
            var zooConsole = new ZooConsole();
        }

        [Fact]
        public void ShouldBeAbleToWriteToConsole()
        {
            var zooConsole = new ZooConsole();
            zooConsole.WriteLine("Message");

            Assert.Equal("Message", zooConsole.Messages[0]);
        }
    }
}
