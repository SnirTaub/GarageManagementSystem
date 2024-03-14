using Ex03.Types;
using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        public eColor Color { get; set; }
        public eNumberOfDoors NumberOfDoors { get; set; }
        private const int k_NumberOfWheels = 5;
        private const int k_MaximumWheelPressure = 30;
        private readonly List<string> r_QuestionForUserToUpdateDetails = new List<string>()
        {
            "What is the color of your car? (Red, White, Blue or Yellow)",
            "How many doors does your car have? (2, 3, 4, 5)"
        };

        public Car()
        {
            UserUpdatingAdditionalParamsQuestions = r_QuestionForUserToUpdateDetails;
            TireSet = new List<Tire>(k_NumberOfWheels);
            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                TireSet.Add(new Tire(k_MaximumWheelPressure));
            }
        }

        public override void UpdateDetails(List<string> i_UserInputs)
        {
            int numberOfDoors;

            try
            {
                this.Color = (eColor)Enum.Parse(typeof(eColor), i_UserInputs[0]);
                numberOfDoors = int.Parse(i_UserInputs[1]);
                if (Enum.IsDefined(typeof(eNumberOfDoors), numberOfDoors))
                {
                    this.NumberOfDoors = (eNumberOfDoors)numberOfDoors;
                }
                else
                {
                    throw new FormatException("Invalid number of doors.");
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public override string GetAdditionalDetails()
        {
            return string.Format(@"Vehicle color: {0}.
Number of doors: {1}", Color.ToString(), (int)NumberOfDoors);
        }
    }
}
