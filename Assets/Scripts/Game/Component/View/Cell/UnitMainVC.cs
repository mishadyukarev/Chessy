using Game.Common;
using System;
using UnityEngine;

namespace Game.Game
{
    public struct UnitMainVC : IUnitCellV
    {
        private SpriteRenderer _main_SR;

        public UnitMainVC(GameObject cell_GO)
        {
            _main_SR = cell_GO.transform.Find("MainUnit_SR").GetComponent<SpriteRenderer>();
        }


        public void SetEnabled(bool enabled) => _main_SR.enabled = enabled;

        public void SetSprite(UnitTypes unit, LevelTypes levUnit, bool isCornered)
        {
            switch (unit)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    _main_SR.sprite = SpritesResC.Sprite(SpriteTypes.King);
                    break;

                case UnitTypes.Pawn:
                    switch (levUnit)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: _main_SR.sprite = SpritesResC.Sprite(SpriteTypes.PawnWood); break;
                        case LevelTypes.Second: _main_SR.sprite = SpritesResC.Sprite(SpriteTypes.PawnIron); break;
                        default: throw new Exception();
                    }
                    break;
                case UnitTypes.Archer:
                    switch (levUnit)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First:
                            {
                                if(isCornered) _main_SR.sprite = SpritesResC.Sprite(SpriteTypes.RookBow);
                                else _main_SR.sprite = SpritesResC.Sprite(SpriteTypes.BishopBow);
                            }
                            break;
                        case LevelTypes.Second:
                            {
                                if(isCornered) _main_SR.sprite = SpritesResC.Sprite(SpriteTypes.RookCrossbow);
                                else _main_SR.sprite = SpritesResC.Sprite(SpriteTypes.BishopCrossbow);
                            }
                            break;
                        default: throw new Exception();
                    }
                    break;
                case UnitTypes.Scout:
                    switch (levUnit)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: _main_SR.sprite = SpritesResC.Sprite(SpriteTypes.Scout); break;
                        case LevelTypes.Second: throw new Exception();
                        default: throw new Exception();
                    }
                    break;
                case UnitTypes.Elfemale:
                    switch (levUnit)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: _main_SR.sprite = SpritesResC.Sprite(SpriteTypes.Elfemale); break;
                        case LevelTypes.Second: throw new Exception();
                        default: throw new Exception();
                    }
                    break;
                default:
                    throw new Exception();
            }
        }
        public void SetAlpha(bool isVisible)
        {
            if (isVisible) _main_SR.color = new Color(_main_SR.color.r, _main_SR.color.g, _main_SR.color.b, 1);
            else _main_SR.color = new Color(_main_SR.color.r, _main_SR.color.g, _main_SR.color.b, 0.8f);
        }

        public void SetFlipX(bool isActive) => _main_SR.flipX = isActive;
        public void SetLocRot(Vector3 rotation) => _main_SR.transform.localEulerAngles = rotation;
    }
}

