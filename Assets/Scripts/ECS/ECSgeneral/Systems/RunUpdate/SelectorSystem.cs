using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Main;

public sealed class SelectorSystem : CellReductionSystem, IEcsInitSystem, IEcsRunSystem
{
    #region Other classes and else

    private PhotonPunRPC _photonPunRPC = default;
    private NameManager _nameManager;
    private StartValuesConfig _startValues;

    #endregion


    #region Variables for this system

    private int[] _xyPreviousCell = default;
    private int[] _xyPreviousVisionCell = default;

    private List<int[]> _xyAvailableCellsForShift = new List<int[]>();
    private List<int[]> _xyAvailableCellsForAttack = new List<int[]>();

    private bool _canShiftUnit = false;
    private bool _canExecuteStartClick = true;
    private bool _isStartSelectedDirect = true;

    #endregion


    #region ComponentsRef

    private EcsComponentRef<RayComponent> _rayComponentRef = default;
    private EcsComponentRef<InputComponent> _inputComponentRef = default;
    private EcsComponentRef<SupportVisionComponent> _supportVisionComponentRef = default;
    private EcsComponentRef<UnitPathComponent> _unitPathComponentRef = default;
    private EcsComponentRef<SelectedUnitComponent> _selectedUnitComponentRef = default;
    private EcsComponentRef<GetterCellComponent> _getterCellComponentRef = default;
    private EcsComponentRef<SelectorComponent> _selectorComponentRef = default;
    private EcsComponentRef<ButtonComponent> _buttonComponent = default;
    private EcsComponentRef<SoundComponent> _soundComponentRef = default;

    private int[] _xySelectedCell => _selectorComponentRef.Unref().XYselectedCell;

    #endregion





    internal SelectorSystem(ECSmanager eCSmanager, SupportManager supportManager, PhotonManager photonManager) : base(eCSmanager, supportManager)
    {
        _photonPunRPC = photonManager.PhotonPunRPC;
        _nameManager = supportManager.NameManager;
        _startValues = supportManager.StartValuesConfig;

        _rayComponentRef = eCSmanager.EntitiesGeneralManager.RayComponentRef;
        _inputComponentRef = eCSmanager.EntitiesGeneralManager.InputComponentRef;
        _supportVisionComponentRef = eCSmanager.EntitiesGeneralManager.SupportVisionComponentRef;
        _unitPathComponentRef = eCSmanager.EntitiesGeneralManager.UnitPathComponentRef;
        _selectedUnitComponentRef = eCSmanager.EntitiesGeneralManager.SelectedUnitComponentRef;
        _getterCellComponentRef = eCSmanager.EntitiesGeneralManager.GetterCellComponentRef;
        _selectorComponentRef = eCSmanager.EntitiesGeneralManager.SelectorComponentRef;
        _buttonComponent = eCSmanager.EntitiesGeneralManager.ButtonComponentRef;
        _soundComponentRef = eCSmanager.EntitiesGeneralManager.SoundComponentRef;
    }


    public void Init()
    {
        _xyPreviousCell = new int[XYforArray];
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
                                    ActivateSelectorAndSelectorVision(true, _xyPreviousCell, _xySelectedCell);

                                _cellManager.CopyXYinTo(_xySelectedCell, _xyPreviousCell);
                                _canExecuteStartClick = false;

                            }

                            else
                            {
                                if (!_cellManager.CompareXY(_xySelectedCell, xyCurrentCell))
                                    _cellManager.CopyXYinTo(_xySelectedCell, _xyPreviousCell);


                                _cellManager.CopyXYinTo(xyCurrentCell, _xySelectedCell);
                                ActivateSelectorAndSelectorVision(true, _xyPreviousCell, _xySelectedCell);
                            }
                        }

                        else
                        {
                            if (_selectedUnitComponentRef.Unref().IsSelectedUnit)
                            {
                                if (!CellEnvironmentComponent(xyCurrentCell).HaveMountain && !CellUnitComponent(xyCurrentCell).HaveUnit)
                                {
                                    if (Instance.IsMasterClient && CellComponent(xyCurrentCell).IsStartMaster)
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
                                    ActivateSelectorAndSelectorVision(true, _xyPreviousCell, _xySelectedCell);

                                if (CellUnitComponent(_xySelectedCell).HaveUnit)
                                {
                                    if (CellUnitComponent(_xySelectedCell).IsMine)
                                    {
                                        if (CellUnitComponent(_xySelectedCell).AmountSteps >= _startValues.TakeAmountSteps)
                                        {
                                            _unitPathComponentRef.Unref().GetAvailableCellsForShift(_xySelectedCell, Instance.LocalPlayer, out _xyAvailableCellsForShift);
                                            _unitPathComponentRef.Unref().GetAvailableCellsForAttack(_xySelectedCell, Instance.LocalPlayer,  out _xyAvailableCellsForAttack);
                                            
                                            _supportVisionComponentRef.Unref().ActiveWayUnitVisionInvoke(true, _xyAvailableCellsForShift);
                                            _supportVisionComponentRef.Unref().ActiveEnemyVision(true, _xyAvailableCellsForAttack);

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
                                ActivateSelectorAndSelectorVision(true, _xyPreviousCell, _xySelectedCell);


                                if (CellUnitComponent(_xySelectedCell).HaveUnit)
                                {
                                    if (CellUnitComponent(_xySelectedCell).IsMine)
                                    {
                                        if (CellUnitComponent(_xySelectedCell).HaveAmountSteps)
                                        {
                                            _supportVisionComponentRef.Unref().ActiveWayUnitVisionInvoke(false, _xyAvailableCellsForShift);

                                            _unitPathComponentRef.Unref().GetAvailableCellsForShift(_xySelectedCell, Instance.LocalPlayer, out _xyAvailableCellsForShift);
                                            _unitPathComponentRef.Unref().GetAvailableCellsForAttack(_xySelectedCell, Instance.LocalPlayer, out _xyAvailableCellsForAttack);

                                            _supportVisionComponentRef.Unref().ActiveWayUnitVisionInvoke(true, _xyAvailableCellsForShift);
                                            _supportVisionComponentRef.Unref().ActiveEnemyVision(true, _xyAvailableCellsForAttack);

                                            _canShiftUnit = true;
                                        }

                                        else
                                        {
                                            _supportVisionComponentRef.Unref().ActiveWayUnitVisionInvoke(false, _xyAvailableCellsForShift);
                                            _supportVisionComponentRef.Unref().ActiveEnemyVision(false, _xyAvailableCellsForAttack);

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
                                                _photonPunRPC.AttackUnit(_xyPreviousCell, _xySelectedCell);
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

                                        ActivateSelectorAndSelectorVision(false, _xyPreviousCell, _xySelectedCell);
                                        _supportVisionComponentRef.Unref().ActiveWayUnitVisionInvoke(false, _xyAvailableCellsForShift);
                                        _supportVisionComponentRef.Unref().ActiveEnemyVision(false, _xyAvailableCellsForAttack);
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
                            if (_isStartSelectedDirect)
                            {
                                _supportVisionComponentRef.Unref().ActiveSpawnVisionInvoke(true);

                                if (!CellUnitComponent(xyCurrentCell).HaveUnit)
                                    CellUnitComponent(xyCurrentCell).EnableVisionCell(true, UnitTypes.Pawn, Instance.LocalPlayer);

                                _cellManager.CopyXYinTo(xyCurrentCell, _xyPreviousVisionCell);
                                _isStartSelectedDirect = false;
                            }
                            else
                            {
                                _supportVisionComponentRef.Unref().ActiveSpawnVisionInvoke(true);

                                if (!CellUnitComponent(_xyPreviousVisionCell).HaveUnit)
                                    CellUnitComponent(_xyPreviousVisionCell).EnableVisionCell(false, UnitTypes.Pawn, Instance.LocalPlayer);

                                CellUnitComponent(xyCurrentCell).EnableVisionCell(true, UnitTypes.Pawn, Instance.LocalPlayer);
                                _cellManager.CopyXYinTo(xyCurrentCell, _xyPreviousVisionCell);
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

                    ActivateSelectorAndSelectorVision(false, _xyPreviousCell, _xySelectedCell);
                    _supportVisionComponentRef.Unref().ActiveWayUnitVisionInvoke(false, _xyAvailableCellsForShift);
                    _supportVisionComponentRef.Unref().ActiveEnemyVision(false, _xyAvailableCellsForAttack);

                    _cellManager.CleanXY(_xyPreviousCell);
                }
                else
                {

                }
            }
        }
    }


    #region Methods

    private void ActivateSelectorAndSelectorVision(in bool isActive, in int[] xyPreviousCell, in int[] xySelectedCell)
    {
        CellComponent(xyPreviousCell).SetIsSelected(false);
        CellComponent(xySelectedCell).SetIsSelected(isActive);

        _supportVisionComponentRef.Unref().ActiveSelectorVisionInvoke(isActive, xyPreviousCell, xySelectedCell);
    }

    private void IsSetted(bool isSetted)
    {
        if (isSetted)
        {
            Debug.Log("Done");

            _selectedUnitComponentRef.Unref().SetReset(UnitTypes.None);
            _supportVisionComponentRef.Unref().ActiveSpawnVisionInvoke(false);
            _isStartSelectedDirect = true;
        }
    }

    private void IsAttacked(bool isAttacked)
    {
        if (isAttacked)
        {
            _supportVisionComponentRef.Unref().ActiveWayUnitVisionInvoke(false, _xyAvailableCellsForShift);
            _supportVisionComponentRef.Unref().ActiveEnemyVision(false, _xyAvailableCellsForAttack);
            _supportVisionComponentRef.Unref().ActiveSelectorVisionInvoke(false, _xyPreviousCell, _xySelectedCell);

            _xyAvailableCellsForShift.Clear();
            _xyAvailableCellsForAttack.Clear();
        }
    }

    private void SetIsShifted(bool isShifted)
    {
        if (isShifted)
        {
            //ActivateSelectorAndSelectorVision(false, in _xyPreviousCell, in _xySelectedCell);
            //_supportVisionComponentRef.Unref().SetWayInvoke(false, _xyAvailableCells);
            //_canShiftUnit = false;

            //_photonPunRPC.ContainerRPC.SetResetShifted(false);
        }
    }

    #endregion
}