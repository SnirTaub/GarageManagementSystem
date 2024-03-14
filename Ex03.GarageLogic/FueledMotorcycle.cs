using Ex03.Types;

namespace Ex03.GarageLogic
{
    public class FueledMotorcycle : Motorcycle
    {
        private const float k_MaximumFuelCapacity = 5.8f;
        private const eFuelType k_FuelType = eFuelType.Octan98;
        public eFuelType FuelType { get; }
        internal FueledMotorcycle()
        {
            Engine = new FuelEngine(k_FuelType, k_MaximumFuelCapacity);
        }
    }
}
