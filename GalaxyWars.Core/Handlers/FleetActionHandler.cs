using System;
using System.Linq;
using GalaxyWars.Models;

namespace GalaxyWars.Handlers
{
    public class FleetActionHandler : IFleetActionHandler
    {
        private readonly Game _game;

        public FleetActionHandler(Game game)
        {
            _game = game;
        }

        public void MoveFleet(Player player)
        {
            try
            {
                if (!player.Fleets.Any())
                {
                    Console.WriteLine("You do not have any fleets to move.");
                    return;
                }

                Console.WriteLine("Available Fleets for Movement:");
                DisplayFleets(player);
                Console.WriteLine("Select a fleet to move:");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                string input = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                if (int.TryParse(input, out int fleetIndex) && fleetIndex > 0 && fleetIndex <= player.Fleets.Count)
                {
                    Fleet selectedFleet = player.Fleets[fleetIndex - 1];

                    // Gezegenlerin listesini göster
                    var planets = _game.Planets;
                    Console.WriteLine("Available Planets:");
                    for (int i = 0; i < planets.Count; i++)
                    {
                        var planet = planets[i];
                        string occupiedBy = planet.OccupiedBy != null ? planet.OccupiedBy.Name : "None";
                        Console.WriteLine($"{i + 1}. {planet.Name} (Occupied by: {occupiedBy})");
                    }

                    Console.WriteLine("Select a planet to move the fleet to:");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                    string planetInput = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                    if (int.TryParse(planetInput, out int planetIndex) && planetIndex > 0 && planetIndex <= planets.Count)
                    {
                        var targetPlanet = planets[planetIndex - 1];
                        if (targetPlanet.OccupiedBy == null || targetPlanet.OccupiedBy == player)
                        {
                            // Gezegen ele geçirilecek veya zaten bu oyuncuya ait
                            targetPlanet.BeOccupied(player);
                            selectedFleet.CurrentLocation = targetPlanet.Position;
                            Console.WriteLine($"Fleet {selectedFleet.Name} has moved to {targetPlanet.Name} and occupied it.");
                        }
                        else
                        {
                            Console.WriteLine("This planet is occupied by another player.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid planet selection.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid fleet selection.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error moving fleet: {ex.Message}");
            }
        }

        public void AttackSequence(Player player)
        {
            if (!player.Fleets.Any())
            {
                Console.WriteLine("You do not have any fleets to attack with.");
                return;
            }

            Console.WriteLine("Available Fleets for Attack:");
            for (int i = 0; i < player.Fleets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {player.Fleets[i].Name}");
            }
            Console.WriteLine("Select a fleet to attack with:");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string fleetInput = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (int.TryParse(fleetInput, out int fleetIndex) && fleetIndex > 0 && fleetIndex <= player.Fleets.Count)
            {
                Fleet attackingFleet = player.Fleets[fleetIndex - 1];
                Console.WriteLine("Select a target fleet number:");
                var enemyFleets = _game.Players.Where(p => p != player)
                                                .SelectMany(p => p.Fleets)
                                                .ToList();
                for (int i = 0; i < enemyFleets.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {enemyFleets[i].Name} - Owner: {enemyFleets[i].Owner.Name}");
                }
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                string targetInput = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                if (int.TryParse(targetInput, out int targetIndex) && targetIndex > 0 && targetIndex <= enemyFleets.Count)
                {
                    Fleet targetFleet = enemyFleets[targetIndex - 1];
                    attackingFleet.Attack(targetFleet);
                }
                else
                {
                    Console.WriteLine("Invalid target fleet selection.");
                }
            }
            else
            {
                Console.WriteLine("Invalid attacking fleet selection.");
            }
        }

        private void DisplayFleets(Player player)
        {
            for (int i = 0; i < player.Fleets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {player.Fleets[i].Name}");
            }
        }
    }
}
