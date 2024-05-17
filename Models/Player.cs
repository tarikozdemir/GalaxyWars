using System;
using System.Collections.Generic;
using System.Linq;

namespace GalaxyWars
{
    public class Player
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Gold { get; set; }
        public List<Fleet> Fleets { get; set; }
        public List<SpaceShip> SpaceShips { get; set; }
        public bool IsAlive => Health > 0;
        public Dictionary<string, int> Resources { get; private set; }
        public Cell HomeBase { get; set; }
        public ConsoleColor Color { get; set; }

        // Yeni constructor
        public Player(string name, ConsoleColor color, Cell homeBase)
        {
            Name = name;
            Health = 100;
            Gold = 1000;
            Resources = new Dictionary<string, int>();
            Fleets = new List<Fleet>();
            SpaceShips = new List<SpaceShip>();
            Color = color;
            HomeBase = homeBase;
        }

        public void AddResource(string resource, int amount)
        {
            if (Resources.ContainsKey(resource))
            {
                Resources[resource] += amount;
            }
            else
            {
                Resources.Add(resource, amount);
            }
        }

        public bool CanCreateFleet()
        {
            // Oyuncunun filolara atanmamış gemileri var mı kontrol eder
            return SpaceShips.Any(ship => !Fleets.SelectMany(f => f.Ships).Contains(ship));
        }

        public void AddShipsToFleet(Fleet fleet)
        {
            Console.WriteLine("Select ships to add to the fleet:");
            var availableShips = SpaceShips.Where(ship => !Fleets.SelectMany(f => f.Ships).Contains(ship)).ToList();
            if (availableShips.Count == 0)
            {
                Console.WriteLine("No available ships to add to the fleet.");
                return;
            }

            for (int i = 0; i < availableShips.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableShips[i].Name}");
            }

            Console.WriteLine("Enter the numbers of the ships to add to the fleet (comma-separated):");
            string input = Console.ReadLine()!;
            var shipNumbers = input.Split(',').Select(int.Parse).ToList();

            foreach (var number in shipNumbers)
            {
                if (number > 0 && number <= availableShips.Count)
                {
                    var ship = availableShips[number - 1];
                    fleet.AddShip(ship);
                }
                else
                {
                    Console.WriteLine("Invalid ship number.");
                }
            }
        }

        public void UpgradePlanetDefense(Planet planet, int amount)
        {
            if (Gold >= amount)
            {
                planet.DefenseCapacity += amount;
                Gold -= amount;
                Console.WriteLine($"{planet.Name}'s defense has been upgraded by {amount}. New defense capacity: {planet.DefenseCapacity}");
            }
            else
            {
                Console.WriteLine("Not enough gold to upgrade the planet's defense.");
            }
        }
    }
}
