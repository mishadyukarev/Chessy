using UnityEngine;

namespace Chessy.Model.Component
{
    public readonly struct StartPositionCellC
    {
        public readonly Vector3 Pos;

        internal StartPositionCellC(in Vector3 pos) => Pos = pos;
    }
}