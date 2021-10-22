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

                case ToolWeaponTypes.Pick:
                    _extraUnit_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.PickWood);
                    break;

                case ToolWeaponTypes.Sword:
                    _extraUnit_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.SwordIron);
                    break;

                case ToolWeaponTypes.Shield:
                    _extraUnit_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.ShieldWood);
                    break;

                default:
                    throw new Exception();
            }
        }

        internal void SetAlpha(bool isVisible)
        {
            if (isVisible) _extraUnit_SR.color = new Color(_extraUnit_SR.color.r, _extraUnit_SR.color.g, _extraUnit_SR.color.b, 1);
            else _extraUnit_SR.color = new Color(_extraUnit_SR.color.r, _extraUnit_SR.color.g, _extraUnit_SR.color.b, 0.8f);
        }
        internal void SetFlipX(bool isFliped) => _extraUnit_SR.flipX = isFliped;
    }
}
