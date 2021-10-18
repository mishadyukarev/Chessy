using Scripts.Common;
using System;
using UnityEngine;

namespace Scripts.Game
{
    internal struct CellUnitMainViewCom
    {
        private SpriteRenderer _main_SR;

        internal CellUnitMainViewCom(GameObject cell_GO)
        {
            _main_SR = cell_GO.transform.Find("MainUnit_SR").GetComponent<SpriteRenderer>();
        }


        internal void Enable_SR() => _main_SR.enabled = true;
        internal void Disable_SR() => _main_SR.enabled = false;

        internal void SetSprite(UnitTypes unitType, UpgradeUnitTypes upgradeUnitType)
        {   
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    _main_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.King);
                    break;

                case UnitTypes.Pawn:
                    switch (upgradeUnitType)
                    {
                        case UpgradeUnitTypes.None: throw new Exception();
                        case UpgradeUnitTypes.First: _main_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.PawnWood); break;
                        case UpgradeUnitTypes.Second: _main_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.PawnIron); break;
                        default: throw new Exception();
                    }                  
                    break;
                case UnitTypes.Rook:
                    switch (upgradeUnitType)
                    {
                        case UpgradeUnitTypes.None: throw new Exception();
                        case UpgradeUnitTypes.First: _main_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.RookBow); break;
                        case UpgradeUnitTypes.Second: _main_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.RookCrossbow); break;
                        default: throw new Exception();
                    }      
                    break;
                case UnitTypes.Bishop:
                    switch (upgradeUnitType)
                    {
                        case UpgradeUnitTypes.None: throw new Exception();
                        case UpgradeUnitTypes.First: _main_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.BishopBow); break;
                        case UpgradeUnitTypes.Second: _main_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.BishopCrossbow); break;
                        default: throw new Exception();
                    }
                    break;
                default:
                    throw new Exception();
            }
        }
        internal void SetAlpha(bool isVisible)
        {
            if(isVisible) _main_SR.color = new Color(_main_SR.color.r, _main_SR.color.g, _main_SR.color.b, 1);
            else _main_SR.color = new Color(_main_SR.color.r, _main_SR.color.g, _main_SR.color.b, 0.8f);
        }

        internal void SetFlipX(bool isActive) => _main_SR.flipX = isActive;
        internal void Set_LocRotEuler(Vector3 rotation) => _main_SR.transform.localEulerAngles = rotation;
    }
}

