using static Assets.Scripts.Abstractions.ValuesConst;

namespace Assets.Scripts.ECS.Components
{
    internal struct FromToXyComponent
    {
        private int[] _fromXy;
        private int[] _toXy;

        internal int[] FromXy
        {
            get => (int[])_fromXy.Clone();
            set => _fromXy = (int[])value.Clone();
        }

        internal int[] ToXy
        {
            get => (int[])_toXy.Clone();
            set => _toXy = (int[])value.Clone();
        }

        internal void StartFill()
        {
            _fromXy = new int[XY_FOR_ARRAY];
            _toXy = new int[XY_FOR_ARRAY];
        }
    }
}
