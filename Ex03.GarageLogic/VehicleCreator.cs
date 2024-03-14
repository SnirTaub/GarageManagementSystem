using Ex03.Types;

namespace Ex03.GarageLogic
{
    public class VehicleCreator
    {
        public static Vehicle CreateVehicle(eVehicleType i_VehicleType)
        {
            Vehicle newVehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleType.ElectricCar:
                    newVehicle = new ElectricCar();
                    break;
                case eVehicleType.ElectricMotorcycle:
                    newVehicle = new ElectricMotorcycle();
                    break;
                case eVehicleType.FueledCar:
                    newVehicle = new FueledCar();
                    break;
                case eVehicleType.FueledMotorcycle:
                    newVehicle = new FueledMotorcycle();
                    break;
                case eVehicleType.FueledTruck:
                    newVehicle = new FueledTruck();
                    break;
            }

            return newVehicle;
        }
    }
}
