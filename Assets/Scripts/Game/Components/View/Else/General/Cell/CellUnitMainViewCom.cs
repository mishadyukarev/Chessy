using Scripts.Common;
using System;
using UnityEngine;

namespace Scripts.Game
{
    public struct CellUnitMainViewCom
    {
        private SpriteRenderer _main_SR;

        public CellUnitMainViewCom(GameObject cell_GO)
        {
            _main_SR = cell_GO.transform.Find("MainUnit_SR").GetComponent<SpriteRenderer>();
        }


        public void Enable_SR() => _main_SR.enabled = true;
        public void Disable_SR() => _main_SR.enabled = false;

        public void SetSprite(UnitTypes unitType, LevelUnitTypes upgradeUnitType)
        {   
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    _main_SR.sprite = SpritesResComCom.Sprite(SpriteGameTypes.King);
                    break;

                case UnitTypes.Pawn:
                    switch (upgradeUnitType)
                    {
                        case LevelUnitTypes.None: throw new Exception();
                        case LevelUnitTypes.Wood: _main_SR.sprite = SpritesResComCom.Sprite(SpriteGameTypes.PawnWood); break;
                        case LevelUnitTypes.Iron: _main_SR.sprite = SpritesResComCom.Sprite(SpriteGameTypes.PawnIron); break;
                        default: throw new Exception();
                    }                  
                    break;
                case UnitTypes.Rook:
                    switch (upgradeUnitType)
                    {
                        case LevelUnitTypes.None: throw new Exception();
                        case LevelUnitTypes.Wood: _main_SR.sprite = SpritesResComCom.Sprite(SpriteGameTypes.RookBow); break;
                        case LevelUnitTypes.Iron: _main_SR.sprite = SpritesResComCom.Sprite(SpriteGameTypes.RookCrossbow); break;
                        default: throw new Exception();
                    }      
                    break;
                case UnitTypes.Bishop:
                    switch (upgradeUnitType)
                    {
                        case LevelUnitTypes.None: throw new Exception();
                        case LevelUnitTypes.Wood: _main_SR.sprite = SpritesResComCom.Sprite(SpriteGameTypes.BishopBow); break;
                        case LevelUnitTypes.Iron: _main_SR.sprite = SpritesResComCom.Sprite(SpriteGameTypes.BishopCrossbow); break;
                        default: throw new Exception();
                    }
                    break;
                case UnitTypes.Scout:
                    switch (upgradeUnitType)
                    {
                        case LevelUnitTypes.None: throw new Exception();
                        case LevelUnitTypes.Wood: _main_SR.sprite = SpritesResComCom.Sprite(SpriteGameTypes.Scout); break;
                        case LevelUnitTypes.Iron: throw new Exception();
                        default: throw new Exception();
                    }
                    break;
                default:
                    throw new Exception();
            }
        }
        public void SetAlpha(bool isVisible)
        {
            if(isVisible) _main_SR.color = new Color(_main_SR.color.r, _main_SR.color.g, _main_SR.color.b, 1);
            else _main_SR.color = new Color(_main_SR.color.r, _main_SR.color.g, _main_SR.color.b, 0.8f);
        }

        public void SetFlipX(bool isActive) => _main_SR.flipX = isActive;
        public void Set_LocRotEuler(Vector3 rotation) => _main_SR.transform.localEulerAngles = rotation;
    }
}

