using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using static Assets.Scripts.Main;
using static Assets.Scripts.Workers.CellBaseOperations;
using static Assets.Scripts.CellUnitWorker;
using static Assets.Scripts.PhotonPunRPC;
using static Assets.Scripts.Workers.SelectorWorker;
using Assets.Scripts.Workers;

internal sealed class SelectorSystem : SystemGeneralReduction
{
    private int[] XyPreviousCell { get => GetXy(SelectorCellTypes.Previous); set => SetXy(SelectorCellTypes.Previous, value); }
    private int[] XySelectedCell { get => GetXy(SelectorCellTypes.Selected); set => SetXy(SelectorCellTypes.Selected, value); }
    private int[] XyCurrentCell { get => GetXy(SelectorCellTypes.Current); set => SetXy(SelectorCellTypes.Current, value); }
    private int[] XyPreviousVisionCell { get => GetXy(SelectorCellTypes.PreviousVision); set => SetXy(SelectorCellTypes.PreviousVision, value); }

    private bool IsSelected { get => _eGM.SelectorEnt_SelectorCom.IsSelected; set => _eGM.SelectorEnt_SelectorCom.IsSelected = value; }

    public override void Run()
    {
        base.Run();

        if (_eGM.SelectorEnt_RayCom.IsGettedType(RaycastGettedTypes.UI))
        {
            if (_eGM.InputEnt_InputCom.IsClick)
            {
                _eGM.SelectorEnt_SelectorCom.CanShiftUnit = false;

                ClearAvailableCells();

                XyPreviousCell.Clean();
            }
        }

        else if (_eGM.SelectorEnt_RayCom.IsGettedType(RaycastGettedTypes.Cell))
        {
            if (_eGM.InputEnt_InputCom.IsClick)
            {
                if (_eGGUIM.DonerUIEnt_IsActivatedDictCom.IsActivated(Instance.IsMasterClient))
                {
                    if (_eGM.SelectorEnt_SelectorCom.CanExecuteStartClick)
                    {
                        XySelectedCell = XyCurrentCell;

                        if (!XyPreviousCell.Compare(XySelectedCell))
                            IsSelected = true;

                        XyPreviousCell = XySelectedCell;
                        _eGM.SelectorEnt_SelectorCom.CanExecuteStartClick = false;
                    }

                    else
                    {
                        if (!XySelectedCell.Compare(XyCurrentCell))
                            XyPreviousCell = XySelectedCell;

                        XySelectedCell = XyCurrentCell;
                        IsSelected = true;
                    }
                }

                else
                {
                    if (_eGM.SelectorEnt_UpgradeModTypeCom.IsUpgradeModType(UpgradeModTypes.Unit))
                    {
                        if (HaveAnyUnit(XyCurrentCell))
                        {
                            UpgradeUnitToMaster(XyCurrentCell);
                        }
                        else
                        {
                            _eGM.SelectorEnt_UpgradeModTypeCom.ResetUpgradeModType();
                        }
                    }

                    if (HaveAnySelectorUnit)
                    {
                        SetUniToMaster(XyCurrentCell, SelectorUnitType);
                    }

                    else if (_eGM.SelectorEnt_SelectorCom.CanExecuteStartClick)
                    {
                        XySelectedCell = XyCurrentCell;

                        if (!XyPreviousCell.Compare(XySelectedCell))
                            IsSelected = true;

                        if (HaveAnyUnit(XySelectedCell))
                        {
                            if (HaveOwner(XySelectedCell))
                            {
                                if (IsMine(XySelectedCell))
                                {
                                    if (IsMelee(XySelectedCell))
                                    {
                                        _eGM.PickMeleeEnt_AudioSourceCom.Play();
                                    }
                                    else
                                    {
                                        _eGM.PickArcherEnt_AudioSourceCom.Play();
                                    }

                                    if (HaveMinAmountSteps(XySelectedCell))
                                    {
                                        GetCells();

                                        _eGM.SelectorEnt_SelectorCom.CanShiftUnit = true;
                                    }
                                }
                            }
                        }
                        XyPreviousCell = XySelectedCell;
                        _eGM.SelectorEnt_SelectorCom.CanExecuteStartClick = false;
                    }

                    else
                    {
                        if (!XySelectedCell.Compare(XyCurrentCell))
                            XyPreviousCell = XySelectedCell;


                        XySelectedCell = XyCurrentCell;
                        IsSelected = true;


                        if (HaveAnyUnit(XySelectedCell))
                        {
                            if (HaveOwner(XySelectedCell))
                            {
                                if (IsMine(XySelectedCell))
                                {
                                    if (IsMelee(XySelectedCell))
                                    {
                                        _eGM.PickMeleeEnt_AudioSourceCom.Play();
                                    }
                                    else
                                    {
                                        _eGM.PickArcherEnt_AudioSourceCom.Play();
                                    }

                                    if (HaveMinAmountSteps(XySelectedCell))
                                    {
                                        GetCells();

                                        _eGM.SelectorEnt_SelectorCom.CanShiftUnit = true;
                                    }

                                    else
                                    {
                                        ClearAvailableCells();
                                        _eGM.SelectorEnt_SelectorCom.CanShiftUnit = false;
                                    }
                                }

                                else
                                {
                                    _eGM.SelectorEnt_SelectorCom.CanShiftUnit = false;

                                    if (TryFindCell(AvailableCellTypes.SimpleAttack, XySelectedCell))
                                    {
                                        AttackUnitToMaster(XyPreviousCell, XySelectedCell);
                                    }

                                    else if (TryFindCell(AvailableCellTypes.UniqueAttack, XySelectedCell))
                                    {
                                        AttackUnitToMaster(XyPreviousCell, XySelectedCell);
                                    }

                                    ClearAvailableCells();
                                }
                            }

                            else if (IsBot(XySelectedCell))
                            {
                                if (TryFindCell(AvailableCellTypes.SimpleAttack, XySelectedCell))
                                {
                                    AttackUnitToMaster(XyPreviousCell, XySelectedCell);
                                }

                                else if (TryFindCell(AvailableCellTypes.UniqueAttack, XySelectedCell))
                                {
                                    AttackUnitToMaster(XyPreviousCell, XySelectedCell);
                                }

                                ClearAvailableCells();
                            }
                        }

                        else
                        {
                            if (_eGM.SelectorEnt_SelectorCom.CanShiftUnit)
                            {
                                if (HaveAnyUnit(XyPreviousCell))
                                {
                                    if (HaveOwner(XyPreviousCell))
                                    {
                                        if (IsMine(XyPreviousCell))
                                        {
                                            if (HaveMinAmountSteps(XyPreviousCell))
                                            {
                                                if (TryFindCell(AvailableCellTypes.Shift, XySelectedCell))
                                                {
                                                    ShiftUnitToMaster(XyPreviousCell, XySelectedCell);
                                                }
                                            }
                                        }
                                    }
                                }
                                ClearAvailableCells();
                                _eGM.SelectorEnt_SelectorCom.CanShiftUnit = false;
                            }
                        }
                    }
                }
            }

            else
            {
                if (HaveAnySelectorUnit)
                {
                    if (!HaveAnyUnit(XyCurrentCell) || !IsActivated(Instance.IsMasterClient, XyCurrentCell))
                    {
                        if (_eGM.SelectorEnt_SelectorCom.IsStartSelectedDirect)
                        {
                            if (!HaveAnyUnit(XyCurrentCell))
                                ActiveSelectorVisionUnit(true, _eGM.SelectorEnt_UnitTypeCom.UnitType, XyCurrentCell);

                            XyPreviousVisionCell = XyCurrentCell;
                            _eGM.SelectorEnt_SelectorCom.IsStartSelectedDirect = false;
                        }
                        else
                        {
                            ActiveSelectorVisionUnit(false, _eGM.SelectorEnt_UnitTypeCom.UnitType, XyPreviousVisionCell);
                            ActiveSelectorVisionUnit(true, _eGM.SelectorEnt_UnitTypeCom.UnitType, XyCurrentCell);

                            XyPreviousVisionCell = XyCurrentCell;
                        }
                    }
                }
            }
        }

        else
        {
            if (_eGM.InputEnt_InputCom.IsClick)
            {
                _eGM.SelectorEnt_SelectorCom.CanShiftUnit = false;
                _eGM.SelectorEnt_SelectorCom.CanExecuteStartClick = true;

                IsSelected = false;

                ClearAvailableCells();

                SetEnabledUnit(false, XyPreviousVisionCell);
                _eGM.SelectorEnt_UnitTypeCom.UnitType = default;

                XyPreviousCell.Clean();

                _eGM.SelectorEnt_UpgradeModTypeCom.ResetUpgradeModType();
            }
        }
    }


    private void ClearAvailableCells()
    {
        SelectorWorker.ClearAvailableCells(AvailableCellTypes.Shift);
        SelectorWorker.ClearAvailableCells(AvailableCellTypes.SimpleAttack);
        SelectorWorker.ClearAvailableCells(AvailableCellTypes.UniqueAttack);
    }

    private void GetCells()
    {
        SetAllCells(AvailableCellTypes.Shift, GetCellsForShift(XySelectedCell));

        GetCellsForAttack(Instance.LocalPlayer, out var availableCellsSimpleAttack, out var availableCellsUniqueAttack, XySelectedCell);
        SetAllCells(AvailableCellTypes.SimpleAttack, availableCellsSimpleAttack);
        SetAllCells(AvailableCellTypes.UniqueAttack, availableCellsUniqueAttack);
    }
}