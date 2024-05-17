using System;

namespace GalaxyWars.Handlers
{
    public class PlayerActionHandler : IPlayerActionHandler
    {
        private Game _game;

        public PlayerActionHandler(Game game)
        {
            _game = game;
        }

        public void DisplayPlayerOptions(Player player)
        {
            Console.ForegroundColor = player.Color;
            Console.WriteLine($"{player.Name}, choose an action:");
            Console.WriteLine("1. Buy SpaceShip");
            Console.WriteLine("2. Create Fleet");
            Console.WriteLine("3. Move Fleet");
            Console.WriteLine("4. Attack");
            Console.WriteLine("5. View Occupied Planets and Fleets");
            Console.WriteLine("6. Upgrade Planet Defense");
            Console.WriteLine("0. End Turn");
            Console.ResetColor();
        }

        public void ProcessCommand(Player player, string command)
        {
            switch (command)
            {
                case "1":
                    player.BuySpaceShip();
                    break;
                case "2":
                    _game.CreateFleetOption(player);
                    break;
                case "3":
                    if (player.Fleets.Count > 0)
                    {
                        _game.MoveFleet(player);
                    }
                    else
                    {
                        Console.WriteLine("No fleets available to move.");
                    }
                    break;
                case "4":
                    if (player.Fleets.Count > 0)
                    {
                        _game.AttackSequence(player);
                    }
                    else
                    {
                        Console.WriteLine("No fleets available to attack.");
                    }
                    break;
                case "5":
                    _game.DisplayOccupiedPlanets(player);
                    break;
                case "6":
                    _game.UpgradePlanetDefenseOption(player);
                    break;
                case "0":
                    Console.WriteLine("Ending turn.");
                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }
    }
}
