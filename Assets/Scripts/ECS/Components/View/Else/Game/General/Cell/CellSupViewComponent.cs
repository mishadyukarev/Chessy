using System;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.ColorsValues;

namespace Assets.Scripts.ECS.Component.View.Else.Game.General.Cell
{
    internal struct CellSupViewComponent
    {
        private SpriteRenderer _supVis_SR;

        internal CellSupViewComponent(GameObject cell_GO)
        {
            var parentGO = cell_GO.transform.Find("SupportVision").gameObject;
            _supVis_SR = parentGO.GetComponent<SpriteRenderer>();
        }

        internal void SetColor(SupportVisionTypes supportVisionType)
        {
            switch (supportVisionType)
            {
                case SupportVisionTypes.None:
                    throw new Exception();

                case SupportVisionTypes.Selector:
                    _supVis_SR.color = SelectorColor;
                    break;

                case SupportVisionTypes.Spawn:
                    _supVis_SR.color = SpawnColor;
                    break;

                case SupportVisionTypes.Shift:
                    _supVis_SR.color = ShiftColor;
                    break;

                case SupportVisionTypes.SimpleAttack:
                    _supVis_SR.color = SimpleAttackColor;
                    break;

                case SupportVisionTypes.UniqueAttack:
                    _supVis_SR.color = UniqueAttackColor;
                    break;

                case SupportVisionTypes.Upgrade:
                    _supVis_SR.color = GiveTakeColor;
                    break;

                case SupportVisionTypes.FireSelector:
                    _supVis_SR.color = FireSelectorColor;
                    break;

                case SupportVisionTypes.GivePawnTool:
                    _supVis_SR.color = GiveTakeColor;
                    break;

                case SupportVisionTypes.TakePawnTool:
                    _supVis_SR.color = GiveTakeColor;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal void EnableSR() => _supVis_SR.enabled = true;
        internal void DisableSR() => _supVis_SR.enabled = false;
    }
}
