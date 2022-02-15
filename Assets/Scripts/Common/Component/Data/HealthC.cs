using Game.Common;

namespace Game.Game
{
    public class HealthC : AmountFloatC
    {
        public float Health
        {
            get => Amount;
            set => Amount = value;
        }

        public bool IsAlive => Health > 0;

        public HealthC() { }
        public HealthC(in int health) : base(health) { }

        public void Destroy() => Health = 0;
    }
}