using Game.Common;

namespace Game.Game
{
    public class ProtectionC : AmountFloatC
    {
        public float Protection
        {
            get => Amount;
            set => Amount = value;
        }

        public bool HaveProtection => Protection > 0;
    }
}