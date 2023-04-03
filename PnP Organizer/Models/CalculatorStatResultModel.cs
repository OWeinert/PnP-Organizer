namespace PnP_Organizer.Models
{
    public class CalculatorStatResultModel
    {
        public string StatName { get; set; }
        public int StatValue { get; set; }
        public int StatDifference { get; set; }

        public CalculatorStatResultModel(string statName, int statValue, int statDifference) 
        { 
            StatName = statName;
            StatValue = statValue;
            StatDifference = statDifference;
        }
    }
}
