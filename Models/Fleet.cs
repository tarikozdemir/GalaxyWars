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
        public Player Owner { get; set; } // Filonun sahibi olan oyuncu

        public Fleet(string name, Point startingLocation, Player owner)
        {
            Name = name;
            Ships = new List<SpaceShip>();
            CurrentLocation = startingLocation;
            Owner = owner;
        }

        public void AddShip(SpaceShip ship)
        {
            Ships.Add(ship);
            Console.WriteLine($"{Name} fleet added a {ship.Name}.");
        }

        public void DisplayFleetInfo()
        {
            Console.WriteLine($"Fleet {Name} consists of {Ships.Count} ships with a minimum speed of {Ships.Min(ship => ship.MaxSpeed)}.");
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
                targetFleet.Ships.Clear();
            }
            else
            {
                Console.WriteLine($"{Name} has lost the battle against {targetFleet.Name}.");
                Ships.Clear();
            }
        }

        public void Attack(Planet targetPlanet)
        {
            int totalFirePower = Ships.Sum(ship => ship.FirePower);

            Console.WriteLine($"{Name} fleet is attacking {targetPlanet.Name} with {totalFirePower} firepower.");

            if (totalFirePower > targetPlanet.DefenseCapacity)
            {
                Console.WriteLine($"{Name} fleet has successfully conquered {targetPlanet.Name}!");
                targetPlanet.DefenseCapacity = 0;
                targetPlanet.BeOccupied(this.Owner); // Owner kullanarak gezegeni işgal et
            }
            else
            {
                Console.WriteLine($"{Name} fleet failed to conquer {targetPlanet.Name}. Remaining defense: {targetPlanet.DefenseCapacity - totalFirePower}");
                targetPlanet.DefenseCapacity -= totalFirePower;
            }
        }
    }
}
