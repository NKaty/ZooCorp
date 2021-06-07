using System;
using System.Collections.Generic;
using ZooCorp.BusinessLogic;
using ZooCorp.BusinessLogic.Common;
using ZooCorp.BusinessLogic.Exceptions;
using ZooCorp.BusinessLogic.Animals;

namespace ZooCorp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var animals = new List<string>() { "Parrot", "Penguin", "Lion", "Bison", "Elephant", "Snake", "Turtle" };
            var console = new ZooConsole();
            var zooApp = new ZooApp(console);
            var zoo1 = zooApp.CreateZoo("London");
            var zoo2 = zooApp.CreateZoo("Paris");
            int count = 0;

            foreach (var zoo in zooApp.GetZoos())
            {
                for (int i = 0; i < 5; i++)
                {
                    zoo.AddEnclosure($"Enclosure{++count}", 2000);
                }
            }

                foreach (var zoo in zooApp.GetZoos())
            {
                foreach (var animalType in animals)
                {
                    var animal = zoo.AddAnimals(animalType);
                    
                    if (animalType is "Lion" or "Parrot")
                    {
                        animal.MarkSick();
                    }
                }
            }

            zoo1.CreateEmployee("ZooKeeper", "Bob", "Smith", animals);
            zoo1.CreateEmployee("ZooKeeper", "Tom", "Ford", animals);
            zoo1.CreateEmployee("Veterinarian", "Sam", "Ivanov", animals);
            zoo1.CreateEmployee("Veterinarian", "Lee", "Roy", animals);

            zoo2.CreateEmployee("ZooKeeper", "Anna", "Smith", animals);
            zoo2.CreateEmployee("ZooKeeper", "John", "Star", animals);
            zoo2.CreateEmployee("Veterinarian", "Harry", "James", animals);
            zoo2.CreateEmployee("Veterinarian", "Jane", "Miller", animals);

            zoo1.HealAnimals();
            zoo2.HealAnimals();

            zoo1.FeedAnimals(DateTime.Now);
            zoo2.FeedAnimals(DateTime.Now);

            foreach (var message in console.Messages)
            {
                Console.WriteLine(message);
            }
        }
    }
}
