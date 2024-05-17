using System;
using System.Collections.Generic;
using System.Drawing;
using GalaxyWars.Models;

namespace GalaxyWars.Setup
{
    public class PlayerSetup
    {
        private readonly Cell[,] _gameBoard;
        private readonly List<Player> _players;
        private readonly Game _game;
        private readonly int _playerCount;

        public PlayerSetup(Cell[,] gameBoard, List<Player> players, Game game, int playerCount)
        {
            _gameBoard = gameBoard;
            _players = players;
            _game = game;
            _playerCount = playerCount;
        }

        public void SetupPlayers()
        {
            var random = new Random();
            for (int i = 0; i < _playerCount; i++)
            {
                int x, y;
                do
                {
                    x = random.Next(_gameBoard.GetLength(0));
                    y = random.Next(_gameBoard.GetLength(1));
                } while (_gameBoard[x, y].OccupiedByPlanet != null); // Gezegenlerin üzerine oyuncu koyma

                var player = new Player($"Player {i + 1}", (ConsoleColor)(i + 1), _gameBoard[x, y]);
                _players.Add(player);
                _gameBoard[x, y].OccupiedByPlayer = player;
            }
        }
    }
}
