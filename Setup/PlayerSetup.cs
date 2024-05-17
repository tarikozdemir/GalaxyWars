using System;
using System.Collections.Generic;

namespace GalaxyWars.Setup
{
    public class PlayerSetup
    {
        private const int BoardWidth = 20;
        private const int BoardHeight = 20;
        private Cell[,] GameBoard;
        private List<Player> Players;
        private readonly Game _game; // Game nesnesi

        public PlayerSetup(Cell[,] gameBoard, List<Player> players, Game game)
        {
            GameBoard = gameBoard;
            Players = players;
            _game = game;
        }

        public void SetupPlayers()
        {
            var random = new Random();
            var playerColors = new[] { ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Yellow };

            for (int i = 0; i < 2; i++) // Örnek olarak 2 oyuncu ekliyoruz
            {
                int x, y;
                do
                {
                    x = random.Next(BoardWidth);
                    y = random.Next(BoardHeight);
                } while (GameBoard[x, y].OccupiedByPlanet != null || GameBoard[x, y].OccupiedByPlayer != null);

                var homeBase = GameBoard[x, y];
                Player player = new Player($"Player {i + 1}", playerColors[i % playerColors.Length], homeBase, _game);
                Players.Add(player);
                GameBoard[x, y].OccupiedByPlayer = player;
            }
        }
    }
}
