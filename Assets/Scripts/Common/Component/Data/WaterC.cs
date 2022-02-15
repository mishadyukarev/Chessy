using Game.Common;

namespace Game.Game
{
    public class WaterC : AmountFloatC
    {
        public float Water
        {
            get => Amount;
            set => Amount = value;
        }
    }
}