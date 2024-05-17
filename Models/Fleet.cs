using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GalaxyWars.Models
{
    public class Fleet
    {
        public List<SpaceShip> Ships { get; set; }
        public string Name { get; set; }
        public Point CurrentLocation { get; set; }
        public Player Owner { get; set; }
        private Game _game;

        public int Speed => Ships.Min(ship => ship.MaxSpeed);

        public Fleet(string name, Point currentLocation, Player owner, Game game)
        {
            Name = name;
            CurrentLocation = currentLocation;
            Owner = owner;
            _game = game;
            Ships = new List<SpaceShip>();
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

            if (totalFirePower > targetTotalFirePower)
            {
                Console.WriteLine($"{Name} has won the battle against {targetFleet.Name}!");
                targetFleet.Destroy();
            }
            else
            {
                Console.WriteLine($"{Name} has lost the battle against {targetFleet.Name}.");
                this.Destroy();
            }
        }

        public void Destroy()
        {
            Console.WriteLine($"Fleet {Name} has been destroyed.");
            Ships.Clear();
            Owner.Fleets.Remove(this);
        }
    }
}
