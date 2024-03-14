using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Ex03.Types;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        public Dictionary<string, Vehicle> VehiclesInGarage { get; set; }

        public GarageManager(Dictionary<string, Vehicle> i_VehiclesInGarage)
        {
            VehiclesInGarage = i_VehiclesInGarage;
        }

        public List<string> ShowLicencePlatesList(bool i_FilterByVehicleCondition, eVehicleCondition i_VehicleCondition = eVehicleCondition.InRepair)
        {
            List<string> licensePlatesList = new List<string>(VehiclesInGarage.Keys);
            List<string> licensePlatesListToReturn = new List<string>(2);

            if (!i_FilterByVehicleCondition)
            {
                licensePlatesListToReturn = licensePlatesList; 
            }
            else
            {
                foreach (string licensePlate in licensePlatesList)
                {
                    if (VehiclesInGarage[licensePlate].VehicleCondition == i_VehicleCondition)
                    {
                        licensePlatesListToReturn.Add(licensePlate);
                    }
                }
            }

            return licensePlatesListToReturn;
        }

        public void ChangeVehicleCondition(string i_LicensePlate, eVehicleCondition i_VehicleCondition)
        {
            VehiclesInGarage[i_LicensePlate].VehicleCondition = i_VehicleCondition;
        }

        public void AdjustAllTiresPressureToMax(string i_LicensePlate)
        {
            foreach (Tire tire in VehiclesInGarage[i_LicensePlate].TireSet)
            {
                tire.AdjustPressure(tire.MaxTirePressure - tire.CurrentTirePressure);
            }
        }

        public void ReFuel(string i_LicensePlate, eFuelType i_FuelType, float i_AmountOfFuelToAdd)
        {
            Vehicle currentVehicle = VehiclesInGarage[i_LicensePlate];
            Engine currentEngine = currentVehicle.Engine;

            try
            {
                (currentEngine as FuelEngine).ReFuel(i_AmountOfFuelToAdd, i_FuelType);
            }
            catch (Exception error)
            {
                throw error;
            }
            
        }

        public void ReCharge(string i_LicensePlate, float i_AmountOfTimeToCharge)
        {
            Vehicle currentVehicle = VehiclesInGarage[i_LicensePlate];
            Engine currentEngine = currentVehicle.Engine;

            try
            {
                (currentEngine as ElectricEngine).ReCharge(i_AmountOfTimeToCharge);
            }
            catch (ValueOutOfRangeException error)
            {
                throw error;
            }
            catch
            {
                throw new ArgumentException("You are trying to charge a fuled car.");
            }
        }

        public string GetVehicleDetails(string i_LicensePlate)
        {
            StringBuilder vehicleDetails = new StringBuilder();
            Vehicle currentVehicle = VehiclesInGarage[i_LicensePlate];

            vehicleDetails.AppendLine(string.Format(@"--- FULL VEHICLE DETAILS ---
"));
            vehicleDetails.AppendLine(string.Format(@"License plate number: {0}.
Model name: {1}.
Owner name: {2}.
Vehicle Condition: {3}.", i_LicensePlate, currentVehicle.ModelName, currentVehicle.Owner.Name, currentVehicle.VehicleCondition.ToString()));
            vehicleDetails.AppendLine(getFullTiresDetails(currentVehicle));
            vehicleDetails.AppendLine(getFullEngineDetails(currentVehicle));
            vehicleDetails.AppendLine(currentVehicle.GetAdditionalDetails());
            vehicleDetails.AppendLine(string.Format(@"----------------"));

            return vehicleDetails.ToString();
        }

        private string getFullEngineDetails(Vehicle i_CurrentVehicle)
        {
            StringBuilder engineDetails = new StringBuilder();

            if (!i_CurrentVehicle.Engine.IsElectric)
            {
                engineDetails.AppendLine("Engine type: Fueled.");
                engineDetails.AppendLine(string.Format(@"Fuel type: {0}", (i_CurrentVehicle.Engine as FuelEngine).FuelType));
            }
            else
            {
                engineDetails.AppendLine("Engine type: Electric.");
            }

            engineDetails.AppendLine(string.Format(@"Energy left in percentage: {0:0.00}%", i_CurrentVehicle.EnergyLeftInPercentage));

            return engineDetails.ToString();
        }

        private string getFullTiresDetails(Vehicle i_CurrentVehicle)
        {
            StringBuilder tiresDetails = new StringBuilder();
            int tireCounter = 1;

            tiresDetails.AppendLine(@"
--- TIRES DETAILS ---");
            foreach (Tire tire in i_CurrentVehicle.TireSet)
            {
                tiresDetails.AppendLine(string.Format(@"    ------------
    -Tire number: {0}-
    -Air Pressure: {1}-
    -Manufacturer: {2}-
", tireCounter, tire.CurrentTirePressure, tire.Manufacturer));
                tireCounter++;
            }

            tiresDetails.AppendLine(@"--- END OF TIRES DETAILS ---");

            return tiresDetails.ToString();
        }
    }
}
