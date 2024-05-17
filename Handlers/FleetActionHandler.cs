using System;

namespace GalaxyWars.Handlers
{
    public class FleetActionHandler : IFleetActionHandler
    {
        private readonly Game _game;

        public FleetActionHandler(Game game)
        {
            _game = game;
        }

        public void MoveFleet(Player player)
        {
            Console.WriteLine("Available Fleets for Movement:");
            DisplayFleets(player);
            Console.WriteLine("Select a fleet to move:");
            string input = Console.ReadLine()!;
            if (int.TryParse(input, out int fleetIndex) && fleetIndex > 0 && fleetIndex <= player.Fleets.Count)
            {
                Fleet selectedFleet = player.Fleets[fleetIndex - 1];
                // Move fleet logic here
                Console.WriteLine($"Fleet {selectedFleet.Name} has been moved."); // Placeholder
            }
            else
            {
                Console.WriteLine("Invalid fleet selection.");
            }
        }

        public void AttackSequence(Player player)
        {
            Console.WriteLine("Available Fleets for Attack:");
            for (int i = 0; i < player.Fleets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {player.Fleets[i].Name}");
            }
            Console.WriteLine("Select a fleet to attack with:");
            string fleetInput = Console.ReadLine()!;
            if (int.TryParse(fleetInput, out int fleetIndex) && fleetIndex > 0 && fleetIndex <= player.Fleets.Count)
            {
                Fleet attackingFleet = player.Fleets[fleetIndex - 1];
                Console.WriteLine("Select a target fleet number:");
                var enemyFleets = _game.Players.Where(p => p != player)
                                                .SelectMany(p => p.Fleets)
                                                .ToList();
                for (int i = 0; i < enemyFleets.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {enemyFleets[i].Name} - Owner: {enemyFleets[i].Owner.Name}");
                }
                string targetInput = Console.ReadLine()!;
                if (int.TryParse(targetInput, out int targetIndex) && targetIndex > 0 && targetIndex <= enemyFleets.Count)
                {
                    Fleet targetFleet = enemyFleets[targetIndex - 1];
                    attackingFleet.Attack(targetFleet);
                }
                else
                {
                    Console.WriteLine("Invalid target fleet selection.");
                }
            }
            else
            {
                Console.WriteLine("Invalid attacking fleet selection.");
            }
        }

        private void DisplayFleets(Player player)
        {
            for (int i = 0; i < player.Fleets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {player.Fleets[i].Name}");
            }
        }
    }
}
