using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void ShouldBeAbleToCreateAntidepression()
        {
            Antidepression antidepression = new Antidepression();
        }

        [Fact]
        public void ShouldBeAbleToCreateAntiinflammatory()
        {
            Antiinflammatory antiinflammatory = new Antiinflammatory();
        }
    }
}
