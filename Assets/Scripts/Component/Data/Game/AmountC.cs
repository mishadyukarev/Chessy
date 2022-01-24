namespace Game.Game
{
    public struct AmountC : IEnvCell, IUnitUniqueCellE
    {
        public int Amount;

        public bool Have => Amount > 0;
        public bool IsMinus => Amount < 0;

        public AmountC(in int amount) => Amount = amount;

        public static AmountC operator +(AmountC amountC, in int amount)
        {
            amountC.Amount += amount;
            return amountC;
        }
        public static AmountC operator -(AmountC amountC, in int amount)
        {
            if (amountC.Amount > 0)
            {
                amountC.Amount -= amount;
                if (amountC.Amount < 0) amountC.Amount = 0;

                return amountC;
            }
            return amountC;
        }
        public static AmountC operator ++(AmountC amountC)
        {
            amountC.Amount += 1;
            return amountC;
        }

        public void Take(in int taking = 1)
        {
            if(Amount > 0)
            {
                Amount -= taking;
                if (Amount < 0) Amount = 0;
            }
        }
        public void Reset() => Amount = 0;
    }
}