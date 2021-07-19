using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using static Assets.Scripts.Main;
using static Assets.Scripts.Static.CellBaseOperations;

internal sealed class SelectorSystem : RPCGeneralSystemReduction
{
    private int[] XyPreviousCell { get => _eGM.SelectorEnt_SelectorCom.GetXy(SelectorCellTypes.Previous); set => _eGM.SelectorEnt_SelectorCom.SetXy(SelectorCellTypes.Previous, value); }
    private int[] XySelectedCell { get => _eGM.SelectorEnt_SelectorCom.GetXy(SelectorCellTypes.Selected); set => _eGM.SelectorEnt_SelectorCom.SetXy(SelectorCellTypes.Selected, value); }
    private int[] XyCurrentCell { get => _eGM.SelectorEnt_SelectorCom.GetXy(SelectorCellTypes.Current); set => _eGM.SelectorEnt_SelectorCom.SetXy(SelectorCellTypes.Current, value); }
    private int[] XyPreviousVisionCell { get => _eGM.SelectorEnt_SelectorCom.GetXy(SelectorCellTypes.PreviousVision); set => _eGM.SelectorEnt_SelectorCom.SetXy(SelectorCellTypes.PreviousVision, value); }

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
                if (_eGM.DonerUIEnt_IsActivatedDictCom.IsActivated(Instance.IsMasterClient))
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
                        if (_eGM.CellUnitEnt_UnitTypeCom(XyCurrentCell).HaveAnyUnit)
                        {
                            _photonPunRPC.UpgradeUnitToMaster(XyCurrentCell);
                        }
                        else
                        {
                            _eGM.SelectorEnt_UpgradeModTypeCom.ResetUpgradeModType();
                        }
                    }

                    if (_eGM.SelectorEnt_UnitTypeCom.HaveAnyUnit)
                    {
                        _photonPunRPC.SetUniToMaster(XyCurrentCell, _eGM.SelectorEnt_UnitTypeCom.UnitType);
                    }

                    else if (_eGM.SelectorEnt_SelectorCom.CanExecuteStartClick)
                    {
                        XySelectedCell = XyCurrentCell;

                        if (!XyPreviousCell.Compare(XySelectedCell))
                            IsSelected = true;

                        if (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).HaveAnyUnit)
                        {
                            if (_eGM.CellUnitEnt_CellOwnerCom(XySelectedCell).HaveOwner)
                            {
                                if (_eGM.CellUnitEnt_CellOwnerCom(XySelectedCell).IsMine)
                                {
                                    if (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).IsMelee)
                                    {
                                        _eGM.PickMeleeEnt_AudioSourceCom.Play();
                                    }
                                    else
                                    {
                                        _eGM.PickArcherEnt_AudioSourceCom.Play();
                                    }

                                    if (_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).HaveMinAmountSteps)
                                    {
                                        _eGM.SelectorEnt_SelectorCom.GetCellsForShift(XySelectedCell);
                                        _eGM.SelectorEnt_SelectorCom.GetCellsForAllAttack(Instance.LocalPlayer, XySelectedCell);

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


                        if (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).HaveAnyUnit)
                        {
                            if (_eGM.CellUnitEnt_CellOwnerCom(XySelectedCell).HaveOwner)
                            {
                                if (_eGM.CellUnitEnt_CellOwnerCom(XySelectedCell).IsMine)
                                {
                                    if (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).IsMelee)
                                    {
                                        _eGM.PickMeleeEnt_AudioSourceCom.Play();
                                    }
                                    else
                                    {
                                        _eGM.PickArcherEnt_AudioSourceCom.Play();
                                    }

                                    if (_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).HaveMinAmountSteps)
                                    {
                                        _eGM.SelectorEnt_SelectorCom.GetCellsForShift(XySelectedCell);
                                        _eGM.SelectorEnt_SelectorCom.GetCellsForAllAttack(Instance.LocalPlayer, XySelectedCell);

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

                                    if (_eGM.SelectorEnt_SelectorCom.TryFindCell(AvailableCellTypes.SimpleAttack, XySelectedCell))
                                    {
                                        _photonPunRPC.AttackUnitToMaster(XyPreviousCell, XySelectedCell);
                                    }

                                    else if (_eGM.SelectorEnt_SelectorCom.TryFindCell(AvailableCellTypes.UniqueAttack, XySelectedCell))
                                    {
                                        _photonPunRPC.AttackUnitToMaster(XyPreviousCell, XySelectedCell);
                                    }

                                    ClearAvailableCells();
                                }
                            }

                            else if (_eGM.CellUnitEnt_CellOwnerBotCom(XySelectedCell).HaveBot)
                            {
                                if (_eGM.SelectorEnt_SelectorCom.TryFindCell(AvailableCellTypes.SimpleAttack, XySelectedCell))
                                {
                                    _photonPunRPC.AttackUnitToMaster(XyPreviousCell, XySelectedCell);
                                }

                                else if (_eGM.SelectorEnt_SelectorCom.TryFindCell(AvailableCellTypes.UniqueAttack, XySelectedCell))
                                {
                                    _photonPunRPC.AttackUnitToMaster(XyPreviousCell, XySelectedCell);
                                }

                                ClearAvailableCells();
                            }
                        }

                        else
                        {
                            if (_eGM.SelectorEnt_SelectorCom.CanShiftUnit)
                            {
                                if (_eGM.CellUnitEnt_UnitTypeCom(XyPreviousCell).HaveAnyUnit)
                                {
                                    if (_eGM.CellUnitEnt_CellOwnerCom(XyPreviousCell).HaveOwner)
                                    {
                                        if (_eGM.CellUnitEnt_CellOwnerCom(XyPreviousCell).IsMine)
                                        {
                                            if (_eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).HaveMinAmountSteps)
                                            {
                                                if (_eGM.SelectorEnt_SelectorCom.TryFindCell(AvailableCellTypes.Shift, XySelectedCell))
                                                {
                                                    _photonPunRPC.ShiftUnitToMaster(XyPreviousCell, XySelectedCell);
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
                if (_eGM.SelectorEnt_UnitTypeCom.HaveAnyUnit)
                {
                    if (!_eGM.CellUnitEnt_UnitTypeCom(XyCurrentCell).HaveAnyUnit || !_eGM.CellUnitEnt_ActivatedForPlayersCom(XyCurrentCell).IsActivated(Instance.IsMasterClient) /*IsActivatedUnitDict[Instance.IsMasterClient]*/)
                    {
                        if (_eGM.SelectorEnt_SelectorCom.IsStartSelectedDirect)
                        {
                            if (!_eGM.CellUnitEnt_UnitTypeCom(XyCurrentCell).HaveAnyUnit)
                                _eGM.CellUnitEnt_CellUnitCom(XyCurrentCell).EnablePlayerSRAndSetColor(_eGM.SelectorEnt_UnitTypeCom.UnitType, Instance.LocalPlayer);

                            XyPreviousVisionCell = XyCurrentCell;
                            _eGM.SelectorEnt_SelectorCom.IsStartSelectedDirect = false;
                        }
                        else
                        {
                            _eGM.CellUnitEnt_CellUnitCom(XyPreviousVisionCell).SwitchSR(false, _eGM.SelectorEnt_UnitTypeCom.UnitType);

                            _eGM.CellUnitEnt_CellUnitCom(XyCurrentCell).EnablePlayerSRAndSetColor(_eGM.SelectorEnt_UnitTypeCom.UnitType, Instance.LocalPlayer);

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

                _eGM.CellUnitEnt_CellUnitCom(XyPreviousVisionCell).SwitchSR(false, _eGM.SelectorEnt_UnitTypeCom.UnitType);
                _eGM.SelectorEnt_UnitTypeCom.ResetUnit();

                XyPreviousCell.Clean();

                _eGM.SelectorEnt_UpgradeModTypeCom.ResetUpgradeModType();
            }
        }
    }


    private void ClearAvailableCells()
    {
        _eGM.SelectorEnt_SelectorCom.ClearAvailableCells(AvailableCellTypes.Shift);
        _eGM.SelectorEnt_SelectorCom.ClearAvailableCells(AvailableCellTypes.SimpleAttack);
        _eGM.SelectorEnt_SelectorCom.ClearAvailableCells(AvailableCellTypes.UniqueAttack);
    }
}