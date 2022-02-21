using UnityEngine;

namespace Game.Game
{
    public struct ParentTransformVC : ITrailCellV
    {
        public Transform Transform;

        public ParentTransformVC(in Transform t) => Transform = t;

    }
}