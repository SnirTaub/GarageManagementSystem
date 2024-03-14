using System.Collections.Generic;
using Ex03.Types;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private Owner m_Owner = new Owner();
        public string ModelName { get; set; }
        public string LicensePlate { get; set; }
        public Engine Engine { get; set; }
        public eVehicleCondition VehicleCondition { get; set; }

        public List<Tire> TireSet { get; set; }
        public List<string> UserUpdatingAdditionalParamsQuestions { get; set; }
        public abstract void UpdateDetails(List<string> i_UserInputs);
        public abstract string GetAdditionalDetails();

        public Owner Owner
        {
            get { return m_Owner; }
        }

        public float EnergyLeftInPercentage
        {
            get { return (Engine.EnergyLeft / Engine.EnergyCapacity) * 100; }
        }
    }
}
