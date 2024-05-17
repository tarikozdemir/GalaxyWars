using System;
using System.Collections.Generic;
using System.Drawing;

namespace GalaxyWars.Setup
{
    public class PlayerSetup
    {
        private const int BoardWidth = 20;
        private const int BoardHeight = 20;
        private Cell[,] GameBoard;
        private List<Player> Players;

        public PlayerSetup(Cell[,] gameBoard, List<Player> players)
        {
            GameBoard = gameBoard;
            Players = players;
        }

        public void SetupPlayers()
        {
            var colors = new ConsoleColor[] { ConsoleColor.Red, ConsoleColor.Blue };
            var random = new Random();
            for (int i = 0; i < 2; i++)
            {
                int x, y;
                do
                {
                    x = random.Next(BoardWidth);
                    y = random.Next(BoardHeight);
                } while (GameBoard[x, y].OccupiedByPlanet != null);

                Player player = new Player($"Player {i + 1}", colors[i]);
                Players.Add(player);
                GameBoard[x, y].OccupiedByPlayer = player;
            }
        }
    }
}
