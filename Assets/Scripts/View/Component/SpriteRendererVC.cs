using UnityEngine;

namespace Chessy.View.Component
{
    public readonly struct SpriteRendererVC
    {
        public readonly SpriteRenderer SR;

        public GameObject GO => SR.gameObject;
        public Transform Transform => SR.transform;
        public Transform ParentTransform => Transform.parent;

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

        internal void SetEnabled(in bool needActive)
        {
            if (SR.enabled != needActive)
                SR.enabled = needActive;
        }
        internal void Enable()
        {
            if (SR.enabled == false)
                SR.enabled = true;
        }
        internal void Disable()
        {
            if (SR.enabled == true)
                SR.enabled = false;
        }

        internal void SetActiveGO(in bool needActive)
        {
            if (GO.activeSelf != needActive) GO.SetActive(needActive);
        }

        internal void SetColor(in Color color)
        {
            /*if (color != Color)*/ Color = color;
        }
    }
}