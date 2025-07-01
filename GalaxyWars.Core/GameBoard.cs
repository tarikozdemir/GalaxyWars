using System;
using GalaxyWars.Models;

namespace GalaxyWars
{
    public class GameBoard
    {
        private readonly Cell[,] _gameBoard;

        public GameBoard(int width, int height)
        {
            _gameBoard = new Cell[width, height];
            InitializeGameBoard();
        }

        private void InitializeGameBoard()
        {
            for (int x = 0; x < _gameBoard.GetLength(0); x++)
            {
                for (int y = 0; y < _gameBoard.GetLength(1); y++)
                {
                    _gameBoard[x, y] = new Cell(x, y);
                }
            }
        }

        public Cell[,] GetGameBoard()
        {
            return _gameBoard;
        }

        public Cell? GetCell(int x, int y)
        {
            if (x < 0 || y < 0 || x >= _gameBoard.GetLength(0) || y >= _gameBoard.GetLength(1))
                return null;
            return _gameBoard[x, y];
        }
    }
}
