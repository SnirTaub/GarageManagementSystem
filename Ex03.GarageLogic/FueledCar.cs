using Ex03.Types;

namespace Ex03.GarageLogic
{
    public class FueledCar : Car
    {
        private const float k_MaximumFuelCapacity = 58f;
        private const eFuelType k_FuelType = eFuelType.Octan95;
        public eFuelType FuelType { get; set; }
        internal FueledCar()
        {
            Engine = new FuelEngine(k_FuelType, k_MaximumFuelCapacity);
        }
    }
}
