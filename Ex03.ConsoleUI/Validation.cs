using System;
using System.Collections.Generic;
using Ex03.GarageLogic;
using Ex03.Types;

namespace Ex03.ConsoleUI
{
    internal class Validation
    {
        internal static bool CheckIfLicensePlateExistInGarage(Dictionary<string, GarageLogic.Vehicle> i_GarageVehicles, string i_LicenseNumberToChangeStatus)
        {
            bool licensePlateNumberIsValid = true;

            if (i_LicenseNumberToChangeStatus.Equals(string.Empty) || !i_GarageVehicles.ContainsKey(i_LicenseNumberToChangeStatus))
            {
                licensePlateNumberIsValid = false;
            }

            return licensePlateNumberIsValid;
        }

        internal static bool CheckUserTypeInput(int i_TypeInt, Type i_EnumType)
        {
            bool result = false;

            if (i_TypeInt >= 1 && i_TypeInt <= Enum.GetValues(i_EnumType).Length)
            {
                result = true;
            }
            else
            {
                throw new ValueOutOfRangeException(1, Enum.GetValues(i_EnumType).Length);
            }

            return result;
        }

        internal static string InputIsNotEmpty(string i_UserMessage)
        {
            string userInput;

            UserMessage.ClearAndDisplay(i_UserMessage);
            userInput = Console.ReadLine();
            if (userInput.Length == 0)
            {
                UserMessage.ClearAndDisplay(UserMessage.k_EmptyInput, UserMessage.k_TimeToWait);
                userInput = InputIsNotEmpty(i_UserMessage);
            }

            return userInput;
        }

        internal static int GetAndValidateVehicleTypeInputFromUser()
        {
            int vehicleTypeInt;

            UserMessage.ClearAndDisplay(UserMessage.k_VehicleTypeMenu);
            try
            {
                vehicleTypeInt = UserServer.ParseInputToInt();
                Validation.CheckUserTypeInput(vehicleTypeInt, typeof(eVehicleType));
            }
            catch (Exception error)
            {
                UserMessage.ClearAndDisplayException(error);
                vehicleTypeInt = GetAndValidateVehicleTypeInputFromUser();
            }

            return vehicleTypeInt;
        }

        internal static string GetAndValidateOwnerCellphone()
        {
            string ownerCellNumberStr;

            UserMessage.ClearAndDisplay("Please enter your cellphone number:");
            ownerCellNumberStr = Console.ReadLine();
            try
            {
                float.Parse(ownerCellNumberStr);
                if (ownerCellNumberStr.Length != 10)
                {
                    throw new ArgumentException("The phone number must include 10 digits.");
                }
            }
            catch (Exception error)
            {
                UserMessage.ClearAndDisplayException(error);
                ownerCellNumberStr = GetAndValidateOwnerCellphone();
            }

            return ownerCellNumberStr;
        }

        internal static string GetAndValidateLicensePlateFromUser(GarageManager i_Garage)
        {
            string licensePlate;

            UserMessage.ClearAndDisplay("Please enter your license plate number:");
            licensePlate = Console.ReadLine();
            if (!CheckIfLicensePlateExistInGarage(i_Garage.VehiclesInGarage, licensePlate)) 
            {
                throw new ArgumentException(string.Format(@"The car with '{0}' license plate does not exist in our garage.", licensePlate));
            }

            return licensePlate;
        }

        internal static float GetAndCheckCurrentTirePressure(float i_MaxTirePressure, int i_WheelCounter)
        {
            float currentTirePressure;

            UserMessage.ClearAndDisplay(String.Format(@"Please enter wheel number {0} current tire pressure: (max: {1})", i_WheelCounter, i_MaxTirePressure));
            try
            {
                currentTirePressure = float.Parse(Console.ReadLine());
                if (currentTirePressure > i_MaxTirePressure || currentTirePressure < 0)
                {
                    throw new ValueOutOfRangeException(0, i_MaxTirePressure);
                }
            }
            catch (Exception error)
            {
                UserMessage.ClearAndDisplayException(error);
                currentTirePressure = GetAndCheckCurrentTirePressure(i_MaxTirePressure, i_WheelCounter);
            }

            return currentTirePressure;
        }

        internal static int GetAndValidateVehicleConditionFromUser()
        {
            int userConditionChoiceInInt = -1;

            UserMessage.ClearAndDisplay(UserMessage.k_VehicleConditionMenu);
            try
            {
                userConditionChoiceInInt = UserServer.ParseInputToInt();
                Validation.CheckUserTypeInput(userConditionChoiceInInt, typeof(eVehicleCondition));
            }
            catch (Exception error)
            {
                UserMessage.ClearAndDisplayException(error);
                userConditionChoiceInInt = GetAndValidateVehicleConditionFromUser();
            }

            return userConditionChoiceInInt;
        }

        internal static float SetAndValidateCurrentAmountOfEnergy(Vehicle i_Vehicle)
        {
            float energyLeft;

            UserMessage.RequestRemainingEnergy(i_Vehicle.Engine.IsElectric, i_Vehicle.Engine.EnergyCapacity);
            try
            {
                energyLeft = float.Parse(Console.ReadLine());
                if (energyLeft > i_Vehicle.Engine.EnergyCapacity || energyLeft < 0)
                {
                    throw new ValueOutOfRangeException(0, i_Vehicle.Engine.EnergyCapacity);
                }
            }
            catch (Exception error)
            {
                UserMessage.ClearAndDisplayException(error);
                energyLeft = SetAndValidateCurrentAmountOfEnergy(i_Vehicle);
            }

            return energyLeft;
        }
    }
}
