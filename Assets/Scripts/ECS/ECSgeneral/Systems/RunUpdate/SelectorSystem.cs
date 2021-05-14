using Leopotam.Ecs;
using UnityEngine;
using static MainGame;

internal sealed class SelectorSystem : CellReduction, IEcsRunSystem
{
    #region Other classes and else

    private PhotonPunRPC _photonPunRPC = default;
    private Names _nameManager;
    private SystemsGeneralManager _systemsGeneralManager;

    #endregion


    #region Variables for this system

    private int[] _xyPreviousVisionCell = default;

    private bool _canShiftUnit = false;
    private bool _canExecuteStartClick = true;
    private bool _isStartSelectedDirect = true;

    #endregion


    #region ComponentsRef

    private EcsComponentRef<RayComponent> _rayComponentRef = default;
    private EcsComponentRef<InputComponent> _inputComponentRef = default;
    private EcsComponentRef<SelectedUnitComponent> _selectedUnitComponentRef = default;
    private EcsComponentRef<SelectorComponent> _selectorComponentRef = default;
    private EcsComponentRef<DonerComponent> _buttonComponent = default;
    private EcsComponentRef<SoundComponent> _soundComponentRef = default;

    #endregion

    private int[] _xyPreviousCell => _selectorComponentRef.Unref().XYpreviousCell;
    private int[] _xySelectedCell => _selectorComponentRef.Unref().XYselectedCell;


    private RaycastHit2D _raycastHit2D => _rayComponentRef.Unref().RaycastHit2D;


    internal SelectorSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonPunRPC = InstanceGame.PhotonGameManager.PhotonPunRPC;
        _nameManager = InstanceGame.Names;
        _systemsGeneralManager = eCSmanager.SystemsGeneralManager;

        _rayComponentRef = eCSmanager.EntitiesGeneralManager.RayComponentRef;
        _inputComponentRef = eCSmanager.EntitiesGeneralManager.InputComponentRef;
        _selectedUnitComponentRef = eCSmanager.EntitiesGeneralManager.SelectedUnitComponentRef;
        _selectorComponentRef = eCSmanager.EntitiesGeneralManager.SelectorComponentRef;
        _buttonComponent = eCSmanager.EntitiesGeneralManager.DonerComponentRef;
        _soundComponentRef = eCSmanager.EntitiesGeneralManager.SoundComponentRef;

        _xyPreviousVisionCell = new int[XYforArray];

        _selectorComponentRef.Unref().SetterUnitDelegate = IsSetted;
        _selectorComponentRef.Unref().AttackUnitDelegate = IsAttacked;
        _selectorComponentRef.Unref().ShiftUnitDelegate = SetIsShifted;
    }

    public void Run()
    {
        _systemsGeneralManager.InvokeRunSystem(SystemGeneralTypes.ForSelector, nameof(RaySystem));

        if (_raycastHit2D)
        {
            if (_raycastHit2D.collider.gameObject.tag == _nameManager.TAG_CELL)
            {
                _systemsGeneralManager.InvokeRunSystem(SystemGeneralTypes.ForSelector, nameof(GetterCellSystem));

                if (_selectorComponentRef.Unref().IsGettedCell)
                {
                    var xyCurrentCell = _selectorComponentRef.Unref().XYcurrentCell;

                    if (_inputComponentRef.Unref().IsClick)
                    {
                        if (_buttonComponent.Unref().IsDone)
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
                            if (_selectedUnitComponentRef.Unref().IsSelectedUnit)
                            {
                                if (!CellEnvironmentComponent(xyCurrentCell).HaveMountain && !CellUnitComponent(xyCurrentCell).HaveUnit)
                                {
                                    if (InstanceGame.IsMasterClient && CellComponent(xyCurrentCell).IsStartMaster)
                                        _photonPunRPC.SetUnit(xyCurrentCell, _selectedUnitComponentRef.Unref().SelectedUnitType);

                                    else if (CellComponent(xyCurrentCell).IsStartOther)
                                        _photonPunRPC.SetUnit(xyCurrentCell, _selectedUnitComponentRef.Unref().SelectedUnitType);

                                    else _soundComponentRef.Unref().MistakeSoundDelegate();
                                }

                                else _soundComponentRef.Unref().MistakeSoundDelegate();
                            }

                            else if (_canExecuteStartClick)
                            {
                                _cellBaseOperations.CopyXYinTo(xyCurrentCell, _xySelectedCell);

                                if (!_cellBaseOperations.CompareXY(_xyPreviousCell, _xySelectedCell))
                                    ActivateSelector(true, _xyPreviousCell, _xySelectedCell);

                                if (CellUnitComponent(_xySelectedCell).HaveUnit)
                                {
                                    if (CellUnitComponent(_xySelectedCell).IsMine)
                                    {
                                        if (CellUnitComponent(_xySelectedCell).AmountSteps >= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                                        {
                                            _selectorComponentRef.Unref().AvailableCellsForShift = _cellFinderWay.GetCellsForShift(_xySelectedCell);
                                            _cellFinderWay.GetCellsForAttack(_xySelectedCell, InstanceGame.LocalPlayer, out _selectorComponentRef.Unref().AvailableCellsSimpleAttack, out _selectorComponentRef.Unref().AvailableCellsUniqueAttack);

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


                                if (CellUnitComponent(_xySelectedCell).HaveUnit)
                                {
                                    if (CellUnitComponent(_xySelectedCell).IsMine)
                                    {
                                        if (CellUnitComponent(_xySelectedCell).MinAmountSteps)
                                        {
                                            _selectorComponentRef.Unref().AvailableCellsForShift = _cellFinderWay.GetCellsForShift(_xySelectedCell);
                                            _cellFinderWay.GetCellsForAttack
                                                (_xySelectedCell, InstanceGame.LocalPlayer, out _selectorComponentRef.Unref().AvailableCellsSimpleAttack, out _selectorComponentRef.Unref().AvailableCellsUniqueAttack);

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
                                        if (CellUnitComponent(_xyPreviousCell).MinAmountSteps)
                                        {
                                            if (_cellBaseOperations.TryFindCellInList(_xySelectedCell, _selectorComponentRef.Unref().AvailableCellsSimpleAttack))
                                            {
                                                _photonPunRPC.AttackUnit(_xyPreviousCell, _xySelectedCell);

                                                //ClearAvailableCells();
                                            }

                                            else if(_cellBaseOperations.TryFindCellInList(_xySelectedCell, _selectorComponentRef.Unref().AvailableCellsUniqueAttack))
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
                                        if (CellUnitComponent(_xyPreviousCell).IsMine)
                                        {
                                            if (CellUnitComponent(_xyPreviousCell).MinAmountSteps)
                                            {
                                                if (_cellBaseOperations.TryFindCellInList(_xySelectedCell, _selectorComponentRef.Unref().AvailableCellsForShift))
                                                {
                                                    _photonPunRPC.ShiftUnit(_xyPreviousCell, _xySelectedCell);
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
                        if (_selectedUnitComponentRef.Unref().IsSelectedUnit)
                        {
                            if (!CellUnitComponent(xyCurrentCell).HaveUnit)
                            {
                                if (_isStartSelectedDirect)
                                {
                                    if (!CellUnitComponent(xyCurrentCell).HaveUnit)
                                        CellUnitComponent(xyCurrentCell).ActiveVisionCell(true, _selectedUnitComponentRef.Unref().SelectedUnitType, InstanceGame.LocalPlayer);

                                    _cellBaseOperations.CopyXYinTo(xyCurrentCell, _xyPreviousVisionCell);
                                    _isStartSelectedDirect = false;
                                }
                                else
                                {
                                    if (!CellUnitComponent(_xyPreviousVisionCell).HaveUnit)
                                        CellUnitComponent(_xyPreviousVisionCell).ActiveVisionCell(false, _selectedUnitComponentRef.Unref().SelectedUnitType, InstanceGame.LocalPlayer);

                                    CellUnitComponent(xyCurrentCell).ActiveVisionCell(true, _selectedUnitComponentRef.Unref().SelectedUnitType, InstanceGame.LocalPlayer);
                                    _cellBaseOperations.CopyXYinTo(xyCurrentCell, _xyPreviousVisionCell);
                                }

                            }
                        }
                    }
                }
            }

            else if (_raycastHit2D.collider.gameObject.tag == _nameManager.TAG_BACKGROUND)
            {
                if (_inputComponentRef.Unref().IsClick)
                {
                    _canShiftUnit = false;
                    _canExecuteStartClick = true;

                    ActivateSelector(false, _xyPreviousCell, _xySelectedCell);

                    ClearAvailableCells();

                    CellUnitComponent(_xyPreviousVisionCell).ActiveVisionCell(false, _selectedUnitComponentRef.Unref().SelectedUnitType, InstanceGame.LocalPlayer);
                    _selectedUnitComponentRef.Unref().SelectedUnitType = default;

                    _cellBaseOperations.CleanXY(_xyPreviousCell);
                }
            }
        }
    }


    #region Methods

    private void ActivateSelector(in bool isActive, in int[] xyPreviousCell, in int[] xySelectedCell)
    {
        CellComponent(xyPreviousCell).IsSelected = false;
        CellComponent(xySelectedCell).IsSelected = isActive;
    }

    private void ClearAvailableCells()
    {
        _selectorComponentRef.Unref().AvailableCellsForShift.Clear();
        _selectorComponentRef.Unref().AvailableCellsSimpleAttack.Clear();
        _selectorComponentRef.Unref().AvailableCellsUniqueAttack.Clear();
    }


    #region Delegates

    private void IsSetted()
    {
        _selectedUnitComponentRef.Unref().SelectedUnitType = default;
        _isStartSelectedDirect = true;
    }

    private void IsAttacked()
    {
        _selectorComponentRef.Unref().AvailableCellsForShift.Clear();
        _selectorComponentRef.Unref().AvailableCellsSimpleAttack.Clear();
        _selectorComponentRef.Unref().AvailableCellsUniqueAttack.Clear();
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