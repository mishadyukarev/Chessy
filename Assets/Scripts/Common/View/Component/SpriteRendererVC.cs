using UnityEngine;

namespace Chessy.Game
{
    public struct SpriteRendererVC
    {
        public SpriteRenderer SR;

        public GameObject GameObject => SR.gameObject;
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

        public void SetActive(in bool needActive)
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
    }
}