using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ZooCorp.BusinessLogic.Employees;
using ZooCorp.BusinessLogic.Medicines;
using ZooCorp.BusinessLogic.Animals.Birds;

namespace ZooCorp.Tests.Employees
{
    public class VeterinarianTest
    {
        [Fact]
        public void ShouldBeAbleToCreateVeterinarian()
        {
            Veterinarian veterinarian = new Veterinarian("Bob", "Smith", new List<string>() { "Bison" });
            Assert.Equal("Bob", veterinarian.FirstName);
            Assert.Equal("Smith", veterinarian.LastName);
            Assert.Equal("Bison", veterinarian.AnimalExperiences[0]);
        }

        [Fact]
        public void ShouldBeAbleToAddAnimalExperience()
        {
            Veterinarian veterinarian = new Veterinarian("Bob", "Smith");
            veterinarian.AddAnimalExperience(new Parrot(1));
            Assert.Equal("Parrot", veterinarian.AnimalExperiences[0]);
        }

        [Fact]
        public void ShouldHaveAnimalExperience()
        {
            Veterinarian veterinarian = new Veterinarian("Bob", "Smith");
            veterinarian.AddAnimalExperience(new Parrot(1));
            Assert.True(veterinarian.HasAnimalExperience(new Parrot(1)));
        }

        [Fact]
        public void ShouldNotHaveAnimalExperience()
        {
            Veterinarian veterinarian = new Veterinarian("Bob", "Smith");
            Assert.False(veterinarian.HasAnimalExperience(new Parrot(1)));
        }

        [Fact]
        public void ShouldHealAnimalIfHasAnimalExperienceAndAnimalIsSick()
        {
            Veterinarian veterinarian = new Veterinarian("Bob", "Smith", new List<string>() { "Parrot" });
            var parrot = new Parrot(1);
            parrot.MarkSick(new Antibiotics());
            Assert.True(parrot.IsSick());
            Assert.True(veterinarian.HealAnimal(parrot));
        }

        [Fact]
        public void ShouldNotFeedAnimalIfDoesNotHaveAnimalExperience()
        {
            Veterinarian veterinarian = new Veterinarian("Bob", "Smith", new List<string>() { "Parrot" });
            Assert.False(veterinarian.HealAnimal(new Penguin(1)));
        }

        [Fact]
        public void ShouldNotHealAnimalIfAnimalINotsSick()
        {
            Veterinarian veterinarian = new Veterinarian("Bob", "Smith", new List<string>() { "Parrot" });
            var parrot = new Parrot(1);
            Assert.False(parrot.IsSick());
            Assert.False(veterinarian.HealAnimal(new Penguin(1)));
        }
    }
}
