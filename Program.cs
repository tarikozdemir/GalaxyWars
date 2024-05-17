using GalaxyWars;
using GalaxyWars.Display;
using GalaxyWars.Handlers;

namespace GalaxyWars
{
    class Program
    {
        static void Main(string[] args)
        {
            // Önce GameDisplay nesnesini oluşturuyoruz
            var gameDisplay = new GameDisplay(new Cell[20, 20]);

            // Önce Game nesnesini null handler'lar ile oluşturuyoruz
            Game game = new Game(null, null);

            // Daha sonra PlayerActionHandler ve FleetActionHandler nesnelerini oluşturuyoruz
            var playerActionHandler = new PlayerActionHandler(game);
            var fleetActionHandler = new FleetActionHandler(game, gameDisplay);

            // Şimdi game nesnesini güncelliyoruz ve handler'ları enjekte ediyoruz
            game.SetHandlers(playerActionHandler, fleetActionHandler);

            // Oyunu başlatıyoruz
            game.InitializeGame();
            game.StartGameLoop();
        }
    }
}
