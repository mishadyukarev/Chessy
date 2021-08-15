using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

internal sealed class SelectorSystem : IEcsRunSystem
{
    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellUnitDataComponent, OwnerComponent, OwnerBotComponent> _cellUnitFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvironDataFilter = default;

    private EcsFilter<SelectorComponent> _selectorFilter = default;
    private EcsFilter<IdxAvailableCellsComponent> _availCellsFilter = default;
    private EcsFilter<InputComponent> _inputFilter = default;
    private EcsFilter<DonerDataUIComponent> _donerUIFilter = default;
    private EcsFilter<ForFillAvailCellsCom> _forFillAvailCellsFilter = default;

    public void Run()
    {
        CellUnitDataComponent CellUnitDataCom(byte idxCell) => _cellUnitFilter.Get1(idxCell);
        OwnerComponent OwnerCellUnitCom(byte idxCell) => _cellUnitFilter.Get2(idxCell);
        OwnerBotComponent OwnerBotCellUnitCom(byte idxCell) => _cellUnitFilter.Get3(idxCell);
        CellEnvironDataCom CellEnvironDataCom(byte idxCell) => _cellEnvironDataFilter.Get1(idxCell);


        ref var selectorCom = ref _selectorFilter.Get1(0);
        ref var availCellsCom = ref _availCellsFilter.Get1(0);


        if (_inputFilter.Get1(0).IsClicked)
        {
            if (selectorCom.RaycastGettedType == RaycastGettedTypes.UI)
            {
                selectorCom.ResetSelectedUnit();
                //selectorCom.CanShiftUnit = false;

                availCellsCom.ClearAvailableCells(AvailableCellTypes.Shift);
                availCellsCom.ClearAvailableCells(AvailableCellTypes.SimpleAttack);
                availCellsCom.ClearAvailableCells(AvailableCellTypes.UniqueAttack);

                //selectorCom.XyPreviousCell.Clean();
            }

            else if (selectorCom.RaycastGettedType == RaycastGettedTypes.Cell)
            {
                if (_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient))
                {
                    if (!selectorCom.IsSelectedCell)
                    {
                        if (selectorCom.IdxPreviousCell != selectorCom.IdxSelectedCell)
                        {
                            selectorCom.IdxSelectedCell = selectorCom.IdxCurrentCell;
                        }
                        else
                        {
                            selectorCom.IdxSelectedCell = default;
                        }


                        selectorCom.IdxPreviousCell = selectorCom.IdxSelectedCell;
                        selectorCom.ResetSelectedCell();
                    }

                    else
                    {
                        if (selectorCom.IdxSelectedCell != selectorCom.IdxCurrentCell)
                            selectorCom.IdxPreviousCell = selectorCom.IdxSelectedCell;

                        selectorCom.IdxSelectedCell = selectorCom.IdxCurrentCell;
                    }
                }

                else
                {
                    if (selectorCom.IsSelectedUnit)
                    {
                        RPCGameSystem.SetUniToMaster(selectorCom.IdxCurrentCell, selectorCom.SelectedUnitType);
                    }

                    else if (selectorCom.CellClickType == CellClickTypes.UpgradeUnit)
                    {
                        if (CellUnitDataCom(selectorCom.IdxCurrentCell).HaveUnit)
                        {
                            RPCGameSystem.UpgradeUnitToMaster(selectorCom.IdxCurrentCell);
                        }
                        else
                        {
                            selectorCom.ResetSelectedCell();
                        }
                    }

                    else if (selectorCom.IsCellClickType(CellClickTypes.PickFire))
                    {
                        RPCGameSystem.FireToMaster(selectorCom.IdxSelectedCell, selectorCom.IdxCurrentCell);
                        selectorCom.ResetSelectedCell();
                    }

                    else if (selectorCom.IsCellClickType(CellClickTypes.GiveExtraThing))
                    {
                        if (CellUnitDataCom(selectorCom.IdxCurrentCell).IsUnitType(UnitTypes.Pawn))
                        {
                            if(selectorCom.PawnExtraToolTypeForGive != default)
                            {
                                RPCGameSystem.GivePawnExtraTool(selectorCom.IdxCurrentCell, selectorCom.PawnExtraToolTypeForGive);
                            }
                            else if(selectorCom.PawnExtraWeaponTypeForGive != default)
                            {
                                RPCGameSystem.GivePawnExtraWeapon(selectorCom.IdxCurrentCell, selectorCom.PawnExtraWeaponTypeForGive);
                            }
                            else
                            {
                                selectorCom.IdxSelectedCell = selectorCom.IdxCurrentCell;
                                selectorCom.CellClickType = default;
                            }                        
                        }
                        else
                        {
                            selectorCom.IdxSelectedCell = selectorCom.IdxCurrentCell;
                            selectorCom.CellClickType = default;
                        }
                    }

                    else if (selectorCom.IsCellClickType(CellClickTypes.TakeExtraThing))
                    {
                        if (CellUnitDataCom(selectorCom.IdxCurrentCell).IsUnitType(UnitTypes.Pawn))
                        {
                            RPCGameSystem.TakePawnExtraTool(selectorCom.IdxCurrentCell);
                        }
                        else
                        {
                            selectorCom.IdxSelectedCell = selectorCom.IdxCurrentCell;
                            selectorCom.CellClickType = default;
                        }
                       
                    }

                    else if (!selectorCom.IsSelectedCell)
                    {
                        if (selectorCom.IdxPreviousCell != selectorCom.IdxSelectedCell || selectorCom.IdxPreviousCell == 0)
                        {
                            selectorCom.IdxSelectedCell = selectorCom.IdxCurrentCell;
                        }
                        else
                        {
                            selectorCom.ResetSelectedCell();
                        }

                        if (CellUnitDataCom(selectorCom.IdxSelectedCell).HaveUnit)
                        {
                            if (OwnerCellUnitCom(selectorCom.IdxSelectedCell).HaveOwner)
                            {
                                if (OwnerCellUnitCom(selectorCom.IdxSelectedCell).IsMine)
                                {
                                    if (CellUnitDataCom(selectorCom.IdxSelectedCell).IsMelee)
                                    {
                                        //SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickMelee);
                                    }
                                    else
                                    {
                                        //SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickArcher);
                                    }

                                    if (CellUnitDataCom(selectorCom.IdxSelectedCell).HaveMinAmountSteps)
                                    {
                                        _forFillAvailCellsFilter.Get1(0).IdxUnitCell = selectorCom.IdxSelectedCell;
                                        GameGeneralSystemManager.GetUnitWaySystems.Run();

                                        //selectorCom.CanShiftUnit = true;
                                    }
                                }
                            }
                        }
                        selectorCom.IdxPreviousCell = selectorCom.IdxSelectedCell;
                    }

                    else
                    {
                        if (selectorCom.IdxSelectedCell != selectorCom.IdxCurrentCell)
                            selectorCom.IdxPreviousCell = selectorCom.IdxSelectedCell;

                        selectorCom.IdxSelectedCell = selectorCom.IdxCurrentCell;


                        if (CellUnitDataCom(selectorCom.IdxSelectedCell).HaveUnit)
                        {
                            if (OwnerCellUnitCom(selectorCom.IdxSelectedCell).HaveOwner)
                            {
                                if (OwnerCellUnitCom(selectorCom.IdxSelectedCell).IsMine)
                                {
                                    if (CellUnitDataCom(selectorCom.IdxSelectedCell).IsMelee)
                                    {
                                        //SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickMelee);
                                    }
                                    else
                                    {
                                        //SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickArcher);
                                    }

                                    if (CellUnitDataCom(selectorCom.IdxSelectedCell).HaveMinAmountSteps)
                                    {
                                        _forFillAvailCellsFilter.Get1(0).IdxUnitCell = selectorCom.IdxSelectedCell;
                                        GameGeneralSystemManager.GetUnitWaySystems.Run();

                                        //selectorCom.CanShiftUnit = true;
                                    }

                                    else
                                    {
                                        availCellsCom.ClearAvailableCells(AvailableCellTypes.Shift);
                                        availCellsCom.ClearAvailableCells(AvailableCellTypes.SimpleAttack);
                                        availCellsCom.ClearAvailableCells(AvailableCellTypes.UniqueAttack);

                                        //selectorCom.CanShiftUnit = false;
                                    }
                                }

                                else
                                {
                                    //selectorCom.CanShiftUnit = false;

                                    //if (availCellsCom.TryFindCell(AvailableCellTypes.SimpleAttack, selectorCom.IdxSelectedCell))
                                    //{
                                    RPCGameSystem.AttackUnitToMaster(selectorCom.IdxPreviousCell, selectorCom.IdxSelectedCell);
                                    //}

                                    //else if (availCellsCom.TryFindCell(AvailableCellTypes.UniqueAttack, selectorCom.XySelectedCell))
                                    //{
                                    //    RPCGameSystem.AttackUnitToMaster(selectorCom.XyPreviousCell, selectorCom.XySelectedCell);
                                    //}

                                    availCellsCom.ClearAvailableCells(AvailableCellTypes.Shift);
                                    availCellsCom.ClearAvailableCells(AvailableCellTypes.SimpleAttack);
                                    availCellsCom.ClearAvailableCells(AvailableCellTypes.UniqueAttack);
                                }
                            }

                            else if (OwnerBotCellUnitCom(selectorCom.IdxSelectedCell).IsBot)
                            {
                                //if (availCellsCom.TryFindCell(AvailableCellTypes.SimpleAttack, selectorCom.XySelectedCell))
                                //{
                                //    RPCGameSystem.AttackUnitToMaster(selectorCom.XyPreviousCell, selectorCom.XySelectedCell);
                                //}

                                //else if (availCellsCom.TryFindCell(AvailableCellTypes.UniqueAttack, selectorCom.XySelectedCell))
                                //{
                                //    RPCGameSystem.AttackUnitToMaster(selectorCom.XyPreviousCell, selectorCom.XySelectedCell);
                                //}

                                availCellsCom.ClearAvailableCells(AvailableCellTypes.Shift);
                                availCellsCom.ClearAvailableCells(AvailableCellTypes.SimpleAttack);
                                availCellsCom.ClearAvailableCells(AvailableCellTypes.UniqueAttack);
                            }
                        }

                        else
                        {
                            if (CellUnitDataCom(selectorCom.IdxPreviousCell).HaveUnit && OwnerCellUnitCom(selectorCom.IdxPreviousCell).HaveOwner && OwnerCellUnitCom(selectorCom.IdxPreviousCell).IsMine)
                            {
                                if (availCellsCom.TryFindCell(AvailableCellTypes.Shift, selectorCom.IdxSelectedCell))
                                {
                                    RPCGameSystem.ShiftUnitToMaster(selectorCom.IdxPreviousCell, selectorCom.IdxSelectedCell);
                                }

                                availCellsCom.ClearAvailableCells(AvailableCellTypes.Shift);
                                availCellsCom.ClearAvailableCells(AvailableCellTypes.SimpleAttack);
                                availCellsCom.ClearAvailableCells(AvailableCellTypes.UniqueAttack);

                               //selectorCom.CanShiftUnit = false;
                            }
                        }
                    }
                }
            }

            else
            {
                //selectorCom.CanShiftUnit = false;

                selectorCom.IdxSelectedCell = 0;
                selectorCom.ResetSelectedUnit();

                availCellsCom.ClearAvailableCells(AvailableCellTypes.Shift);
                availCellsCom.ClearAvailableCells(AvailableCellTypes.SimpleAttack);
                availCellsCom.ClearAvailableCells(AvailableCellTypes.UniqueAttack);

                selectorCom.ResetSelectedCell();// CellClickType = CellClickTypes.Start;
            }
        }

        else
        {
            if (selectorCom.RaycastGettedType == RaycastGettedTypes.UI)
            {
            }

            else if (selectorCom.RaycastGettedType == RaycastGettedTypes.Cell)
            {
                if (selectorCom.IsSelectedUnit)
                {
                    if (!CellUnitDataCom(selectorCom.IdxCurrentCell).HaveUnit || !CellUnitDataCom(selectorCom.IdxCurrentCell).IsVisibleUnit(PhotonNetwork.IsMasterClient))
                    {
                        if (selectorCom.IsStartDirectToCell)
                        {
                            //if (!CellUnitDataCom(selectorCom.IdxCurrentCell).HaveAnyUnit)
                            //    CellUnitViewSystem.ActiveSelectorVisionUnit(true, selectorCom.SelectedUnitType, selectorCom.XyCurrentCell);

                            selectorCom.IdxPreviousVisionCell = selectorCom.IdxCurrentCell;
                            selectorCom.IdxCurrentCell = default;// IsStartSelectedDirect = false;
                        }
                        else
                        {
                            //CellUnitViewSystem.ActiveSelectorVisionUnit(false, selectorCom.SelectedUnitType, selectorCom.XyPreviousVisionCell);
                            //CellUnitViewSystem.ActiveSelectorVisionUnit(true, selectorCom.SelectedUnitType, selectorCom.XyCurrentCell);

                            selectorCom.IdxPreviousVisionCell = selectorCom.IdxCurrentCell;
                        }
                    }
                }
            }

            else
            {

            }
        }
    }
}