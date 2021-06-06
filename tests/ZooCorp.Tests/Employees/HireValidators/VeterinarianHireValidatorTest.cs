using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ZooCorp.BusinessLogic.Animals;
using ZooCorp.BusinessLogic.Animals.Birds;
using ZooCorp.BusinessLogic.Animals.Mammals;
using ZooCorp.BusinessLogic.Employees;
using ZooCorp.BusinessLogic.Exceptions;
using ZooCorp.BusinessLogic.Employees.HireValidators;
using ZooCorp.BusinessLogic.Employees.HireValidators.ValidationErrors;

namespace ZooCorp.Tests.Employees.HireValidators
{
    public class VeterinarianHireValidatorTest
    {
        [Fact]
        public void ShouldBeAbleToCreateZooKeeperHireValidator()
        {
            VeterinarianHireValidator veterinarianHireValidator = new VeterinarianHireValidator();
        }

        [Fact]
        public void ShouldZooKeeperHaveNeededExperience()
        {
            VeterinarianHireValidator veterinarianHireValidator = new VeterinarianHireValidator();
            Veterinarian veterinarian = new Veterinarian("Bob", "Smith", new List<string>() { "Elephant", "Parrot", "Lion" });
            Assert.Empty(veterinarianHireValidator.ValidateEmployee(veterinarian, new List<Animal>() {
                new Parrot(1),
                new Lion(2),
                new Elephant(3)
            }));
        }

        [Fact]
        public void ShouldNotZooKeeperHaveNeededExperience()
        {
            VeterinarianHireValidator veterinarianHireValidator = new VeterinarianHireValidator();
            Veterinarian veterinarian = new Veterinarian("Bob", "Smith", new List<string>() { "Elephant", "Parrot", "Lion" });
            var errors = veterinarianHireValidator.ValidateEmployee(veterinarian, new List<Animal>() {
                new Parrot(1),
                new Penguin(2),
                new Elephant(3)
            });
            Assert.NotEmpty(errors);
            Assert.Equal("Penguin", (errors[0] as AnimalExperiencesValidationError)?.Animal);
        }

        [Fact]
        public void ShouldThrowUnknownEmployeeException()
        {
            VeterinarianHireValidator veterinarianHireValidator = new VeterinarianHireValidator();
            ZooKeeper veterinarian = new ZooKeeper("Bob", "Smith", new List<string>() { "Elephant" });
            Assert.Throws<UnknownEmployeeException>(() => veterinarianHireValidator.ValidateEmployee(veterinarian, new List<Animal>() { new Parrot(1) }));
        }
    }
}
