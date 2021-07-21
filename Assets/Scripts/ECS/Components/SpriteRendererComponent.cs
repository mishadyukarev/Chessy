using UnityEngine;

namespace Assets.Scripts.ECS.Components
{
    internal struct SpriteRendererComponent
    {
        private SpriteRenderer _sR;

        internal bool Enabled
        {
            get => _sR.enabled;
            set => _sR.enabled = value;
        }
        internal Sprite Sprite { set => _sR.sprite = value; }
        internal Color Color { set => _sR.color = value; }
        internal Vector3 LocalScale { set => _sR.transform.localScale = value; }


        internal void StartFill(SpriteRenderer sR) => _sR = sR;
    }
}
