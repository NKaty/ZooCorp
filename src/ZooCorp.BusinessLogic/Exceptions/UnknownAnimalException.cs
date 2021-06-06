using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooCorp.BusinessLogic.Exceptions
{
    public class UnknownAnimalException : Exception
    {
        public UnknownAnimalException(string message) : base(message) { }
    }
}
