using UnityEngine;

namespace Chessy.View.Component
{
    public readonly struct SpriteRendererVC
    {
        public readonly SpriteRenderer SR;

        public GameObject GO => SR.gameObject;
        public Transform Transform => SR.transform;

        internal Sprite Sprite
        {
            get => SR.sprite;
            set => SR.sprite = value;
        }
        internal Color Color
        {
            get => SR.color;
            set => SR.color = value;
        }

        internal SpriteRendererVC(in SpriteRenderer sr) => SR = sr;

        internal void TrySetEnabled(in bool needActive)
        {
            if (SR.enabled != needActive)
                SR.enabled = needActive;
        }
        internal void TrySetActiveGO(in bool needActive)
        {
            if (GO.activeSelf != needActive) GO.SetActive(needActive);
        }
    }
}