using System;
using System.Collections.Generic;
using System.Drawing;

namespace GalaxyWars.Setup
{
    public class PlanetSetup
    {
        private const int BoardWidth = 20;
        private const int BoardHeight = 20;
        private Cell[,] GameBoard;
        private List<Planet> Planets;

        public PlanetSetup(Cell[,] gameBoard, List<Planet> planets)
        {
            GameBoard = gameBoard;
            Planets = planets;
        }

        public void SetupPlanets()
        {
            var random = new Random();
            for (int i = 0; i < 5; i++)
            {
                int x = random.Next(BoardWidth);
                int y = random.Next(BoardHeight);
                Planet planet = new Planet($"Planet {i + 1}", new Point(x, y));
                Planets.Add(planet);
                GameBoard[x, y].OccupiedByPlanet = planet;
            }
        }
    }
}
