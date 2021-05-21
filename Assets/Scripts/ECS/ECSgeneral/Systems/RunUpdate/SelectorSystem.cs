using Leopotam.Ecs;
using static MainGame;

internal sealed class SelectorSystem : SystemGeneralReduction, IEcsRunSystem
{
    private PhotonPunRPC _photonPunRPC = default;
    private Names _nameManager;
    private SystemsGeneralManager _systemsGeneralManager;


    private int[] _xyPreviousVisionCell = default;

    private bool _canShiftUnit = false;
    private bool _canExecuteStartClick = true;
    private bool _isStartSelectedDirect = true;



    private int[] _xyPreviousCell => _eGM.SelectorEntSelectorCom.XYpreviousCell;
    private int[] _xySelectedCell => _eGM.SelectorEntSelectorCom.XYselectedCell;



    internal SelectorSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonPunRPC = Instance.PhotonGameManager.PhotonPunRPC;
        _nameManager = Instance.Names;
        _systemsGeneralManager = eCSmanager.SystemsGeneralManager;

        _xyPreviousVisionCell = new int[_eGM.XYForArray];

        _eGM.SelectorEntSelectorCom.SetterUnitDelegate = IsSetted;
        _eGM.SelectorEntSelectorCom.AttackUnitAction = IsAttacked;
        _eGM.SelectorEntSelectorCom.ShiftUnitDelegate = SetIsShifted;
    }

    public void Run()
    {
        _systemsGeneralManager.TryInvokeRunSystem(nameof(RaySystem), _systemsGeneralManager.ForSelectorRunUpdateSystem);

        if (_eGM.RayComponentSelectorEnt.RaycastHit2D)
        {
            if (_eGM.RayComponentSelectorEnt.RaycastHit2D.collider.gameObject.tag == _nameManager.TAG_CELL)
            {
                _systemsGeneralManager.TryInvokeRunSystem(nameof(GetterCellSystem), _systemsGeneralManager.ForSelectorRunUpdateSystem);

                if (_eGM.SelectorEntSelectorCom.IsGettedCell)
                {
                    var xyCurrentCell = _eGM.SelectorEntSelectorCom.XYcurrentCell;

                    if (_eGM.InputEntityMouseClickComponent.IsClick)
                    {
                        if (_eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[Instance.IsMasterClient])
                        {
                            if (_canExecuteStartClick)
                            {
                                _eGM.CellBaseOperEnt_CellBaseOperCom.CopyXYinTo(xyCurrentCell,_xySelectedCell);

                                if (!_eGM.CellBaseOperEnt_CellBaseOperCom.CompareXY(_xyPreviousCell, _xySelectedCell))
                                    ActivateSelector(true, _xyPreviousCell, _xySelectedCell);

                                _eGM.CellBaseOperEnt_CellBaseOperCom.CopyXYinTo(_xySelectedCell, _xyPreviousCell);
                                _canExecuteStartClick = false;

                            }

                            else
                            {
                                if (!_eGM.CellBaseOperEnt_CellBaseOperCom.CompareXY(_xySelectedCell, xyCurrentCell))
                                    _eGM.CellBaseOperEnt_CellBaseOperCom.CopyXYinTo(_xySelectedCell, _xyPreviousCell);


                                _eGM.CellBaseOperEnt_CellBaseOperCom.CopyXYinTo(xyCurrentCell, _xySelectedCell);
                                ActivateSelector(true, _xyPreviousCell, _xySelectedCell);
                            }
                        }

                        else
                        {
                            if (_eGM.SelectedUnitEntUnitTypeCom.HaveUnit)
                            {
                                if (!_eGM.CellEnvEnt_CellEnvironmentCom(xyCurrentCell).HaveMountain && !_eGM.CellUnitEnt_UnitTypeCom(xyCurrentCell).HaveUnit)
                                {
                                    if (Instance.IsMasterClient && _eGM.CellEnt_CellCom(xyCurrentCell).IsStartMaster)
                                        _photonPunRPC.SetUniToMaster(xyCurrentCell, _eGM.SelectedUnitEntUnitTypeCom.UnitType);

                                    else if (_eGM.CellEnt_CellCom(xyCurrentCell).IsStartOther)
                                        _photonPunRPC.SetUniToMaster(xyCurrentCell, _eGM.SelectedUnitEntUnitTypeCom.UnitType);

                                    else _eGM.SoundEntSoundCom.MistakeSoundAction();
                                }

                                else _eGM.SoundEntSoundCom.MistakeSoundAction();
                            }

                            else if (_canExecuteStartClick)
                            {
                                _eGM.CellBaseOperEnt_CellBaseOperCom.CopyXYinTo(xyCurrentCell, _xySelectedCell);

                                if (!_eGM.CellBaseOperEnt_CellBaseOperCom.CompareXY(_xyPreviousCell, _xySelectedCell))
                                    ActivateSelector(true, _xyPreviousCell, _xySelectedCell);

                                if (_eGM.CellUnitEnt_UnitTypeCom(_xySelectedCell).HaveUnit)
                                {
                                    if (_eGM.CellUnitEnt_OwnerCom(_xySelectedCell).IsMine)
                                    {
                                        if (_eGM.CellUnitEnt_CellUnitCom(_xySelectedCell).AmountSteps >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                                        {
                                            _eGM.SelectorEntSelectorCom.AvailableCellsForShift = _eGM.CellUnitEnt_CellUnitCom(_xySelectedCell).GetCellsForShift();
                                            _eGM.CellUnitEnt_CellUnitCom(_xySelectedCell).GetCellsForAttack(Instance.LocalPlayer, out _eGM.SelectorEntSelectorCom.AvailableCellsSimpleAttack, out _eGM.SelectorEntSelectorCom.AvailableCellsUniqueAttack);

                                            _canShiftUnit = true;
                                        }
                                    }
                                }

                                _eGM.CellBaseOperEnt_CellBaseOperCom.CopyXYinTo(_xySelectedCell, _xyPreviousCell);
                                _canExecuteStartClick = false;
                            }

                            else
                            {
                                if (!_eGM.CellBaseOperEnt_CellBaseOperCom.CompareXY(_xySelectedCell, xyCurrentCell))
                                    _eGM.CellBaseOperEnt_CellBaseOperCom.CopyXYinTo(_xySelectedCell, _xyPreviousCell);


                                _eGM.CellBaseOperEnt_CellBaseOperCom.CopyXYinTo(xyCurrentCell, _xySelectedCell);
                                ActivateSelector(true, _xyPreviousCell, _xySelectedCell);


                                if (_eGM.CellUnitEnt_UnitTypeCom(_xySelectedCell).HaveUnit)
                                {
                                    if (_eGM.CellUnitEnt_OwnerCom(_xySelectedCell).IsMine)
                                    {
                                        if (_eGM.CellUnitEnt_CellUnitCom(_xySelectedCell).MinAmountSteps)
                                        {
                                            _eGM.SelectorEntSelectorCom.AvailableCellsForShift = _eGM.CellUnitEnt_CellUnitCom(_xySelectedCell).GetCellsForShift();
                                            _eGM.CellUnitEnt_CellUnitCom(_xySelectedCell).GetCellsForAttack
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
                                        if (_eGM.CellUnitEnt_CellUnitCom(_xyPreviousCell).MinAmountSteps)
                                        {
                                            if (_eGM.CellBaseOperEnt_CellBaseOperCom.TryFindCellInList(_xySelectedCell, _eGM.SelectorEntSelectorCom.AvailableCellsSimpleAttack))
                                            {
                                                _photonPunRPC.AttackUnitToMaster(_xyPreviousCell, _xySelectedCell);
                                            }

                                            else if (_eGM.CellBaseOperEnt_CellBaseOperCom.TryFindCellInList(_xySelectedCell, _eGM.SelectorEntSelectorCom.AvailableCellsUniqueAttack))
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
                                        if (_eGM.CellUnitEnt_OwnerCom(_xyPreviousCell).IsMine)
                                        {
                                            if (_eGM.CellUnitEnt_CellUnitCom(_xyPreviousCell).MinAmountSteps)
                                            {
                                                if (_eGM.CellBaseOperEnt_CellBaseOperCom.TryFindCellInList(_xySelectedCell, _eGM.SelectorEntSelectorCom.AvailableCellsForShift))
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
                        if (_eGM.SelectedUnitEntUnitTypeCom.HaveUnit)
                        {
                            if (!_eGM.CellUnitEnt_UnitTypeCom(xyCurrentCell).HaveUnit)
                            {
                                if (_isStartSelectedDirect)
                                {
                                    if (!_eGM.CellUnitEnt_UnitTypeCom(xyCurrentCell).HaveUnit)
                                        _eGM.CellUnitEnt_CellUnitCom(xyCurrentCell).ActiveVisionCell(true, _eGM.SelectedUnitEntUnitTypeCom.UnitType, Instance.LocalPlayer);

                                    _eGM.CellBaseOperEnt_CellBaseOperCom.CopyXYinTo(xyCurrentCell, _xyPreviousVisionCell);
                                    _isStartSelectedDirect = false;
                                }
                                else
                                {
                                    if (!_eGM.CellUnitEnt_UnitTypeCom(_xyPreviousVisionCell).HaveUnit)
                                        _eGM.CellUnitEnt_CellUnitCom(_xyPreviousVisionCell).ActiveVisionCell(false, _eGM.SelectedUnitEntUnitTypeCom.UnitType, Instance.LocalPlayer);

                                    _eGM.CellUnitEnt_CellUnitCom(xyCurrentCell).ActiveVisionCell(true, _eGM.SelectedUnitEntUnitTypeCom.UnitType, Instance.LocalPlayer);
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

                    ActivateSelector(false, _xyPreviousCell, _xySelectedCell);

                    ClearAvailableCells();

                    _eGM.CellUnitEnt_CellUnitCom(_xyPreviousVisionCell).ActiveVisionCell(false, _eGM.SelectedUnitEntUnitTypeCom.UnitType, Instance.LocalPlayer);
                    _eGM.SelectedUnitEntUnitTypeCom.UnitType = default;

                    _eGM.CellBaseOperEnt_CellBaseOperCom.CleanXY(_xyPreviousCell);
                }
            }
        }
    }


    #region Methods

    private void ActivateSelector(in bool isActive, in int[] xyPreviousCell, in int[] xySelectedCell)
    {
        _eGM.CellEnt_CellCom(xyPreviousCell).IsSelected = false;
        _eGM.CellEnt_CellCom(xySelectedCell).IsSelected = isActive;
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