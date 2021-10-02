using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
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

        internal void SetToolWeapon_Sprite(ToolWeaponTypes toolAndWeaponType)
        {
            switch (toolAndWeaponType)
            {
                case ToolWeaponTypes.None:
                    throw new Exception();

                case ToolWeaponTypes.Hoe:
                    _extraUnit_SR.sprite = ResourcesComponent.SpritesConfig.Hoe_Sprite;
                    break;

                case ToolWeaponTypes.Axe:
                    _extraUnit_SR.sprite = ResourcesComponent.SpritesConfig.Axe_Sprite;
                    break;

                case ToolWeaponTypes.Pick:
                    _extraUnit_SR.sprite = ResourcesComponent.SpritesConfig.Pick_Sprite;
                    break;

                case ToolWeaponTypes.Sword:
                    _extraUnit_SR.sprite = ResourcesComponent.SpritesConfig.Sword_Sprite;
                    break;

                case ToolWeaponTypes.Bow:
                    break;

                case ToolWeaponTypes.Crossbow:
                    break;

                default:
                    throw new Exception();
            }
        }

        internal void SetFlipX(bool isFliped) => _extraUnit_SR.flipX = isFliped;
    }
}
