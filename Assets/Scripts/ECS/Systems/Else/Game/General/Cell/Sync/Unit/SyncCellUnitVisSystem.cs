using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets.Scripts.ECS.Game.General.Systems.SupportVision
{
    internal sealed class SyncCellUnitVisSystem : IEcsRunSystem
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


                if (curCellUnitDataCom.IsVisibleUnit(PhotonNetwork.IsMasterClient))
                {
                    if (curCellUnitDataCom.HaveUnit)
                    {
                        curCellUnitViewCom.EnableSR();
                        curCellUnitViewCom.SetSprite(curCellUnitDataCom.UnitType);
                    }

                    else
                    {
                        curCellUnitViewCom.DisableSR();
                    }
                }

                if (selectorCom.IsSelectedUnit)
                {
                    if (curCellUnitDataCom.HaveUnit)
                    {
                        _cellUnitFilter.Get2(selectorCom.IdxPreviousVisionCell).SetSprite(selectorCom.SelectedUnitType);
                        _cellUnitFilter.Get2(selectorCom.IdxPreviousVisionCell).EnableSR();
                    }
                    else
                    {
                        if (selectorCom.IdxCurrentCell == idxCurCell)
                        {
                            curCellUnitViewCom.SetSprite(selectorCom.SelectedUnitType);
                            curCellUnitViewCom.EnableSR();
                        }
                    }
                }
            }
        }
    }
}
