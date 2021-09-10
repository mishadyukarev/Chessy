using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Leopotam.Ecs;
using Photon.Pun;
using System;

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

                        if (curCellUnitDataCom.IsUnitType(UnitTypes.King))
                        {
                            curMainCellUnitViewCom.SetKing_Sprite();
                        }

                        else if (curCellUnitDataCom.IsUnitType(UnitTypes.Pawn))
                        {
                            curMainCellUnitViewCom.SetPawn_Spriter();


                            if (curCellUnitDataCom.HaveExtraToolWeaponPawn)
                            {
                                curExtraCellUnitViewCom.Enable_SR();
                                curExtraCellUnitViewCom.SetToolWeapon_Sprite(curCellUnitDataCom.ExtraToolWeaponPawnType);
                            }
                            else
                            {
                                curExtraCellUnitViewCom.Disable_SR();
                            }
                        }

                        else if (curCellUnitDataCom.IsUnitType(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
                        {
                            if (curCellUnitDataCom.ArcherWeapon)
                            {
                                curMainCellUnitViewCom.SetArcher_Sprite(curCellUnitDataCom.UnitType, curCellUnitDataCom.ArcherWeaponType);
                            }
                            else
                            {
                                throw new Exception();
                            }

                            if (curCellUnitDataCom.HaveExtraToolWeaponPawn)
                            {
                                curExtraCellUnitViewCom.Enable_SR();
                                curExtraCellUnitViewCom.SetToolWeapon_Sprite(curCellUnitDataCom.ExtraToolWeaponPawnType);
                            }
                        }

                        else
                        {
                            throw new Exception();
                        }
                    }

                    else
                    {
                        curMainCellUnitViewCom.Disable_SR();
                        curExtraCellUnitViewCom.Disable_SR();
                    }

                    if (selectorCom.IsSelectedUnit)
                    {
                    }
                }

                else
                {
                    curMainCellUnitViewCom.Disable_SR();
                    curExtraCellUnitViewCom.Disable_SR();

                    if (selectorCom.IsSelectedUnit)
                    {
                        if (selectorCom.IdxCurrentCell == idxCurCell)
                        {
                            _cellUnitFilter.Get2(idxCurCell).Enable_SR();

                            if (selectorCom.SelectedUnitType == UnitTypes.King)
                            {
                                _cellUnitFilter.Get2(idxCurCell).SetKing_Sprite();
                            }
                            else if (selectorCom.SelectedUnitType == UnitTypes.Pawn)
                            {
                                _cellUnitFilter.Get2(idxCurCell).SetPawn_Spriter();
                            }
                            else if (selectorCom.SelectedUnitType == UnitTypes.Rook || selectorCom.SelectedUnitType == UnitTypes.Bishop)
                            {
                                _cellUnitFilter.Get2(idxCurCell).SetArcher_Sprite(selectorCom.SelectedUnitType, ToolWeaponTypes.Bow);
                            }
                        }
                    }
                }
            }
        }
    }
}
