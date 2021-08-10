using Assets.Scripts.Abstractions.Enums;
using System;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.View.Else.Game.General.Cell
{
    internal struct CellUnitViewComponent
    {
        private SpriteRenderer _cellUnit_SR;

        internal CellUnitViewComponent(GameObject cell_GO)
        {
            _cellUnit_SR = cell_GO.transform.Find("Unit").GetComponent<SpriteRenderer>();
        }



        internal void EnableSR() => _cellUnit_SR.enabled = true;
        internal void DisableSR() => _cellUnit_SR.enabled = false;

        internal void SetSprite(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    _cellUnit_SR.sprite = ResourcesComponent.SpritesConfig.KingSprite;
                    break;

                case UnitTypes.Pawn:
                    _cellUnit_SR.sprite = ResourcesComponent.SpritesConfig.PawnSprite;
                    break;

                case UnitTypes.PawnSword:
                    _cellUnit_SR.sprite = ResourcesComponent.SpritesConfig.PawnSwordSprite;
                    break;

                case UnitTypes.Rook:
                    _cellUnit_SR.sprite = ResourcesComponent.SpritesConfig.RookSprite;
                    break;

                case UnitTypes.RookCrossbow:
                    _cellUnit_SR.sprite = ResourcesComponent.SpritesConfig.RookCrossbowSprite;
                    break;

                case UnitTypes.Bishop:
                    _cellUnit_SR.sprite = ResourcesComponent.SpritesConfig.BishopSprite;
                    break;

                case UnitTypes.BishopCrossbow:
                    _cellUnit_SR.sprite = ResourcesComponent.SpritesConfig.BishopCrossbowSprite;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal void SetColorSR(Color color) => _cellUnit_SR.color = color;

        internal void SetEnabledUnit(bool isEnabled) => _cellUnit_SR.enabled = isEnabled;

        internal void Flip(bool isActivated, XyTypes flipType)
        {
            switch (flipType)
            {
                case XyTypes.X:
                    _cellUnit_SR.flipX = isActivated;
                    break;

                case XyTypes.Y:
                    _cellUnit_SR.flipY = isActivated;
                    break;

                default:
                    break;
            }
        }
        internal void SetRotation(Vector3 rotation) => _cellUnit_SR.transform.rotation = Quaternion.Euler(rotation);
    }
}

