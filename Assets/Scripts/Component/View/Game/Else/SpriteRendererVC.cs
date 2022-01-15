using UnityEngine;

namespace Game.Game
{
    public struct SpriteRendererVC : ICellVE, IUnitCellV, IFireCellVE, IEnvCellV, ITrailCellV, ICloudCellV
    {
        readonly SpriteRenderer _sr;

        public Quaternion RotParent
        {
            get => _sr.transform.parent.rotation;
            set => _sr.transform.parent.rotation = value;
        }
        public Sprite Sprite
        {
            get => _sr.sprite;
            set => _sr.sprite = value;
        }
        public Vector3 LocalEulerAngles
        {
            get => _sr.transform.localEulerAngles;
            set => _sr.transform.localEulerAngles = value;
        }
        public bool FlipX
        {
            get => _sr.flipX;
            set => _sr.flipX = value;
        }

        internal SpriteRendererVC(in SpriteRenderer sr) => _sr = sr;

        public void SetActive(in bool needActive) => _sr.enabled = needActive;
        public void Enable() => _sr.enabled = true;
        public void Disable() => _sr.enabled = false;
    }
}