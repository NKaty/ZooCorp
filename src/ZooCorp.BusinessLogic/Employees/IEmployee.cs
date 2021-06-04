using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooCorp.BusinessLogic.Employees
{
    public interface IEmployee
    {
        string FirstName { get; }

        string LastName { get; }
    }
}
