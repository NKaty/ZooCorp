using System;
using System.Collections.Generic;
using Xunit;
using ZooCorp.BusinessLogic.Exceptions;
using ZooCorp.BusinessLogic.Employees;
using ZooCorp.BusinessLogic.Employees.HireValidators;
using ZooCorp.BusinessLogic.Animals;

namespace ZooCorp.Tests.Employees.HireValidators
{
    public class HireValidatorProviderTest
    {
        public static IEnumerable<object[]> DataEmployee =>
            new List<object[]>
            {
                new object[] {new ZooKeeper("Bob", "Smith"), "ZooKeeperHireValidator"},
                new object[] {new Veterinarian("Bob", "Smith"), "VeterinarianHireValidator"}
            };

        [Theory]
        [MemberData(nameof(DataEmployee))]
        public void ShouldBeAbleToProvideCorrectHireValidator(IEmployee employee, string type)
        {
            Assert.Equal(HireValidatorProvider.GetHireValidator(employee).GetType().Name, type);
        }

        class Janitor : IEmployee
        {
            public string FirstName => throw new NotImplementedException();

            public string LastName => throw new NotImplementedException();

            public bool HasAnimalExperience(Animal animal)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void ShouldThrowUnknownEmployeeException()
        {
            Assert.Throws<UnknownEmployeeException>(() => HireValidatorProvider.GetHireValidator(new Janitor()));
        }
    }
}