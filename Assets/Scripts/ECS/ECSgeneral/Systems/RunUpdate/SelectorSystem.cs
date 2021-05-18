using Leopotam.Ecs;
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


    private int[] _xyPreviousCell => _eGM.SelectorComponentSelectorEnt.XYpreviousCell;
    private int[] _xySelectedCell => _eGM.SelectorComponentSelectorEnt.XYselectedCell;



    internal SelectorSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonPunRPC = InstanceGame.PhotonGameManager.PhotonPunRPC;
        _nameManager = InstanceGame.Names;
        _systemsGeneralManager = eCSmanager.SystemsGeneralManager;

        _soundComponentRef = eCSmanager.EntitiesGeneralManager.SoundComponentRef;

        _xyPreviousVisionCell = new int[_eGM.XYForArray];

        _eGM.SelectorComponentSelectorEnt.SetterUnitDelegate = IsSetted;
        _eGM.SelectorComponentSelectorEnt.AttackUnitAction = IsAttacked;
        _eGM.SelectorComponentSelectorEnt.ShiftUnitDelegate = SetIsShifted;
    }

    public void Run()
    {
        _systemsGeneralManager.TryInvokeRunSystem(nameof(RaySystem), _systemsGeneralManager.ForSelectorSystem);

        if (_eGM.RayComponentSelectorEnt.RaycastHit2D)
        {
            if (_eGM.RayComponentSelectorEnt.RaycastHit2D.collider.gameObject.tag == _nameManager.TAG_CELL)
            {
                _systemsGeneralManager.TryInvokeRunSystem(nameof(GetterCellSystem), _systemsGeneralManager.ForSelectorSystem);

                if (_eGM.SelectorComponentSelectorEnt.IsGettedCell)
                {
                    var xyCurrentCell = _eGM.SelectorComponentSelectorEnt.XYcurrentCell;

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
                            if (_eGM.SelectedUnitComponentSelectorEnt.IsSelectedUnit)
                            {
                                if (!_eGM.CellEnvironmentComponent(xyCurrentCell).HaveMountain && !_eGM.CellUnitComponent(xyCurrentCell).HaveUnit)
                                {
                                    if (InstanceGame.IsMasterClient && _eGM.CellComponent(xyCurrentCell).IsStartMaster)
                                        _photonPunRPC.SetUnit(xyCurrentCell, _eGM.SelectedUnitComponentSelectorEnt.SelectedUnitType);

                                    else if (_eGM.CellComponent(xyCurrentCell).IsStartOther)
                                        _photonPunRPC.SetUnit(xyCurrentCell, _eGM.SelectedUnitComponentSelectorEnt.SelectedUnitType);

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
                                            _eGM.SelectorComponentSelectorEnt.AvailableCellsForShift = _cellFinderWay.GetCellsForShift(_xySelectedCell);
                                            _cellFinderWay.GetCellsForAttack(_xySelectedCell, InstanceGame.LocalPlayer, out _eGM.SelectorComponentSelectorEnt.AvailableCellsSimpleAttack, out _eGM.SelectorComponentSelectorEnt.AvailableCellsUniqueAttack);

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
                                            _eGM.SelectorComponentSelectorEnt.AvailableCellsForShift = _cellFinderWay.GetCellsForShift(_xySelectedCell);
                                            _cellFinderWay.GetCellsForAttack
                                                (_xySelectedCell, InstanceGame.LocalPlayer, out _eGM.SelectorComponentSelectorEnt.AvailableCellsSimpleAttack, out _eGM.SelectorComponentSelectorEnt.AvailableCellsUniqueAttack);

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
                                            if (_cellBaseOperations.TryFindCellInList(_xySelectedCell, _eGM.SelectorComponentSelectorEnt.AvailableCellsSimpleAttack))
                                            {
                                                _photonPunRPC.AttackUnit(_xyPreviousCell, _xySelectedCell);

                                                //ClearAvailableCells();
                                            }

                                            else if(_cellBaseOperations.TryFindCellInList(_xySelectedCell, _eGM.SelectorComponentSelectorEnt.AvailableCellsUniqueAttack))
                                            {
                                                _photonPunRPC.AttackUnit(_xyPreviousCell, _xySelectedCell);

                                                //ClearAvailableCells();
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
                                                if (_cellBaseOperations.TryFindCellInList(_xySelectedCell, _eGM.SelectorComponentSelectorEnt.AvailableCellsForShift))
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
                        if (_eGM.SelectedUnitComponentSelectorEnt.IsSelectedUnit)
                        {
                            if (!_eGM.CellUnitComponent(xyCurrentCell).HaveUnit)
                            {
                                if (_isStartSelectedDirect)
                                {
                                    if (!_eGM.CellUnitComponent(xyCurrentCell).HaveUnit)
                                        _eGM.CellUnitComponent(xyCurrentCell).ActiveVisionCell(true, _eGM.SelectedUnitComponentSelectorEnt.SelectedUnitType, InstanceGame.LocalPlayer);

                                    _cellBaseOperations.CopyXYinTo(xyCurrentCell, _xyPreviousVisionCell);
                                    _isStartSelectedDirect = false;
                                }
                                else
                                {
                                    if (!_eGM.CellUnitComponent(_xyPreviousVisionCell).HaveUnit)
                                        _eGM.CellUnitComponent(_xyPreviousVisionCell).ActiveVisionCell(false, _eGM.SelectedUnitComponentSelectorEnt.SelectedUnitType, InstanceGame.LocalPlayer);

                                    _eGM.CellUnitComponent(xyCurrentCell).ActiveVisionCell(true, _eGM.SelectedUnitComponentSelectorEnt.SelectedUnitType, InstanceGame.LocalPlayer);
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

                    _eGM.CellUnitComponent(_xyPreviousVisionCell).ActiveVisionCell(false, _eGM.SelectedUnitComponentSelectorEnt.SelectedUnitType, InstanceGame.LocalPlayer);
                    _eGM.SelectedUnitComponentSelectorEnt.SelectedUnitType = default;

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
        _eGM.SelectorComponentSelectorEnt.AvailableCellsForShift.Clear();
        _eGM.SelectorComponentSelectorEnt.AvailableCellsSimpleAttack.Clear();
        _eGM.SelectorComponentSelectorEnt.AvailableCellsUniqueAttack.Clear();
    }


    #region Delegates

    private void IsSetted()
    {
        _eGM.SelectedUnitComponentSelectorEnt.SelectedUnitType = default;
        _isStartSelectedDirect = true;
    }

    private void IsAttacked()
    {
        _eGM.SelectorComponentSelectorEnt.AvailableCellsForShift.Clear();
        _eGM.SelectorComponentSelectorEnt.AvailableCellsSimpleAttack.Clear();
        _eGM.SelectorComponentSelectorEnt.AvailableCellsUniqueAttack.Clear();
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