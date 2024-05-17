using System;
using System.Drawing;

namespace GalaxyWars
{
    public class Cell
    {
        public Point Position { get; set; }
        public Player? OccupiedByPlayer { get; set; }
        public Planet? OccupiedByPlanet { get; set; }

        public Cell(int x, int y)
        {
            Position = new Point(x, y);
            OccupiedByPlayer = null;
            OccupiedByPlanet = null;
        }

        public string Display()
        {
            if (OccupiedByPlanet != null)
            {
                return "@";  // Gezegeni simgeleyen karakter
            }
            else if (OccupiedByPlayer != null)
            {
                return "P";  // Oyuncuyu simgeleyen karakter
            }
            return ".";  // Boş hücreyi simgeleyen karakter
        }
    }
}
