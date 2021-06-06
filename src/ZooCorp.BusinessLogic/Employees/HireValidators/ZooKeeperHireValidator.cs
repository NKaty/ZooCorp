using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Animals;
using ZooCorp.BusinessLogic.Exceptions;
using ZooCorp.BusinessLogic.Employees.HireValidators.ValidationErrors;

namespace ZooCorp.BusinessLogic.Employees.HireValidators
{
    public class ZooKeeperHireValidator : HireValidator, IHireValidator
    {
        public override List<ValidationError> ValidateEmployee(IEmployee employee, List<Animal> animals)
        {
            List<ValidationError> validationErrors = new List<ValidationError>();
            ZooKeeper zooKeeper = employee as ZooKeeper;

            if (zooKeeper == null)
            {
                throw new UnknownEmployeeException("The zoo does not have this type of employees.");
            }

            foreach (var animal in animals)
            {
                if (!zooKeeper.HasAnimalExperience(animal))
                {
                    validationErrors.Add(new AnimalExperiencesValidationError(animal.GetType().Name, "No experience"));
                }
            }

            return validationErrors;
        }
    }
}
