namespace ZooCorp.BusinessLogic.Employees.HireValidators.ValidationErrors
{
    public class AnimalExperiencesValidationError : ValidationError
    {
        public string Animal { get; }

        public AnimalExperiencesValidationError(string animal, string message)
            : base("AnimalExperiences", message)
        {
            Animal = animal;
        }
    }
}