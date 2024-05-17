using System;
using System.Linq;
using GalaxyWars.Display;

namespace GalaxyWars.Handlers
{
    public class FleetActionHandler : IFleetActionHandler
    {
        private readonly Game _game;
        private readonly GameDisplay _gameDisplay;

        public FleetActionHandler(Game game, GameDisplay gameDisplay)
        {
            _game = game;
            _gameDisplay = gameDisplay;
        }

        public void MoveFleet(Player player)
        {
            Console.WriteLine("Available Fleets for Movement:");
            _gameDisplay.DisplayFleets(player);
            Console.WriteLine("Select a fleet to move:");
            var input = Console.ReadLine();
            if (int.TryParse(input, out var fleetIndex) && fleetIndex > 0 && fleetIndex <= player.Fleets.Count)
            {
                var selectedFleet = player.Fleets[fleetIndex - 1];

                var availablePlanets = _game.Planets.Where(p => p.OccupiedBy == null).ToList();
                if (availablePlanets.Any())
                {
                    Console.WriteLine("Select a target planet:");
                    for (int i = 0; i < availablePlanets.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {availablePlanets[i].Name} - Position: ({availablePlanets[i].Position.X}, {availablePlanets[i].Position.Y})");
                    }

                    var planetInput = Console.ReadLine();
                    if (int.TryParse(planetInput, out var planetIndex) && planetIndex > 0 && planetIndex <= availablePlanets.Count)
                    {
                        var targetPlanet = availablePlanets[planetIndex - 1];
                        selectedFleet.CurrentLocation = targetPlanet.Position;
                        Console.WriteLine($"Fleet {selectedFleet.Name} has moved to {targetPlanet.Name}.");

                        // Eğer hedef gezegen boşsa, onu oyuncu tarafından işgal edin
                        if (targetPlanet.OccupiedBy == null)
                        {
                            targetPlanet.BeOccupied(player);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid planet selection.");
                    }
                }
                else
                {
                    Console.WriteLine("No unoccupied planets available.");
                }
            }
            else
            {
                Console.WriteLine("Invalid fleet selection.");
            }
        }

        public void AttackSequence(Player player)
        {
            Console.WriteLine("Available Fleets for Attack:");
            _gameDisplay.DisplayFleets(player);
            Console.WriteLine("Select a fleet to attack with:");
            var fleetInput = Console.ReadLine();
            if (int.TryParse(fleetInput, out var fleetIndex) && fleetIndex > 0 && fleetIndex <= player.Fleets.Count)
            {
                var attackingFleet = player.Fleets[fleetIndex - 1];

                // Düşman filoları ve bulundukları gezegenleri listele
                _gameDisplay.DisplayEnemyFleets(player, _game.Players, _game.Planets);

                var enemyFleets = _game.Players.Where(p => p != player)
                    .SelectMany(p => p.Fleets)
                    .ToList();

                Console.WriteLine("Select a target fleet number:");
                var targetInput = Console.ReadLine();
                if (int.TryParse(targetInput, out var targetIndex) && targetIndex > 0 && targetIndex <= enemyFleets.Count)
                {
                    var targetFleet = enemyFleets[targetIndex - 1];
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
    }
}
