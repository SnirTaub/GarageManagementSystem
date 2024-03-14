using System;
using System.Collections.Generic;
using Ex03.GarageLogic;
using Ex03.Types;

namespace Ex03.ConsoleUI
{
    internal class UserServer
    {
        private readonly GarageManager m_Garage;

        public GarageManager Garage
        {
            get { return m_Garage; }
        }

        internal static void Start()
        {
            UserServer CurrentUser = new UserServer();
            eUserChoice userChoice = eUserChoice.PrintVehicleDetails;

            UserMessage.ClearAndDisplay(UserMessage.k_Welcome, UserMessage.k_TimeToWait);
            while (userChoice != eUserChoice.Exit)
            {
                UserMessage.ClearAndDisplay(UserMessage.k_MainMenu);
                userChoice = (eUserChoice)getUserMainMenuChoice();
                try
                {
                    switch (userChoice)
                    {
                        case eUserChoice.AddNewVehicle:
                            GarageUI.InsertVehicleToGarage(CurrentUser.m_Garage.VehiclesInGarage);
                            break;
                        case eUserChoice.DisplayLicenseNumbers:
                            GarageUI.DisplayCarsLicensePlatesInGarageList(CurrentUser.Garage);
                            break;
                        case eUserChoice.ChangeStatus:
                            GarageUI.ChangeVehicleCondition(CurrentUser.m_Garage);
                            break;
                        case eUserChoice.AdjustTirePressure:
                            GarageUI.AdjustTiresPressureToMax(CurrentUser.m_Garage);
                            break;
                        case eUserChoice.RefuelVehicle:
                            GarageUI.RefuelVehicle(CurrentUser.m_Garage); 
                            break;
                        case eUserChoice.RechargeElectricEngine:
                            GarageUI.RechargeVehicle(CurrentUser.m_Garage);
                            break;
                        case eUserChoice.PrintVehicleDetails:
                            GarageUI.PrintVehicleDetails(CurrentUser.m_Garage);
                            break;
                    }
                }
                catch (Exception error)
                {
                    UserMessage.ClearAndDisplayException(error);
                }
            }
        }

        public UserServer()
        {
            m_Garage = new GarageManager(new Dictionary<string, Vehicle>());
        }

        public static int getUserMainMenuChoice()
        {
            int userChoice = -1;

            try
            {
                userChoice = ParseInputToInt();
                Validation.CheckUserTypeInput(userChoice, typeof(eUserChoice));
            }
            catch (Exception error)
            {
                UserMessage.ClearAndDisplayException(error);
            }

            return userChoice;
        }

        internal static int ParseInputToInt()
        {
            int userInputToInt;
            string userInput = Console.ReadLine();

            if (!int.TryParse(userInput, out userInputToInt))
            {
                throw new FormatException();
            }

            return userInputToInt;
        }
    }
}