namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        internal ElectricEngine(float i_MaxElectricCapacity)
        {
            IsElectric = true;
            EnergyCapacity = i_MaxElectricCapacity;
            EnergyLeft = 0;
        }

        internal void ReCharge(float i_ChargingTime)
        {
            if (EnergyLeft + i_ChargingTime > EnergyCapacity || EnergyCapacity < 0)
            {
                throw new ValueOutOfRangeException(0, EnergyCapacity - EnergyLeft);
            }
            else
            {
                EnergyLeft += i_ChargingTime;
            }
        }
    }
}
