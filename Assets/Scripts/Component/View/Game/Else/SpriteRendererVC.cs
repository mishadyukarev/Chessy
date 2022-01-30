using UnityEngine;

namespace Game.Game
{
    public readonly struct SpriteRendererVC : ICellVE, IUnitCellV, IFireCellVE, IEnvCellV, ITrailCellV, ICloudCellV, ISupportVE, IBlockCellVE, IBarCellVE, IStunCellVE
    {
        readonly SpriteRenderer _sr;

        public Quaternion RotParent
        {
            get => _sr.transform.parent.rotation;
            set => _sr.transform.parent.rotation = value;
        }
        public Vector3 LocalEulerAngles
        {
            get => _sr.transform.localEulerAngles;
            set => _sr.transform.localEulerAngles = value;
        }
        public Vector3 LocalScale
        {
            get => _sr.transform.localScale;
            set => _sr.transform.localScale = value;
        }

        public Sprite Sprite
        {
            get => _sr.sprite;
            set => _sr.sprite = value;
        }
        public bool FlipX
        {
            get => _sr.flipX;
            set => _sr.flipX = value;
        }
        public Color Color
        {
            get => _sr.color;
            set => _sr.color = value;
        }
        public bool Enabled
        {
            get => _sr.enabled;
            set => _sr.enabled = value;
        }

        public SpriteRendererVC(in SpriteRenderer sr) => _sr = sr;

        public void SetActive(in bool needActive) => _sr.enabled = needActive;
        public void Enable() => _sr.enabled = true;
        public void Disable() => _sr.enabled = false;
    }
}