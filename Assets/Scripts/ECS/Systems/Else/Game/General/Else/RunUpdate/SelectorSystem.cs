using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Game.General;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class SelectorSystem : IEcsRunSystem
{
    private EcsFilter<XyCellComponent> _cellFilter;
    private EcsFilter<SelectorComponent, AvailableCellsComponent> _selectorFilter = default;
    private EcsFilter<InputComponent> _inputFilter = default;

    private bool IsClicked => _inputFilter.Get1(0).IsClicked;


    public void Run()
    {
        var v = _cellFilter.GetXyCell(100);

        //var v = _cellFilter.GetIndexCell(new int[] { 5, 6 });

        ref var selectorCom = ref _selectorFilter.Get1(0);
        ref var availCellsCom = ref _selectorFilter.Get2(0);

        if (IsClicked)
        {
            if (selectorCom.RaycastGettedType == RaycastGettedTypes.UI)
            {
                selectorCom.CanShiftUnit = false;

                availCellsCom.ClearAvailableCells(AvailableCellTypes.Shift);
                availCellsCom.ClearAvailableCells(AvailableCellTypes.SimpleAttack);
                availCellsCom.ClearAvailableCells(AvailableCellTypes.UniqueAttack);

                //selectorCom.XyPreviousCell.Clean();
            }

            else if (selectorCom.RaycastGettedType == RaycastGettedTypes.Cell)
            {
                if (DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient))
                {
                    if (selectorCom.SelectorType == SelectorTypes.StartClick)
                    {
                        selectorCom.XySelectedCell = selectorCom.XyCurrentCell;

                        if (!selectorCom.XyPreviousCell.Compare(selectorCom.XySelectedCell))
                            selectorCom.IsSelectedCell = true;

                        selectorCom.XyPreviousCell = selectorCom.XySelectedCell;
                        selectorCom.SelectorType = SelectorTypes.NotStartClick;
                    }

                    else
                    {
                        if (!selectorCom.XySelectedCell.Compare(selectorCom.XyCurrentCell))
                            selectorCom.XyPreviousCell = selectorCom.XySelectedCell;

                        selectorCom.XySelectedCell = selectorCom.XyCurrentCell;
                        selectorCom.IsSelectedCell = true;
                    }
                }

                else
                {
                    if (selectorCom.HaveAnySelectorUnit)
                    {
                        RPCGameSystem.SetUniToMaster(selectorCom.XyCurrentCell, selectorCom.SelectedUnitType);
                    }

                    else if (selectorCom.SelectorType == SelectorTypes.UpgradeUnit)
                    {
                        if (CellUnitsDataSystem.HaveAnyUnit(selectorCom.XyCurrentCell))
                        {
                            RPCGameSystem.UpgradeUnitToMaster(selectorCom.XyCurrentCell);
                        }
                        else
                        {
                            selectorCom.SelectorType = SelectorTypes.StartClick;
                        }
                    }

                    else if (selectorCom.SelectorType == SelectorTypes.PickFire)
                    {
                        RPCGameSystem.FireToMaster(selectorCom.XySelectedCell, selectorCom.XyCurrentCell);
                        selectorCom.SelectorType = SelectorTypes.StartClick;
                    }

                    else if (selectorCom.SelectorType == SelectorTypes.StartClick)
                    {
                        selectorCom.XySelectedCell = selectorCom.XyCurrentCell;

                        if (!selectorCom.XyPreviousCell.Compare(selectorCom.XySelectedCell))
                            selectorCom.IsSelectedCell = true;

                        if (CellUnitsDataSystem.HaveAnyUnit(selectorCom.XySelectedCell))
                        {
                            if (CellUnitsDataSystem.HaveOwner(selectorCom.XySelectedCell))
                            {
                                if (CellUnitsDataSystem.IsMine(selectorCom.XySelectedCell))
                                {
                                    if (CellUnitsDataSystem.IsMelee(selectorCom.XySelectedCell))
                                    {
                                        SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickMelee);
                                    }
                                    else
                                    {
                                        SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickArcher);
                                    }

                                    if (CellUnitsDataSystem.HaveMinAmountSteps(selectorCom.XySelectedCell))
                                    {
                                        availCellsCom.SetAllCellsCopy(AvailableCellTypes.Shift, CellUnitsDataSystem.GetCellsForShift(selectorCom.XySelectedCell));

                                        CellUnitsDataSystem.GetCellsForAttack(PhotonNetwork.LocalPlayer, out var availableCellsSimpleAttack, out var availableCellsUniqueAttack, selectorCom.XySelectedCell);
                                        availCellsCom.SetAllCellsCopy(AvailableCellTypes.SimpleAttack, availableCellsSimpleAttack);
                                        availCellsCom.SetAllCellsCopy(AvailableCellTypes.UniqueAttack, availableCellsUniqueAttack);

                                        selectorCom.CanShiftUnit = true;
                                    }
                                }
                            }
                        }
                        selectorCom.XyPreviousCell = selectorCom.XySelectedCell;
                        selectorCom.SelectorType = SelectorTypes.NotStartClick;
                    }

                    else if (selectorCom.SelectorType == SelectorTypes.NotStartClick)
                    {
                        if (!selectorCom.XySelectedCell.Compare(selectorCom.XyCurrentCell))
                            selectorCom.XyPreviousCell = selectorCom.XySelectedCell;


                        selectorCom.XySelectedCell = selectorCom.XyCurrentCell;
                        selectorCom.IsSelectedCell = true;


                        if (CellUnitsDataSystem.HaveAnyUnit(selectorCom.XySelectedCell))
                        {
                            if (CellUnitsDataSystem.HaveOwner(selectorCom.XySelectedCell))
                            {
                                if (CellUnitsDataSystem.IsMine(selectorCom.XySelectedCell))
                                {
                                    if (CellUnitsDataSystem.IsMelee(selectorCom.XySelectedCell))
                                    {
                                        SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickMelee);
                                    }
                                    else
                                    {
                                        SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickArcher);
                                    }

                                    if (CellUnitsDataSystem.HaveMinAmountSteps(selectorCom.XySelectedCell))
                                    {
                                        availCellsCom.SetAllCellsCopy(AvailableCellTypes.Shift, CellUnitsDataSystem.GetCellsForShift(selectorCom.XySelectedCell));

                                        CellUnitsDataSystem.GetCellsForAttack(PhotonNetwork.LocalPlayer, out var availableCellsSimpleAttack, out var availableCellsUniqueAttack, selectorCom.XySelectedCell);
                                        availCellsCom.SetAllCellsCopy(AvailableCellTypes.SimpleAttack, availableCellsSimpleAttack);
                                        availCellsCom.SetAllCellsCopy(AvailableCellTypes.UniqueAttack, availableCellsUniqueAttack);

                                        selectorCom.CanShiftUnit = true;
                                    }

                                    else
                                    {
                                        availCellsCom.ClearAvailableCells(AvailableCellTypes.Shift);
                                        availCellsCom.ClearAvailableCells(AvailableCellTypes.SimpleAttack);
                                        availCellsCom.ClearAvailableCells(AvailableCellTypes.UniqueAttack);

                                        selectorCom.CanShiftUnit = false;
                                    }
                                }

                                else
                                {
                                    selectorCom.CanShiftUnit = false;

                                    if (availCellsCom.TryFindCell(AvailableCellTypes.SimpleAttack, selectorCom.XySelectedCell))
                                    {
                                        RPCGameSystem.AttackUnitToMaster(selectorCom.XyPreviousCell, selectorCom.XySelectedCell);
                                    }

                                    else if (availCellsCom.TryFindCell(AvailableCellTypes.UniqueAttack, selectorCom.XySelectedCell))
                                    {
                                        RPCGameSystem.AttackUnitToMaster(selectorCom.XyPreviousCell, selectorCom.XySelectedCell);
                                    }

                                    availCellsCom.ClearAvailableCells(AvailableCellTypes.Shift);
                                    availCellsCom.ClearAvailableCells(AvailableCellTypes.SimpleAttack);
                                    availCellsCom.ClearAvailableCells(AvailableCellTypes.UniqueAttack);
                                }
                            }

                            else if (CellUnitsDataSystem.IsBot(selectorCom.XySelectedCell))
                            {
                                if (availCellsCom.TryFindCell(AvailableCellTypes.SimpleAttack, selectorCom.XySelectedCell))
                                {
                                    RPCGameSystem.AttackUnitToMaster(selectorCom.XyPreviousCell, selectorCom.XySelectedCell);
                                }

                                else if (availCellsCom.TryFindCell(AvailableCellTypes.UniqueAttack, selectorCom.XySelectedCell))
                                {
                                    RPCGameSystem.AttackUnitToMaster(selectorCom.XyPreviousCell, selectorCom.XySelectedCell);
                                }

                                availCellsCom.ClearAvailableCells(AvailableCellTypes.Shift);
                                availCellsCom.ClearAvailableCells(AvailableCellTypes.SimpleAttack);
                                availCellsCom.ClearAvailableCells(AvailableCellTypes.UniqueAttack);
                            }
                        }

                        else
                        {
                            if (selectorCom.CanShiftUnit)
                            {
                                if (CellUnitsDataSystem.HaveAnyUnit(selectorCom.XyPreviousCell))
                                {
                                    if (CellUnitsDataSystem.HaveOwner(selectorCom.XyPreviousCell))
                                    {
                                        if (CellUnitsDataSystem.IsMine(selectorCom.XyPreviousCell))
                                        {
                                            if (CellUnitsDataSystem.HaveMinAmountSteps(selectorCom.XyPreviousCell))
                                            {
                                                if (availCellsCom.TryFindCell(AvailableCellTypes.Shift, selectorCom.XySelectedCell))
                                                {
                                                    RPCGameSystem.ShiftUnitToMaster(selectorCom.XyPreviousCell, selectorCom.XySelectedCell);
                                                }
                                            }
                                        }
                                    }
                                }
                                availCellsCom.ClearAvailableCells(AvailableCellTypes.Shift);
                                availCellsCom.ClearAvailableCells(AvailableCellTypes.SimpleAttack);
                                availCellsCom.ClearAvailableCells(AvailableCellTypes.UniqueAttack);

                                selectorCom.CanShiftUnit = false;
                            }
                        }
                    }
                }
            }

            else
            {
                selectorCom.CanShiftUnit = false;

                selectorCom.IsSelectedCell = false;

                availCellsCom.ClearAvailableCells(AvailableCellTypes.Shift);
                availCellsCom.ClearAvailableCells(AvailableCellTypes.SimpleAttack);
                availCellsCom.ClearAvailableCells(AvailableCellTypes.UniqueAttack);

                if (selectorCom.SelectedUnitType != default)
                {
                    CellUnitViewSystem.SetEnabledUnit(false, selectorCom.XyPreviousVisionCell);
                    selectorCom.SelectedUnitType = default;
                    //selectorCom.XyPreviousCell.Clean();
                }

                selectorCom.SelectorType = SelectorTypes.StartClick;
            }
        }

        else
        {
            if (selectorCom.RaycastGettedType == RaycastGettedTypes.UI)
            {
            }

            else if (selectorCom.RaycastGettedType == RaycastGettedTypes.Cell)
            {
                if (selectorCom.HaveAnySelectorUnit)
                {
                    if (!CellUnitsDataSystem.HaveAnyUnit(selectorCom.XyCurrentCell) || !CellUnitsDataSystem.IsVisibleUnit(PhotonNetwork.IsMasterClient, selectorCom.XyCurrentCell))
                    {
                        if (selectorCom.IsStartSelectedDirect)
                        {
                            if (!CellUnitsDataSystem.HaveAnyUnit(selectorCom.XyCurrentCell))
                                CellUnitViewSystem.ActiveSelectorVisionUnit(true, selectorCom.SelectedUnitType, selectorCom.XyCurrentCell);

                            selectorCom.XyPreviousVisionCell = selectorCom.XyCurrentCell;
                            selectorCom.IsStartSelectedDirect = false;
                        }
                        else
                        {
                            CellUnitViewSystem.ActiveSelectorVisionUnit(false, selectorCom.SelectedUnitType, selectorCom.XyPreviousVisionCell);
                            CellUnitViewSystem.ActiveSelectorVisionUnit(true, selectorCom.SelectedUnitType, selectorCom.XyCurrentCell);

                            selectorCom.XyPreviousVisionCell = selectorCom.XyCurrentCell;
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