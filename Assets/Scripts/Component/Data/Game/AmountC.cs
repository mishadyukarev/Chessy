namespace Game.Game
{
    public struct AmountC
    {
        public int Amount;

        public bool Have => Amount > 0;
        public bool IsMinus => Amount < 0;

        public AmountC(in int amount) => Amount = amount;
    }
}