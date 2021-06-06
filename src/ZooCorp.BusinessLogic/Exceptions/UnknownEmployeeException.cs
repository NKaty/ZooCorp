using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooCorp.BusinessLogic.Exceptions
{
    public class UnknownEmployeeException : Exception
    {
        public UnknownEmployeeException(string message) : base(message) { }
    }
}
