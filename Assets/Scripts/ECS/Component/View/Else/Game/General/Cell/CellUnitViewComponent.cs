using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.Cell;
using System;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.View.Else.Game.General.Cell
{
    internal struct CellUnitViewComponent
    {
        private SpriteRenderer _unit_SR;
        private SpriteRenderer _secondTool_SR;

        internal CellUnitViewComponent(GameObject cell_GO)
        {
            _unit_SR = cell_GO.transform.Find("Unit_SR").GetComponent<SpriteRenderer>();
            _secondTool_SR = _unit_SR.transform.Find("SecondTool_SR").GetComponent<SpriteRenderer>();
        }


        internal void EnableMain_SR() => _unit_SR.enabled = true;
        internal void DisableMain_SR() => _unit_SR.enabled = false;

        internal void SetMainUnit_Sprite(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    _unit_SR.sprite = ResourcesComponent.SpritesConfig.KingSprite;
                    break;

                case UnitTypes.Pawn:
                    _unit_SR.sprite = ResourcesComponent.SpritesConfig.PawnSprite;
                    break;

                case UnitTypes.Rook:
                    _unit_SR.sprite = ResourcesComponent.SpritesConfig.RookSprite;
                    break;

                case UnitTypes.RookCrossbow:
                    _unit_SR.sprite = ResourcesComponent.SpritesConfig.RookCrossbowSprite;
                    break;

                case UnitTypes.Bishop:
                    _unit_SR.sprite = ResourcesComponent.SpritesConfig.BishopSprite;
                    break;

                case UnitTypes.BishopCrossbow:
                    _unit_SR.sprite = ResourcesComponent.SpritesConfig.BishopCrossbowSprite;
                    break;

                default:
                    throw new Exception();
            }
        }
        internal void SetSecondTool_Sprite(SecondToolTypes secondToolType)
        {
            switch (secondToolType)
            {
                case SecondToolTypes.None:
                    throw new Exception();

                case SecondToolTypes.Hoe:
                    _secondTool_SR.sprite = default;
                    break;

                case SecondToolTypes.Pick:
                    break;

                case SecondToolTypes.Sword:
                    break;

                default:
                    throw new Exception();
            }
        }

        internal void Flip(bool isActivated, XyTypes flipType)
        {
            switch (flipType)
            {
                case XyTypes.X:
                    _unit_SR.flipX = isActivated;
                    break;

                case XyTypes.Y:
                    _unit_SR.flipY = isActivated;
                    break;

                default:
                    break;
            }
        }
        internal void Set_Rotation(Vector3 rotation) => _unit_SR.transform.rotation = Quaternion.Euler(rotation);
    }
}

