using System.Collections.Generic;
using ZooCorp.BusinessLogic.Animals;
using ZooCorp.BusinessLogic.Exceptions;
using ZooCorp.BusinessLogic.Employees.HireValidators.ValidationErrors;

namespace ZooCorp.BusinessLogic.Employees.HireValidators
{
    public class VeterinarianHireValidator : HireValidator, IHireValidator
    {
        public override List<ValidationError> ValidateEmployee(IEmployee employee, List<Animal> animals)
        {
            List<ValidationError> validationErrors = new List<ValidationError>();
            Veterinarian veterinarian = employee as Veterinarian;

            if (veterinarian is null)
            {
                throw new UnknownEmployeeException("The zoo does not have this type of employees.");
            }

            foreach (var animal in animals)
            {
                if (!veterinarian.HasAnimalExperience(animal))
                {
                    validationErrors.Add(new AnimalExperiencesValidationError(animal.GetType().Name, "No experience"));
                }
            }

            return validationErrors;
        }
    }
}