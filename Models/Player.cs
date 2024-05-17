using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

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

        public Player(string name, ConsoleColor color)
        {
            Name = name;
            Health = 100;
            Gold = 1000;
            Resources = new Dictionary<string, int>();
            Fleets = new List<Fleet>();
            SpaceShips = new List<SpaceShip>();
            Color = color;
        }

        public List<SpaceShip> AvailableShipsForFleet()
        {
            var usedShips = new HashSet<SpaceShip>(Fleets.SelectMany(f => f.Ships));
            return SpaceShips.Where(ship => !usedShips.Contains(ship)).ToList();
        }

        public void DisplayAvailableShips()
        {
            var availableShips = AvailableShipsForFleet();
            if (availableShips.Count > 0)
            {
                Console.WriteLine("Available Ships for a new Fleet:");
                for (int i = 0; i < availableShips.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {availableShips[i].Name} - Type: {availableShips[i].Type}");
                }
            }
            else
            {
                Console.WriteLine("No available ships to form a new fleet.");
            }
        }

        public void AddShipsToFleet(Fleet fleet)
        {
            var availableShips = AvailableShipsForFleet();
            if (availableShips.Count > 0)
            {
                DisplayAvailableShips();
                Console.WriteLine("Enter the indices of the ships to add to your fleet (comma separated, e.g., 1,3):");
                var indices = Console.ReadLine()!.Split(',').Select(int.Parse);
                foreach (var index in indices)
                {
                    if (index > 0 && index <= availableShips.Count)
                    {
                        var selectedShip = availableShips[index - 1];
                        fleet.AddShip(selectedShip);
                        Console.WriteLine($"{selectedShip.Name} added to {fleet.Name}.");
                    }
                    else
                    {
                        Console.WriteLine($"Invalid index: {index}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No available ships to add to the fleet.");
            }
        }

        public void BuySpaceShip()
        {
            var availableShips = new List<SpaceShip>
            {
                new SpaceShip("Scout", SpaceShipType.Scout, 600, 200, 100, 50, 50, 200),
                new SpaceShip("Fighter", SpaceShipType.Fighter, 700, 100, 50, 150, 100, 300),
                new SpaceShip("Frigate", SpaceShipType.Frigate, 400, 300, 150, 200, 200, 500),
                new SpaceShip("Destroyer", SpaceShipType.Destroyer, 500, 200, 200, 250, 250, 600),
                new SpaceShip("Cruiser", SpaceShipType.Cruiser, 300, 400, 300, 300, 300, 800),
                new SpaceShip("Capital Ship", SpaceShipType.CapitalShip, 200, 500, 400, 400, 400, 1000),
                new SpaceShip("Freighter", SpaceShipType.Freighter, 150, 600, 500, 50, 150, 400)
            };

            Console.WriteLine("Available Spaceships:");
            for (int i = 0; i < availableShips.Count; i++)
            {
                var ship = availableShips[i];
                Console.WriteLine($"{i + 1}. {ship.Name} - Type: {ship.Type}, Speed: {ship.MaxSpeed}, Cargo: {ship.CargoCapacity}, FirePower: {ship.FirePower}, Shield: {ship.ShieldStrength}, Cost: {ship.Cost}");
            }

            Console.WriteLine("Enter the number of the spaceship you want to buy:");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= availableShips.Count)
            {
                var selectedShip = availableShips[choice - 1];
                if (Gold >= selectedShip.Cost)
                {
                    SpaceShips.Add(selectedShip);
                    Gold -= selectedShip.Cost;
                    Console.WriteLine($"You have purchased a {selectedShip.Name}.");
                }
                else
                {
                    Console.WriteLine("Not enough gold to purchase this spaceship.");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter a valid number.");
            }
        }

        public void UpgradePlanetDefense(Planet planet, int amount)
        {
            if (Gold >= amount)
            {
                planet.UpgradeDefense(amount);
                Gold -= amount;
                Console.WriteLine($"{Name} upgraded the defense of {planet.Name} by {amount}. Remaining gold: {Gold}");
            }
            else
            {
                Console.WriteLine("Not enough gold to upgrade the defense.");
            }
        }
    }
}
