using UnityEngine;

namespace Chessy.Model.Component
{
    public readonly struct StartPositionCellC
    {
        public readonly Vector3 Possition;

        internal StartPositionCellC(in Vector3 pos) => Possition = pos;
    }
}