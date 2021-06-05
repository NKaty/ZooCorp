using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ZooCorp.BusinessLogic.Exceptions;

namespace ZooCorp.Tests.Exceptions
{
    public class ExceptionTest
    {
        [Fact]
        public void ShouldBeAbleToCreateNotFavoriteFoodException()
        {
            NotFavoriteFoodException exception = new NotFavoriteFoodException("Message");
            Assert.Equal("Message", exception.Message);
        }

        [Fact]
        public void ShouldBeAbleToCreateNotNeededMedicineException()
        {
            NotNeededMedicineException exception = new NotNeededMedicineException("Message");
            Assert.Equal("Message", exception.Message);
        }

        [Fact]
        public void ShouldBeAbleToCreateNoAvailableSpaceException()
        {
            NoAvailableSpaceException exception = new NoAvailableSpaceException("Message");
            Assert.Equal("Message", exception.Message);
        }

        [Fact]
        public void ShouldBeAbleToCreateNotFriendlyAnimalException()
        {
            NotFriendlyAnimalException exception = new NotFriendlyAnimalException("Message");
            Assert.Equal("Message", exception.Message);
        }
    }
}
