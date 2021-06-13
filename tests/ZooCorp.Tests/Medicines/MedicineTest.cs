using Xunit;
using ZooCorp.BusinessLogic.Medicines;

namespace ZooCorp.Tests.Medicines
{
    public class MedicineTest
    {
        [Fact]
        public void ShouldBeAbleToCreateAntibiotics()
        {
            Antibiotics antibiotics = new Antibiotics();
        }

        [Fact]
        public void ShouldBeAbleToCreateAntiDepression()
        {
            AntiDepression antiDepression = new AntiDepression();
        }

        [Fact]
        public void ShouldBeAbleToCreateAntiInflammatory()
        {
            AntiInflammatory antiInflammatory = new AntiInflammatory();
        }
    }
}