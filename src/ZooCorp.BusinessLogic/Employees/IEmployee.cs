using ZooCorp.BusinessLogic.Animals;

namespace ZooCorp.BusinessLogic.Employees
{
    public interface IEmployee
    {
        string FirstName { get; }

        string LastName { get; }

        bool HasAnimalExperience(Animal animal);
    }
}