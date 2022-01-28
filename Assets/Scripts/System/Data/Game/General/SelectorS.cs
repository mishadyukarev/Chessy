using System;

namespace Game.Game
{
    readonly struct SelectorS : IEcsRunSystem
    {
        public void Run()
        {
            var idx_cur = Entities.CurrentIdxE.IdxC.Idx;
            var idx_sel = Entities.SelectedIdxE.IdxC.Idx;

            ref var unit_cur = ref CellUnitEs.Else(idx_cur).UnitC;
            ref var levUnit_cur = ref CellUnitEs.Else(idx_cur).LevelC;
            ref var ownUnit_cur = ref CellUnitEs.Else(idx_cur).OwnerC;

            ref var unit_sel = ref CellUnitEs.Else(idx_cur).UnitC;

            ref var raycast = ref Entities.ClickerObject.RayCastTC;
            ref var cellClick = ref Entities.ClickerObject.CellClickC;

            if (Entities.InputE.IsClickedC.IsClicked)
            {
                if (raycast.Is(RaycastTypes.Cell))
                {
                    if (!Entities.WhoseMove.IsMyMove)
                    {
                        Entities.SelectedIdxE.IdxC.Idx = Entities.CurrentIdxE.IdxC.Idx;
                    }

                    else
                    {
                        switch (cellClick.Click)
                        {
                            case CellClickTypes.None: throw new Exception();

                            case CellClickTypes.SimpleClick:
                                {
                                    if (Entities.SelectedIdxE.IsSelCell)
                                    {
                                        if (CellsForAttackUnitsEs.CanAttack(Entities.SelectedIdxE.IdxC.Idx, Entities.CurrentIdxE.IdxC.Idx, Entities.WhoseMove.CurPlayerI, out var attack))
                                        {
                                            Entities.Rpc.AttackUnitToMaster(Entities.SelectedIdxE.IdxC.Idx, Entities.CurrentIdxE.IdxC.Idx);
                                        }

                                        else if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(Entities.WhoseMove.CurPlayerI, Entities.SelectedIdxE.IdxC.Idx).Contains(Entities.CurrentIdxE.IdxC.Idx))
                                        {
                                            Entities.Rpc.ShiftUnitToMaster(Entities.SelectedIdxE.IdxC.Idx, Entities.CurrentIdxE.IdxC.Idx);
                                        }

                                        else
                                        {
                                            if (unit_cur.Have)
                                            {
                                                if (ownUnit_cur.Is(Entities.WhoseMove.CurPlayerI))
                                                {
                                                    if (unit_cur.Is(UnitTypes.Scout))
                                                    {

                                                    }
                                                    else if (unit_cur.IsMelee)
                                                    {
                                                        Entities.Sound(ClipTypes.PickMelee).Sound.Invoke();
                                                    }
                                                    else
                                                    {
                                                        Entities.Sound(ClipTypes.PickArcher).Sound.Invoke();
                                                    }
                                                }
                                            }
                                        }

                                        Entities.SelectedIdxE.IdxC.Idx = Entities.CurrentIdxE.IdxC.Idx;
                                    }

                                    else
                                    {
                                        if (unit_cur.Have)
                                        {
                                            if (ownUnit_cur.Is(Entities.WhoseMove.CurPlayerI))
                                            {
                                                if (unit_cur.Is(UnitTypes.Scout))
                                                {

                                                }
                                                else if (unit_cur.IsMelee)
                                                {
                                                    Entities.Sound(ClipTypes.PickMelee).Sound.Invoke();
                                                }
                                                else
                                                {
                                                    Entities.Sound(ClipTypes.PickArcher).Sound.Invoke();
                                                }
                                            }
                                        }

                                        Entities.SelectedIdxE.IdxC.Idx = Entities.CurrentIdxE.IdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.SetUnit:
                                {
                                    Entities.Rpc.SetUniToMaster(Entities.CurrentIdxE.IdxC.Idx, Entities.SelectedUnitE.UnitTC.Unit);
                                    cellClick.Click = CellClickTypes.SimpleClick;
                                }
                                break;

                            case CellClickTypes.GiveTakeTW:
                                {
                                    if (unit_cur.Is(UnitTypes.Pawn) && ownUnit_cur.Is(Entities.WhoseMove.CurPlayerI))
                                    {
                                        Entities.Rpc.GiveTakeToolWeaponToMaster(SelectedToolWeaponE.SelectedTW<ToolWeaponC>().ToolWeapon, SelectedToolWeaponE.SelectedTW<LevelTC>().Level, Entities.CurrentIdxE.IdxC.Idx);
                                    }
                                    else
                                    {
                                        cellClick.Click = CellClickTypes.SimpleClick;
                                        Entities.SelectedIdxE.IdxC.Idx = Entities.CurrentIdxE.IdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.UpgradeUnit:
                                {
                                    if (unit_cur.Is(new[] { UnitTypes.Pawn, UnitTypes.Archer })
                                        && ownUnit_cur.Is(Entities.WhoseMove.CurPlayerI)
                                        && !levUnit_cur.Is(LevelTypes.Second))
                                    {
                                        Entities.Rpc.UpgradeUnitToMaster(Entities.CurrentIdxE.IdxC.Idx);
                                    }
                                    else
                                    {
                                        cellClick.Click = CellClickTypes.SimpleClick;
                                        Entities.SelectedIdxE.IdxC.Idx = Entities.CurrentIdxE.IdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.GiveScout:
                                {
                                    if (unit_cur.Is(UnitTypes.Pawn)
                                        && ownUnit_cur.Is(Entities.WhoseMove.CurPlayerI))
                                    {
                                        Entities.Rpc.FromNewUnitToMas(UnitTypes.Scout, Entities.CurrentIdxE.IdxC.Idx);
                                    }

                                    cellClick.Click = CellClickTypes.SimpleClick;
                                    Entities.SelectedIdxE.IdxC.Idx = Entities.CurrentIdxE.IdxC.Idx;
                                }
                                break;

                            case CellClickTypes.GiveHero:
                                {
                                    if (InventorUnitsE.HaveHero(Entities.WhoseMove.CurPlayerI, out var hero))
                                    {
                                        if (hero == UnitTypes.Elfemale || hero == UnitTypes.Snowy)
                                        {
                                            if (Entities.SelectedIdxE.IdxC.Idx == 0)
                                            {
                                                Entities.SelectedIdxE.IdxC.Idx = Entities.CurrentIdxE.IdxC.Idx;

                                                if (unit_cur.Is(UnitTypes.Archer))
                                                {
                                                    Entities.Sound(ClipTypes.PickArcher).Sound.Invoke();
                                                }
                                                else
                                                {
                                                    cellClick.Click = CellClickTypes.SimpleClick;
                                                }
                                            }
                                            else
                                            {
                                                if (idx_cur != idx_sel)
                                                {
                                                    if (unit_cur.Is(UnitTypes.Archer))
                                                    {
                                                        if (unit_sel.Is(UnitTypes.Archer))
                                                        {
                                                            Entities.Rpc.FromToNewUnitToMas(hero, Entities.SelectedIdxE.IdxC.Idx, Entities.CurrentIdxE.IdxC.Idx);
                                                            cellClick.Click = CellClickTypes.SimpleClick;
                                                        }

                                                        Entities.Sound(ClipTypes.ClickToTable).Sound.Invoke();
                                                    }
                                                    else
                                                    {
                                                        cellClick.Click = CellClickTypes.SimpleClick;
                                                    }

                                                    Entities.SelectedIdxE.IdxC.Idx = Entities.CurrentIdxE.IdxC.Idx;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception();
                                    }
                                }
                                break;

                            case CellClickTypes.UniqueAbility:
                                {
                                    if (Entities.SelectedUniqueAbilityE.AbilityC.Is(AbilityTypes.FireArcher))
                                    {
                                        Entities.Rpc.FireArcherToMas(Entities.SelectedIdxE.IdxC.Idx, Entities.CurrentIdxE.IdxC.Idx);
                                    }

                                    else if (Entities.SelectedUniqueAbilityE.AbilityC.Is(AbilityTypes.ChangeDirectionWind))
                                    {
                                        CellSpaceSupport.TryGetIdxAround(Entities.CurrentIdxE.IdxC.Idx, out var directs);

                                        foreach (var item in directs)
                                        {
                                            if (item.Value == Entities.CurrentIdxE.IdxC.Idx)
                                            {
                                                Entities.Rpc.PutOutFireElffToMas(Entities.SelectedIdxE.IdxC.Idx, Entities.CurrentIdxE.IdxC.Idx);
                                            }
                                        }
                                    }

                                    else if (Entities.SelectedUniqueAbilityE.AbilityC.Is(AbilityTypes.StunElfemale))
                                    {
                                        Entities.Rpc.StunElfemaleToMas(Entities.SelectedIdxE.IdxC.Idx, Entities.CurrentIdxE.IdxC.Idx);
                                    }

                                    else if (Entities.SelectedUniqueAbilityE.AbilityC.Is(AbilityTypes.FreezeDirectEnemy))
                                    {
                                        Entities.Rpc.FreezeDirectEnemyToMaster(Entities.SelectedIdxE.IdxC.Idx, Entities.CurrentIdxE.IdxC.Idx);
                                    }

                                    cellClick.Click = CellClickTypes.SimpleClick;
                                    Entities.SelectedIdxE.IdxC.Idx = Entities.CurrentIdxE.IdxC.Idx;
                                }
                                break;

                            default: throw new Exception();
                        }
                    }
                }

                else if (raycast.Is(RaycastTypes.UI))
                {
                    cellClick.Click = CellClickTypes.SimpleClick;
                }

                else if (raycast.Is(RaycastTypes.Background))
                {
                    cellClick.Click = CellClickTypes.SimpleClick;
                }
            }

            else
            {
                if (raycast.Is(RaycastTypes.Cell))
                {
                    if (cellClick.Is(CellClickTypes.SetUnit))
                    {
                        if (!unit_cur.Have || !CellUnitEs.VisibleE(Entities.WhoseMove.CurPlayerI, idx_cur).VisibleC.IsVisible)
                        {
                            if (Entities.CurrentIdxE.IsStartDirectToCell)
                            {
                                Entities.PreviousVisionIdxE.IdxC.Idx = Entities.CurrentIdxE.IdxC.Idx;
                            }
                            else
                            {
                                Entities.PreviousVisionIdxE.IdxC.Idx = Entities.CurrentIdxE.IdxC.Idx;
                            }
                        }
                    }
                }
            }
        }
    }
}