using UnityEngine;

namespace Chessy.Model.Component
{
    public sealed class PositionC
    {
        internal Vector3 Position;

        public Vector3 PositionP => Position;

        public float X
        {
            get => Position.x;
            internal set => Position.x = value;
        }
        public float Y
        {
            get => Position.y;
            internal set => Position.y = value;
        }

        internal void Dispose()
        {
            Position = default;
        }
        internal void Clone(in PositionC positionC)
        {
            Position = positionC.Position;
        }
    }
}