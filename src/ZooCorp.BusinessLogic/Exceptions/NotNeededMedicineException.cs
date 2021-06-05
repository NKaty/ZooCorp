using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooCorp.BusinessLogic.Exceptions
{
    public class NotNeededMedicineException : Exception
    {
        public NotNeededMedicineException(string message) : base(message) { }
    }
}
