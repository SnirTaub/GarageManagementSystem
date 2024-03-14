using Ex03.Types;

namespace Ex03.GarageLogic
{
    public class FueledTruck : Truck
    {
        private const float k_MaximumFuelCapacity = 110f;
        private const eFuelType k_FuelType = eFuelType.Soler;
        public eFuelType FuelType { get; set; }
        internal FueledTruck()
        {
            Engine = new FuelEngine(k_FuelType, k_MaximumFuelCapacity);
        }
    }
}
