using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GalaxyWars
{
    public class Fleet
    {
        public List<SpaceShip> Ships { get; set; }
        public string Name { get; set; }
        public Point CurrentLocation { get; set; }
        public Player Owner { get; set; }
        private readonly Game _game;

        public int Speed => Ships.Min(ship => ship.MaxSpeed);

        public Fleet(string name, Point currentLocation, Player owner, Game game)
        {
            Name = name;
            Ships = new List<SpaceShip>();
            CurrentLocation = currentLocation;
            Owner = owner;
            _game = game;
        }

        public void AddShip(SpaceShip ship)
        {
            Ships.Add(ship);
            Console.WriteLine($"{Name} fleet added a {ship.Name}.");
        }

        public void DisplayFleetInfo()
        {
            Console.WriteLine($"Fleet {Name} consists of {Ships.Count} ships with a minimum speed of {Speed}.");
            foreach (var ship in Ships)
            {
                ship.DisplaySpaceShipInfo();
            }
        }

        public void Attack(Fleet targetFleet)
        {
            int totalFirePower = Ships.Sum(ship => ship.FirePower);
            int targetTotalFirePower = targetFleet.Ships.Sum(ship => ship.FirePower);

            Console.WriteLine($"{Name} is attacking {targetFleet.Name}.");

            if (totalFirePower > targetTotalFirePower)
            {
                Console.WriteLine($"{Name} has won the battle against {targetFleet.Name}!");
                targetFleet.Ships.Clear();
                Console.WriteLine($"{targetFleet.Name} has been destroyed.");
                var targetPlanet = _game.Planets.FirstOrDefault(p => p.Position == targetFleet.CurrentLocation);
                if (targetPlanet != null)
                {
                    targetPlanet.BeOccupied(Owner);
                }
            }
            else if (totalFirePower < targetTotalFirePower)
            {
                Console.WriteLine($"{Name} has lost the battle against {targetFleet.Name}.");
                Ships.Clear();
                Console.WriteLine($"{Name} has been destroyed.");
            }
            else
            {
                Console.WriteLine("The battle ended in a draw.");
                Ships.Clear();
                targetFleet.Ships.Clear();
                Console.WriteLine("Both fleets have been destroyed.");
            }
        }
    }
}
