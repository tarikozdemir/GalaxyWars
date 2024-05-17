using System;
using GalaxyWars.Display;
using GalaxyWars.Handlers;
using GalaxyWars.Setup;

namespace GalaxyWars
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(new PlayerActionHandler(null!), new FleetActionHandler(null!, null!));
            var playerActionHandler = new PlayerActionHandler(game);
            var fleetActionHandler = new FleetActionHandler(game, new GameDisplay(game.GameBoard));
            game.SetHandlers(playerActionHandler, fleetActionHandler);

            game.InitializeGame();
            game.StartGameLoop();
        }
    }
}
