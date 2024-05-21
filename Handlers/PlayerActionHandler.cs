using System;
using System.Linq;
using GalaxyWars.Models;

namespace GalaxyWars.Handlers
{
    public class PlayerActionHandler : IPlayerActionHandler
    {
        private readonly Game _game;

        public PlayerActionHandler(Game game)
        {
            _game = game;
        }

        public void DisplayPlayerOptions(Player player)
        {
            Console.ForegroundColor = player.Color; // Oyuncunun rengiyle uyumlu metin rengi
            Console.WriteLine($"{player.Name}, choose an action:");
            Console.WriteLine("1. Buy SpaceShip");
            Console.WriteLine("2. Create Fleet");
            Console.WriteLine("3. Move Fleet");
            Console.WriteLine("4. Attack");
            Console.WriteLine("5. Upgrade Planet Defense");
            Console.WriteLine("6. View Your Fleets");
            Console.WriteLine("7. View Occupied Planets and Fleets");
            Console.WriteLine("8. View All Occupied Planets");
            Console.WriteLine("9. End Turn");
            Console.WriteLine("0. End Game"); // "End Game" seçeneği en sona taşındı
            Console.ResetColor();
        }

        public bool ProcessCommand(Player player, string command)
        {
            bool endTurn = false;

            switch (command)
            {
                case "1":
                    BuySpaceShip(player);
                    break;
                case "2":
                    _game.CreateFleetOption(player);
                    break;
                case "3":
                    if (player.Fleets.Any())
                    {
                        _game.MoveFleet(player);
                        endTurn = true;
                    }
                    else
                    {
                        Console.WriteLine("You do not have any fleets to move.");
                    }
                    break;
                case "4":
                    if (player.Fleets.Any())
                    {
                        _game.AttackSequence(player);
                        endTurn = true;
                    }
                    else
                    {
                        Console.WriteLine("You do not have any fleets to attack with.");
                    }
                    break;
                case "5":
                    _game.UpgradePlanetDefenseOption(player);
                    endTurn = true;
                    break;
                case "6":
                    _game.DisplayPlayerFleets(player);
                    break;
                case "7":
                    _game.DisplayOccupiedPlanetsAndFleets(player);
                    break;
                case "8":
                    _game.DisplayAllOccupiedPlanets();
                    break;
                case "9":
                    Console.WriteLine("Ending turn.");
                    endTurn = true;
                    break;
                case "0":
                    _game.EndGame();
                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
            return endTurn;
        }

        private void BuySpaceShip(Player player)
        {
            var availableShips = new[]
            {
                new SpaceShip("Swift Scout", SpaceShipType.Scout, 500, 100, 50, 100, 50, 300),
                new SpaceShip("Eagle Eye", SpaceShipType.Scout, 520, 110, 60, 110, 60, 320),
                new SpaceShip("Rapid Explorer", SpaceShipType.Scout, 540, 120, 70, 120, 70, 340),
                new SpaceShip("Blaze Fighter", SpaceShipType.Fighter, 400, 150, 100, 200, 100, 450),
                new SpaceShip("Thunder Strike", SpaceShipType.Fighter, 420, 160, 110, 220, 110, 470),
                new SpaceShip("Viper Fang", SpaceShipType.Fighter, 440, 170, 120, 240, 120, 490),
                new SpaceShip("Guardian Frigate", SpaceShipType.Frigate, 300, 200, 150, 300, 200, 600),
                new SpaceShip("Protector", SpaceShipType.Frigate, 320, 210, 160, 320, 210, 620),
                new SpaceShip("Defender", SpaceShipType.Frigate, 340, 220, 170, 340, 220, 640),
                new SpaceShip("Annihilator Destroyer", SpaceShipType.Destroyer, 250, 300, 200, 400, 300, 800),
                new SpaceShip("Devastator", SpaceShipType.Destroyer, 270, 310, 210, 420, 310, 820),
                new SpaceShip("Obliterator", SpaceShipType.Destroyer, 290, 320, 220, 440, 320, 840),
                new SpaceShip("Titan Cruiser", SpaceShipType.Cruiser, 200, 400, 300, 500, 400, 1000),
                new SpaceShip("Colossus", SpaceShipType.Cruiser, 220, 410, 310, 520, 410, 1020),
                new SpaceShip("Behemoth", SpaceShipType.Cruiser, 240, 420, 320, 540, 420, 1040),
                new SpaceShip("Imperial Capital Ship", SpaceShipType.CapitalShip, 150, 500, 400, 600, 500, 1200),
                new SpaceShip("Majestic", SpaceShipType.CapitalShip, 170, 510, 410, 620, 510, 1220),
                new SpaceShip("Sovereign", SpaceShipType.CapitalShip, 190, 520, 420, 640, 520, 1240),
                new SpaceShip("Mammoth Freighter", SpaceShipType.Freighter, 100, 600, 500, 100, 600, 1400),
                new SpaceShip("Goliath", SpaceShipType.Freighter, 120, 610, 510, 120, 610, 1420),
                new SpaceShip("Leviathan", SpaceShipType.Freighter, 140, 620, 520, 140, 620, 1440),
            };

            Console.WriteLine("Available Spaceships:");
            for (int i = 0; i < availableShips.Length; i++)
            {
                var ship = availableShips[i];
                Console.WriteLine($"{i + 1}. {ship.Name} - Type: {ship.Type}, Speed: {ship.MaxSpeed}, Cost: {ship.Cost}");
            }

            Console.WriteLine("Enter the number of the spaceship you want to buy:");
            string input = Console.ReadLine()!;
            if (int.TryParse(input, out int choice) && choice > 0 && choice <= availableShips.Length)
            {
                var selectedShip = availableShips[choice - 1];
                if (player.Gold >= selectedShip.Cost)
                {
                    player.Gold -= selectedShip.Cost;
                    player.SpaceShips.Add(selectedShip);
                    Console.WriteLine($"You have purchased a {selectedShip.Name}.");
                }
                else
                {
                    Console.WriteLine("Not enough gold to purchase this spaceship.");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
    }
}
