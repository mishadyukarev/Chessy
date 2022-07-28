using UnityEngine;

namespace Chessy.Model.Component
{
    public sealed class PositionCellC
    {
        internal readonly Vector3 Position;

        public Vector3 PositionP => Position;

        public float X => Position.x;
        public float Y => Position.y;

        internal PositionCellC(in Vector3 position)
        {
            Position = position;
        }
    }
}