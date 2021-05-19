using Leopotam.Ecs;
using Photon.Pun;
using static MainGame;

internal sealed class SelectorSystem : CellGeneralReduction, IEcsRunSystem
{
    private PhotonPunRPC _photonPunRPC = default;
    private Names _nameManager;
    private SystemsGeneralManager _systemsGeneralManager;


    private int[] _xyPreviousVisionCell = default;

    private bool _canShiftUnit = false;
    private bool _canExecuteStartClick = true;
    private bool _isStartSelectedDirect = true;



    private EcsComponentRef<SoundComponent> _soundComponentRef = default;


    private int[] _xyPreviousCell => _eGM.SelectorESelectorC.XYpreviousCell;
    private int[] _xySelectedCell => _eGM.SelectorESelectorC.XYselectedCell;



    internal SelectorSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonPunRPC = InstanceGame.PhotonGameManager.PhotonPunRPC;
        _nameManager = InstanceGame.Names;
        _systemsGeneralManager = eCSmanager.SystemsGeneralManager;

        _soundComponentRef = eCSmanager.EntitiesGeneralManager.SoundComponentRef;

        _xyPreviousVisionCell = new int[_eGM.XYForArray];

        _eGM.SelectorESelectorC.SetterUnitDelegate = IsSetted;
        _eGM.SelectorESelectorC.AttackUnitAction = IsAttacked;
        _eGM.SelectorESelectorC.ShiftUnitDelegate = SetIsShifted;
    }

    public void Run()
    {
        _systemsGeneralManager.TryInvokeRunSystem(nameof(RaySystem), _systemsGeneralManager.ForSelectorSystem);

        if (_eGM.RayComponentSelectorEnt.RaycastHit2D)
        {
            if (_eGM.RayComponentSelectorEnt.RaycastHit2D.collider.gameObject.tag == _nameManager.TAG_CELL)
            {
                _systemsGeneralManager.TryInvokeRunSystem(nameof(GetterCellSystem), _systemsGeneralManager.ForSelectorSystem);

                if (_eGM.SelectorESelectorC.IsGettedCell)
                {
                    var xyCurrentCell = _eGM.SelectorESelectorC.XYcurrentCell;

                    if (_eGM.InputEntityMouseClickComponent.IsClick)
                    {
                        if (_eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[InstanceGame.IsMasterClient])
                        {
                            if (_canExecuteStartClick)
                            {
                                _cellBaseOperations.CopyXYinTo(xyCurrentCell, _xySelectedCell);

                                if (!_cellBaseOperations.CompareXY(_xyPreviousCell, _xySelectedCell))
                                    ActivateSelector(true, _xyPreviousCell, _xySelectedCell);

                                _cellBaseOperations.CopyXYinTo(_xySelectedCell, _xyPreviousCell);
                                _canExecuteStartClick = false;

                            }

                            else
                            {
                                if (!_cellBaseOperations.CompareXY(_xySelectedCell, xyCurrentCell))
                                    _cellBaseOperations.CopyXYinTo(_xySelectedCell, _xyPreviousCell);


                                _cellBaseOperations.CopyXYinTo(xyCurrentCell, _xySelectedCell);
                                ActivateSelector(true, _xyPreviousCell, _xySelectedCell);
                            }
                        }

                        else
                        {
                            if (_eGM.SelectedUnitEUnitTypeC.HaveUnit)
                            {
                                if (!_eGM.CellEnvironmentComponent(xyCurrentCell).HaveMountain && !_eGM.CellUnitComponent(xyCurrentCell).HaveUnit)
                                {
                                    if (InstanceGame.IsMasterClient && _eGM.CellComponent(xyCurrentCell).IsStartMaster)
                                        _photonPunRPC.SetUniToMaster(xyCurrentCell, _eGM.SelectedUnitEUnitTypeC.UnitType);

                                    else if (_eGM.CellComponent(xyCurrentCell).IsStartOther)
                                        _photonPunRPC.SetUniToMaster(xyCurrentCell, _eGM.SelectedUnitEUnitTypeC.UnitType);

                                    else _soundComponentRef.Unref().MistakeSoundAction();
                                }

                                else _soundComponentRef.Unref().MistakeSoundAction();
                            }

                            else if (_canExecuteStartClick)
                            {
                                _cellBaseOperations.CopyXYinTo(xyCurrentCell, _xySelectedCell);

                                if (!_cellBaseOperations.CompareXY(_xyPreviousCell, _xySelectedCell))
                                    ActivateSelector(true, _xyPreviousCell, _xySelectedCell);

                                if (_eGM.CellUnitComponent(_xySelectedCell).HaveUnit)
                                {
                                    if (_eGM.CellUnitComponent(_xySelectedCell).IsMine)
                                    {
                                        if (_eGM.CellUnitComponent(_xySelectedCell).AmountSteps >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                                        {
                                            _eGM.SelectorESelectorC.AvailableCellsForShift = _cellFinderWay.GetCellsForShift(_xySelectedCell);
                                            _cellFinderWay.GetCellsForAttack(_xySelectedCell, InstanceGame.LocalPlayer, out _eGM.SelectorESelectorC.AvailableCellsSimpleAttack, out _eGM.SelectorESelectorC.AvailableCellsUniqueAttack);

                                            _canShiftUnit = true;
                                        }
                                    }
                                }

                                _cellBaseOperations.CopyXYinTo(_xySelectedCell, _xyPreviousCell);
                                _canExecuteStartClick = false;
                            }

                            else
                            {
                                if (!_cellBaseOperations.CompareXY(_xySelectedCell, xyCurrentCell))
                                    _cellBaseOperations.CopyXYinTo(_xySelectedCell, _xyPreviousCell);


                                _cellBaseOperations.CopyXYinTo(xyCurrentCell, _xySelectedCell);
                                ActivateSelector(true, _xyPreviousCell, _xySelectedCell);


                                if (_eGM.CellUnitComponent(_xySelectedCell).HaveUnit)
                                {
                                    if (_eGM.CellUnitComponent(_xySelectedCell).IsMine)
                                    {
                                        if (_eGM.CellUnitComponent(_xySelectedCell).MinAmountSteps)
                                        {
                                            _eGM.SelectorESelectorC.AvailableCellsForShift = _cellFinderWay.GetCellsForShift(_xySelectedCell);
                                            _cellFinderWay.GetCellsForAttack
                                                (_xySelectedCell, InstanceGame.LocalPlayer, out _eGM.SelectorESelectorC.AvailableCellsSimpleAttack, out _eGM.SelectorESelectorC.AvailableCellsUniqueAttack);

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
                                        if (_eGM.CellUnitComponent(_xyPreviousCell).MinAmountSteps)
                                        {
                                            if (_cellBaseOperations.TryFindCellInList(_xySelectedCell, _eGM.SelectorESelectorC.AvailableCellsSimpleAttack))
                                            {
                                                _photonPunRPC.AttackUnitToMaster(_xyPreviousCell, _xySelectedCell);
                                            }

                                            else if(_cellBaseOperations.TryFindCellInList(_xySelectedCell, _eGM.SelectorESelectorC.AvailableCellsUniqueAttack))
                                            {
                                                _photonPunRPC.AttackUnitToMaster(_xyPreviousCell, _xySelectedCell);
                                            }
                                        }
                                    }
                                }

                                else
                                {
                                    if (_canShiftUnit)
                                    {
                                        if (_eGM.CellUnitComponent(_xyPreviousCell).IsMine)
                                        {
                                            if (_eGM.CellUnitComponent(_xyPreviousCell).MinAmountSteps)
                                            {
                                                if (_cellBaseOperations.TryFindCellInList(_xySelectedCell, _eGM.SelectorESelectorC.AvailableCellsForShift))
                                                {
                                                    _photonPunRPC.ShiftUnitToMaster(_xyPreviousCell, _xySelectedCell);
                                                }
                                            }
                                        }

                                        ActivateSelector(false, _xyPreviousCell, _xySelectedCell);

                                        ClearAvailableCells();

                                        _canShiftUnit = false;

                                    }
                                }
                            }
                        }
                    }

                    else
                    {
                        if (_eGM.SelectedUnitEUnitTypeC.HaveUnit)
                        {
                            if (!_eGM.CellUnitComponent(xyCurrentCell).HaveUnit)
                            {
                                if (_isStartSelectedDirect)
                                {
                                    if (!_eGM.CellUnitComponent(xyCurrentCell).HaveUnit)
                                        _eGM.CellUnitComponent(xyCurrentCell).ActiveVisionCell(true, _eGM.SelectedUnitEUnitTypeC.UnitType, InstanceGame.LocalPlayer);

                                    _cellBaseOperations.CopyXYinTo(xyCurrentCell, _xyPreviousVisionCell);
                                    _isStartSelectedDirect = false;
                                }
                                else
                                {
                                    if (!_eGM.CellUnitComponent(_xyPreviousVisionCell).HaveUnit)
                                        _eGM.CellUnitComponent(_xyPreviousVisionCell).ActiveVisionCell(false, _eGM.SelectedUnitEUnitTypeC.UnitType, InstanceGame.LocalPlayer);

                                    _eGM.CellUnitComponent(xyCurrentCell).ActiveVisionCell(true, _eGM.SelectedUnitEUnitTypeC.UnitType, InstanceGame.LocalPlayer);
                                    _cellBaseOperations.CopyXYinTo(xyCurrentCell, _xyPreviousVisionCell);
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

                    ActivateSelector(false, _xyPreviousCell, _xySelectedCell);

                    ClearAvailableCells();

                    _eGM.CellUnitComponent(_xyPreviousVisionCell).ActiveVisionCell(false, _eGM.SelectedUnitEUnitTypeC.UnitType, InstanceGame.LocalPlayer);
                    _eGM.SelectedUnitEUnitTypeC.UnitType = default;

                    _cellBaseOperations.CleanXY(_xyPreviousCell);
                }
            }
        }
    }


    #region Methods

    private void ActivateSelector(in bool isActive, in int[] xyPreviousCell, in int[] xySelectedCell)
    {
        _eGM.CellComponent(xyPreviousCell).IsSelected = false;
        _eGM.CellComponent(xySelectedCell).IsSelected = isActive;
    }

    private void ClearAvailableCells()
    {
        _eGM.SelectorESelectorC.AvailableCellsForShift.Clear();
        _eGM.SelectorESelectorC.AvailableCellsSimpleAttack.Clear();
        _eGM.SelectorESelectorC.AvailableCellsUniqueAttack.Clear();
    }


    #region Delegates

    private void IsSetted()
    {
        _eGM.SelectedUnitEUnitTypeC.UnitType = default;
        _isStartSelectedDirect = true;
    }

    private void IsAttacked()
    {
        _eGM.SelectorESelectorC.AvailableCellsForShift.Clear();
        _eGM.SelectorESelectorC.AvailableCellsSimpleAttack.Clear();
        _eGM.SelectorESelectorC.AvailableCellsUniqueAttack.Clear();
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