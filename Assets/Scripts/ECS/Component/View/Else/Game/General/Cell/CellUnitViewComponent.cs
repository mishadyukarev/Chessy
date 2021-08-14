using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.Cell;
using System;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.View.Else.Game.General.Cell
{
    internal struct CellUnitViewComponent
    {
        private SpriteRenderer _mainUnit_SR;
        private SpriteRenderer _extraUnit_SR;

        internal CellUnitViewComponent(GameObject cell_GO)
        {
            _mainUnit_SR = cell_GO.transform.Find("MainUnit_SR").GetComponent<SpriteRenderer>();
            _extraUnit_SR = _mainUnit_SR.transform.Find("ExtraTool_SR").GetComponent<SpriteRenderer>();
        }


        internal void EnableUnitTool_SR() => _mainUnit_SR.enabled = true;
        internal void DisableMainTool_SR() => _mainUnit_SR.enabled = false;

        internal void EnableExtraTool_SR() => _extraUnit_SR.enabled = true;
        internal void DisableExtraTool_SR() => _extraUnit_SR.enabled = false;



        internal void SetMainUnit_Sprite(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    _mainUnit_SR.sprite = ResourcesComponent.SpritesConfig.KingSprite;
                    break;

                case UnitTypes.Pawn_Axe:
                    _mainUnit_SR.sprite = ResourcesComponent.SpritesConfig.PawnMainAxe_Sprite;
                    break;

                case UnitTypes.Rook_Bow:
                    _mainUnit_SR.sprite = ResourcesComponent.SpritesConfig.RookSprite;
                    break;

                case UnitTypes.Rook_Crossbow:
                    _mainUnit_SR.sprite = ResourcesComponent.SpritesConfig.RookCrossbowSprite;
                    break;

                case UnitTypes.Bishop_Bow:
                    _mainUnit_SR.sprite = ResourcesComponent.SpritesConfig.BishopSprite;
                    break;

                case UnitTypes.Bishop_Crossbow:
                    _mainUnit_SR.sprite = ResourcesComponent.SpritesConfig.BishopCrossbowSprite;
                    break;

                default:
                    throw new Exception();
            }       
        }

        internal void SetExtraTool_Sprite(PawnExtraToolTypes pawnExtraToolType)
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

                case PawnExtraToolTypes.Sword:
                    _extraUnit_SR.sprite = ResourcesComponent.SpritesConfig.SwordPawnExtra_Sprite;
                    break;

                default:
                    throw new Exception();
            }
        }


        internal void Flip(bool isActivated, XyTypes flipType)
        {
            switch (flipType)
            {
                case XyTypes.None:
                    throw new Exception();

                case XyTypes.X:
                    _mainUnit_SR.flipX = isActivated;
                    break;

                case XyTypes.Y:
                    _mainUnit_SR.flipY = isActivated;
                    break;

                default:
                    throw new Exception();
            }
        }
        internal void Set_Rotation(Vector3 rotation) => _mainUnit_SR.transform.rotation = Quaternion.Euler(rotation);
    }
}

