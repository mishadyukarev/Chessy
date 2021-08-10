namespace Assets.Scripts.ECS.Components
{
    internal struct XyFromToComponent
    {
        private byte[] _fromXy;
        private byte[] _toXy;

        internal byte[] FromXy
        {
            get => (byte[])_fromXy.Clone();
            set => _fromXy = (byte[])value.Clone();
        }

        internal byte[] ToXy
        {
            get => (byte[])_toXy.Clone();
            set => _toXy = (byte[])value.Clone();
        }

        internal XyFromToComponent(byte[] fromXy, byte[] toXy)
        {
            _fromXy = fromXy;
            _toXy = toXy;
        }

        internal void SetAllXy(byte[] fromXy, byte[] toXy)
        {
            FromXy = fromXy;
            ToXy = toXy;
        }
    }
}
