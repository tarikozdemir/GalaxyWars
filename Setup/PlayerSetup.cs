using System;
using System.Collections.Generic;

namespace GalaxyWars.Setup
{
    public class PlayerSetup
    {
        private readonly Cell[,] _gameBoard;
        private readonly List<Player> _players;
        private readonly Game _game;
        private readonly int _numberOfPlayers;

        public PlayerSetup(Cell[,] gameBoard, List<Player> players, Game game, int numberOfPlayers)
        {
            _gameBoard = gameBoard;
            _players = players;
            _game = game;
            _numberOfPlayers = numberOfPlayers;
        }

        public void SetupPlayers()
        {
            var random = new Random();
            for (int i = 0; i < _numberOfPlayers; i++)
            {
                int x, y;
                do
                {
                    x = random.Next(_gameBoard.GetLength(0));
                    y = random.Next(_gameBoard.GetLength(1));
                } while (_gameBoard[x, y].OccupiedByPlayer != null || _gameBoard[x, y].OccupiedByPlanet != null);

                var homeBase = _gameBoard[x, y];
                var player = new Player($"Player {i + 1}", (i % 2 == 0) ? ConsoleColor.Red : ConsoleColor.Blue, homeBase);
                homeBase.OccupiedByPlayer = player;
                _players.Add(player);
            }
        }
    }
}
