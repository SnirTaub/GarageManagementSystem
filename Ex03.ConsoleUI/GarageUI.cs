using System;
using System.Collections.Generic;
using Ex03.GarageLogic;
using Ex03.Types;

namespace Ex03.ConsoleUI
{
    internal class GarageUI
    {
        internal static void DisplayCarsLicensePlatesInGarageList(GarageManager i_Garage)
        {
            List<string> licensePlatesInGarage;
            bool filter;
            eVehicleCondition vehicleCondition = eVehicleCondition.InRepair;

            UserMessage.ClearAndDisplay(@"Do you wish to filter the results please type - [Y]es / [N]o");
            try
            {
                filter = getUserBoolInput();
            }
            catch (Exception error)
            {
                throw error;
            }

            if (filter)
            {
                UserMessage.ClearAndDisplay("Please choose one of the following condition types that you wish to filter by:");
                vehicleCondition = (eVehicleCondition)Validation.GetAndValidateVehicleConditionFromUser();
            }

            licensePlatesInGarage = i_Garage.ShowLicencePlatesList(filter, vehicleCondition);
            UserMessage.DisplayList(licensePlatesInGarage);
        }

        private static bool getUserBoolInput()
        {
            bool result = false;
            string userInputStr = string.Empty;

            userInputStr = Console.ReadLine();
            if (userInputStr.Equals("Y"))
            {
                result = true;
            }
            else if (!userInputStr.Equals("N"))
            {
                throw new FormatException();
            }

            return result;
        }

        internal static void AdjustTiresPressureToMax(GarageManager i_Garage)
        {
            string licensePlate;

            try
            {
                licensePlate = Validation.GetAndValidateLicensePlateFromUser(i_Garage);
                i_Garage.AdjustAllTiresPressureToMax(licensePlate);
                UserMessage.ClearAndDisplay(string.Format(@"You have successfully adjusted all tires pressure of vehicle with license plate number '{0}' to maximum.", licensePlate),
                    UserMessage.k_TimeToWait);
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        internal static void PrintVehicleDetails(GarageManager i_Garage)
        {
            string licensePlate, vehicleDetails;

            try
            {
                licensePlate = Validation.GetAndValidateLicensePlateFromUser(i_Garage);
                vehicleDetails = i_Garage.GetVehicleDetails(licensePlate);
                UserMessage.ClearAndDisplay(vehicleDetails, 20000);
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        internal static void RechargeVehicle(GarageManager i_Garage)
        {
            string licensePlate;
            float amountOfTimeToCharge;

            try
            {
                licensePlate = Validation.GetAndValidateLicensePlateFromUser(i_Garage);
                UserMessage.ClearAndDisplay(string.Format(@"Please enter your desired amount of time to charge the vehicle battery: (max: {0}).", i_Garage.VehiclesInGarage[licensePlate].Engine.EnergyCapacity));
                amountOfTimeToCharge = float.Parse(Console.ReadLine());
                i_Garage.ReCharge(licensePlate, amountOfTimeToCharge);
                UserMessage.ClearAndDisplay(string.Format(@"You have successfully recharged the vehicle with license plate number: '{0}', your current energy percentage is {1}%.", licensePlate, i_Garage.VehiclesInGarage[licensePlate].EnergyLeftInPercentage),
                    UserMessage.k_TimeToWait);
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        internal static void RefuelVehicle(GarageManager i_Garage)
        {
            string licensePlate;
            int fuelType;
            float amountToFuel;
            
            try
            {
                licensePlate = Validation.GetAndValidateLicensePlateFromUser(i_Garage);
                UserMessage.ClearAndDisplay(UserMessage.k_FuelTypeMenu);
                fuelType = int.Parse(Console.ReadLine());
                Validation.CheckUserTypeInput(fuelType, typeof(eFuelType));
                UserMessage.ClearAndDisplay(string.Format(@"Please enter your desired amount of fuel: (max: {0}).", i_Garage.VehiclesInGarage[licensePlate].Engine.EnergyCapacity));
                amountToFuel = float.Parse(Console.ReadLine());
                i_Garage.ReFuel(licensePlate, (eFuelType)fuelType, amountToFuel);
                UserMessage.ClearAndDisplay(string.Format(@"You have successfully refueld the vehicle with license plate number: '{0}', your current fuel percentage is {1}%.", licensePlate, i_Garage.VehiclesInGarage[licensePlate].EnergyLeftInPercentage),
                    UserMessage.k_TimeToWait);
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        internal static void InsertVehicleToGarage(Dictionary<string, Vehicle> i_VehiclesInGarage)
        {
            string licensePlate;
            eVehicleType vehicleType;
            Vehicle newGarageVehicle;

            UserMessage.ClearAndDisplay("Please enter your license plate number: ");
            licensePlate = Console.ReadLine();
            if (Validation.CheckIfLicensePlateExistInGarage(i_VehiclesInGarage, licensePlate))
            {
                UserMessage.ClearAndDisplay(UserMessage.k_VehicleIsAlreadyInGarage, UserMessage.k_TimeToWait);
                i_VehiclesInGarage[licensePlate].VehicleCondition = eVehicleCondition.InRepair;
            }
            else
            {
                vehicleType = (eVehicleType)Validation.GetAndValidateVehicleTypeInputFromUser();
                newGarageVehicle = VehicleCreator.CreateVehicle(vehicleType);
                newGarageVehicle.LicensePlate = licensePlate;
                newGarageVehicle.ModelName = Validation.InputIsNotEmpty("Please enter the vehicle model name:");
                newGarageVehicle.Owner.Name = Validation.InputIsNotEmpty("Please enter the vehicle's owner name:");
                newGarageVehicle.Owner.PhoneNumber = Validation.GetAndValidateOwnerCellphone();
                newGarageVehicle.Engine.EnergyLeft = Validation.SetAndValidateCurrentAmountOfEnergy(newGarageVehicle);
                setTiresCurrentPressure(newGarageVehicle.TireSet);
                setTiresManufacturers(newGarageVehicle.TireSet);
                updateAdditionalDetails(newGarageVehicle);
                i_VehiclesInGarage.Add(newGarageVehicle.LicensePlate, newGarageVehicle);
            }
        }

        private static void setTiresManufacturers(List<Tire> i_TireSet)
        {
            int wheelCounter = 1;

            foreach (Tire tire in i_TireSet)
            {
                tire.Manufacturer = Validation.InputIsNotEmpty(String.Format(@"Please enter wheel number {0} manufacturer", wheelCounter));
                wheelCounter++;
            }
        }

        private static void setTiresCurrentPressure(List<Tire> i_TireSet)
        {
            int wheelCounter = 1;

            foreach (Tire tire in i_TireSet)
            {
                tire.CurrentTirePressure = Validation.GetAndCheckCurrentTirePressure(tire.MaxTirePressure, wheelCounter);
                wheelCounter++;
            }
        }

        private static void updateAdditionalDetails(Vehicle i_Vehicle)
        {
            List<string> additionalParamsForVehicleObject = new List<string>(2);

            foreach (string QuestionForUser in i_Vehicle.UserUpdatingAdditionalParamsQuestions)
            {
                UserMessage.ClearAndDisplay(QuestionForUser);
                additionalParamsForVehicleObject.Add(Console.ReadLine());
            }

            try
            {
                i_Vehicle.UpdateDetails(additionalParamsForVehicleObject);
            }
            catch (Exception error)
            {
                UserMessage.ClearAndDisplayException(error);
                updateAdditionalDetails(i_Vehicle);
            }
        }

        internal static void ChangeVehicleCondition(GarageManager i_Garage)
        {
            string licenseNumberToChangeStatus = string.Empty;
            eVehicleCondition VehicleCondition;

            try
            {
                licenseNumberToChangeStatus = Validation.GetAndValidateLicensePlateFromUser(i_Garage);
                VehicleCondition = (eVehicleCondition)Validation.GetAndValidateVehicleConditionFromUser();
                i_Garage.ChangeVehicleCondition(licenseNumberToChangeStatus, VehicleCondition);
                UserMessage.ConditionChangeSucceed(licenseNumberToChangeStatus, VehicleCondition.ToString());
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}