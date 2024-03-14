namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        private const float k_MaximumElectricCarCapcity = 4.8f;
        internal ElectricCar()
        {
            Engine = new ElectricEngine(k_MaximumElectricCarCapcity);
        }
    }
}
