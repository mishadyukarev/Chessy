using UnityEngine;

namespace Assets.Scripts.ECS.Components
{
    internal struct SpriteRendererComponent
    {
        internal SpriteRenderer SpriteRenderer { get; set; }

        internal SpriteRendererComponent(SpriteRenderer spriteRenderer) => SpriteRenderer = spriteRenderer;
    }
}
