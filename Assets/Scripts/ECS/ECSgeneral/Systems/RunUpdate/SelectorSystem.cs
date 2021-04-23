using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;
using static MainGame;

public sealed class SelectorSystem : CellReduction, IEcsRunSystem
{
    #region Other classes and else

    private PhotonPunRPC _photonPunRPC = default;
    private NameManager _nameManager;
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
    private EcsComponentRef<UnitPathsComponent> _unitPathComponentRef = default;
    private EcsComponentRef<SelectedUnitComponent> _selectedUnitComponentRef = default;
    private EcsComponentRef<GetterCellComponent> _getterCellComponentRef = default;
    private EcsComponentRef<SelectorComponent> _selectorComponentRef = default;
    private EcsComponentRef<ButtonComponent> _buttonComponent = default;
    private EcsComponentRef<SoundComponent> _soundComponentRef = default;

    #endregion


    private int[] _xySelectedCell => _selectorComponentRef.Unref().XYselectedCell;
    private int[] _xyPreviousCell => _selectorComponentRef.Unref().XYpreviousCell;
    private List<int[]> _xyAvailableCellsForShift
    {
        get { return _selectorComponentRef.Unref().XYavailableCellsForShift; }
        set { _selectorComponentRef.Unref().XYavailableCellsForShift = value; }
    }
    private List<int[]> _xyAvailableCellsForAttack
    {
        get { return _selectorComponentRef.Unref().XYavailableCellsForAttack; }
        set { _selectorComponentRef.Unref().XYavailableCellsForAttack = value; }
    }


    internal SelectorSystem(ECSmanager eCSmanager, SupportGameManager supportManager, PhotonGameManager photonManager) : base(eCSmanager, supportManager)
    {
        _photonPunRPC = photonManager.PhotonPunRPC;
        _nameManager = supportManager.NameManager;
        _systemsGeneralManager = eCSmanager.SystemsGeneralManager;

        _rayComponentRef = eCSmanager.EntitiesGeneralManager.RayComponentRef;
        _inputComponentRef = eCSmanager.EntitiesGeneralManager.InputComponentRef;
        _unitPathComponentRef = eCSmanager.EntitiesGeneralManager.UnitPathComponentRef;
        _selectedUnitComponentRef = eCSmanager.EntitiesGeneralManager.SelectedUnitComponentRef;
        _getterCellComponentRef = eCSmanager.EntitiesGeneralManager.GetterCellComponentRef;
        _selectorComponentRef = eCSmanager.EntitiesGeneralManager.SelectorComponentRef;
        _buttonComponent = eCSmanager.EntitiesGeneralManager.ButtonComponentRef;
        _soundComponentRef = eCSmanager.EntitiesGeneralManager.SoundComponentRef;

        _xyPreviousVisionCell = new int[XYforArray];

        _selectorComponentRef.Unref().SetterUnitDelegate = IsSetted;
        _selectorComponentRef.Unref().AttackUnitDelegate = IsAttacked;
        _selectorComponentRef.Unref().ShiftUnitDelegate = SetIsShifted;
    }

    public void Run()
    {
        if (_rayComponentRef.Unref().TryGetRaycastHit2D(out RaycastHit2D raycastHit2D))
        {
            if (raycastHit2D.collider.gameObject.tag == _nameManager.TAG_CELL)
            {
                if (_getterCellComponentRef.Unref().TryGetXYCurrentCell(raycastHit2D, out var xyCurrentCell))
                {
                    if (_inputComponentRef.Unref().IsClick)
                    {
                        if (_buttonComponent.Unref().IsDone)
                        {
                            if (_canExecuteStartClick)
                            {
                                _cellManager.CopyXYinTo(xyCurrentCell, _xySelectedCell);

                                if (!_cellManager.CompareXY(_xyPreviousCell, _xySelectedCell))
                                    ActivateSelector(true, _xyPreviousCell, _xySelectedCell);

                                _cellManager.CopyXYinTo(_xySelectedCell, _xyPreviousCell);
                                _canExecuteStartClick = false;

                            }

                            else
                            {
                                if (!_cellManager.CompareXY(_xySelectedCell, xyCurrentCell))
                                    _cellManager.CopyXYinTo(_xySelectedCell, _xyPreviousCell);


                                _cellManager.CopyXYinTo(xyCurrentCell, _xySelectedCell);
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
                                _cellManager.CopyXYinTo(xyCurrentCell, _xySelectedCell);

                                if (!_cellManager.CompareXY(_xyPreviousCell, _xySelectedCell))
                                    ActivateSelector(true, _xyPreviousCell, _xySelectedCell);

                                if (CellUnitComponent(_xySelectedCell).HaveUnit)
                                {
                                    if (CellUnitComponent(_xySelectedCell).IsMine)
                                    {
                                        if (CellUnitComponent(_xySelectedCell).AmountSteps >= _startValues.AMOUNT_FOR_TAKE_UNIT)
                                        {
                                            _xyAvailableCellsForShift = _unitPathComponentRef.Unref().GetAvailableCellsForShift(_xySelectedCell, InstanceGame.LocalPlayer);
                                            _xyAvailableCellsForAttack = _unitPathComponentRef.Unref().GetAvailableCellsForAttack(_xySelectedCell, InstanceGame.LocalPlayer);

                                            _canShiftUnit = true;
                                        }
                                    }
                                }

                                _cellManager.CopyXYinTo(_xySelectedCell, _xyPreviousCell);
                                _canExecuteStartClick = false;
                            }

                            else
                            {
                                if (!_cellManager.CompareXY(_xySelectedCell, xyCurrentCell))
                                    _cellManager.CopyXYinTo(_xySelectedCell, _xyPreviousCell);


                                _cellManager.CopyXYinTo(xyCurrentCell, _xySelectedCell);
                                ActivateSelector(true, _xyPreviousCell, _xySelectedCell);


                                if (CellUnitComponent(_xySelectedCell).HaveUnit)
                                {
                                    if (CellUnitComponent(_xySelectedCell).IsMine)
                                    {
                                        if (CellUnitComponent(_xySelectedCell).HaveAmountSteps)
                                        {
                                            _xyAvailableCellsForShift = _unitPathComponentRef.Unref().GetAvailableCellsForShift(_xySelectedCell, InstanceGame.LocalPlayer);
                                            _xyAvailableCellsForAttack = _unitPathComponentRef.Unref().GetAvailableCellsForAttack(_xySelectedCell, InstanceGame.LocalPlayer);

                                            _canShiftUnit = true;
                                        }

                                        else
                                        {
                                            _xyAvailableCellsForShift.Clear();
                                            _xyAvailableCellsForAttack.Clear();

                                            _canShiftUnit = false;
                                        }
                                    }

                                    else
                                    {
                                        if (CellUnitComponent(_xyPreviousCell).HaveAmountSteps)
                                        {
                                            if (_cellManager.TryFindCellInList(_xySelectedCell, _xyAvailableCellsForAttack))
                                            {
                                                _photonPunRPC.AttackUnit(_xyPreviousCell, _xySelectedCell);

                                                _xyAvailableCellsForAttack.Clear();
                                                _xyAvailableCellsForShift.Clear();
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
                                            if (CellUnitComponent(_xyPreviousCell).HaveAmountSteps)
                                            {
                                                if (_cellManager.TryFindCellInList(_xySelectedCell, _xyAvailableCellsForShift))
                                                {
                                                    _photonPunRPC.ShiftUnit(_xyPreviousCell, _xySelectedCell);
                                                }
                                            }
                                        }

                                        ActivateSelector(false, _xyPreviousCell, _xySelectedCell);

                                        _xyAvailableCellsForShift.Clear();
                                        _xyAvailableCellsForAttack.Clear();

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

                                    _cellManager.CopyXYinTo(xyCurrentCell, _xyPreviousVisionCell);
                                    _isStartSelectedDirect = false;
                                }
                                else
                                {
                                    if (!CellUnitComponent(_xyPreviousVisionCell).HaveUnit)
                                        CellUnitComponent(_xyPreviousVisionCell).ActiveVisionCell(false, _selectedUnitComponentRef.Unref().SelectedUnitType, InstanceGame.LocalPlayer);

                                    CellUnitComponent(xyCurrentCell).ActiveVisionCell(true, _selectedUnitComponentRef.Unref().SelectedUnitType, InstanceGame.LocalPlayer);
                                    _cellManager.CopyXYinTo(xyCurrentCell, _xyPreviousVisionCell);
                                }

                            }
                        }
                    }
                }
            }

            else if (raycastHit2D.collider.gameObject.tag == _nameManager.TAG_BACKGROUND)
            {
                if (_inputComponentRef.Unref().IsClick)
                {
                    _canShiftUnit = false;
                    _canExecuteStartClick = true;

                    ActivateSelector(false, _xyPreviousCell, _xySelectedCell);

                    _xyAvailableCellsForShift.Clear();
                    _xyAvailableCellsForAttack.Clear();

                    CellUnitComponent(_xyPreviousVisionCell).ActiveVisionCell(false, _selectedUnitComponentRef.Unref().SelectedUnitType, InstanceGame.LocalPlayer);
                    _selectedUnitComponentRef.Unref().ResetSelectedUnit();

                    _cellManager.CleanXY(_xyPreviousCell);
                }
                else
                {

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

    private void IsSetted()
    {
        _selectedUnitComponentRef.Unref().ResetSelectedUnit();
        _isStartSelectedDirect = true;
    }

    private void IsAttacked()
    {

        _selectorComponentRef.Unref().XYavailableCellsForAttack.Clear();

        _selectorComponentRef.Unref().XYavailableCellsForShift.Clear();
        _selectorComponentRef.Unref().XYavailableCellsForAttack.Clear();
    }

    private void SetIsShifted()
    {

        //ActivateSelectorAndSelectorVision(false, in _xyPreviousCell, in _xySelectedCell);
        //_supportVisionComponentRef.Unref().SetWayInvoke(false, _xyAvailableCells);
        //_canShiftUnit = false;

        //_photonPunRPC.ContainerRPC.SetResetShifted(false);

    }

    #endregion
}