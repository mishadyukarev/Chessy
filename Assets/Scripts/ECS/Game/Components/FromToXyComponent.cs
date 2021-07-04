using static Assets.Scripts.Abstractions.ValuesConst;

namespace Assets.Scripts.ECS.Components
{
    internal struct FromToXyComponent
    {
        private int[] _fromXy;
        private int[] _toXy;

        internal int[] FromXy => _fromXy;
        internal int[] ToXy => _toXy;

        internal int[] FromXyCopy
        {
            get
            {
                var xy = new int[XY_FOR_ARRAY];

                xy[X] = _fromXy[X];
                xy[Y] = _fromXy[Y];

                return xy;
            }
            set
            {
                _fromXy[X] = value[X];
                _fromXy[Y] = value[Y];
            }
        }

        internal int[] ToXyCopy
        {
            get
            {
                var xy = new int[XY_FOR_ARRAY];

                xy[X] = _toXy[X];
                xy[Y] = _toXy[Y];

                return xy;
            }
            set
            {
                _toXy[X] = value[X];
                _toXy[Y] = value[Y];
            }
        }

        internal void StartFill()
        {
            _fromXy = new int[XY_FOR_ARRAY];
            _toXy = new int[XY_FOR_ARRAY];
        }
    }
}
