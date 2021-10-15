using Scripts.Common;
using System;
using UnityEngine;

namespace Scripts.Game
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
                    throw new Exception();

                case ToolWeaponTypes.Axe:
                    _extraUnit_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.Pawn);
                    break;

                case ToolWeaponTypes.Pick:
                    _extraUnit_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.Pick);
                    break;

                case ToolWeaponTypes.Sword:
                    _extraUnit_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.Sword);
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
