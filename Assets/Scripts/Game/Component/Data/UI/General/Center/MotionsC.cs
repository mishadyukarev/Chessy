namespace Chessy.Game
{
    public struct MotionsC
    {
        public static bool IsActivated { get; set; }
        public static int AmountMotions { get; set; }

        public MotionsC(int amountMotions)
        {
            AmountMotions = amountMotions;
        }
    }
}
