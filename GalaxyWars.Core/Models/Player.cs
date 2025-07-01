using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace GalaxyWars.Models
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
        public string Color { get; set; }

        public Player(string name, string color, Cell homeBase)
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

        public bool CanCreateFleet()
        {
            return SpaceShips.Any(ship => !Fleets.SelectMany(f => f.Ships).Contains(ship));
        }

        public void CreateFleet(string fleetName, Game game)
        {
            if (CanCreateFleet())
            {
                Fleet newFleet = new Fleet(fleetName, HomeBase.Position, this, game);
                AddShipsToFleet(newFleet);
                Fleets.Add(newFleet);
                Console.WriteLine($"Fleet {fleetName} has been created.");
            }
            else
            {
                Console.WriteLine("No available ships to create a new fleet.");
            }
        }

        public void AddShipsToFleet(Fleet fleet)
        {
            Console.WriteLine("Select ships to add to the fleet (comma-separated indices):");
            for (int i = 0; i < SpaceShips.Count; i++)
            {
                if (!Fleets.SelectMany(f => f.Ships).Contains(SpaceShips[i]))
                {
                    Console.WriteLine($"{i + 1}. {SpaceShips[i].Name}");
                }
            }

            string input = Console.ReadLine()!;
            var indices = input.Split(',').Select(index => int.Parse(index.Trim()) - 1).ToList();
            foreach (var index in indices)
            {
                if (index >= 0 && index < SpaceShips.Count)
                {
                    fleet.AddShip(SpaceShips[index]);
                }
            }
        }

        public void UpgradePlanetDefense(Planet planet, int amount)
        {
            if (Gold >= amount)
            {
                Gold -= amount;
                planet.DefenseCapacity += amount;
                Console.WriteLine($"{planet.Name} defense upgraded by {amount}.");
            }
            else
            {
                Console.WriteLine("Not enough gold to upgrade defense.");
            }
        }
    }
}
