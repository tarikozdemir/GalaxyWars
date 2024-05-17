using System;
using GalaxyWars.Display;

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
            Console.WriteLine("6. View Occupied Planets and Fleets");
            Console.WriteLine("7. View All Occupied Planets");
            Console.WriteLine("0. End Turn");
            Console.ResetColor();
        }

        public void ProcessCommand(Player player, string command)
        {
            switch (command)
            {
                case "1":
                    BuySpaceShip(player);
                    break;
                case "2":
                    _game.CreateFleetOption(player);
                    break;
                case "3":
                    _game.MoveFleet(player);
                    break;
                case "4":
                    _game.AttackSequence(player);
                    break;
                case "5":
                    _game.UpgradePlanetDefenseOption(player);
                    break;
                case "6":
                    _game.DisplayOccupiedPlanetsAndFleets(player);
                    break;
                case "7":
                    _game.DisplayAllOccupiedPlanets();
                    break;
                case "0":
                    Console.WriteLine("Ending turn.");
                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
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
                new SpaceShip("Sovereign", SpaceShipType.CapitalShip, 170, 510, 410, 620, 510, 1220),
                new SpaceShip("Emperor", SpaceShipType.CapitalShip, 190, 520, 420, 640, 520, 1240),
                new SpaceShip("Cargo Freighter", SpaceShipType.Freighter, 100, 600, 500, 100, 600, 500),
                new SpaceShip("Heavy Hauler", SpaceShipType.Freighter, 120, 610, 510, 120, 610, 520),
                new SpaceShip("Bulk Transporter", SpaceShipType.Freighter, 140, 620, 520, 140, 620, 540)
            };

            Console.WriteLine("Available Spaceships:");
            for (int i = 0; i < availableShips.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {availableShips[i].Name} - Type: {availableShips[i].Type}, Speed: {availableShips[i].MaxSpeed}, FirePower: {availableShips[i].FirePower}, Cost: {availableShips[i].Cost}");
            }

            Console.WriteLine("Enter the number of the spaceship you want to buy:");
            string input = Console.ReadLine()!;
            if (int.TryParse(input, out int choice) && choice > 0 && choice <= availableShips.Length)
            {
                var chosenShip = availableShips[choice - 1];
                if (player.Gold >= chosenShip.Cost)
                {
                    player.SpaceShips.Add(chosenShip);
                    player.Gold -= chosenShip.Cost;
                    Console.WriteLine($"You have purchased a {chosenShip.Name}.");
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
