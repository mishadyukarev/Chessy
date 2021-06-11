using static Main;

internal sealed class SelectorSystem : RPCGeneralSystemReduction
{
    private Names _nameManager;

    private int[] _xyPreviousVisionCell;

    private bool _canShiftUnit = false;
    private bool _canExecuteStartClick = true;
    private bool _isStartSelectedDirect = true;

    private int[] XyPreviousCell => _eGM.SelectorEnt_SelectorCom.XYpreviousCell;
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;



    internal SelectorSystem()
    {
        _nameManager = Instance.Names;
    }

    public override void Init()
    {
        base.Init();

        _xyPreviousVisionCell = new int[_eGM.XyForArray];
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

                _cM.CellBaseOperations.CleanXY(XyPreviousCell);

                //ActivateSelector(false, XyPreviousCell, XySelectedCell);
            }
        }
        else
        {
            if (_eGM.SelectorEnt_RayCom.RaycastHit2D)
            {
                if (_eGM.SelectorEnt_RayCom.RaycastHit2D.collider.gameObject.tag == _nameManager.TAG_CELL)
                {
                    _sGM.TryInvokeRunSystem(nameof(GetterCellSystem), _sGM.ForSelectorRunUpdateSystem);

                    if (_eGM.SelectorEnt_SelectorCom.IsGettedCell)
                    {
                        var xyCurrentCell = _eGM.SelectorEnt_SelectorCom.XYcurrentCell;

                        if (_eGM.InputEnt_InputCom.IsClick)
                        {
                            if (_eGM.DonerEnt_IsActivatedDictCom.IsActivated(Instance.IsMasterClient))
                            {
                                if (_canExecuteStartClick)
                                {
                                    _cM.CellBaseOperations.CopyXYinTo(xyCurrentCell, XySelectedCell);

                                    if (!_cM.CellBaseOperations.CompareXY(XyPreviousCell, XySelectedCell))
                                        ActivateSelector(true, XyPreviousCell, XySelectedCell);

                                    _cM.CellBaseOperations.CopyXYinTo(XySelectedCell, XyPreviousCell);
                                    _canExecuteStartClick = false;

                                }

                                else
                                {
                                    if (!_cM.CellBaseOperations.CompareXY(XySelectedCell, xyCurrentCell))
                                        _cM.CellBaseOperations.CopyXYinTo(XySelectedCell, XyPreviousCell);


                                    _cM.CellBaseOperations.CopyXYinTo(xyCurrentCell, XySelectedCell);
                                    ActivateSelector(true, XyPreviousCell, XySelectedCell);
                                }
                            }

                            else
                            {
                                if (_eGM.SelectorEnt_UnitTypeCom.HaveUnit)
                                {
                                    if (!_eGM.CellEnvEnt_CellEnvCom(xyCurrentCell).HaveMountain && !_eGM.CellUnitEnt_UnitTypeCom(xyCurrentCell).HaveUnit)
                                    {
                                        if (Instance.IsMasterClient && _eGM.CellEnt_CellBaseCom(xyCurrentCell).IsStarted(true))
                                            _photonPunRPC.SetUniToMaster(xyCurrentCell, _eGM.SelectorEnt_UnitTypeCom.UnitType);

                                        else if (_eGM.CellEnt_CellBaseCom(xyCurrentCell).IsStarted(false))
                                            _photonPunRPC.SetUniToMaster(xyCurrentCell, _eGM.SelectorEnt_UnitTypeCom.UnitType);

                                        else _eGM.SoundEntSoundCom.MistakeSoundAction();
                                    }

                                    else _eGM.SoundEntSoundCom.MistakeSoundAction();
                                }

                                else if (_canExecuteStartClick)
                                {
                                    _cM.CellBaseOperations.CopyXYinTo(xyCurrentCell, XySelectedCell);

                                    if (!_cM.CellBaseOperations.CompareXY(XyPreviousCell, XySelectedCell))
                                        ActivateSelector(true, XyPreviousCell, XySelectedCell);

                                    if (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).HaveUnit)
                                    {
                                        if (_eGM.CellUnitEnt_CellOwnerCom(XySelectedCell).IsMine)
                                        {
                                            if (_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).HaveMinAmountSteps)
                                            {
                                                _eGM.SelectorEnt_SelectorCom.AvailableCellsForShift = _cM.CellUnitWorker.GetCellsForShift(XySelectedCell);
                                                _cM.CellUnitWorker.GetCellsForAttack(Instance.LocalPlayer, out _eGM.SelectorEnt_SelectorCom.AvailableCellsSimpleAttack, out _eGM.SelectorEnt_SelectorCom.AvailableCellsUniqueAttack, XySelectedCell);

                                                _canShiftUnit = true;
                                            }
                                        }
                                    }

                                    _cM.CellBaseOperations.CopyXYinTo(XySelectedCell, XyPreviousCell);
                                    _canExecuteStartClick = false;
                                }

                                else
                                {
                                    if (!_cM.CellBaseOperations.CompareXY(XySelectedCell, xyCurrentCell))
                                        _cM.CellBaseOperations.CopyXYinTo(XySelectedCell, XyPreviousCell);


                                    _cM.CellBaseOperations.CopyXYinTo(xyCurrentCell, XySelectedCell);
                                    ActivateSelector(true, XyPreviousCell, XySelectedCell);


                                    if (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).HaveUnit)
                                    {
                                        if (_eGM.CellUnitEnt_CellOwnerCom(XySelectedCell).IsMine)
                                        {
                                            if (_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).HaveMinAmountSteps)
                                            {
                                                _eGM.SelectorEnt_SelectorCom.AvailableCellsForShift = _cM.CellUnitWorker.GetCellsForShift(XySelectedCell);
                                                _cM.CellUnitWorker.GetCellsForAttack(Instance.LocalPlayer, out _eGM.SelectorEnt_SelectorCom.AvailableCellsSimpleAttack, out _eGM.SelectorEnt_SelectorCom.AvailableCellsUniqueAttack, XySelectedCell);

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
                                            if (_eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).HaveMinAmountSteps)
                                            {
                                                if (_cM.CellBaseOperations.TryFindCellInList(XySelectedCell, _eGM.SelectorEnt_SelectorCom.AvailableCellsSimpleAttack))
                                                {
                                                    _photonPunRPC.AttackUnitToMaster(XyPreviousCell, XySelectedCell);
                                                }

                                                else if (_cM.CellBaseOperations.TryFindCellInList(XySelectedCell, _eGM.SelectorEnt_SelectorCom.AvailableCellsUniqueAttack))
                                                {
                                                    _photonPunRPC.AttackUnitToMaster(XyPreviousCell, XySelectedCell);
                                                }
                                            }
                                        }
                                    }

                                    else
                                    {
                                        if (_canShiftUnit)
                                        {
                                            if (_eGM.CellUnitEnt_UnitTypeCom(XyPreviousCell).HaveUnit && _eGM.CellUnitEnt_CellOwnerCom(XyPreviousCell).IsMine)
                                            {
                                                if (_eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).HaveMinAmountSteps)
                                                {
                                                    if (_cM.CellBaseOperations.TryFindCellInList(XySelectedCell, _eGM.SelectorEnt_SelectorCom.AvailableCellsForShift))
                                                    {
                                                        _photonPunRPC.ShiftUnitToMaster(XyPreviousCell, XySelectedCell);
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
                                if (!_eGM.CellUnitEnt_UnitTypeCom(xyCurrentCell).HaveUnit)
                                {
                                    if (_isStartSelectedDirect)
                                    {
                                        if (!_eGM.CellUnitEnt_UnitTypeCom(xyCurrentCell).HaveUnit)
                                            _eGM.CellUnitEnt_CellUnitCom(xyCurrentCell).EnableSR(true, _eGM.SelectorEnt_UnitTypeCom.UnitType, Instance.LocalPlayer);

                                        _cM.CellBaseOperations.CopyXYinTo(xyCurrentCell, _xyPreviousVisionCell);
                                        _isStartSelectedDirect = false;
                                    }
                                    else
                                    {
                                        if (!_eGM.CellUnitEnt_UnitTypeCom(_xyPreviousVisionCell).HaveUnit)
                                            _eGM.CellUnitEnt_CellUnitCom(_xyPreviousVisionCell).EnableSR(false, _eGM.SelectorEnt_UnitTypeCom.UnitType, Instance.LocalPlayer);

                                        _eGM.CellUnitEnt_CellUnitCom(xyCurrentCell).EnableSR(true, _eGM.SelectorEnt_UnitTypeCom.UnitType, Instance.LocalPlayer);
                                        _cM.CellBaseOperations.CopyXYinTo(xyCurrentCell, _xyPreviousVisionCell);
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

                        ActivateSelector(false, XyPreviousCell, XySelectedCell);

                        ClearAvailableCells();

                        _eGM.CellUnitEnt_CellUnitCom(_xyPreviousVisionCell).EnableSR(false, _eGM.SelectorEnt_UnitTypeCom.UnitType, Instance.LocalPlayer);
                        _eGM.SelectorEnt_UnitTypeCom.UnitType = default;

                        _cM.CellBaseOperations.CleanXY(XyPreviousCell);
                    }
                }
            }
        }
    }


    #region Methods

    private void ActivateSelector(in bool isActive, in int[] xyPreviousCell, in int[] xySelectedCell)
    {
        _eGM.CellEnt_CellBaseCom(xyPreviousCell).IsSelected = false;
        _eGM.CellEnt_CellBaseCom(xySelectedCell).IsSelected = isActive;
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
        _eGM.SelectorEnt_UnitTypeCom.UnitType = default;
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