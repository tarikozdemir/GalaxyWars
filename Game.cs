using System;
using System.Collections.Generic;
using System.Linq;
using GalaxyWars.Display;
using GalaxyWars.Handlers;
using GalaxyWars.Models;
using GalaxyWars.Setup;

namespace GalaxyWars
{
    public class Game
    {
        public List<Player> Players { get; set; }
        public List<Planet> Planets { get; set; }
        public Cell[,] GameBoard { get; set; }
        private const int BoardWidth = 20;
        private const int BoardHeight = 20;

        private IPlayerActionHandler _playerActionHandler;
        private IFleetActionHandler _fleetActionHandler;
        private GameDisplay _gameDisplay;
        private PlayerSetup _playerSetup;
        private PlanetSetup _planetSetup;

        public enum GameState
        {
            Setup,
            Running,
            Paused,
            Ended
        }

        private GameState _currentState;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Game()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            Players = new List<Player>();
            Planets = new List<Planet>();
            GameBoard = new Cell[BoardWidth, BoardHeight];
            InitializeGameBoard();
        }

        public void SetHandlers(IPlayerActionHandler playerActionHandler, IFleetActionHandler fleetActionHandler)
        {
            _playerActionHandler = playerActionHandler;
            _fleetActionHandler = fleetActionHandler;
            _gameDisplay = new GameDisplay(GameBoard);
            _playerSetup = new PlayerSetup(GameBoard, Players, this, 2); // 2 oyunculu ayarlama
            _planetSetup = new PlanetSetup(GameBoard, Planets);
        }

        private void InitializeGameBoard()
        {
            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    GameBoard[x, y] = new Cell(x, y);
                }
            }
        }

        public void InitializeGame()
        {
            Console.WriteLine("Initializing game...");
            _planetSetup.SetupPlanets();
            _playerSetup.SetupPlayers();
        }

        public void StartGameLoop()
        {
            _currentState = GameState.Running;
            int currentPlayerIndex = 0;

            if (Players.Count == 0)
            {
                Console.WriteLine("No players initialized. Exiting game loop.");
                return;
            }

            while (_currentState == GameState.Running)
            {
                _gameDisplay.DisplayGameBoard();
                Player currentPlayer = Players[currentPlayerIndex];
                Console.WriteLine($"It's {currentPlayer.Name}'s turn. Your gold amount: {currentPlayer.Gold} golds.");
                _playerActionHandler.DisplayPlayerOptions(currentPlayer);

                string command = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(command))
                {
                    bool endTurn = _playerActionHandler.ProcessCommand(currentPlayer, command);
                    if (endTurn)
                    {
                        currentPlayerIndex = (currentPlayerIndex + 1) % Players.Count;
                    }
                }

                foreach (var planet in Planets)
                {
                    planet.ProduceResources();
                }

                CheckVictoryConditions();
            }
        }


        public void CreateFleetOption(Player player)
        {
            if (player.CanCreateFleet())
            {
                Console.WriteLine("Enter the name of the new fleet:");
                string fleetName = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(fleetName))
                {
                    var newFleet = new Fleet(fleetName, player.HomeBase.Position, player, this);
                    player.AddShipsToFleet(newFleet);
                    player.Fleets.Add(newFleet);
                    Console.WriteLine($"Fleet '{fleetName}' has been created.");
                }
                else
                {
                    Console.WriteLine("Invalid fleet name entered.");
                }
            }
            else
            {
                Console.WriteLine("No available ships to create a new fleet.");
            }
        }

        public void MoveFleet(Player player)
        {
            _fleetActionHandler.MoveFleet(player);
        }

        public void AttackSequence(Player player)
        {
            _fleetActionHandler.AttackSequence(player);
        }

        public void UpgradePlanetDefenseOption(Player player)
        {
            var occupiedPlanets = Planets.Where(p => p.OccupiedBy == player).ToList();
            if (occupiedPlanets.Any())
            {
                Console.WriteLine("Select a planet to upgrade its defense:");
                for (int i = 0; i < occupiedPlanets.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {occupiedPlanets[i].Name} - Current defense: {occupiedPlanets[i].DefenseCapacity}");
                }

                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= occupiedPlanets.Count)
                {
                    var selectedPlanet = occupiedPlanets[choice - 1];
                    Console.WriteLine("Enter the amount of gold to invest in defense upgrade:");
                    if (int.TryParse(Console.ReadLine(), out int amount))
                    {
                        player.UpgradePlanetDefense(selectedPlanet, amount);
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
            else
            {
                Console.WriteLine("You do not occupy any planets.");
            }
        }

        public void DisplayOccupiedPlanetsAndFleets(Player player)
        {
            _gameDisplay.DisplayOccupiedPlanetsAndFleets(player, Planets);
        }

        public void DisplayAllOccupiedPlanets()
        {
            _gameDisplay.DisplayAllOccupiedPlanets(Planets);
        }

        public void DisplayPlayerFleets(Player player)
        {
            _gameDisplay.DisplayPlayerFleets(player);
        }

        public void EndGame()
        {
            _currentState = GameState.Ended;
            Console.WriteLine("Game has ended.");
        }

        private void CheckVictoryConditions()
        {
            var activePlayers = Players.Where(p => p.IsAlive).ToList();
            if (activePlayers.Count == 1)
            {
                var winner = activePlayers.First();
                Console.WriteLine($"Game Over! {winner.Name} has won!");
                EndGame();
            }
        }
    }
}
