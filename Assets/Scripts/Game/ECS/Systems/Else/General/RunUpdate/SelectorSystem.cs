using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.View.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Leopotam.Ecs;

internal sealed class SelectorSystem : IEcsRunSystem
{
    private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

    private EcsFilter<SelectorCom> _selectorFilter = default;
    private EcsFilter<InputComponent> _inputFilter = default;
    private EcsFilter<SoundEffectsComp> _soundFilter = default;
    private EcsFilter<CellsForAttackCom> _cellsAttackFilt = default;
    private EcsFilter<CellsForShiftCom> _cellsShiftFilt = default;

    public void Run()
    {
        CellUnitDataCom UnitDatCom(byte idxCell) => _cellUnitFilter.Get1(idxCell);
        OwnerCom OwnUnitCom(byte idxCell) => _cellUnitFilter.Get2(idxCell);


        ref var selCom = ref _selectorFilter.Get1(0);
        ref var cellsAttackCom = ref _cellsAttackFilt.Get1(0);
        ref var cellsShiftCom = ref _cellsShiftFilt.Get1(0);
        ref var soundEffectCom = ref _soundFilter.Get1(0);


        if (_inputFilter.Get1(0).IsClicked)
        {
            if (selCom.Is(RaycastGettedTypes.UI))
            {
                selCom.DefSelectedUnit();
            }

            else if (selCom.Is(RaycastGettedTypes.Cell))
            {
                if (GameModesCom.IsOnlineMode && !WhoseMoveCom.IsMyOnlineMove)
                {
                    if (!selCom.IsSelCell)
                    {
                        if (selCom.IdxPreCell != selCom.IdxSelCell)
                        {
                            selCom.IdxSelCell = selCom.IdxCurCell;
                        }
                        else
                        {
                            selCom.IdxSelCell = default;
                        }


                        selCom.IdxPreCell = selCom.IdxSelCell;
                    }

                    else
                    {
                        if (selCom.IdxSelCell != selCom.IdxCurCell)
                            selCom.IdxPreCell = selCom.IdxSelCell;

                        selCom.IdxSelCell = selCom.IdxCurCell;
                    }
                }

                else if (selCom.IsSelUnit)
                {
                    RpcSys.SetUniToMaster(selCom.IdxCurCell, selCom.SelUnitType);
                    selCom.SelUnitType = default;
                }

                else if (selCom.IsCellClickType(CellClickTypes.PickFire))
                {
                    RpcSys.FireToMaster(selCom.IdxSelCell, selCom.IdxCurCell);

                    selCom.CellClickType = default;
                    selCom.IdxSelCell = selCom.IdxCurCell;
                }

                else if (selCom.IsCellClickType(CellClickTypes.GiveTakeTW))
                {
                    if (UnitDatCom(selCom.IdxCurCell).Is(new[] { UnitTypes.Pawn, UnitTypes.Rook, UnitTypes.Bishop }))
                    {
                        RpcSys.GiveTakeToolWeapon(selCom.ToolWeaponTypeForGiveTake, selCom.IdxCurCell);
                    }
                    else
                    {
                        selCom.IdxSelCell = selCom.IdxCurCell;
                        selCom.DefCellClickType();
                    }
                }

                else if (selCom.IsSelCell)
                {
                    if (selCom.IdxSelCell != selCom.IdxCurCell)
                        selCom.IdxPreCell = selCom.IdxSelCell;

                    selCom.IdxSelCell = selCom.IdxCurCell;

                    var b1 = cellsAttackCom.FindByIdx(WhoseMoveCom.CurPlayer, AttackTypes.Simple, selCom.IdxPreCell, selCom.IdxSelCell);
                    var b2 = cellsAttackCom.FindByIdx(WhoseMoveCom.CurPlayer, AttackTypes.Unique, selCom.IdxPreCell, selCom.IdxSelCell);

                    if (b1 || b2)
                    {
                        RpcSys.AttackUnitToMaster(selCom.IdxPreCell, selCom.IdxSelCell);
                    }

                    if (cellsShiftCom.HaveIdxCell(WhoseMoveCom.CurPlayer, selCom.IdxPreCell, selCom.IdxSelCell))
                    {
                        RpcSys.ShiftUnitToMaster(selCom.IdxPreCell, selCom.IdxSelCell);
                    }

                    else if (UnitDatCom(selCom.IdxSelCell).HaveUnit)
                    {
                        if (OwnUnitCom(selCom.IdxSelCell).IsMine)
                        {
                            if (UnitDatCom(selCom.IdxSelCell).IsMelee)
                            {
                                soundEffectCom.Play(SoundEffectTypes.PickMelee);
                            }
                            else
                            {
                                soundEffectCom.Play(SoundEffectTypes.PickArcher);
                            }
                        }
                    }
                }

                else
                {
                    if (selCom.IdxPreCell != selCom.IdxSelCell || selCom.IdxPreCell == 0)
                    {
                        selCom.IdxSelCell = selCom.IdxCurCell;
                    }
                    else
                    {
                        selCom.DefSelectedCell();
                    }

                    if (UnitDatCom(selCom.IdxSelCell).HaveUnit)
                    {
                        if (OwnUnitCom(selCom.IdxSelCell).IsMine)
                        {
                            if (UnitDatCom(selCom.IdxSelCell).IsMelee)
                            {
                                soundEffectCom.Play(SoundEffectTypes.PickMelee);
                            }
                            else
                            {
                                soundEffectCom.Play(SoundEffectTypes.PickArcher);
                            }
                        }
                    }



                    selCom.IdxPreCell = selCom.IdxSelCell;
                }

            }

            else
            {
                selCom.DefSelectedUnit();
                selCom.DefSelectedCell();
                selCom.DefCellClickType();
            }
        }

        else
        {
            if (selCom.Is(RaycastGettedTypes.UI))
            {
            }

            else if (selCom.Is(RaycastGettedTypes.Cell))
            {
                if (selCom.IsSelUnit)
                {
                    if (!UnitDatCom(selCom.IdxCurCell).HaveUnit || !UnitDatCom(selCom.IdxCurCell).IsVisibleUnit(WhoseMoveCom.CurPlayer))
                    {
                        if (selCom.IsStartDirectToCell)
                        {
                            selCom.IdxPreVisionCell = selCom.IdxCurCell;
                            selCom.IdxCurCell = default;
                        }
                        else
                        {
                            selCom.IdxPreVisionCell = selCom.IdxCurCell;
                        }
                    }
                }
            }
        }
    }
}