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

        public Planet(string name, Point position)
        {
            Name = name;
            Position = position;
            Resources = new Dictionary<string, int>
            {
                { "Iron", 100 },
                { "Gold", 50 }
            };
            DefenseCapacity = 0;
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
            Console.WriteLine($"{player.Name} has taken control of {Name}.");
        }

        public void ProduceResources()
        {
            foreach (var resource in Resources)
            {
                Resources[resource.Key] += 10; // Her turda 10 birim kaynak üretiyoruz
            }

            if (OccupiedBy != null)
            {
                foreach (var resource in Resources)
                {
                    OccupiedBy.AddResource(resource.Key, resource.Value);
                }

                Console.WriteLine($"{OccupiedBy.Name} has collected resources from {Name}.");
            }
        }
    }
}
