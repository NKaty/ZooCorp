using System.Collections.Generic;
using ZooCorp.BusinessLogic.Common;

namespace ZooCorp.BusinessLogic
{
    public class ZooApp
    {
        private readonly IConsole _console;

        private readonly List<Zoo> _zoos = new List<Zoo>();

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
            _console?.WriteLine($"ZooApp: Created zoo in {zoo.Location}.");
            AddZoo(zoo);
            return zoo;
        }

        public List<Zoo> GetZoos()
        {
            return _zoos;
        }
    }
}