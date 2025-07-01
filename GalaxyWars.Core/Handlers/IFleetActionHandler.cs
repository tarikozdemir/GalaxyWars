using GalaxyWars.Models;

namespace GalaxyWars.Handlers
{
    public interface IFleetActionHandler
    {
        void MoveFleet(Player player);
        void AttackSequence(Player player);
    }
}
