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
            for (int i = 0; i < 5; i++) // Örnek olarak 5 gezegen ekleyelim
            {
                int x = random.Next(_gameBoard.GetLength(0));
                int y = random.Next(_gameBoard.GetLength(1));
                var planet = new Planet($"Planet {i + 1}", new Point(x, y));
                _planets.Add(planet);
                _gameBoard[x, y].OccupiedByPlanet = planet;
            }
        }
    }
}
