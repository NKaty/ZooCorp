using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooCorp.BusinessLogic.Exceptions
{
    public class NoAvailableEnclosureException : Exception
    {
        public NoAvailableEnclosureException(string message) : base(message) { }
    }
}
