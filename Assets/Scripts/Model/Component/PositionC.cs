using UnityEngine;

namespace Chessy.Model.Component
{
    public struct PositionC
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
    }
}