using UnityEngine;

namespace Game.Game
{
    public readonly struct TransformVC
    {
        readonly Transform _t;

        public Vector3 EulerAngles
        {
            get => _t.eulerAngles;
            set => _t.eulerAngles = value;
        }
        public Vector3 LocalEulerAngles
        {
            get => _t.localEulerAngles;
            set => _t.localEulerAngles = value;
        }
        public Vector3 LocalScale
        {
            get => _t.localScale;
            set => _t.localScale = value;
        }

        public TransformVC(in Transform t) => _t = t;
    }
}