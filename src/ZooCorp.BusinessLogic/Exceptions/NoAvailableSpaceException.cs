using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooCorp.BusinessLogic.Exceptions
{
    public class NoAvailableSpaceException : Exception
    {
        public NoAvailableSpaceException(string message) : base(message) { }
    }
}
