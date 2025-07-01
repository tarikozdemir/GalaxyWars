using GalaxyWars.Models;

namespace GalaxyWars.Handlers
{
    public interface IPlayerActionHandler
    {
        void DisplayPlayerOptions(Player player);
        bool ProcessCommand(Player player, string command);
    }
}
