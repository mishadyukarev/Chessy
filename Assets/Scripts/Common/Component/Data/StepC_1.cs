using Game.Common;

namespace Game.Game
{
    public class StepsC : AmountFloatC
    {
        public float Steps
        {
            get => Amount;
            set => Amount = value;
        }

        public bool HaveSteps => HaveAny;
    }
}