namespace Game.Common
{
    public abstract class AmountFloatC
    {
        protected float Amount;

        public bool HaveAny => Amount > 0;
        public bool IsMinus => Amount < 0;
        public bool Have(in float amount) => Amount >= amount;

        public AmountFloatC() { }
        public AmountFloatC(in float amount) { Amount = amount; }


        public virtual void Set(in float amount) => Amount = amount;
        public virtual void Take(in float taking) => Amount -= taking;
        public virtual void Add(in float adding) => Amount += adding;

        public virtual void Reset() => Amount = 0;
    }
}