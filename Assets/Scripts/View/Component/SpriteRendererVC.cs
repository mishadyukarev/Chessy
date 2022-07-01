using UnityEngine;

namespace Chessy.View.Component
{
    public readonly struct SpriteRendererVC
    {
        public readonly SpriteRenderer SR;

        public GameObject GO => SR.gameObject;
        public Transform Transform => SR.transform;
        public Transform ParentTransform => Transform.parent;

        public Sprite Sprite
        {
            get => SR.sprite;
            set => SR.sprite = value;
        }
        public Color Color
        {
            get => SR.color;
            set => SR.color = value;
        }

        public SpriteRendererVC(in SpriteRenderer sr) => SR = sr;

        public void SetEnabled(in bool needActive)
        {
            if (SR.enabled != needActive)
                SR.enabled = needActive;
        }
        public void Enable()
        {
            if (SR.enabled == false)
                SR.enabled = true;
        }
        public void Disable()
        {
            if (SR.enabled == true)
                SR.enabled = false;
        }

        public void SetActiveGO(in bool needActive)
        {
            if (GO.activeSelf != needActive) GO.SetActive(needActive);
        }
    }
}