using System;
using GalaxyWars.Handlers;

namespace GalaxyWars
{
    class Program
    {
        static void Main(string[] args)
        {
            // İlk olarak Game nesnesini tanımlıyoruz
            Game game = null!;

            // Sonra PlayerActionHandler ve FleetActionHandler nesnelerini oluşturup ayarlıyoruz
            var playerActionHandler = new PlayerActionHandler(game);
            var fleetActionHandler = new FleetActionHandler(game);

            // Game nesnesini tanımlıyoruz ve oluşturuyoruz
            game = new Game(playerActionHandler, fleetActionHandler);

            game.InitializeGame();
            game.StartGameLoop();
        }
    }
}
