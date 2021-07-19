using UnityEngine;

namespace Assets.Scripts.ECS.Components
{
    internal struct SpriteRendererComponent
    {
        private SpriteRenderer _sR;

        internal void StartFill(SpriteRenderer sR) => _sR = sR;

        internal void ActivateSR(bool isActive) => _sR.enabled = isActive;
        internal void SetColorSR(Color color) => _sR.color = color;
        internal void SetSprite(Sprite sprite) => _sR.sprite = sprite;
    }
}
