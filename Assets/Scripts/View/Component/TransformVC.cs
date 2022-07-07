using UnityEngine;
namespace Chessy.Model
{
    public readonly struct TransformVC
    {
        public readonly Transform Transform;

        public Vector3 EulerAngles
        {
            get => Transform.eulerAngles;
            set => Transform.eulerAngles = value;
        }
        public Vector3 LocalEulerAngles
        {
            get => Transform.localEulerAngles;
            set => Transform.localEulerAngles = value;
        }
        public Vector3 LocalScale
        {
            get => Transform.localScale;
            set => Transform.localScale = value;
        }

        public TransformVC(in Transform t) => Transform = t;
    }
}