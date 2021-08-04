using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;

internal sealed class SelectorSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world;

    private EcsFilter<SelectorComponent, AvailableCellsComponent> _selectorFilter;

    private static EcsEntity _selEnt;
    internal static AvailableCellsComponent AvailableCellsCom => _selEnt.Get<AvailableCellsComponent>();

    internal static SelectorTypes SelectorType
    {
        get => _selEnt.Get<SelectorComponent>().SelectorType;
        set => _selEnt.Get<SelectorComponent>().SelectorType = value;
    }
    internal static UnitTypes SelectedUnitType
    {
        get => _selEnt.Get<SelectorComponent>().SelectedUnitType;
        set => _selEnt.Get<SelectorComponent>().SelectedUnitType = value;
    }
    internal static bool IsSelectedCell
    {
        get => _selEnt.Get<SelectorComponent>().IsSelectedCell;
        set => _selEnt.Get<SelectorComponent>().IsSelectedCell = value;
    }
    internal static int[] XySelectedCell
    {
        get => _selEnt.Get<SelectorComponent>().XySelectedCell;
        set => _selEnt.Get<SelectorComponent>().XySelectedCell = value;
    }


    public void Init()
    {
        _selEnt = _world.NewEntity()
            .Replace(new SelectorComponent(new int[2]))
            .Replace(new AvailableCellsComponent(new Dictionary<AvailableCellTypes, List<int[]>>()));
    }

    public void Run()
    {
        ref var selectorCom = ref _selectorFilter.Get1(0);

        if (InputSystem.IsClick)
        {
            if (selectorCom.RaycastGettedType == RaycastGettedTypes.UI)
            {
                selectorCom.CanShiftUnit = false;
                ClearAvailableCells();
                selectorCom.XyPreviousCell.Clean();
            }

            else if (selectorCom.RaycastGettedType == RaycastGettedTypes.Cell)
            {
                if (DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient))
                {
                    if (selectorCom.SelectorType == SelectorTypes.StartClick)
                    {
                        selectorCom.XySelectedCell = selectorCom.XyCurrentCell;

                        if (!selectorCom.XyPreviousCell.Compare(selectorCom.XySelectedCell))
                            selectorCom.IsSelectedCell = true;

                        selectorCom.XyPreviousCell = selectorCom.XySelectedCell;
                        selectorCom.SelectorType = SelectorTypes.NotStartClick;
                    }

                    else
                    {
                        if (!selectorCom.XySelectedCell.Compare(selectorCom.XyCurrentCell))
                            selectorCom.XyPreviousCell = selectorCom.XySelectedCell;

                        selectorCom.XySelectedCell = selectorCom.XyCurrentCell;
                        selectorCom.IsSelectedCell = true;
                    }
                }

                else
                {
                    if (selectorCom.HaveAnySelectorUnit)
                    {
                        PhotonPunRPC.SetUniToMaster(selectorCom.XyCurrentCell, selectorCom.SelectedUnitType);
                    }

                    else if (selectorCom.SelectorType == SelectorTypes.UpgradeUnit)
                    {
                        if (CellUnitsDataSystem.HaveAnyUnit(selectorCom.XyCurrentCell))
                        {
                            PhotonPunRPC.UpgradeUnitToMaster(selectorCom.XyCurrentCell);
                        }
                        else
                        {
                            selectorCom.SelectorType = SelectorTypes.StartClick;
                        }
                    }

                    else if (selectorCom.SelectorType == SelectorTypes.PickFire)
                    {
                        PhotonPunRPC.FireToMaster(selectorCom.XySelectedCell, selectorCom.XyCurrentCell);
                        selectorCom.SelectorType = SelectorTypes.StartClick;
                    }

                    else if (selectorCom.SelectorType == SelectorTypes.StartClick)
                    {
                        selectorCom.XySelectedCell = selectorCom.XyCurrentCell;

                        if (!selectorCom.XyPreviousCell.Compare(selectorCom.XySelectedCell))
                            selectorCom.IsSelectedCell = true;

                        if (CellUnitsDataSystem.HaveAnyUnit(selectorCom.XySelectedCell))
                        {
                            if (CellUnitsDataSystem.HaveOwner(selectorCom.XySelectedCell))
                            {
                                if (CellUnitsDataSystem.IsMine(selectorCom.XySelectedCell))
                                {
                                    if (CellUnitsDataSystem.IsMelee(selectorCom.XySelectedCell))
                                    {
                                        SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickMelee);
                                    }
                                    else
                                    {
                                        SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickArcher);
                                    }

                                    if (CellUnitsDataSystem.HaveMinAmountSteps(selectorCom.XySelectedCell))
                                    {
                                        AvailableCellsCom.SetAllCellsCopy(AvailableCellTypes.Shift, CellUnitsDataSystem.GetCellsForShift(selectorCom.XySelectedCell));

                                        CellUnitsDataSystem.GetCellsForAttack(PhotonNetwork.LocalPlayer, out var availableCellsSimpleAttack, out var availableCellsUniqueAttack, selectorCom.XySelectedCell);
                                        AvailableCellsCom.SetAllCellsCopy(AvailableCellTypes.SimpleAttack, availableCellsSimpleAttack);
                                        AvailableCellsCom.SetAllCellsCopy(AvailableCellTypes.UniqueAttack, availableCellsUniqueAttack);

                                        selectorCom.CanShiftUnit = true;
                                    }
                                }
                            }
                        }
                        selectorCom.XyPreviousCell = selectorCom.XySelectedCell;
                        selectorCom.SelectorType = SelectorTypes.NotStartClick;
                    }

                    else if (selectorCom.SelectorType == SelectorTypes.NotStartClick)
                    {
                        if (!selectorCom.XySelectedCell.Compare(selectorCom.XyCurrentCell))
                            selectorCom.XyPreviousCell = selectorCom.XySelectedCell;


                        selectorCom.XySelectedCell = selectorCom.XyCurrentCell;
                        selectorCom.IsSelectedCell = true;


                        if (CellUnitsDataSystem.HaveAnyUnit(selectorCom.XySelectedCell))
                        {
                            if (CellUnitsDataSystem.HaveOwner(selectorCom.XySelectedCell))
                            {
                                if (CellUnitsDataSystem.IsMine(selectorCom.XySelectedCell))
                                {
                                    if (CellUnitsDataSystem.IsMelee(selectorCom.XySelectedCell))
                                    {
                                        SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickMelee);
                                    }
                                    else
                                    {
                                        SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickArcher);
                                    }

                                    if (CellUnitsDataSystem.HaveMinAmountSteps(selectorCom.XySelectedCell))
                                    {
                                        AvailableCellsCom.SetAllCellsCopy(AvailableCellTypes.Shift, CellUnitsDataSystem.GetCellsForShift(selectorCom.XySelectedCell));

                                        CellUnitsDataSystem.GetCellsForAttack(PhotonNetwork.LocalPlayer, out var availableCellsSimpleAttack, out var availableCellsUniqueAttack, selectorCom.XySelectedCell);
                                        AvailableCellsCom.SetAllCellsCopy(AvailableCellTypes.SimpleAttack, availableCellsSimpleAttack);
                                        AvailableCellsCom.SetAllCellsCopy(AvailableCellTypes.UniqueAttack, availableCellsUniqueAttack);

                                        selectorCom.CanShiftUnit = true;
                                    }

                                    else
                                    {
                                        ClearAvailableCells();
                                        selectorCom.CanShiftUnit = false;
                                    }
                                }

                                else
                                {
                                    selectorCom.CanShiftUnit = false;

                                    if (AvailableCellsCom.TryFindCell(AvailableCellTypes.SimpleAttack, selectorCom.XySelectedCell))
                                    {
                                        PhotonPunRPC.AttackUnitToMaster(selectorCom.XyPreviousCell, selectorCom.XySelectedCell);
                                    }

                                    else if (AvailableCellsCom.TryFindCell(AvailableCellTypes.UniqueAttack, selectorCom.XySelectedCell))
                                    {
                                        PhotonPunRPC.AttackUnitToMaster(selectorCom.XyPreviousCell, selectorCom.XySelectedCell);
                                    }

                                    ClearAvailableCells();
                                }
                            }

                            else if (CellUnitsDataSystem.IsBot(selectorCom.XySelectedCell))
                            {
                                if (AvailableCellsCom.TryFindCell(AvailableCellTypes.SimpleAttack, selectorCom.XySelectedCell))
                                {
                                    PhotonPunRPC.AttackUnitToMaster(selectorCom.XyPreviousCell, selectorCom.XySelectedCell);
                                }

                                else if (AvailableCellsCom.TryFindCell(AvailableCellTypes.UniqueAttack, selectorCom.XySelectedCell))
                                {
                                    PhotonPunRPC.AttackUnitToMaster(selectorCom.XyPreviousCell, selectorCom.XySelectedCell);
                                }

                                ClearAvailableCells();
                            }
                        }

                        else
                        {
                            if (selectorCom.CanShiftUnit)
                            {
                                if (CellUnitsDataSystem.HaveAnyUnit(selectorCom.XyPreviousCell))
                                {
                                    if (CellUnitsDataSystem.HaveOwner(selectorCom.XyPreviousCell))
                                    {
                                        if (CellUnitsDataSystem.IsMine(selectorCom.XyPreviousCell))
                                        {
                                            if (CellUnitsDataSystem.HaveMinAmountSteps(selectorCom.XyPreviousCell))
                                            {
                                                if (AvailableCellsCom.TryFindCell(AvailableCellTypes.Shift, selectorCom.XySelectedCell))
                                                {
                                                    PhotonPunRPC.ShiftUnitToMaster(selectorCom.XyPreviousCell, selectorCom.XySelectedCell);
                                                }
                                            }
                                        }
                                    }
                                }
                                ClearAvailableCells();
                                selectorCom.CanShiftUnit = false;
                            }
                        }
                    }
                }
            }

            else
            {
                selectorCom.CanShiftUnit = false;

                selectorCom.IsSelectedCell = false;

                ClearAvailableCells();

                if (selectorCom.SelectedUnitType != default)
                {
                    CellUnitViewSystem.SetEnabledUnit(false, selectorCom.XyPreviousVisionCell);
                    selectorCom.SelectedUnitType = default;
                    //selectorCom.XyPreviousCell.Clean();
                }

                selectorCom.SelectorType = SelectorTypes.StartClick;
            }
        }

        else
        {
            if (selectorCom.RaycastGettedType == RaycastGettedTypes.UI)
            {
            }

            else if (selectorCom.RaycastGettedType == RaycastGettedTypes.Cell)
            {
                if (selectorCom.HaveAnySelectorUnit)
                {
                    if (!CellUnitsDataSystem.HaveAnyUnit(selectorCom.XyCurrentCell) || !CellUnitsDataSystem.IsVisibleUnit(PhotonNetwork.IsMasterClient, selectorCom.XyCurrentCell))
                    {
                        if (selectorCom.IsStartSelectedDirect)
                        {
                            if (!CellUnitsDataSystem.HaveAnyUnit(selectorCom.XyCurrentCell))
                                CellUnitViewSystem.ActiveSelectorVisionUnit(true, selectorCom.SelectedUnitType, selectorCom.XyCurrentCell);

                            selectorCom.XyPreviousVisionCell = selectorCom.XyCurrentCell;
                            selectorCom.IsStartSelectedDirect = false;
                        }
                        else
                        {
                            CellUnitViewSystem.ActiveSelectorVisionUnit(false, selectorCom.SelectedUnitType, selectorCom.XyPreviousVisionCell);
                            CellUnitViewSystem.ActiveSelectorVisionUnit(true, selectorCom.SelectedUnitType, selectorCom.XyCurrentCell);

                            selectorCom.XyPreviousVisionCell = selectorCom.XyCurrentCell;
                        }
                    }
                }
            }

            else
            {

            }
        }
    }


    private void ClearAvailableCells()
    {
        AvailableCellsCom.ClearAvailableCells(AvailableCellTypes.Shift);
        AvailableCellsCom.ClearAvailableCells(AvailableCellTypes.SimpleAttack);
        AvailableCellsCom.ClearAvailableCells(AvailableCellTypes.UniqueAttack);
    }

}