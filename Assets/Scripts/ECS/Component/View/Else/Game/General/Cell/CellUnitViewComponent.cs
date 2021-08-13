using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.Cell;
using System;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.View.Else.Game.General.Cell
{
    internal struct CellUnitViewComponent
    {
        private SpriteRenderer _unitTool_SR;
        private SpriteRenderer _extraTool_SR;

        internal CellUnitViewComponent(GameObject cell_GO)
        {
            _unitTool_SR = cell_GO.transform.Find("UnitTool_SR").GetComponent<SpriteRenderer>();
            _extraTool_SR = _unitTool_SR.transform.Find("ExtraTool_SR").GetComponent<SpriteRenderer>();
        }


        internal void EnableUnitTool_SR() => _unitTool_SR.enabled = true;
        internal void DisableMainTool_SR() => _unitTool_SR.enabled = false;

        internal void EnableExtraTool_SR() => _extraTool_SR.enabled = true;
        internal void DisableExtraTool_SR() => _extraTool_SR.enabled = false;



        internal void SetToKingMainTool_Sprite() => _unitTool_SR.sprite = ResourcesComponent.SpritesConfig.KingSprite;
        internal void SetToPawnFirstTool_Sprite(PawnFirstToolTypes pawnFirstToolType)
        {
            switch (pawnFirstToolType)
            {
                case PawnFirstToolTypes.None:
                    throw new Exception();

                case PawnFirstToolTypes.Axe:
                    _unitTool_SR.sprite = ResourcesComponent.SpritesConfig.AxePawnSecondTool_Sprite;
                    break;

                default:
                    throw new Exception();
            }
        }
        internal void SetToRookFirstTool_Sprite(RookFirstToolTypes rookFirstToolType)
        {
            switch (rookFirstToolType)
            {
                case RookFirstToolTypes.None:
                    throw new Exception();

                case RookFirstToolTypes.Bow:
                    _unitTool_SR.sprite = ResourcesComponent.SpritesConfig.RookSprite;
                    break;

                case RookFirstToolTypes.Crossbow:
                    _unitTool_SR.sprite = ResourcesComponent.SpritesConfig.RookCrossbowSprite;
                    break;

                default:
                    throw new Exception();
            }
        }
        internal void SetToBishopFirstTool_Sprite(BishopFirstToolTypes bishopFirstToolType)
        {
            switch (bishopFirstToolType)
            {
                case BishopFirstToolTypes.None:
                    throw new Exception();

                case BishopFirstToolTypes.Bow:
                    _unitTool_SR.sprite = ResourcesComponent.SpritesConfig.BishopSprite;
                    break;

                case BishopFirstToolTypes.Crossbow:
                    _unitTool_SR.sprite = ResourcesComponent.SpritesConfig.BishopCrossbowSprite;
                    break;

                default:
                    throw new Exception();
            }
        }


        internal void SetToPawnSecondTool_Sprite(PawnSecondToolTypes pawnSecondToolType)
        {
            switch (pawnSecondToolType)
            {
                case PawnSecondToolTypes.None:
                    throw new Exception();

                case PawnSecondToolTypes.Hoe:
                    break;

                case PawnSecondToolTypes.Pick:
                    break;

                case PawnSecondToolTypes.Sword:
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
                    _unitTool_SR.flipX = isActivated;
                    break;

                case XyTypes.Y:
                    _unitTool_SR.flipY = isActivated;
                    break;

                default:
                    break;
            }
        }
        internal void Set_Rotation(Vector3 rotation) => _unitTool_SR.transform.rotation = Quaternion.Euler(rotation);
    }
}

