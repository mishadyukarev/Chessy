using Assets.Scripts;
using Assets.Scripts.Abstractions;
using Assets.Scripts.Abstractions.Enums;
using static Assets.Scripts.Main;
using static Assets.Scripts.Abstractions.ValuesConst;
using static Assets.Scripts.Abstractions.NameConst;
using static Assets.Scripts.Static.CellBaseOperations;

internal sealed class SelectorSystem : RPCGeneralSystemReduction
{
    private int[] _xyPreviousVisionCell;

    private bool _canShiftUnit = false;
    private bool _canExecuteStartClick = true;
    private bool _isStartSelectedDirect = true;

    private int[] XyPreviousCell => _eGM.SelectorEnt_SelectorCom.XyPreviousCell;
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;
    private bool IsSelected { get => _eGM.SelectorEnt_SelectorCom.IsSelected; set => _eGM.SelectorEnt_SelectorCom.IsSelected = value; }


    public override void Init()
    {
        base.Init();

        _xyPreviousVisionCell = new int[XY_FOR_ARRAY];
        _eGM.SelectorEnt_SelectorCom.SetterUnitDelegate = IsSetted;
        _eGM.SelectorEnt_SelectorCom.AttackUnitAction = IsAttacked;
        _eGM.SelectorEnt_SelectorCom.ShiftUnitDelegate = SetIsShifted;
    }

    public override void Run()
    {
        base.Run();

        _sGM.TryInvokeRunSystem(nameof(RaySystem), _sGM.ForSelectorRunUpdateSystem);

        if (_eGM.SelectorEnt_RayCom.IsUI)
        {
            if (_eGM.InputEnt_InputCom.IsClick)
            {
                _canShiftUnit = false;

                ClearAvailableCells();

                CleanXY(XyPreviousCell);
            }
        }
        else
        {
            if (_eGM.SelectorEnt_RayCom.RaycastHit2D)
            {
                if (_eGM.SelectorEnt_RayCom.RaycastHit2D.collider.gameObject.tag == TAG_CELL)
                {
                    _sGM.TryInvokeRunSystem(nameof(GetterCellSystem), _sGM.ForSelectorRunUpdateSystem);

                    if (_eGM.SelectorEnt_SelectorCom.IsGettedCell)
                    {
                        var xyCurrentCell = _eGM.SelectorEnt_SelectorCom.XyCurrentCell;

                        if (_eGM.InputEnt_InputCom.IsClick)
                        {
                            if (_eGM.SelectorEnt_SelectorCom.UpgradeModType == UpgradeModTypes.Unit)
                            {
                                _photonPunRPC.UpgradeUnitToMaster(xyCurrentCell);
                            }

                            else if (_eGM.DonerEnt_IsActivatedDictCom.IsActivated(Instance.IsMasterClient))
                            {
                                if (_canExecuteStartClick)
                                {
                                    CopyXyInTo(xyCurrentCell, XySelectedCell);

                                    if (!CompareXy(XyPreviousCell, XySelectedCell))
                                        IsSelected = true;

                                    CopyXyInTo(XySelectedCell, XyPreviousCell);
                                    _canExecuteStartClick = false;
                                }

                                else
                                {
                                    if (!CompareXy(XySelectedCell, xyCurrentCell))
                                        CopyXyInTo(XySelectedCell, XyPreviousCell);


                                    CopyXyInTo(xyCurrentCell, XySelectedCell);
                                    IsSelected = true;
                                }
                            }

                            else
                            {
                                if (_eGM.SelectorEnt_UnitTypeCom.HaveUnit)
                                {
                                    if (!_eGM.CellEnvEnt_CellEnvCom(xyCurrentCell).HaveEnvironment(EnvironmentTypes.Mountain) && !_eGM.CellUnitEnt_UnitTypeCom(xyCurrentCell).HaveUnit)
                                    {
                                        if (Instance.IsMasterClient && _eGM.CellEnt_CellBaseCom(xyCurrentCell).IsStartedCell(true))
                                            _photonPunRPC.SetUniToMaster(xyCurrentCell, _eGM.SelectorEnt_UnitTypeCom.UnitType);

                                        else if (_eGM.CellEnt_CellBaseCom(xyCurrentCell).IsStartedCell(false))
                                            _photonPunRPC.SetUniToMaster(xyCurrentCell, _eGM.SelectorEnt_UnitTypeCom.UnitType);

                                        else _photonPunRPC.SoundToGeneral(Instance.LocalPlayer, SoundEffectTypes.Mistake);
                                    }

                                    else _photonPunRPC.SoundToGeneral(Instance.LocalPlayer, SoundEffectTypes.Mistake);
                                }

                                else if (_canExecuteStartClick)
                                {
                                    CopyXyInTo(xyCurrentCell, XySelectedCell);

                                    if (!CompareXy(XyPreviousCell, XySelectedCell))
                                        IsSelected = true;

                                    if (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).HaveUnit)
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
                                                    _eGM.SelectorEnt_SelectorCom.AvailableCellsForShift = CellUnitWorker.GetCellsForShift(XySelectedCell);
                                                    CellUnitWorker.GetCellsForAttack(Instance.LocalPlayer, out _eGM.SelectorEnt_SelectorCom.AvailableCellsSimpleAttack, out _eGM.SelectorEnt_SelectorCom.AvailableCellsUniqueAttack, XySelectedCell);

                                                    _canShiftUnit = true;
                                                }
                                            }
                                        }
                                    }

                                    CopyXyInTo(XySelectedCell, XyPreviousCell);
                                    _canExecuteStartClick = false;
                                }

                                else
                                {
                                    if (!CompareXy(XySelectedCell, xyCurrentCell))
                                        CopyXyInTo(XySelectedCell, XyPreviousCell);


                                    CopyXyInTo(xyCurrentCell, XySelectedCell);
                                    IsSelected = true;


                                    if (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).HaveUnit)
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
                                                    _eGM.SelectorEnt_SelectorCom.AvailableCellsForShift = CellUnitWorker.GetCellsForShift(XySelectedCell);
                                                    CellUnitWorker.GetCellsForAttack(Instance.LocalPlayer, out _eGM.SelectorEnt_SelectorCom.AvailableCellsSimpleAttack, out _eGM.SelectorEnt_SelectorCom.AvailableCellsUniqueAttack, XySelectedCell);

                                                    _canShiftUnit = true;
                                                }

                                                else
                                                {
                                                    ClearAvailableCells();
                                                    _canShiftUnit = false;
                                                }
                                            }

                                            else
                                            {
                                                ClearAvailableCells();
                                                _canShiftUnit = false;

                                                if (TryFindCellInList(XySelectedCell, _eGM.SelectorEnt_SelectorCom.AvailableCellsSimpleAttack))
                                                {
                                                    _photonPunRPC.AttackUnitToMaster(XyPreviousCell, XySelectedCell);
                                                }

                                                else if (TryFindCellInList(XySelectedCell, _eGM.SelectorEnt_SelectorCom.AvailableCellsUniqueAttack))
                                                {
                                                    _photonPunRPC.AttackUnitToMaster(XyPreviousCell, XySelectedCell);
                                                }
                                            }
                                        }

                                        else if (_eGM.CellUnitEnt_CellOwnerBotCom(XySelectedCell).HaveBot)
                                        {
                                            if (TryFindCellInList(XySelectedCell, _eGM.SelectorEnt_SelectorCom.AvailableCellsSimpleAttack))
                                            {
                                                _photonPunRPC.AttackUnitToMaster(XyPreviousCell, XySelectedCell);
                                            }

                                            else if (TryFindCellInList(XySelectedCell, _eGM.SelectorEnt_SelectorCom.AvailableCellsUniqueAttack))
                                            {
                                                _photonPunRPC.AttackUnitToMaster(XyPreviousCell, XySelectedCell);
                                            }

                                            ClearAvailableCells();
                                        }
                                    }

                                    else
                                    {
                                        if (_canShiftUnit)
                                        {
                                            if (_eGM.CellUnitEnt_UnitTypeCom(XyPreviousCell).HaveUnit)
                                            {
                                                if (_eGM.CellUnitEnt_CellOwnerCom(XyPreviousCell).HaveOwner)
                                                {
                                                    if (_eGM.CellUnitEnt_CellOwnerCom(XyPreviousCell).IsMine)
                                                    {
                                                        if (_eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).HaveMinAmountSteps)
                                                        {
                                                            if (TryFindCellInList(XySelectedCell, _eGM.SelectorEnt_SelectorCom.AvailableCellsForShift))
                                                            {
                                                                _photonPunRPC.ShiftUnitToMaster(XyPreviousCell, XySelectedCell);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            ClearAvailableCells();
                                            _canShiftUnit = false;
                                        }
                                    }
                                }
                            }
                        }

                        else
                        {
                            if (_eGM.SelectorEnt_UnitTypeCom.HaveUnit)
                            {
                                if (!_eGM.CellUnitEnt_UnitTypeCom(xyCurrentCell).HaveUnit || !_eGM.CellUnitEnt_ActivatedForPlayersCom(xyCurrentCell).IsActivated(Instance.IsMasterClient) /*IsActivatedUnitDict[Instance.IsMasterClient]*/)
                                {
                                    if (_isStartSelectedDirect)
                                    {
                                        if (!_eGM.CellUnitEnt_UnitTypeCom(xyCurrentCell).HaveUnit)
                                            _eGM.CellUnitEnt_CellUnitCom(xyCurrentCell).EnablePlayerSR(true, _eGM.SelectorEnt_UnitTypeCom.UnitType, Instance.LocalPlayer);

                                        CopyXyInTo(xyCurrentCell, _xyPreviousVisionCell);
                                        _isStartSelectedDirect = false;
                                    }
                                    else
                                    {
                                        //if (!_eGM.CellUnitEnt_UnitTypeCom(_xyPreviousVisionCell).HaveUnit)
                                            _eGM.CellUnitEnt_CellUnitCom(_xyPreviousVisionCell).EnablePlayerSR(false, _eGM.SelectorEnt_UnitTypeCom.UnitType, Instance.LocalPlayer);

                                        _eGM.CellUnitEnt_CellUnitCom(xyCurrentCell).EnablePlayerSR(true, _eGM.SelectorEnt_UnitTypeCom.UnitType, Instance.LocalPlayer);
                                        CopyXyInTo(xyCurrentCell, _xyPreviousVisionCell);
                                    }

                                }
                            }
                        }
                    }
                }

                else
                {
                    if (_eGM.InputEnt_InputCom.IsClick)
                    {
                        _canShiftUnit = false;
                        _canExecuteStartClick = true;

                        IsSelected = false;

                        ClearAvailableCells();

                        _eGM.CellUnitEnt_CellUnitCom(_xyPreviousVisionCell).EnablePlayerSR(false, _eGM.SelectorEnt_UnitTypeCom.UnitType, Instance.LocalPlayer);
                        _eGM.SelectorEnt_UnitTypeCom.ResetUnit();

                        CleanXY(XyPreviousCell);

                        _eGM.SelectorEnt_SelectorCom.UpgradeModType = UpgradeModTypes.None;
                    }
                }
            }
        }
    }


    #region Methods

    private void ActivateSelector(in bool isActive)
    {
        _eGM.SelectorEnt_SelectorCom.IsSelected = isActive;
    }

    private void ClearAvailableCells()
    {
        _eGM.SelectorEnt_SelectorCom.AvailableCellsForShift.Clear();
        _eGM.SelectorEnt_SelectorCom.AvailableCellsSimpleAttack.Clear();
        _eGM.SelectorEnt_SelectorCom.AvailableCellsUniqueAttack.Clear();
    }


    #region Delegates

    private void IsSetted()
    {
        _eGM.SelectorEnt_UnitTypeCom.ResetUnit();
        _isStartSelectedDirect = true;
    }

    private void IsAttacked()
    {
        _eGM.SelectorEnt_SelectorCom.AvailableCellsForShift.Clear();
        _eGM.SelectorEnt_SelectorCom.AvailableCellsSimpleAttack.Clear();
        _eGM.SelectorEnt_SelectorCom.AvailableCellsUniqueAttack.Clear();
    }

    private void SetIsShifted()
    {

        //ActivateSelectorAndSelectorVision(false, in _xyPreviousCell, in _xySelectedCell);
        //_supportVisionComponentRef.Unref().SetWayInvoke(false, _xyAvailableCells);
        //_canShiftUnit = false;

        //_photonPunRPC.ContainerRPC.SetResetShifted(false);

    }

    #endregion

    #endregion
}