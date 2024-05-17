using System;
using GalaxyWars.Handlers;

namespace GalaxyWars
{
    class Program
    {
        static void Main(string[] args)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Game game = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            // Game nesnesini oluşturduktan sonra PlayerActionHandler ve FleetActionHandler oluşturulacak
            game = new Game();
            
            var playerActionHandler = new PlayerActionHandler(game);
            var fleetActionHandler = new FleetActionHandler(game);

            // Handlers'ları game nesnesine set et
            game.SetHandlers(playerActionHandler, fleetActionHandler);

            game.InitializeGame();
            game.StartGameLoop();
        }
    }
}
