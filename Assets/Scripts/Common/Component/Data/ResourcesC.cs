using Game.Common;

namespace Game.Game
{
    public class ResourcesC : AmountFloatC
    {
        public float Resources
        {
            get => Amount;
            set => Amount = value;
        }

        public ResourcesC() { }
        public ResourcesC(in float resources): base(resources) { }
    }
}