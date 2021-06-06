using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooCorp.BusinessLogic.Employees.HireValidators.ValidationErrors
{
    public class ValidationError
    {
        public string Property { get; }
        public string Message { get; }

        public ValidationError(string property, string message)
        {
            Property = property;
            Message = message;
        }
    }
}
