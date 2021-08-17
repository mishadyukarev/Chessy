using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets.Scripts.ECS.Game.General.Systems.SupportVision
{
    internal sealed class SyncCellUnitViewSystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataComponent, CellUnitMainViewComp, CellUnitExtraViewComp> _cellUnitFilter = default;
        private EcsFilter<SelectorComponent> _selectorFilter = default;

        public void Run()
        {
            ref var selectorCom = ref _selectorFilter.Get1(0);

            foreach (byte idxCurCell in _cellUnitFilter)
            {
                ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxCurCell);
                ref var curMainCellUnitViewCom = ref _cellUnitFilter.Get2(idxCurCell);
                ref var curExtraCellUnitViewCom = ref _cellUnitFilter.Get3(idxCurCell);


                if (curCellUnitDataCom.HaveUnit)
                {
                    if (curCellUnitDataCom.IsVisibleUnit(PhotonNetwork.IsMasterClient))
                    {
                        curMainCellUnitViewCom.Enable_SR();
                        curMainCellUnitViewCom.Set_Sprite(curCellUnitDataCom.UnitType);

                        if (curCellUnitDataCom.IsUnitType(UnitTypes.Pawn))
                        {
                            if (curCellUnitDataCom.HaveExtraThing)
                            {
                                curExtraCellUnitViewCom.Enable_SR();
                                curExtraCellUnitViewCom.SetToolOrWeapon_Sprite(curCellUnitDataCom.ExtraToolAndWeaponType);
                            }

                            else
                            {
                                curExtraCellUnitViewCom.Disable_SR();
                            }
                        }

                        else
                        {
                            curExtraCellUnitViewCom.Disable_SR();
                        }
                    }
                    else
                    {
                        curMainCellUnitViewCom.Disable_SR();
                        curExtraCellUnitViewCom.Disable_SR();
                    }
                }
                else
                {
                    curMainCellUnitViewCom.Disable_SR();
                    curExtraCellUnitViewCom.Disable_SR();
                }

                if (selectorCom.IsSelectedUnit)
                {
                    if (curCellUnitDataCom.HaveUnit)
                    {
                        _cellUnitFilter.Get2(selectorCom.IdxPreviousVisionCell).Set_Sprite(selectorCom.SelectedUnitType);
                        _cellUnitFilter.Get2(selectorCom.IdxPreviousVisionCell).Enable_SR();
                    }
                    else
                    {
                        if (selectorCom.IdxCurrentCell == idxCurCell)
                        {
                            curMainCellUnitViewCom.Set_Sprite(selectorCom.SelectedUnitType);
                            curMainCellUnitViewCom.Enable_SR();
                        }
                    }
                }
            }
        }
    }
}
