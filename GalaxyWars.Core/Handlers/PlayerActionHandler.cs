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
            // Console.ForegroundColor = player.Color; // Artık string, konsolda renk yok
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
            Console.WriteLine("10. End Game"); 
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
                case "10":
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
                new SpaceShip("Swift Scout", SpaceShipType.Scout, 600, 80, 30, 50, 30, 200),
                new SpaceShip("Eagle Eye", SpaceShipType.Scout, 620, 90, 35, 60, 35, 220),
                new SpaceShip("Rapid Explorer", SpaceShipType.Scout, 640, 100, 40, 70, 40, 240),
                new SpaceShip("Blaze Fighter", SpaceShipType.Fighter, 500, 150, 100, 200, 150, 500),
                new SpaceShip("Thunder Strike", SpaceShipType.Fighter, 520, 160, 110, 220, 160, 520),
                new SpaceShip("Viper Fang", SpaceShipType.Fighter, 540, 170, 120, 240, 170, 540),
                new SpaceShip("Guardian Frigate", SpaceShipType.Frigate, 400, 200, 150, 300, 250, 700),
                new SpaceShip("Protector", SpaceShipType.Frigate, 420, 210, 160, 320, 260, 720),
                new SpaceShip("Defender", SpaceShipType.Frigate, 440, 220, 170, 340, 270, 740),
                new SpaceShip("Annihilator Destroyer", SpaceShipType.Destroyer, 350, 300, 200, 500, 350, 900),
                new SpaceShip("Devastator", SpaceShipType.Destroyer, 370, 310, 210, 520, 360, 920),
                new SpaceShip("Obliterator", SpaceShipType.Destroyer, 390, 320, 220, 540, 370, 940),
                new SpaceShip("Titan Cruiser", SpaceShipType.Cruiser, 300, 400, 300, 600, 450, 1100),
                new SpaceShip("Colossus", SpaceShipType.Cruiser, 320, 410, 310, 620, 460, 1120),
                new SpaceShip("Behemoth", SpaceShipType.Cruiser, 340, 420, 320, 640, 470, 1140),
                new SpaceShip("Imperial Capital Ship", SpaceShipType.CapitalShip, 250, 500, 400, 800, 600, 1300),
                new SpaceShip("Majestic", SpaceShipType.CapitalShip, 270, 510, 410, 820, 610, 1320),
                new SpaceShip("Sovereign", SpaceShipType.CapitalShip, 290, 520, 420, 840, 620, 1340),
                new SpaceShip("Mammoth Freighter", SpaceShipType.Freighter, 200, 600, 800, 100, 100, 1000),
                new SpaceShip("Goliath", SpaceShipType.Freighter, 220, 610, 810, 110, 110, 1020),
                new SpaceShip("Leviathan", SpaceShipType.Freighter, 240, 620, 820, 120, 120, 1040),
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
