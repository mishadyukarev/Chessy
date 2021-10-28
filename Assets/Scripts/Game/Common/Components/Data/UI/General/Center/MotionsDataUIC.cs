namespace Scripts.Game
{
    public struct MotionsDataUIC
    {
        public static int AmountMotions { get; set; }
        public static bool IsActivatedUI { get; set; }

        public MotionsDataUIC(int amountMotions)
        {
            AmountMotions = amountMotions;
            IsActivatedUI = false;
        }
    }
}
