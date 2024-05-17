using System;
using System.Collections.Generic;
using System.Linq;

namespace GalaxyWars.Display
{
    public class GameDisplay
    {
        private Cell[,] GameBoard;

        public GameDisplay(Cell[,] gameBoard)
        {
            GameBoard = gameBoard;
        }

        public void DisplayGameBoard()
        {
            for (int y = 0; y < GameBoard.GetLength(1); y++)
            {
                for (int x = 0; x < GameBoard.GetLength(0); x++)
                {
                    if (GameBoard[x, y].OccupiedByPlanet != null)
                    {
                        var planet = GameBoard[x, y].OccupiedByPlanet;
                        if (planet!.OccupiedBy != null)
                        {
                            Console.ForegroundColor = planet.OccupiedBy.Color;
                        }
                        Console.Write(" @ ");
                        Console.ResetColor();
                    }
                    else if (GameBoard[x, y].OccupiedByPlayer != null)
                    {
                        Console.ForegroundColor = GameBoard[x, y].OccupiedByPlayer!.Color;
                        Console.Write(" P ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(" . ");
                    }
                }
                Console.WriteLine();
            }
        }

        public void DisplayFleets(Player player)
        {
            for (int i = 0; i < player.Fleets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {player.Fleets[i].Name} at ({player.Fleets[i].CurrentLocation.X}, {player.Fleets[i].CurrentLocation.Y})");
            }
        }

        public void DisplayEnemyFleets(Player player, List<Player> players, List<Planet> planets)
        {
            var enemyFleets = players.Where(p => p != player)
                                     .SelectMany(p => p.Fleets)
                                     .ToList();

            Console.WriteLine("Enemy Fleets:");
            for (int i = 0; i < enemyFleets.Count; i++)
            {
                var fleet = enemyFleets[i];
                var planet = planets.FirstOrDefault(p => p.Position == fleet.CurrentLocation);
                string location = planet != null ? $"{planet.Name} ({planet.Position.X}, {planet.Position.Y})" : $"({fleet.CurrentLocation.X}, {fleet.CurrentLocation.Y})";
                Console.WriteLine($"{i + 1}. {fleet.Name} at {location}");
            }
        }

        public void DisplayOccupiedPlanetsAndFleets(Player player, List<Planet> planets)
        {
            Console.WriteLine($"{player.Name}'s occupied planets and fleets:");
            var occupiedPlanets = planets.Where(p => p.OccupiedBy == player).ToList();
            if (occupiedPlanets.Any())
            {
                Console.WriteLine("Occupied Planets:");
                foreach (var planet in occupiedPlanets)
                {
                    Console.WriteLine($"- {planet.Name} at ({planet.Position.X}, {planet.Position.Y}) - Defense: {planet.DefenseCapacity}");
                }
            }
            else
            {
                Console.WriteLine("No occupied planets.");
            }

            if (player.Fleets.Any())
            {
                Console.WriteLine("Fleets:");
                foreach (var fleet in player.Fleets)
                {
                    Console.WriteLine($"- {fleet.Name} at ({fleet.CurrentLocation.X}, {fleet.CurrentLocation.Y})");
                }
            }
            else
            {
                Console.WriteLine("No fleets.");
            }
        }

        public void DisplayAllOccupiedPlanets(List<Planet> planets)
        {
            Console.WriteLine("All occupied planets:");
            var occupiedPlanets = planets.Where(p => p.OccupiedBy != null).ToList();
            if (occupiedPlanets.Any())
            {
                foreach (var planet in occupiedPlanets)
                {
                    var ownerName = planet.OccupiedBy != null ? planet.OccupiedBy.Name : "None";
                    Console.ForegroundColor = planet.OccupiedBy!.Color;
                    Console.WriteLine($"- {planet.Name} at ({planet.Position.X}, {planet.Position.Y}) - Defense: {planet.DefenseCapacity} - Occupied by: {ownerName}");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("No occupied planets.");
            }
        }
    }
}
