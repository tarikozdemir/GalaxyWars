using System;

namespace GalaxyWars
{
    public enum SpaceShipType
    {
        Scout,
        Fighter,
        Frigate,
        Destroyer,
        Cruiser,
        CapitalShip,
        Freighter
    }

    public class SpaceShip
    {
        public string Name { get; set; }
        public SpaceShipType Type { get; set; }
        public int MaxSpeed { get; set; }
        public int FuelCapacity { get; set; }
        public int CargoCapacity { get; set; }
        public int FirePower { get; set; }
        public int ShieldStrength { get; set; }
        public int Cost { get; set; }

        public SpaceShip(string name, SpaceShipType type, int maxSpeed, int fuelCapacity, int cargoCapacity, int firePower, int shieldStrength, int cost)
        {
            Name = name;
            Type = type;
            MaxSpeed = maxSpeed;
            FuelCapacity = fuelCapacity;
            CargoCapacity = cargoCapacity;
            FirePower = firePower;
            ShieldStrength = shieldStrength;
            Cost = cost;
        }

        public void DisplaySpaceShipInfo()
        {
            Console.WriteLine($"Name: {Name}, Type: {Type}, Speed: {MaxSpeed}, Fuel: {FuelCapacity}, Cargo: {CargoCapacity}, FirePower: {FirePower}, Shield: {ShieldStrength}, Cost: {Cost}");
        }
    }
}
