using Assets.Scripts.Abstractions.Enums;
using System;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.View.Else.Game.General.Cell
{
    internal struct CellUnitMainViewComp
    {
        private SpriteRenderer _main_SR;

        internal CellUnitMainViewComp(GameObject cell_GO)
        {
            _main_SR = cell_GO.transform.Find("MainUnit_SR").GetComponent<SpriteRenderer>();
        }


        internal void Enable_SR() => _main_SR.enabled = true;
        internal void Disable_SR() => _main_SR.enabled = false;



        internal void SetKing_Sprite() => _main_SR.sprite = ResourcesComponent.SpritesConfig.KingSprite;
        internal void Set_Sprite(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    _main_SR.sprite = ResourcesComponent.SpritesConfig.KingSprite;
                    break;

                case UnitTypes.Pawn:
                    _main_SR.sprite = ResourcesComponent.SpritesConfig.CellAxe_Sprite;
                    break;

                case UnitTypes.Rook:
                    _main_SR.sprite = ResourcesComponent.SpritesConfig.RookSprite;
                    break;

                case UnitTypes.Bishop:
                    _main_SR.sprite = ResourcesComponent.SpritesConfig.BishopSprite;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal void SetMainPawnTool_Sprite(PawnMainToolTypes pawnMainToolType)
        {
            switch (pawnMainToolType)
            {
                case PawnMainToolTypes.None:
                    throw new Exception();

                case PawnMainToolTypes.Axe:
                    _main_SR.sprite = ResourcesComponent.SpritesConfig.CellAxe_Sprite;
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
                    _main_SR.flipX = isActivated;
                    break;

                case XyTypes.Y:
                    _main_SR.flipY = isActivated;
                    break;

                default:
                    throw new Exception();
            }
        }
        internal void Set_Rotation(Vector3 rotation) => _main_SR.transform.rotation = Quaternion.Euler(rotation);
    }
}

