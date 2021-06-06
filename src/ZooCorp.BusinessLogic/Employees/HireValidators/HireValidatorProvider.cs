using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Exceptions;

namespace ZooCorp.BusinessLogic.Employees.HireValidators
{
    public static class HireValidatorProvider
    {
        public static IHireValidator GetHireValidator(IEmployee employee)
        {
            string emploeeType = employee.GetType().Name;

            if (emploeeType == "Veterinarian")
            {
                return new VeterinarianHireValidator();
            }
            if (emploeeType == "ZooKeeper")
            {
                return new ZooKeeperHireValidator();
            }

            throw new UnknownEmployeeException("The zoo does not have this type of employees.");
        }
    }
}
