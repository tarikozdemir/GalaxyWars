using System;
using System.Collections.Generic;
using System.Drawing;

namespace GalaxyWars
{
    public class Planet
    {
        public string Name { get; set; }
        public Dictionary<string, int> Resources { get; set; }
        public Point Position { get; set; }
        public Player? OccupiedBy { get; set; }
        public int DefenseCapacity { get; set; }
        public ConsoleColor Color { get; set; }

        public Planet(string name, Point position)
        {
            Name = name;
            Position = position;
            Resources = new Dictionary<string, int>
            {
                { "Iron", 100 },
                { "Gold", 50 }
            };
            DefenseCapacity = 100;
            Color = ConsoleColor.Gray; // Varsayılan renk
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

        public void BeOccupied(Player player)
        {
            if (OccupiedBy != null)
            {
                Console.WriteLine($"{OccupiedBy.Name} has lost control of {Name}.");
            }
            OccupiedBy = player;
            Color = player.Color; // Gezegenin rengi oyuncunun rengiyle değiştiriliyor
            Console.WriteLine($"{player.Name} has taken control of {Name}.");
        }

        public void UpgradeDefense(int amount)
        {
            DefenseCapacity += amount;
            Console.WriteLine($"{Name} defense upgraded by {amount}. Current defense: {DefenseCapacity}");
        }

        public void ProduceResources()
        {
            foreach (var resource in Resources.Keys.ToList())
            {
                Resources[resource] += 10;
            }
            Console.WriteLine($"{Name} produced resources. Current resources: {string.Join(", ", Resources.Select(r => $"{r.Key}: {r.Value}"))}");
        }
    }
}
