using System;
using UnityEngine;

namespace Game.Game
{
    public struct UnitExtraVC : IUnitCellV
    {
        private SpriteRenderer _extraUnit;

        public UnitExtraVC(GameObject cellUnit)
        {
            _extraUnit = cellUnit.transform.Find("ExtraUnit_SR").GetComponent<SpriteRenderer>();
        }

        public void Enable_SR() => _extraUnit.enabled = true;
        public void Disable_SR() => _extraUnit.enabled = false;

        public void SetToolWeapon_Sprite(TWTypes tWType, LevelTypes levelTW)
        {
            switch (tWType)
            {
                case TWTypes.None: throw new Exception();
                case TWTypes.Pick:
                    switch (levelTW)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: throw new Exception();
                        case LevelTypes.Second: _extraUnit.sprite = SpritesResC.Sprite(SpriteTypes.PickWood); return;
                        default: throw new Exception();
                    }
                case TWTypes.Sword:
                    switch (levelTW)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: throw new Exception();
                        case LevelTypes.Second: _extraUnit.sprite = SpritesResC.Sprite(SpriteTypes.SwordIron); return;
                        default: throw new Exception();
                    }
                case TWTypes.Shield:
                    switch (levelTW)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: _extraUnit.sprite = SpritesResC.Sprite(SpriteTypes.ShieldWood); return;
                        case LevelTypes.Second: _extraUnit.sprite = SpritesResC.Sprite(SpriteTypes.ShieldIron); return;
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }
        }

        public void SetAlpha(bool isVisible)
        {
            if (isVisible) _extraUnit.color = new Color(_extraUnit.color.r, _extraUnit.color.g, _extraUnit.color.b, 1);
            else _extraUnit.color = new Color(_extraUnit.color.r, _extraUnit.color.g, _extraUnit.color.b, 0.8f);
        }
        public void SetFlipX(bool isFliped) => _extraUnit.flipX = isFliped;
    }
}
