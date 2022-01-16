using UnityEngine;

namespace Game.Game
{
    public struct TransformVC
    {
        Transform _t;

        public Vector3 EulerAngles
        {
            get => _t.eulerAngles;
            set => _t.eulerAngles = value;
        }

        public TransformVC(in Transform t) => _t = t;
    }
}