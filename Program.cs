using System;
using GalaxyWars.Handlers;

namespace GalaxyWars
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = null;

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
