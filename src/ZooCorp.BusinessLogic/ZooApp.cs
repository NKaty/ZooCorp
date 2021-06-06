using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.BusinessLogic
{
    public class ZooApp
    {
        private IConsole _console;

        private List<Zoo> _zoos = new List<Zoo>();

        public ZooApp(IConsole console = null)
        {
            _console = console;
        }

        public void AddZoo(Zoo zoo)
        {
            _zoos.Add(zoo);
            _console?.WriteLine($"Added zoo in <<{zoo.Location}>> to zoo app.");
        }

        public void CreateZoo(string location)
        {
            var zoo = new Zoo(location, _console);
            AddZoo(zoo);
            _console?.WriteLine($"Creatrd and added zoo in <<{zoo.Location}>> to zoo app.");
        }

        public List<Zoo> GetZoos()
        {
            return _zoos;
        }
    }
}
