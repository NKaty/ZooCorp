using System.Collections.Generic;
using Xunit;
using ZooCorp.BusinessLogic.Common;
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
            Veterinarian veterinarian = new Veterinarian("Bob", "Smith", new List<string>() {"Bison"});
            Assert.Equal("Bob", veterinarian.FirstName);
            Assert.Equal("Smith", veterinarian.LastName);
            Assert.Equal("Bison", veterinarian.AnimalExperiences[0]);
        }

        [Fact]
        public void ShouldBeAbleToAddAnimalExperience()
        {
            ZooConsole console = new ZooConsole();
            Veterinarian veterinarian = new Veterinarian("Bob", "Smith", null, console);
            veterinarian.AddAnimalExperience(new Parrot(1));
            Assert.Equal("Parrot", veterinarian.AnimalExperiences[0]);
            Assert.Equal("Veterinarian: Added experience with Parrot to Veterinarian Bob Smith.", console.Messages[0]);
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
            ZooConsole console = new ZooConsole();
            Veterinarian veterinarian = new Veterinarian("Bob", "Smith", new List<string>() {"Parrot"}, console);
            var parrot = new Parrot(1);
            parrot.MarkSick(new Antibiotics());
            Assert.True(parrot.IsSick());
            Assert.True(veterinarian.HealAnimal(parrot));
            Assert.Equal("Veterinarian: Veterinarian Bob Smith healed Parrot ID 1.", console.Messages[0]);
        }

        [Fact]
        public void ShouldNotHealHealthyAnimal()
        {
            Veterinarian veterinarian = new Veterinarian("Bob", "Smith", new List<string>() {"Parrot"});
            var parrot = new Parrot(1);
            Assert.False(veterinarian.HealAnimal(parrot));
        }

        [Fact]
        public void ShouldNotFeedAnimalIfDoesNotHaveAnimalExperience()
        {
            Veterinarian veterinarian = new Veterinarian("Bob", "Smith", new List<string>() {"Parrot"});
            Assert.False(veterinarian.HealAnimal(new Penguin(1)));
        }

        [Fact]
        public void ShouldNotHealAnimalIfAnimalINotsSick()
        {
            Veterinarian veterinarian = new Veterinarian("Bob", "Smith", new List<string>() {"Parrot"});
            var parrot = new Parrot(1);
            Assert.False(parrot.IsSick());
            Assert.False(veterinarian.HealAnimal(new Penguin(1)));
        }
    }
}