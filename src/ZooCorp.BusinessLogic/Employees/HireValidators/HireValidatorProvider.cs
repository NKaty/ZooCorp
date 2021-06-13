using ZooCorp.BusinessLogic.Exceptions;

namespace ZooCorp.BusinessLogic.Employees.HireValidators
{
    public static class HireValidatorProvider
    {
        public static IHireValidator GetHireValidator(IEmployee employee)
        {
            string employeeType = employee.GetType().Name;

            if (employeeType == "Veterinarian")
            {
                return new VeterinarianHireValidator();
            }

            if (employeeType == "ZooKeeper")
            {
                return new ZooKeeperHireValidator();
            }

            throw new UnknownEmployeeException("The zoo does not have this type of employees.");
        }
    }
}