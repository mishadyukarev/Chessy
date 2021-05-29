using static MainGame;

internal sealed class SelectorSystem : RPCGeneralReduction
{
    private Names _nameManager;

    private int[] _xyPreviousVisionCell;

    private bool _canShiftUnit = false;
    private bool _canExecuteStartClick = true;
    private bool _isStartSelectedDirect = true;

    private int[] XyPreviousCell => _eGM.SelectorEntSelectorCom.XYpreviousCell;
    private int[] XySelectedCell => _eGM.SelectorEntSelectorCom.XYselectedCell;



    internal SelectorSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _nameManager = Instance.Names;

        _xyPreviousVisionCell = new int[_eGM.XYForArray];

        _eGM.SelectorEntSelectorCom.SetterUnitDelegate = IsSetted;
        _eGM.SelectorEntSelectorCom.AttackUnitAction = IsAttacked;
        _eGM.SelectorEntSelectorCom.ShiftUnitDelegate = SetIsShifted;
    }

    public override void Run()
    {
        base.Run();

        _sGM.TryInvokeRunSystem(nameof(RaySystem), _sGM.ForSelectorRunUpdateSystem);

        if (_eGM.RayComponentSelectorEnt.RaycastHit2D)
        {
            if (_eGM.RayComponentSelectorEnt.RaycastHit2D.collider.gameObject.tag == _nameManager.TAG_CELL)
            {
                _sGM.TryInvokeRunSystem(nameof(GetterCellSystem), _sGM.ForSelectorRunUpdateSystem);

                if (_eGM.SelectorEntSelectorCom.IsGettedCell)
                {
                    var xyCurrentCell = _eGM.SelectorEntSelectorCom.XYcurrentCell;

                    if (_eGM.InputEntityMouseClickComponent.IsClick)
                    {
                        if (_eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[Instance.IsMasterClient])
                        {
                            if (_canExecuteStartClick)
                            {
                                _eGM.CellBaseOperEnt_CellBaseOperCom.CopyXYinTo(xyCurrentCell,XySelectedCell);

                                if (!_eGM.CellBaseOperEnt_CellBaseOperCom.CompareXY(XyPreviousCell, XySelectedCell))
                                    ActivateSelector(true, XyPreviousCell, XySelectedCell);

                                _eGM.CellBaseOperEnt_CellBaseOperCom.CopyXYinTo(XySelectedCell, XyPreviousCell);
                                _canExecuteStartClick = false;

                            }

                            else
                            {
                                if (!_eGM.CellBaseOperEnt_CellBaseOperCom.CompareXY(XySelectedCell, xyCurrentCell))
                                    _eGM.CellBaseOperEnt_CellBaseOperCom.CopyXYinTo(XySelectedCell, XyPreviousCell);


                                _eGM.CellBaseOperEnt_CellBaseOperCom.CopyXYinTo(xyCurrentCell, XySelectedCell);
                                ActivateSelector(true, XyPreviousCell, XySelectedCell);
                            }
                        }

                        else
                        {
                            if (_eGM.SelectedUnitEntUnitTypeCom.HaveUnit)
                            {
                                if (!_eGM.CellEnvEnt_CellEnvCom(xyCurrentCell).HaveMountain && !_eGM.CellEnt_CellUnitCom(xyCurrentCell).HaveUnit)
                                {
                                    if (Instance.IsMasterClient && _eGM.CellEnt_CellBaseCom(xyCurrentCell).IsStarted(true))
                                        _photonPunRPC.SetUniToMaster(xyCurrentCell, _eGM.SelectedUnitEntUnitTypeCom.UnitType);

                                    else if (_eGM.CellEnt_CellBaseCom(xyCurrentCell).IsStarted(false))
                                        _photonPunRPC.SetUniToMaster(xyCurrentCell, _eGM.SelectedUnitEntUnitTypeCom.UnitType);

                                    else _eGM.SoundEntSoundCom.MistakeSoundAction();
                                }

                                else _eGM.SoundEntSoundCom.MistakeSoundAction();
                            }

                            else if (_canExecuteStartClick)
                            {
                                _eGM.CellBaseOperEnt_CellBaseOperCom.CopyXYinTo(xyCurrentCell, XySelectedCell);

                                if (!_eGM.CellBaseOperEnt_CellBaseOperCom.CompareXY(XyPreviousCell, XySelectedCell))
                                    ActivateSelector(true, XyPreviousCell, XySelectedCell);

                                if (_eGM.CellEnt_CellUnitCom(XySelectedCell).HaveUnit)
                                {
                                    if (_eGM.CellEnt_CellUnitCom(XySelectedCell).IsMine)
                                    {
                                        if (_eGM.CellEnt_CellUnitCom(XySelectedCell).AmountSteps >= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                                        {
                                            _eGM.SelectorEntSelectorCom.AvailableCellsForShift = _eGM.CellEnt_CellUnitCom(XySelectedCell).GetCellsForShift();
                                            _eGM.CellEnt_CellUnitCom(XySelectedCell).GetCellsForAttack(Instance.LocalPlayer, out _eGM.SelectorEntSelectorCom.AvailableCellsSimpleAttack, out _eGM.SelectorEntSelectorCom.AvailableCellsUniqueAttack);

                                            _canShiftUnit = true;
                                        }
                                    }
                                }

                                _eGM.CellBaseOperEnt_CellBaseOperCom.CopyXYinTo(XySelectedCell, XyPreviousCell);
                                _canExecuteStartClick = false;
                            }

                            else
                            {
                                if (!_eGM.CellBaseOperEnt_CellBaseOperCom.CompareXY(XySelectedCell, xyCurrentCell))
                                    _eGM.CellBaseOperEnt_CellBaseOperCom.CopyXYinTo(XySelectedCell, XyPreviousCell);


                                _eGM.CellBaseOperEnt_CellBaseOperCom.CopyXYinTo(xyCurrentCell, XySelectedCell);
                                ActivateSelector(true, XyPreviousCell, XySelectedCell);


                                if (_eGM.CellEnt_CellUnitCom(XySelectedCell).HaveUnit)
                                {
                                    if (_eGM.CellEnt_CellUnitCom(XySelectedCell).IsMine)
                                    {
                                        if (_eGM.CellEnt_CellUnitCom(XySelectedCell).HaveMinAmountSteps)
                                        {
                                            _eGM.SelectorEntSelectorCom.AvailableCellsForShift = _eGM.CellEnt_CellUnitCom(XySelectedCell).GetCellsForShift();
                                            _eGM.CellEnt_CellUnitCom(XySelectedCell).GetCellsForAttack
                                                (Instance.LocalPlayer, out _eGM.SelectorEntSelectorCom.AvailableCellsSimpleAttack, out _eGM.SelectorEntSelectorCom.AvailableCellsUniqueAttack);

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
                                        if (_eGM.CellEnt_CellUnitCom(XyPreviousCell).HaveMinAmountSteps)
                                        {
                                            if (_eGM.CellBaseOperEnt_CellBaseOperCom.TryFindCellInList(XySelectedCell, _eGM.SelectorEntSelectorCom.AvailableCellsSimpleAttack))
                                            {
                                                _photonPunRPC.AttackUnitToMaster(XyPreviousCell, XySelectedCell);
                                            }

                                            else if (_eGM.CellBaseOperEnt_CellBaseOperCom.TryFindCellInList(XySelectedCell, _eGM.SelectorEntSelectorCom.AvailableCellsUniqueAttack))
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
                                        if (_eGM.CellEnt_CellUnitCom(XyPreviousCell).IsMine)
                                        {
                                            if (_eGM.CellEnt_CellUnitCom(XyPreviousCell).HaveMinAmountSteps)
                                            {
                                                if (_eGM.CellBaseOperEnt_CellBaseOperCom.TryFindCellInList(XySelectedCell, _eGM.SelectorEntSelectorCom.AvailableCellsForShift))
                                                {
                                                    _photonPunRPC.ShiftUnitToMaster(XyPreviousCell, XySelectedCell);
                                                }
                                            }
                                        }

                                        ActivateSelector(false, XyPreviousCell, XySelectedCell);

                                        ClearAvailableCells();

                                        _canShiftUnit = false;

                                    }
                                }
                            }
                        }
                    }

                    else
                    {
                        if (_eGM.SelectedUnitEntUnitTypeCom.HaveUnit)
                        {
                            if (!_eGM.CellEnt_CellUnitCom(xyCurrentCell).HaveUnit)
                            {
                                if (_isStartSelectedDirect)
                                {
                                    if (!_eGM.CellEnt_CellUnitCom(xyCurrentCell).HaveUnit)
                                        _eGM.CellEnt_CellUnitCom(xyCurrentCell).ActiveVisionCell(true, _eGM.SelectedUnitEntUnitTypeCom.UnitType, Instance.LocalPlayer);

                                    _eGM.CellBaseOperEnt_CellBaseOperCom.CopyXYinTo(xyCurrentCell, _xyPreviousVisionCell);
                                    _isStartSelectedDirect = false;
                                }
                                else
                                {
                                    if (!_eGM.CellEnt_CellUnitCom(_xyPreviousVisionCell).HaveUnit)
                                        _eGM.CellEnt_CellUnitCom(_xyPreviousVisionCell).ActiveVisionCell(false, _eGM.SelectedUnitEntUnitTypeCom.UnitType, Instance.LocalPlayer);

                                    _eGM.CellEnt_CellUnitCom(xyCurrentCell).ActiveVisionCell(true, _eGM.SelectedUnitEntUnitTypeCom.UnitType, Instance.LocalPlayer);
                                    _eGM.CellBaseOperEnt_CellBaseOperCom.CopyXYinTo(xyCurrentCell, _xyPreviousVisionCell);
                                }

                            }
                        }
                    }
                }
            }

            else if (_eGM.RayComponentSelectorEnt.RaycastHit2D.collider.gameObject.tag == _nameManager.TAG_BACKGROUND)
            {
                if (_eGM.InputEntityMouseClickComponent.IsClick)
                {
                    _canShiftUnit = false;
                    _canExecuteStartClick = true;

                    ActivateSelector(false, XyPreviousCell, XySelectedCell);

                    ClearAvailableCells();

                    _eGM.CellEnt_CellUnitCom(_xyPreviousVisionCell).ActiveVisionCell(false, _eGM.SelectedUnitEntUnitTypeCom.UnitType, Instance.LocalPlayer);
                    _eGM.SelectedUnitEntUnitTypeCom.UnitType = default;

                    _eGM.CellBaseOperEnt_CellBaseOperCom.CleanXY(XyPreviousCell);
                }
            }
        }
    }


    #region Methods

    private void ActivateSelector(in bool isActive, in int[] xyPreviousCell, in int[] xySelectedCell)
    {
        _eGM.CellEnt_CellBaseCom(xyPreviousCell).IsSelected = false;
        _eGM.CellEnt_CellBaseCom(xySelectedCell).IsSelected = isActive;
    }

    private void ClearAvailableCells()
    {
        _eGM.SelectorEntSelectorCom.AvailableCellsForShift.Clear();
        _eGM.SelectorEntSelectorCom.AvailableCellsSimpleAttack.Clear();
        _eGM.SelectorEntSelectorCom.AvailableCellsUniqueAttack.Clear();
    }


    #region Delegates

    private void IsSetted()
    {
        _eGM.SelectedUnitEntUnitTypeCom.UnitType = default;
        _isStartSelectedDirect = true;
    }

    private void IsAttacked()
    {
        _eGM.SelectorEntSelectorCom.AvailableCellsForShift.Clear();
        _eGM.SelectorEntSelectorCom.AvailableCellsSimpleAttack.Clear();
        _eGM.SelectorEntSelectorCom.AvailableCellsUniqueAttack.Clear();
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