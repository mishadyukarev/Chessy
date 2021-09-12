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
        internal void SetPawn_Spriter() => _main_SR.sprite = ResourcesComponent.SpritesConfig.Axe_Sprite;

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
            else if (toolWeaponType == ToolWeaponTypes.Crossbow)
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

        internal void SetFlipX(bool isActive) => _main_SR.flipX = isActive;
        internal void SetFlipY(bool isActive) => _main_SR.flipY = isActive;
        internal void Set_Rotation(Vector3 rotation) => _main_SR.transform.rotation = Quaternion.Euler(rotation);
    }
}

