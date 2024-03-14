namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        public bool IsElectric { get; set; }
        public float EnergyLeft { get; set; }
        public float EnergyCapacity { get; set; }
    }
}
