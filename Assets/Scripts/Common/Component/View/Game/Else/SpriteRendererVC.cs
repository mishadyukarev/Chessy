using UnityEngine;

namespace Game.Game
{
    public readonly struct SpriteRendererVC : ITrailCellV, ICloudCellV, ISupportVE, IBlockCellVE, IBarCellVE
    {
        public readonly SpriteRenderer SR;

        public Quaternion RotParent
        {
            get => SR.transform.parent.rotation;
            set => SR.transform.parent.rotation = value;
        }
        public Vector3 LocalEulerAngles
        {
            get => SR.transform.localEulerAngles;
            set => SR.transform.localEulerAngles = value;
        }
        public Vector3 LocalScale
        {
            get => SR.transform.localScale;
            set => SR.transform.localScale = value;
        }

        public Sprite Sprite
        {
            get => SR.sprite;
            set => SR.sprite = value;
        }
        public bool FlipX
        {
            get => SR.flipX;
            set => SR.flipX = value;
        }
        public Color Color
        {
            get => SR.color;
            set => SR.color = value;
        }
        public bool Enabled
        {
            get => SR.enabled;
            set => SR.enabled = value;
        }

        public SpriteRendererVC(in SpriteRenderer sr) => SR = sr;

        public void SetActive(in bool needActive) => SR.enabled = needActive;
        public void Enable() => SR.enabled = true;
        public void Disable() => SR.enabled = false;
    }
}