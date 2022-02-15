using Game.Common;

namespace Game.Game
{
    public class TimerC : AmountFloatC
    {
        public float Timer
        {
            get => Amount;
            set => Amount = value;
        }

    }
}