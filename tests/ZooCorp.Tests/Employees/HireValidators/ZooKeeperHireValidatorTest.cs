using System.Collections.Generic;
using Xunit;
using ZooCorp.BusinessLogic.Animals;
using ZooCorp.BusinessLogic.Animals.Birds;
using ZooCorp.BusinessLogic.Animals.Mammals;
using ZooCorp.BusinessLogic.Exceptions;
using ZooCorp.BusinessLogic.Employees;
using ZooCorp.BusinessLogic.Employees.HireValidators;
using ZooCorp.BusinessLogic.Employees.HireValidators.ValidationErrors;

namespace ZooCorp.Tests.Employees.HireValidators
{
    public class ZooKeeperHireValidatorTest
    {
        [Fact]
        public void ShouldBeAbleToCreateZooKeeperHireValidator()
        {
            ZooKeeperHireValidator zooKeeperHireValidator = new ZooKeeperHireValidator();
        }

        [Fact]
        public void ShouldZooKeeperHaveNeededExperience()
        {
            ZooKeeperHireValidator zooKeeperHireValidator = new ZooKeeperHireValidator();
            ZooKeeper zooKeeper = new ZooKeeper("Bob", "Smith", new List<string>() {"Elephant", "Parrot", "Lion"});
            Assert.Empty(zooKeeperHireValidator.ValidateEmployee(zooKeeper, new List<Animal>()
            {
                new Parrot(1),
                new Lion(2),
                new Elephant(3)
            }));
        }

        [Fact]
        public void ShouldNotZooKeeperHaveNeededExperience()
        {
            ZooKeeperHireValidator zooKeeperHireValidator = new ZooKeeperHireValidator();
            ZooKeeper zooKeeper = new ZooKeeper("Bob", "Smith", new List<string>() {"Elephant", "Parrot", "Lion"});
            var errors = zooKeeperHireValidator.ValidateEmployee(zooKeeper, new List<Animal>()
            {
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
            ZooKeeperHireValidator zooKeeperHireValidator = new ZooKeeperHireValidator();
            Veterinarian zooKeeper = new Veterinarian("Bob", "Smith", new List<string>() {"Elephant"});
            Assert.Throws<UnknownEmployeeException>(() =>
                zooKeeperHireValidator.ValidateEmployee(zooKeeper, new List<Animal>() {new Parrot(1)}));
        }
    }
}