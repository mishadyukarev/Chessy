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

        internal FromToXyComponent(int[] fromXy, int[] toXy)
        {
            _fromXy = fromXy;
            _toXy = toXy;
        }

        internal void SetAllXy(int[] fromXy, int[] toXy)
        {
            FromXy = fromXy;
            ToXy = toXy;
        }
    }
}
