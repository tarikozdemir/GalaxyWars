using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GalaxyWars.Display
{
    public class GameDisplay
    {
        private const int BoardWidth = 20;
        private const int BoardHeight = 20;
        private Cell[,] GameBoard;

        public GameDisplay(Cell[,] gameBoard)
        {
            GameBoard = gameBoard;
        }

        public void DisplayGameBoard()
        {
            Console.WriteLine("\nCurrent Game Board:");
            for (int y = 0; y < BoardHeight; y++)
            {
                for (int x = 0; x < BoardWidth; x++)
                {
                    var cell = GameBoard[x, y];
                    if (cell.OccupiedByPlanet != null)
                    {
                        Console.ForegroundColor = cell.OccupiedByPlanet.Color; // Gezegenin rengi
                        Console.Write(" @ ");
                    }
                    else if (cell.OccupiedByPlayer != null)
                    {
                        Console.ForegroundColor = cell.OccupiedByPlayer.Color; // Oyuncunun rengi
                        Console.Write(" P ");
                    }
                    else
                    {
                        Console.Write(" . ");
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void DisplayFleetsAtPlanet(Player player, Planet planet)
        {
            var fleetsAtPlanet = player.Fleets.Where(f => f.CurrentLocation == planet.Position).ToList();
            if (fleetsAtPlanet.Any())
            {
                Console.WriteLine($"  Fleets at {planet.Name}:");
                foreach (var fleet in fleetsAtPlanet)
                {
                    Console.WriteLine($"  - {fleet.Name}");
                }
            }
            else
            {
                Console.WriteLine($"  No fleets at {planet.Name}.");
            }
        }

        public void DisplayFleets(Player player)
        {
            if (player.Fleets.Count > 0)
            {
                for (int i = 0; i < player.Fleets.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {player.Fleets[i].Name}");
                }
            }
            else
            {
                Console.WriteLine("No fleets available.");
            }
        }

        public void DisplayOccupiedPlanets(Player player, List<Planet> planets)
        {
            var occupiedPlanets = planets.Where(p => p.OccupiedBy == player).ToList();
            if (occupiedPlanets.Any())
            {
                Console.WriteLine($"{player.Name}'s Occupied Planets and their Fleets:");
                foreach (var planet in occupiedPlanets)
                {
                    Console.WriteLine($"- {planet.Name} at position ({planet.Position.X}, {planet.Position.Y})");
                    DisplayFleetsAtPlanet(player, planet);
                }
            }
            else
            {
                Console.WriteLine($"{player.Name} does not occupy any planets.");
            }

            var unassignedFleets = player.Fleets.Where(f => f.CurrentLocation == Point.Empty).ToList();
            if (unassignedFleets.Any())
            {
                Console.WriteLine("Unassigned Fleets:");
                foreach (var fleet in unassignedFleets)
                {
                    Console.WriteLine($"- {fleet.Name}");
                }
            }
        }

        public void DisplayEnemyFleets(Player currentPlayer, List<Player> players, List<Planet> planets)
        {
            var enemyPlayers = players.Where(p => p != currentPlayer).ToList();
            if (enemyPlayers.Any())
            {
                Console.WriteLine("Enemy Fleets and their Locations:");
                foreach (var enemy in enemyPlayers)
                {
                    foreach (var fleet in enemy.Fleets)
                    {
                        var planet = planets.FirstOrDefault(p => p.Position == fleet.CurrentLocation);
                        if (planet != null)
                        {
                            Console.WriteLine($"- Fleet {fleet.Name} of {enemy.Name} at Planet {planet.Name} (Position: {planet.Position.X}, {planet.Position.Y})");
                        }
                        else
                        {
                            Console.WriteLine($"- Fleet {fleet.Name} of {enemy.Name} at Position ({fleet.CurrentLocation.X}, {fleet.CurrentLocation.Y})");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No enemy fleets available.");
            }
        }
    }
}
