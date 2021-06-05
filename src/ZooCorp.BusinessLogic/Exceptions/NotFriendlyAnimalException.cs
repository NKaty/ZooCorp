using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooCorp.BusinessLogic.Exceptions
{
    public class NotFriendlyAnimalException : Exception
    {
        public NotFriendlyAnimalException(string message) : base(message) { }
    }
}
