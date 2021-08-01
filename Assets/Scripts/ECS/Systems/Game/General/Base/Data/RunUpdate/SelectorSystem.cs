﻿using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Game.Else.Units;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;
using static Assets.Scripts.Workers.SelectorWorker;

internal sealed class SelectorSystem : IEcsRunSystem
{
    private int[] XyPreviousCell { get => GetXy(SelectorCellTypes.Previous); set => SetXy(SelectorCellTypes.Previous, value); }
    private int[] XySelectedCell { get => GetXy(SelectorCellTypes.Selected); set => SetXy(SelectorCellTypes.Selected, value); }
    private int[] XyCurrentCell { get => GetXy(SelectorCellTypes.Current); set => SetXy(SelectorCellTypes.Current, value); }
    private int[] XyPreviousVisionCell { get => GetXy(SelectorCellTypes.PreviousVision); set => SetXy(SelectorCellTypes.PreviousVision, value); }


    public void Run()
    {
        if (RaycastGettedType == RaycastGettedTypes.UI)
        {
            if (IsClick)
            {
                CanShiftUnit = false;

                ClearAvailableCells();

                XyPreviousCell.Clean();
            }
        }

        else if (RaycastGettedType == RaycastGettedTypes.Cell)
        {
            if (IsClick)
            {
                if (DownDonerUIWorker.IsDoned(PhotonNetwork.IsMasterClient))
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

                    else if (SelectorWorker.SelectorType == SelectorTypes.UpgradeUnit)
                    {
                        if (CellUnitsDataWorker.HaveAnyUnit(XyCurrentCell))
                        {
                            PhotonPunRPC.UpgradeUnitToMaster(XyCurrentCell);
                        }
                        else
                        {
                            SelectorWorker.SelectorType = SelectorTypes.Other;
                        }
                    }

                    else if (SelectorWorker.SelectorType == SelectorTypes.PickFire)
                    {
                        PhotonPunRPC.FireToMaster(XySelectedCell, XyCurrentCell);
                        SelectorWorker.SelectorType = SelectorTypes.Other;
                    }

                    else if (CanExecuteStartClick)
                    {
                        XySelectedCell = XyCurrentCell;

                        if (!XyPreviousCell.Compare(XySelectedCell))
                            IsSelectedCell = true;

                        if (CellUnitsDataWorker.HaveAnyUnit(XySelectedCell))
                        {
                            if (CellUnitsDataWorker.HaveOwner(XySelectedCell))
                            {
                                if (CellUnitsDataWorker.IsMine(XySelectedCell))
                                {
                                    if (CellUnitsDataWorker.IsMelee(XySelectedCell))
                                    {
                                        SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickMelee);
                                    }
                                    else
                                    {
                                        SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickArcher);
                                    }

                                    if (CellUnitsDataWorker.HaveMinAmountSteps(XySelectedCell))
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


                        if (CellUnitsDataWorker.HaveAnyUnit(XySelectedCell))
                        {
                            if (CellUnitsDataWorker.HaveOwner(XySelectedCell))
                            {
                                if (CellUnitsDataWorker.IsMine(XySelectedCell))
                                {
                                    if (CellUnitsDataWorker.IsMelee(XySelectedCell))
                                    {
                                        SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickMelee);
                                    }
                                    else
                                    {
                                        SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickArcher);
                                    }

                                    if (CellUnitsDataWorker.HaveMinAmountSteps(XySelectedCell))
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

                            else if (CellUnitsDataWorker.IsBot(XySelectedCell))
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
                                if (CellUnitsDataWorker.HaveAnyUnit(XyPreviousCell))
                                {
                                    if (CellUnitsDataWorker.HaveOwner(XyPreviousCell))
                                    {
                                        if (CellUnitsDataWorker.IsMine(XyPreviousCell))
                                        {
                                            if (CellUnitsDataWorker.HaveMinAmountSteps(XyPreviousCell))
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
                    if (!CellUnitsDataWorker.HaveAnyUnit(XyCurrentCell) || !CellUnitsDataWorker.IsVisibleUnit(PhotonNetwork.IsMasterClient, XyCurrentCell))
                    {
                        if (IsStartSelectedDirect)
                        {
                            if (!CellUnitsDataWorker.HaveAnyUnit(XyCurrentCell))
                                CellUnitsViewWorker.ActiveSelectorVisionUnit(true, SelectedUnitType, XyCurrentCell);

                            XyPreviousVisionCell = XyCurrentCell;
                            IsStartSelectedDirect = false;
                        }
                        else
                        {
                            CellUnitsViewWorker.ActiveSelectorVisionUnit(false, SelectedUnitType, XyPreviousVisionCell);
                            CellUnitsViewWorker.ActiveSelectorVisionUnit(true, SelectedUnitType, XyCurrentCell);

                            XyPreviousVisionCell = XyCurrentCell;
                        }
                    }
                }
            }
        }

        else
        {
            if (IsClick)
            {
                CanShiftUnit = false;
                CanExecuteStartClick = true;

                IsSelectedCell = false;

                ClearAvailableCells();

                if (SelectedUnitType != default)
                {
                    CellUnitsViewWorker.SetEnabledUnit(false, XyPreviousVisionCell);
                    SelectedUnitType = default;
                    //XyPreviousCell.Clean();
                }


                SelectorWorker.SelectorType = SelectorTypes.Other;
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
        AvailableCellsContainer.SetAllCellsCopy(AvailableCellTypes.Shift, CellUnitsDataWorker.GetCellsForShift(XySelectedCell));

        CellUnitsDataWorker.GetCellsForAttack(PhotonNetwork.LocalPlayer, out var availableCellsSimpleAttack, out var availableCellsUniqueAttack, XySelectedCell);
        AvailableCellsContainer.SetAllCellsCopy(AvailableCellTypes.SimpleAttack, availableCellsSimpleAttack);
        AvailableCellsContainer.SetAllCellsCopy(AvailableCellTypes.UniqueAttack, availableCellsUniqueAttack);
    }
}