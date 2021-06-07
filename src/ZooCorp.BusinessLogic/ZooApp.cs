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
            _console?.WriteLine($"ZooApp: Added zoo in {zoo.Location}.");
        }

        public Zoo CreateZoo(string location)
        {
            var zoo = new Zoo(location, _console);
            AddZoo(zoo);
            _console?.WriteLine($"ZooApp: Created zoo in {zoo.Location}.");
            return zoo;
        }

        public List<Zoo> GetZoos()
        {
            return _zoos;
        }
    }
}
