namespace Game.Game
{
    public struct AmountC
    {
        public int Amount;

        public bool Have => Amount > 0;
        public bool IsMinus => Amount < 0;

        public AmountC(in int amount) => Amount = amount;

        public void Add(in int adding = 1)
        {
            Amount += adding;
        }
        public void Take(in int taking = 1)
        {
            Amount -= taking;
        }
        public void Reset() => Amount = 0;
    }
}