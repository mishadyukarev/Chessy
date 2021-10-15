using Scripts.Common;
using System;
using UnityEngine;

namespace Scripts.Game
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



        internal void SetKing_Sprite() => _main_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.King);
        internal void SetPawn_Spriter() => _main_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.Pawn);

        internal void SetArcher_Sprite(UnitTypes unitType, ToolWeaponTypes toolWeaponType)
        {


            //if (toolWeaponType == ToolWeaponTypes.Bow)
            //{
            //    if (unitType == UnitTypes.Rook)
            //    {
            //        _main_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.Rook);
            //    }
            //    else if (unitType == UnitTypes.Bishop)
            //    {
            //        _main_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.Bishop);
            //    }
            //    else
            //    {
            //        throw new Exception();
            //    }
            //}
            //else if (toolWeaponType == ToolWeaponTypes.Crossbow)
            //{
            //    if (unitType == UnitTypes.Rook)
            //    {
            //        _main_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.RookCrossbow);
            //    }
            //    else if (unitType == UnitTypes.Bishop)
            //    {
            //        _main_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.BishopCrossbow);
            //    }
            //    else
            //    {
            //        throw new Exception();
            //    }
            //}
        }

        internal void SetFlipX(bool isActive) => _main_SR.flipX = isActive;
        internal void Set_LocRotEuler(Vector3 rotation) => _main_SR.transform.localEulerAngles = rotation;
    }
}

