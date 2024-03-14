using System;
using System.Collections.Generic;
using Ex03.Types;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        public eMotorcycleLicenseType LicenseType { get; set; }
        public int EngineVolume { get; set; }
        private const int k_NumberOfWheels = 2;
        private const int k_MaximumWheelPressure = 29;

        private readonly List<string> r_UserUpdatingDetailsQuestion = new List<string> { "What type of license do you posses? (A1/A2/AB/B2)", "What is your engine volume?" };

        public Motorcycle()
        {
            UserUpdatingAdditionalParamsQuestions = r_UserUpdatingDetailsQuestion;
            TireSet = new List<Tire>(k_NumberOfWheels);
            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                TireSet.Add(new Tire(k_MaximumWheelPressure));
            }
        }

        public override void UpdateDetails(List<string> i_UserInputs)
        {
            int intLicenseTypeForValidation;

            try
            {
                if (!int.TryParse(i_UserInputs[0], out intLicenseTypeForValidation))
                {
                    this.LicenseType = (eMotorcycleLicenseType)Enum.Parse(typeof(eMotorcycleLicenseType), i_UserInputs[0]);
                }
                else
                {
                    throw new FormatException("License type cannot be a number.");
                }

                this.EngineVolume = int.Parse(i_UserInputs[1]);
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public override string GetAdditionalDetails()
        {
            return string.Format(@"License type: {0}.
Engine volume: {1}", LicenseType, EngineVolume);
        }
    }
}
