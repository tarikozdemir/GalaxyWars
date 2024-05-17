using System;
using System.Collections.Generic;
using System.Drawing;

namespace GalaxyWars.Models
{
    public class Planet
    {
        public string Name { get; set; }
        public Dictionary<string, int> Resources { get; set; }
        public Point Position { get; set; }
        public Player? OccupiedBy { get; set; }
        public int DefenseCapacity { get; set; }
        public ConsoleColor Color { get; set; } // Gezegenin rengi

        public Planet(string name, Point position)
        {
            Name = name;
            Position = position;
            Resources = new Dictionary<string, int>
            {
                { "Iron", 100 },
                { "Gold", 50 }
            };
            DefenseCapacity = 100; // Varsayılan savunma kapasitesi
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
            Color = player.Color; // Gezegenin rengini oyuncunun rengine ayarla
            Console.WriteLine($"{player.Name} has taken control of {Name}.");
        }

        public void ProduceResources()
        {
            foreach (var resource in Resources)
            {
                // Kaynak üretimi mantığı burada olacak, örneğin:
                Resources[resource.Key] += 10;
            }
        }
    }
}
