using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Truck : Vehicle
    {
        public bool IsTransportingHazardousMaterials { get; set; }
        public float TrunkVolume { get; set; }
        private const int k_NumberOfWheels = 12;
        private const int k_MaximumWheelPressure = 28;
        private readonly List<string> r_QuestionForUserToUpdateDetails = new List<string>
        {
            "Is your truck transporting hazardous materials? (Y/N)",
            "What is your trunk volume?"
        };

        public Truck()
        {
            UserUpdatingAdditionalParamsQuestions = r_QuestionForUserToUpdateDetails;
            TireSet = new List<Tire>(k_NumberOfWheels);
            for (int i = 0; i <k_NumberOfWheels; i++)
            {
                TireSet.Add(new Tire(k_MaximumWheelPressure));
            }
        }

        public override void UpdateDetails(List<string> i_UserInputs)
        {
            if (i_UserInputs[0].Equals("Y"))
            {
                this.IsTransportingHazardousMaterials = true;
            }
            else if (i_UserInputs[0].Equals("N"))
            {
                this.IsTransportingHazardousMaterials = false;
            }
            else
            {
                throw new FormatException("The valid inputs for 'Is your truck transporting hazardous materials' is only (Y/N)");
            }

            this.TrunkVolume = float.Parse(i_UserInputs[1]);
        }

        public override string GetAdditionalDetails()
        {
            return string.Format(@"Is transporting hazardous materials: {0}.
Trunk volume: {1}", IsTransportingHazardousMaterials, TrunkVolume);
        }
    }
}
