using Assets.Scripts.Abstractions.Enums.Cell;
using Assets.Scripts.Abstractions.Enums.Cell.Pawn;
using System;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.View.Else.Game.General.Cell
{
    internal struct CellUnitExtraViewComp
    {
        private SpriteRenderer _extraUnit_SR;

        internal CellUnitExtraViewComp(GameObject cellUnit_GO)
        {
            _extraUnit_SR = cellUnit_GO.transform.Find("ExtraUnit_SR").GetComponent<SpriteRenderer>();
        }

        internal void Enable_SR() => _extraUnit_SR.enabled = true;
        internal void Disable_SR() => _extraUnit_SR.enabled = false;

        internal void SetExtraPawnTool_Sprite(PawnExtraToolTypes pawnExtraToolType)
        {
            switch (pawnExtraToolType)
            {
                case PawnExtraToolTypes.None:
                    throw new Exception();

                case PawnExtraToolTypes.Hoe:
                    _extraUnit_SR.sprite = ResourcesComponent.SpritesConfig.HoePawnExtra_Sprite;
                    break;

                case PawnExtraToolTypes.Pick:
                    _extraUnit_SR.sprite = ResourcesComponent.SpritesConfig.PickPawnExtra_Sprite;
                    break;

                default:
                    throw new Exception();
            }
        }
        internal void SetPawnWeapon_Spriter(PawnExtraWeaponTypes pawnExtraWeaponType)
        {
            switch (pawnExtraWeaponType)
            {
                case PawnExtraWeaponTypes.None:
                    throw new Exception();

                case PawnExtraWeaponTypes.Sword:
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}
