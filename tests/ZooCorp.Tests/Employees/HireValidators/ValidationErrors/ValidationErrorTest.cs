using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ZooCorp.BusinessLogic.Employees.HireValidators.ValidationErrors;

namespace ZooCorp.Tests.Employees.HireValidators.ValidationErrors
{
    public class ValidationErrorTest
    {
        [Fact]
        public void ShouldBeAbleToCreateValidationError()
        {
            ValidationError validationError = new ValidationError("Name", "Invalid");
            Assert.Equal("Name", validationError.Property);
            Assert.Equal("Invalid", validationError.Message);
        }
    }
}
