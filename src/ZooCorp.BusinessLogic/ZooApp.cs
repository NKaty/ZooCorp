using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooCorp.BusinessLogic
{
    public class ZooApp
    {
        private List<Zoo> _zoos = new List<Zoo>();

        public void AddZoo(Zoo zoo)
        {
            _zoos.Add(zoo);
        }

        public List<Zoo> GetZoos()
        {
            return _zoos;
        }
    }
}
