using System;
using System.Collections.Generic;
using GalaxyWars.Models;

namespace GalaxyWars.Display
{
    public class GameDisplay
    {
        private readonly Cell[,] _gameBoard;

        public GameDisplay(Cell[,] gameBoard)
        {
            _gameBoard = gameBoard;
        }

        public void DisplayGameBoard()
        {
            Console.WriteLine("\nCurrent Game Board:");
            for (int y = 0; y < _gameBoard.GetLength(1); y++)
            {
                for (int x = 0; x < _gameBoard.GetLength(0); x++)
                {
                    Cell cell = _gameBoard[x, y];
                    if (cell.OccupiedByPlanet != null)
                    {
                        // Console.ForegroundColor = cell.OccupiedByPlanet.Color; // Blazor uyumlu değil
                        Console.Write(" @ ");
                        // Console.ResetColor();
                    }
                    else if (cell.OccupiedByPlayer != null)
                    {
                        // Console.ForegroundColor = cell.OccupiedByPlayer.Color; // Blazor uyumlu değil
                        Console.Write(" P ");
                        // Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(" . ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void DisplayOccupiedPlanetsAndFleets(Player player, List<Planet> planets)
        {
            var occupiedPlanets = planets.Where(p => p.OccupiedBy == player).ToList();
            if (occupiedPlanets.Any())
            {
                Console.WriteLine($"{player.Name}'s occupied planets and fleets:");
                foreach (var planet in occupiedPlanets)
                {
                    Console.WriteLine($"- {planet.Name} (Defense: {planet.DefenseCapacity})");
                    var fleets = player.Fleets.Where(f => f.CurrentLocation == planet.Position).ToList();
                    if (fleets.Any())
                    {
                        Console.WriteLine("  Fleets:");
                        foreach (var fleet in fleets)
                        {
                            Console.WriteLine($"  - {fleet.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  No fleets at this planet.");
                    }
                }
            }
            else
            {
                Console.WriteLine($"{player.Name} does not occupy any planets.");
            }
        }

        public void DisplayAllOccupiedPlanets(List<Planet> planets)
        {
            var occupiedPlanets = planets.Where(p => p.OccupiedBy != null).ToList();
            if (occupiedPlanets.Any())
            {
                Console.WriteLine("All occupied planets:");
                foreach (var planet in occupiedPlanets)
                {
                    Console.WriteLine($"- {planet.Name} (Occupied by: {planet.OccupiedBy!.Name}, Defense: {planet.DefenseCapacity})");
                }
            }
            else
            {
                Console.WriteLine("No planets are currently occupied.");
            }
        }

        public void DisplayPlayerFleets(Player player)
        {
            if (player.Fleets.Any())
            {
                Console.WriteLine("Your fleets:");
                foreach (var fleet in player.Fleets)
                {
                    Console.WriteLine($"Fleet: {fleet.Name} - Location: ({fleet.CurrentLocation.X},{fleet.CurrentLocation.Y})");
                }
            }
            else
            {
                Console.WriteLine("You do not have any fleets.");
            }
        }
    }
}
