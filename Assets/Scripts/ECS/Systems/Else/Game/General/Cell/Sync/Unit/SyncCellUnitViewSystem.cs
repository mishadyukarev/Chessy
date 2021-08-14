using Assets.Scripts.Abstractions.Enums.Cell;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Leopotam.Ecs;
using Photon.Pun;
using System;

namespace Assets.Scripts.ECS.Game.General.Systems.SupportVision
{
    internal sealed class SyncCellUnitViewSystem : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataComponent, CellUnitViewComponent> _cellUnitFilter = default;
        private EcsFilter<SelectorComponent> _selectorFilter = default;

        public void Run()
        {
            ref var selectorCom = ref _selectorFilter.Get1(0);

            foreach (byte idxCurCell in _cellUnitFilter)
            {
                ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxCurCell);
                ref var curCellUnitViewCom = ref _cellUnitFilter.Get2(idxCurCell);


                if (curCellUnitDataCom.HaveUnit)
                {
                    if (curCellUnitDataCom.IsVisibleUnit(PhotonNetwork.IsMasterClient))
                    {
                        curCellUnitViewCom.EnableUnitTool_SR();
                        curCellUnitViewCom.SetMainUnit_Sprite(curCellUnitDataCom.UnitType);

                        if (curCellUnitDataCom.IsUnitType(UnitTypes.Pawn_Axe))
                        {
                            if (curCellUnitDataCom.HaveExtraPawnTool)
                            {
                                curCellUnitViewCom.EnableExtraTool_SR();
                                curCellUnitViewCom.SetExtraTool_Sprite(curCellUnitDataCom.ExtraPawnToolType);
                            }

                            else
                            {
                                curCellUnitViewCom.DisableExtraTool_SR();
                            }
                        }

                        else
                        {
                            curCellUnitViewCom.DisableExtraTool_SR();
                        }
                    }
                    else
                    {
                        curCellUnitViewCom.DisableMainTool_SR();
                    }
                }
                else
                {
                    curCellUnitViewCom.DisableMainTool_SR();
                    curCellUnitViewCom.DisableExtraTool_SR();
                }

                if (selectorCom.IsSelectedUnit)
                {
                    if (curCellUnitDataCom.HaveUnit)
                    {
                        _cellUnitFilter.Get2(selectorCom.IdxPreviousVisionCell).SetMainUnit_Sprite(selectorCom.SelectedUnitType);
                        _cellUnitFilter.Get2(selectorCom.IdxPreviousVisionCell).EnableUnitTool_SR();
                    }
                    else
                    {
                        if (selectorCom.IdxCurrentCell == idxCurCell)
                        {
                            curCellUnitViewCom.SetMainUnit_Sprite(selectorCom.SelectedUnitType);
                            curCellUnitViewCom.EnableUnitTool_SR();
                        }
                    }
                }
            }
        }
    }
}
