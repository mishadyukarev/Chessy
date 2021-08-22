using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
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



        internal void SetKing_Sprite() => _main_SR.sprite = ResourcesComponent.SpritesConfig.King_Sprite;
        internal void SetToolWeapon_Sprite(ToolWeaponTypes toolWeaponType)
        {
            switch (toolWeaponType)
            {
                case ToolWeaponTypes.None:
                    throw new Exception();

                case ToolWeaponTypes.Hoe:
                    break;

                case ToolWeaponTypes.Axe:
                    _main_SR.sprite = ResourcesComponent.SpritesConfig.Axe_Sprite;
                    break;

                case ToolWeaponTypes.Pick:
                    _main_SR.sprite = ResourcesComponent.SpritesConfig.Pick_Sprite;
                    break;

                case ToolWeaponTypes.Sword:
                    _main_SR.sprite = ResourcesComponent.SpritesConfig.Sword_Sprite;
                    break;

                case ToolWeaponTypes.Bow:
                    _main_SR.sprite = ResourcesComponent.SpritesConfig.BowBishop_Sprite;
                    break;

                case ToolWeaponTypes.Crossbow:
                    break;

                default:
                    throw new Exception();
            }
        }
        internal void SetArcher_Sprite(UnitTypes unitType, ToolWeaponTypes toolWeaponType)
        {
            if (toolWeaponType == ToolWeaponTypes.Bow)
            {
                if (unitType == UnitTypes.Rook)
                {
                    _main_SR.sprite = ResourcesComponent.SpritesConfig.BowRook_Sprite;
                }
                else if (unitType == UnitTypes.Bishop)
                {
                    _main_SR.sprite = ResourcesComponent.SpritesConfig.BowBishop_Sprite;
                }
                else
                {
                    throw new Exception();
                }
            }
            else if (toolWeaponType ==ToolWeaponTypes.Crossbow)
            {
                if (unitType == UnitTypes.Rook)
                {
                    _main_SR.sprite = ResourcesComponent.SpritesConfig.CrossbowRook_Sprite;
                }
                else if (unitType == UnitTypes.Bishop)
                {
                    _main_SR.sprite = ResourcesComponent.SpritesConfig.CrossbowBishop_Sprite;
                }
                else
                {
                    throw new Exception();
                }
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

