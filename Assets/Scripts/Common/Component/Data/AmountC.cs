namespace Game.Game
{
    public class AmountC
    {
        public int Amount;

        public bool HaveAny => Amount > 0;
        public bool IsMinus => Amount < 0;

        public AmountC() { }
        public AmountC(in int amount) { Amount = amount; }


        public void Take(in int taking) => Amount -= taking;
        public void Add(in int adding) => Amount += adding;
    }
}