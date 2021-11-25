using Game.Common;
using System;
using UnityEngine;

namespace Game.Game
{
    public struct UnitExtraVC : IUnitCellV
    {
        private SpriteRenderer _extraUnit_SR;

        public UnitExtraVC(GameObject cellUnit_GO)
        {
            _extraUnit_SR = cellUnit_GO.transform.Find("ExtraUnit_SR").GetComponent<SpriteRenderer>();
        }

        public void Enable_SR() => _extraUnit_SR.enabled = true;
        public void Disable_SR() => _extraUnit_SR.enabled = false;

        public void SetToolWeapon_Sprite(TWTypes tWType, LevelTypes levelTWType)
        {
            switch (tWType)
            {
                case TWTypes.None: throw new Exception();
                case TWTypes.Pick:
                    switch (levelTWType)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: throw new Exception();
                        case LevelTypes.Second: _extraUnit_SR.sprite = SpritesResC.Sprite(SpriteTypes.PickWood); return;
                        default: throw new Exception();
                    }
                case TWTypes.Sword:
                    switch (levelTWType)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: throw new Exception();
                        case LevelTypes.Second: _extraUnit_SR.sprite = SpritesResC.Sprite(SpriteTypes.SwordIron); return;
                        default: throw new Exception();
                    }
                case TWTypes.Shield:
                    switch (levelTWType)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: _extraUnit_SR.sprite = SpritesResC.Sprite(SpriteTypes.ShieldWood); return;
                        case LevelTypes.Second: _extraUnit_SR.sprite = SpritesResC.Sprite(SpriteTypes.ShieldIron); return;
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }
        }

        public void SetAlpha(bool isVisible)
        {
            if (isVisible) _extraUnit_SR.color = new Color(_extraUnit_SR.color.r, _extraUnit_SR.color.g, _extraUnit_SR.color.b, 1);
            else _extraUnit_SR.color = new Color(_extraUnit_SR.color.r, _extraUnit_SR.color.g, _extraUnit_SR.color.b, 0.8f);
        }
        public void SetFlipX(bool isFliped) => _extraUnit_SR.flipX = isFliped;
    }
}
