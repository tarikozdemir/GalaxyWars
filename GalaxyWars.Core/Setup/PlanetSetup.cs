using System;
using System.Collections.Generic;
using System.Drawing;
using GalaxyWars.Models;

namespace GalaxyWars.Setup
{
    public class PlanetSetup
    {
        private readonly Cell[,] _gameBoard;
        private readonly List<Planet> _planets;

        public PlanetSetup(Cell[,] gameBoard, List<Planet> planets)
        {
            _gameBoard = gameBoard;
            _planets = planets;
        }

        public void SetupPlanets()
        {
            var random = new Random();
            int width = _gameBoard.GetLength(0);
            int height = _gameBoard.GetLength(1);
            int totalCells = width * height;
            int planetCount = Math.Clamp(totalCells / 10, 3, 20); // %10, min 3, max 20
            var usedPositions = new HashSet<(int, int)>();

            for (int i = 0; i < planetCount; i++)
            {
                int x, y;
                do
                {
                    x = random.Next(width);
                    y = random.Next(height);
                } while (usedPositions.Contains((x, y)) || _gameBoard[x, y].OccupiedByPlanet != null);

                usedPositions.Add((x, y));
                var planet = new Planet($"Planet {i + 1}", new System.Drawing.Point(x, y));
                _planets.Add(planet);
                _gameBoard[x, y].OccupiedByPlanet = planet;
            }
        }
    }
}
