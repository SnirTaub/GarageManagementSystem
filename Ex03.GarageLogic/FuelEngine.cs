using System;
using Ex03.Types;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private readonly eFuelType r_FuelType;

        internal FuelEngine(eFuelType i_FuelType, float i_MaximumFuelCarCapacity)
        {
            IsElectric = false;
            EnergyCapacity = i_MaximumFuelCarCapacity;
            EnergyLeft = 0;
            r_FuelType = i_FuelType;
        }

        internal void ReFuel(float i_AmountOfFuel, eFuelType i_FuelType)
        {
            if (EnergyLeft + i_AmountOfFuel > EnergyCapacity || i_AmountOfFuel < 0)
            {
                throw new ValueOutOfRangeException(0, EnergyCapacity - EnergyLeft);
            }
            else if (FuelType != i_FuelType)
            {
                throw new ArgumentException(String.Format(@"The vehicle you are trying to refuel is not supporting '{0} fuel.", i_FuelType));
            }
            else
            {
                EnergyLeft += i_AmountOfFuel;
            }
        }

        internal eFuelType FuelType
        {
            get { return r_FuelType; }
        }
    }
}
