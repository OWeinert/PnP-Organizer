namespace PnP_Organizer.Controls.Events
{
    public class BonusValueChangedArgs
    {
        /// <summary>
        /// The bonus value
        /// </summary>
        public int BonusValue { get; set; }

        public BonusValueChangedArgs(int bonusValue)
        {
            BonusValue = bonusValue;
        }
    }
}
