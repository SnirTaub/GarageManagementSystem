using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Ex03.ConsoleUI
{
    class UserMessage
    {
        internal const int k_TimeToWait = 2000;
        internal const string k_Welcome = "Hi! Welcome To Snir's Garage";
        internal const string k_EmptyInput = "You can not enter an empty input";
        internal const string k_MainMenu = @"Choose the number of your desired option and then press 'enter':
1. Add a new vehicle to the garage.
2. Display all license plates numbers.
3. Change vehicle status.
4. Adjust tire pressure to maximum.
5. Refuel a vehicle.
6. Recharge an electric vehicle.
7. Display a vehicle's full details.
8. Exit.";

        internal const string k_VehicleConditionMenu = @"Choose the number of your desired option and then press 'enter':
1. In repair.
2. Repaired.
3. Payed.";

        internal const string k_FuelTypeMenu = @"Choose the number of your desired option and then press 'enter':
1. Octan98.
2. Octan96.
3. Octan95.
4. Soler.";

        internal const string k_VehicleTypeMenu = @"Choose the number of your desired option and the press 'enter':
1. Fueled Car.
2. Electric Car.
3. Fueled Motorcycle.
4. Electric Motorcycle.
5. Fueled Truck.";

        internal const string k_VehicleIsAlreadyInGarage = "The vehicle you mentiond is already in the garage.";

        internal static void ClearAndDisplay(string i_Message, int i_TimeToWait = 0)
        {
            Console.Clear();
            Console.WriteLine(i_Message);
            Wait(i_TimeToWait);
        }

        internal static void Wait(int i_Time)
        {
            Thread.Sleep(i_Time);
        }

        internal static void DisplayList<T>(List<T> i_List)
        {
            Console.Clear();
            StringBuilder listInString = new StringBuilder();
            listInString.AppendLine("starting...");
            foreach (T node in i_List)
            {
                listInString.AppendLine(" " + node.ToString());
            }

            listInString.AppendLine("ending...");
            Console.WriteLine(listInString.ToString());
            Wait(4000);
        }

        internal static void ConditionChangeSucceed(string i_LicenseIdToChangeStatus, string i_Condition)
        {
            Console.Clear();
            string userMessage = string.Format(@"You have successfully change the vehicle license: {0} to '{1}' condition.", i_LicenseIdToChangeStatus, i_Condition);
            Console.WriteLine(userMessage);
            Wait(2000);
        }

        internal static void RequestRemainingEnergy(bool i_IsElectric, float i_EnergyCapacity)
        {
            string userMessage;

            if (i_IsElectric)
            {
                userMessage = string.Format(@"Please enter your vehicle remaining battery time: (max:{0})", i_EnergyCapacity);
            }
            else
            {
                userMessage = string.Format(@"Please enter your vehicle remaining fuel liter status: (max:{0})", i_EnergyCapacity);
            }

            ClearAndDisplay(userMessage);
        }

        internal static void ClearAndDisplayException(Exception error)
        {
            Console.Clear();
            Console.WriteLine(error.Message);
            UserMessage.Wait(2000);
        }
    }
}
