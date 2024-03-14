namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaximumElectricMotorcycleCapacity = 2.8f;
        internal ElectricMotorcycle()
        {
            Engine = new ElectricEngine(k_MaximumElectricMotorcycleCapacity);
        }
    }
}
