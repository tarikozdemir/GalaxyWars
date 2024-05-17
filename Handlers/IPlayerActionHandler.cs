using GalaxyWars.Models;

namespace GalaxyWars.Handlers
{
    public interface IPlayerActionHandler
    {
        void DisplayPlayerOptions(Player player);
        void ProcessCommand(Player player, string command);
    }
}
