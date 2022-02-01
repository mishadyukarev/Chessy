using System;

namespace Game.Game
{
    sealed class SelectorS : SystemCellAbstract, IEcsRunSystem
    {
        public SelectorS(in Entities ents) : base(ents)
        {

        }

        public void Run()
        {
            var idx_cur = Es.CurrentIdxE.IdxC.Idx;
            var idx_sel = Es.SelectedIdxE.IdxC.Idx;

            var unit_cur = UnitEs(idx_cur).MainE.UnitTC;
            var ownUnit_cur = UnitEs(idx_cur).MainE.OwnerC;

            var unit_sel = UnitEs(idx_cur).MainE.UnitTC;

            ref var raycastTC = ref Es.ClickerObject.RayCastTC;
            ref var cellClick = ref Es.ClickerObject.CellClickC;

            if (Es.InputE.IsClickedC.IsClicked)
            {
                if (raycastTC.Is(RaycastTypes.Cell))
                {
                    if (!Es.WhoseMove.IsMyMove)
                    {
                        Es.SelectedIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;
                    }

                    else
                    {
                        switch (cellClick.Click)
                        {
                            case CellClickTypes.None: throw new Exception();

                            case CellClickTypes.SimpleClick:
                                {
                                    if (Es.SelectedIdxE.IsSelCell)
                                    {
                                        if (CellsForAttackUnitsEs.CanAttack(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx, Es.WhoseMove.CurPlayerI, out var attack))
                                        {
                                            Es.Rpc.AttackUnitToMaster(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                        }

                                        else if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(Es.WhoseMove.CurPlayerI, Es.SelectedIdxE.IdxC.Idx).Contains(Es.CurrentIdxE.IdxC.Idx))
                                        {
                                            Es.Rpc.ShiftUnitToMaster(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                        }

                                        else
                                        {
                                            if (UnitEs(idx_cur).MainE.HaveUnit(UnitStatEs(idx_cur)))
                                            {
                                                if (ownUnit_cur.Is(Es.WhoseMove.CurPlayerI))
                                                {
                                                    if (unit_cur.Is(UnitTypes.Scout))
                                                    {

                                                    }
                                                    else if (unit_cur.IsMelee)
                                                    {
                                                        Es.Sound(ClipTypes.PickMelee).Sound.Invoke();
                                                    }
                                                    else
                                                    {
                                                        Es.Sound(ClipTypes.PickArcher).Sound.Invoke();
                                                    }
                                                }
                                            }
                                        }

                                        Es.SelectedIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;
                                    }

                                    else
                                    {
                                        if (UnitEs(idx_cur).MainE.HaveUnit(UnitStatEs(idx_cur)))
                                        {
                                            if (ownUnit_cur.Is(Es.WhoseMove.CurPlayerI))
                                            {
                                                if (unit_cur.Is(UnitTypes.Scout))
                                                {

                                                }
                                                else if (unit_cur.IsMelee)
                                                {
                                                    Es.Sound(ClipTypes.PickMelee).Sound.Invoke();
                                                }
                                                else
                                                {
                                                    Es.Sound(ClipTypes.PickArcher).Sound.Invoke();
                                                }
                                            }
                                        }

                                        Es.SelectedIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.SetUnit:
                                {
                                    Es.Rpc.SetUniToMaster(Es.CurrentIdxE.IdxC.Idx, Es.SelectedUnitE.UnitTC.Unit);
                                    cellClick.Click = CellClickTypes.SimpleClick;
                                }
                                break;

                            case CellClickTypes.GiveTakeTW:
                                {
                                    if (unit_cur.Is(UnitTypes.Pawn) && ownUnit_cur.Is(Es.WhoseMove.CurPlayerI))
                                    {
                                        Es.Rpc.GiveTakeToolWeaponToMaster(Es.SelectedToolWeaponE.ToolWeaponTC.ToolWeapon, Es.SelectedToolWeaponE.LevelTC.Level, Es.CurrentIdxE.IdxC.Idx);
                                    }
                                    else
                                    {
                                        cellClick.Click = CellClickTypes.SimpleClick;
                                        Es.SelectedIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.UpgradeUnit:
                                {
                                    if (unit_cur.Is(new[] { UnitTypes.Pawn, UnitTypes.Archer })
                                        && ownUnit_cur.Is(Es.WhoseMove.CurPlayerI)
                                        && !UnitEs(idx_cur).MainE.LevelTC.Is(LevelTypes.Second))
                                    {
                                        Es.Rpc.UpgradeUnitToMaster(Es.CurrentIdxE.IdxC.Idx);
                                    }
                                    else
                                    {
                                        cellClick.Click = CellClickTypes.SimpleClick;
                                        Es.SelectedIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.GiveScout:
                                {
                                    if (unit_cur.Is(UnitTypes.Pawn)
                                        && ownUnit_cur.Is(Es.WhoseMove.CurPlayerI))
                                    {
                                        Es.Rpc.FromNewUnitToMas(UnitTypes.Scout, Es.CurrentIdxE.IdxC.Idx);
                                    }

                                    cellClick.Click = CellClickTypes.SimpleClick;
                                    Es.SelectedIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;
                                }
                                break;

                            case CellClickTypes.GiveHero:
                                {
                                    if (Es.InventorUnitsEs.HaveHero(Es.WhoseMove.CurPlayerI, out var hero))
                                    {
                                        if (hero == UnitTypes.Elfemale || hero == UnitTypes.Snowy)
                                        {
                                            if (Es.SelectedIdxE.IdxC.Idx == 0)
                                            {
                                                Es.SelectedIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;

                                                if (unit_cur.Is(UnitTypes.Archer))
                                                {
                                                    Es.Sound(ClipTypes.PickArcher).Sound.Invoke();
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
                                                            Es.Rpc.FromToNewUnitToMas(hero, Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                                            cellClick.Click = CellClickTypes.SimpleClick;
                                                        }

                                                        Es.Sound(ClipTypes.ClickToTable).Sound.Invoke();
                                                    }
                                                    else
                                                    {
                                                        cellClick.Click = CellClickTypes.SimpleClick;
                                                    }

                                                    Es.SelectedIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;
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
                                    if (Es.SelectedUniqueAbilityE.AbilityC.Is(AbilityTypes.FireArcher))
                                    {
                                        Es.Rpc.FireArcherToMas(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                    }

                                    else if (Es.SelectedUniqueAbilityE.AbilityC.Is(AbilityTypes.ChangeDirectionWind))
                                    {
                                        CellEsWorker.TryGetIdxAround(Es.CurrentIdxE.IdxC.Idx, out var directs);

                                        foreach (var item in directs)
                                        {
                                            if (item.Value == Es.CurrentIdxE.IdxC.Idx)
                                            {
                                                Es.Rpc.PutOutFireElffToMas(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                            }
                                        }
                                    }

                                    else if (Es.SelectedUniqueAbilityE.AbilityC.Is(AbilityTypes.StunElfemale))
                                    {
                                        Es.Rpc.StunElfemaleToMas(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                    }

                                    cellClick.Click = CellClickTypes.SimpleClick;
                                    Es.SelectedIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;
                                }
                                break;

                            default: throw new Exception();
                        }
                    }
                }

                else if (raycastTC.Is(RaycastTypes.UI))
                {
                    cellClick.Click = CellClickTypes.SimpleClick;
                }

                else if (raycastTC.Is(RaycastTypes.Background))
                {
                    cellClick.Click = CellClickTypes.SimpleClick;
                }
            }

            else
            {
                if (raycastTC.Is(RaycastTypes.Cell))
                {
                    if (cellClick.Is(CellClickTypes.SetUnit))
                    {
                        if (!UnitEs(idx_cur).MainE.HaveUnit(UnitStatEs(idx_cur)) || !UnitEs(idx_cur).VisibleE(Es.WhoseMove.CurPlayerI).IsVisibleC.IsVisible)
                        {
                            if (Es.CurrentIdxE.IsStartDirectToCell)
                            {
                                Es.PreviousVisionIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;
                            }
                            else
                            {
                                Es.PreviousVisionIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;
                            }
                        }
                    }
                }
            }
        }
    }
}