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
        public ConsoleColor Color { get; set; }
        public Cell HomeBase { get; set; }
        private readonly Game _game; // Game nesnesi

        public Player(string name, ConsoleColor color, Cell homeBase, Game game)
        {
            Name = name;
            Health = 100;
            Gold = 1000;
            Color = color;
            HomeBase = homeBase;
            Resources = new Dictionary<string, int>();
            Fleets = new List<Fleet>();
            SpaceShips = new List<SpaceShip>();
            _game = game;
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
            return SpaceShips.Any(ship => !Fleets.SelectMany(f => f.Ships).Contains(ship));
        }

        public void CreateFleet(string fleetName)
        {
            if (CanCreateFleet())
            {
                var newFleet = new Fleet(fleetName, HomeBase.Position, this, _game);
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
            Console.WriteLine("Available ships to add to the fleet:");
            var availableShips = SpaceShips.Where(ship => !Fleets.SelectMany(f => f.Ships).Contains(ship)).ToList();
            for (int i = 0; i < availableShips.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableShips[i].Name}");
            }

            Console.WriteLine("Enter the numbers of the ships to add to the fleet, separated by commas:");
            string input = Console.ReadLine()!;
            var selectedIndices = input.Split(',').Select(int.Parse).ToList();
            foreach (var index in selectedIndices)
            {
                if (index > 0 && index <= availableShips.Count)
                {
                    fleet.AddShip(availableShips[index - 1]);
                }
                else
                {
                    Console.WriteLine($"Invalid ship number: {index}");
                }
            }
        }

        public void UpgradePlanetDefense(Planet planet, int amount)
        {
            if (Gold >= amount)
            {
                planet.DefenseCapacity += amount;
                Gold -= amount;
                Console.WriteLine($"{planet.Name} defense upgraded by {amount}. Remaining gold: {Gold}");
            }
            else
            {
                Console.WriteLine("Not enough gold to upgrade defense.");
            }
        }
    }
}
