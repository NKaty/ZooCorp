using Xunit;
using ZooCorp.BusinessLogic.Employees.HireValidators.ValidationErrors;

namespace ZooCorp.Tests.Employees.HireValidators.ValidationErrors
{
    public class AnimalExperiencesValidationErrorTest
    {
        [Fact]
        public void ShouldBeAbleToCreateAnimalExperienceValidationError()
        {
            AnimalExperiencesValidationError validationError =
                new AnimalExperiencesValidationError("Parrot", "No experience");
            Assert.Equal("AnimalExperiences", validationError.Property);
            Assert.Equal("Parrot", validationError.Animal);
            Assert.Equal("No experience", validationError.Message);
        }
    }
}