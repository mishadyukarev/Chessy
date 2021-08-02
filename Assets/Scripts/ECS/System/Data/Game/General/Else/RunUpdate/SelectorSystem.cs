using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

internal sealed class SelectorSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _currentWorld;


    private static EcsEntity _selectorEnt;


    internal static SelectorTypes SelectorType
    {
        get => _selectorEnt.Get<SelectorComponent>().SelectorType;
        set => _selectorEnt.Get<SelectorComponent>().SelectorType = value;
    }

    internal static UnitTypes SelectedUnitType
    {
        get => _selectorEnt.Get<SelectorComponent>().SelectedUnitType;
        set => _selectorEnt.Get<SelectorComponent>().SelectedUnitType = value;
    }
    internal static bool HaveAnySelectorUnit => SelectedUnitType != UnitTypes.None;


    internal static bool IsSelectedCell
    {
        get => _selectorEnt.Get<SelectorComponent>().IsSelectedCell;
        set => _selectorEnt.Get<SelectorComponent>().IsSelectedCell = value;
    }

    internal static RaycastHit2D RaycastHit2D
    {
        get => _selectorEnt.Get<SelectorComponent>().RaycastHit2D;
        set => _selectorEnt.Get<SelectorComponent>().RaycastHit2D = value;
    }
    internal static RaycastGettedTypes RaycastGettedType
    {
        get => _selectorEnt.Get<SelectorComponent>().RaycastGettedType;
        set => _selectorEnt.Get<SelectorComponent>().RaycastGettedType = value;
    }

    internal static bool CanShiftUnit
    {
        get => _selectorEnt.Get<SelectorComponent>().CanShiftUnit;
        set => _selectorEnt.Get<SelectorComponent>().CanShiftUnit = value;
    }
    internal static bool CanExecuteStartClick
    {
        get => _selectorEnt.Get<SelectorComponent>().CanExecuteStartClick;
        set => _selectorEnt.Get<SelectorComponent>().CanExecuteStartClick = value;
    }
    internal static bool IsStartSelectedDirect
    {
        get => _selectorEnt.Get<SelectorComponent>().IsStartSelectedDirect;
        set => _selectorEnt.Get<SelectorComponent>().IsStartSelectedDirect = value;
    }

    internal static int[] XyPreviousCell
    {
        get => _selectorEnt.Get<SelectorComponent>().XyPreviousCell;
        set => _selectorEnt.Get<SelectorComponent>().XyPreviousCell = value;
    }
    internal static int[] XySelectedCell
    {
        get => _selectorEnt.Get<SelectorComponent>().XySelectedCell; 
        set => _selectorEnt.Get<SelectorComponent>().XySelectedCell = value;
    }
    private static int[] XyCurrentCell
    {
        get => _selectorEnt.Get<SelectorComponent>().XyCurrenCell;
        set => _selectorEnt.Get<SelectorComponent>().XyCurrenCell = value;
    }
    private int[] XyPreviousVisionCell
    {
        get => _selectorEnt.Get<SelectorComponent>().XyPreviousVisionCell;
        set => _selectorEnt.Get<SelectorComponent>().XyPreviousVisionCell = value;
    }

    public void Init()
    {
        _selectorEnt = _currentWorld.NewEntity()
            .Replace(new SelectorComponent(new int[2]))
            .Replace(new UnitTypeComponent());
    }

    public void Run()
    {
        if (RaycastGettedType == RaycastGettedTypes.UI)
        {
            if (InputSystem.IsClick)
            {
                CanShiftUnit = false;

                ClearAvailableCells();

                XyPreviousCell.Clean();
            }
        }

        else if (RaycastGettedType == RaycastGettedTypes.Cell)
        {
            if (InputSystem.IsClick)
            {
                if (DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient))
                {
                    if (CanExecuteStartClick)
                    {
                        XySelectedCell = XyCurrentCell;

                        if (!XyPreviousCell.Compare(XySelectedCell))
                            IsSelectedCell = true;

                        XyPreviousCell = XySelectedCell;
                        CanExecuteStartClick = false;
                    }

                    else
                    {
                        if (!XySelectedCell.Compare(XyCurrentCell))
                            XyPreviousCell = XySelectedCell;

                        XySelectedCell = XyCurrentCell;
                        IsSelectedCell = true;
                    }
                }

                else
                {
                    if (HaveAnySelectorUnit)
                    {
                        PhotonPunRPC.SetUniToMaster(XyCurrentCell, SelectedUnitType);
                    }

                    else if (SelectorType == SelectorTypes.UpgradeUnit)
                    {
                        if (CellUnitsDataSystem.HaveAnyUnit(XyCurrentCell))
                        {
                            PhotonPunRPC.UpgradeUnitToMaster(XyCurrentCell);
                        }
                        else
                        {
                            SelectorType = SelectorTypes.Other;
                        }
                    }

                    else if (SelectorType == SelectorTypes.PickFire)
                    {
                        PhotonPunRPC.FireToMaster(XySelectedCell, XyCurrentCell);
                        SelectorType = SelectorTypes.Other;
                    }

                    else if (CanExecuteStartClick)
                    {
                        XySelectedCell = XyCurrentCell;

                        if (!XyPreviousCell.Compare(XySelectedCell))
                            IsSelectedCell = true;

                        if (CellUnitsDataSystem.HaveAnyUnit(XySelectedCell))
                        {
                            if (CellUnitsDataSystem.HaveOwner(XySelectedCell))
                            {
                                if (CellUnitsDataSystem.IsMine(XySelectedCell))
                                {
                                    if (CellUnitsDataSystem.IsMelee(XySelectedCell))
                                    {
                                        SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickMelee);
                                    }
                                    else
                                    {
                                        SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickArcher);
                                    }

                                    if (CellUnitsDataSystem.HaveMinAmountSteps(XySelectedCell))
                                    {
                                        GetCells();

                                        CanShiftUnit = true;
                                    }
                                }
                            }
                        }
                        XyPreviousCell = XySelectedCell;
                        CanExecuteStartClick = false;
                    }

                    else
                    {
                        if (!XySelectedCell.Compare(XyCurrentCell))
                            XyPreviousCell = XySelectedCell;


                        XySelectedCell = XyCurrentCell;
                        IsSelectedCell = true;


                        if (CellUnitsDataSystem.HaveAnyUnit(XySelectedCell))
                        {
                            if (CellUnitsDataSystem.HaveOwner(XySelectedCell))
                            {
                                if (CellUnitsDataSystem.IsMine(XySelectedCell))
                                {
                                    if (CellUnitsDataSystem.IsMelee(XySelectedCell))
                                    {
                                        SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickMelee);
                                    }
                                    else
                                    {
                                        SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickArcher);
                                    }

                                    if (CellUnitsDataSystem.HaveMinAmountSteps(XySelectedCell))
                                    {
                                        GetCells();

                                        CanShiftUnit = true;
                                    }

                                    else
                                    {
                                        ClearAvailableCells();
                                        CanShiftUnit = false;
                                    }
                                }

                                else
                                {
                                    CanShiftUnit = false;

                                    if (AvailableCellsContainer.TryFindCell(AvailableCellTypes.SimpleAttack, XySelectedCell))
                                    {
                                        PhotonPunRPC.AttackUnitToMaster(XyPreviousCell, XySelectedCell);
                                    }

                                    else if (AvailableCellsContainer.TryFindCell(AvailableCellTypes.UniqueAttack, XySelectedCell))
                                    {
                                        PhotonPunRPC.AttackUnitToMaster(XyPreviousCell, XySelectedCell);
                                    }

                                    ClearAvailableCells();
                                }
                            }

                            else if (CellUnitsDataSystem.IsBot(XySelectedCell))
                            {
                                if (AvailableCellsContainer.TryFindCell(AvailableCellTypes.SimpleAttack, XySelectedCell))
                                {
                                    PhotonPunRPC.AttackUnitToMaster(XyPreviousCell, XySelectedCell);
                                }

                                else if (AvailableCellsContainer.TryFindCell(AvailableCellTypes.UniqueAttack, XySelectedCell))
                                {
                                    PhotonPunRPC.AttackUnitToMaster(XyPreviousCell, XySelectedCell);
                                }

                                ClearAvailableCells();
                            }
                        }

                        else
                        {
                            if (CanShiftUnit)
                            {
                                if (CellUnitsDataSystem.HaveAnyUnit(XyPreviousCell))
                                {
                                    if (CellUnitsDataSystem.HaveOwner(XyPreviousCell))
                                    {
                                        if (CellUnitsDataSystem.IsMine(XyPreviousCell))
                                        {
                                            if (CellUnitsDataSystem.HaveMinAmountSteps(XyPreviousCell))
                                            {
                                                if (AvailableCellsContainer.TryFindCell(AvailableCellTypes.Shift, XySelectedCell))
                                                {
                                                    PhotonPunRPC.ShiftUnitToMaster(XyPreviousCell, XySelectedCell);
                                                }
                                            }
                                        }
                                    }
                                }
                                ClearAvailableCells();
                                CanShiftUnit = false;
                            }
                        }
                    }
                }
            }

            else
            {
                if (HaveAnySelectorUnit)
                {
                    if (!CellUnitsDataSystem.HaveAnyUnit(XyCurrentCell) || !CellUnitsDataSystem.IsVisibleUnit(PhotonNetwork.IsMasterClient, XyCurrentCell))
                    {
                        if (IsStartSelectedDirect)
                        {
                            if (!CellUnitsDataSystem.HaveAnyUnit(XyCurrentCell))
                                CellUnitViewSystem.ActiveSelectorVisionUnit(true, SelectedUnitType, XyCurrentCell);

                            XyPreviousVisionCell = XyCurrentCell;
                            IsStartSelectedDirect = false;
                        }
                        else
                        {
                            CellUnitViewSystem.ActiveSelectorVisionUnit(false, SelectedUnitType, XyPreviousVisionCell);
                            CellUnitViewSystem.ActiveSelectorVisionUnit(true, SelectedUnitType, XyCurrentCell);

                            XyPreviousVisionCell = XyCurrentCell;
                        }
                    }
                }
            }
        }

        else
        {
            if (InputSystem.IsClick)
            {
                CanShiftUnit = false;
                CanExecuteStartClick = true;

                IsSelectedCell = false;

                ClearAvailableCells();

                if (SelectedUnitType != default)
                {
                    CellUnitViewSystem.SetEnabledUnit(false, XyPreviousVisionCell);
                    SelectedUnitType = default;
                    //XyPreviousCell.Clean();
                }


                SelectorType = SelectorTypes.Other;
            }
        }
    }


    private void ClearAvailableCells()
    {
        AvailableCellsContainer.ClearAvailableCells(AvailableCellTypes.Shift);
        AvailableCellsContainer.ClearAvailableCells(AvailableCellTypes.SimpleAttack);
        AvailableCellsContainer.ClearAvailableCells(AvailableCellTypes.UniqueAttack);
    }

    private void GetCells()
    {
        AvailableCellsContainer.SetAllCellsCopy(AvailableCellTypes.Shift, CellUnitsDataSystem.GetCellsForShift(XySelectedCell));

        CellUnitsDataSystem.GetCellsForAttack(PhotonNetwork.LocalPlayer, out var availableCellsSimpleAttack, out var availableCellsUniqueAttack, XySelectedCell);
        AvailableCellsContainer.SetAllCellsCopy(AvailableCellTypes.SimpleAttack, availableCellsSimpleAttack);
        AvailableCellsContainer.SetAllCellsCopy(AvailableCellTypes.UniqueAttack, availableCellsUniqueAttack);
    }
}