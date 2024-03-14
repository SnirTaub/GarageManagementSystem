namespace Ex03.GarageLogic
{
    public class Tire
    {
        public string Manufacturer { get; set; }
        public float CurrentTirePressure { get; set; }
        public float MaxTirePressure { get; set; }
        public Tire(int i_MaxTirePressure)
        {
            MaxTirePressure = i_MaxTirePressure;
        }

        public void AdjustPressure(float i_AmountOfAirToAdd)
        {
            if (CurrentTirePressure + i_AmountOfAirToAdd > MaxTirePressure)
            {
                throw new ValueOutOfRangeException(0, MaxTirePressure - CurrentTirePressure);
            }
            else
            {
                CurrentTirePressure += i_AmountOfAirToAdd;
            }
        }
    }
}
