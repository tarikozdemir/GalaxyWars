using System;
using GalaxyWars.Handlers;

namespace GalaxyWars
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(null!, null!); // Geçici olarak null veriyoruz

            var playerActionHandler = new PlayerActionHandler(game);
            var fleetActionHandler = new FleetActionHandler(game);

            game.SetHandlers(playerActionHandler, fleetActionHandler);

            game.InitializeGame();
            game.StartGameLoop();
        }
    }
}
