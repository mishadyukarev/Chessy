using System;
using static Game.Game.EntityPool;

namespace Game.Game
{
    readonly struct SelectorS : IEcsRunSystem
    {
        public void Run()
        {
            var idx_cur = EntitiesPool.CurrentIdxE.IdxC.Idx;
            var idx_sel = EntitiesPool.SelectedIdxE.IdxC.Idx;

            ref var unit_cur = ref CellUnitEntities.Else(idx_cur).UnitC;
            ref var levUnit_cur = ref CellUnitEntities.Else(idx_cur).LevelC;
            ref var ownUnit_cur = ref CellUnitEntities.Else(idx_cur).OwnerC;

            ref var unit_sel = ref CellUnitEntities.Else(idx_cur).UnitC;

            ref var raycast = ref ClickerObject<RayCastC>();
            ref var cellClick = ref ClickerObject<CellClickC>();

            if (Input<IsClickedC>().IsClicked)
            {
                if (raycast.Is(RaycastTypes.Cell))
                {
                    if (!WhoseMoveE.IsMyMove)
                    {
                        EntitiesPool.SelectedIdxE.IdxC.Idx = EntitiesPool.CurrentIdxE.IdxC.Idx;
                    }

                    else
                    {
                        switch (cellClick.Click)
                        {
                            case CellClickTypes.None: throw new Exception();

                            case CellClickTypes.SimpleClick:
                                {
                                    if (EntitiesPool.SelectedIdxE.IsSelCell)
                                    {
                                        if (CellsForAttackUnitsEs.CanAttack(EntitiesPool.SelectedIdxE.IdxC.Idx, EntitiesPool.CurrentIdxE.IdxC.Idx, WhoseMoveE.CurPlayerI, out var attack))
                                        {
                                            Rpc.AttackUnitToMaster(EntitiesPool.SelectedIdxE.IdxC.Idx, EntitiesPool.CurrentIdxE.IdxC.Idx);
                                        }

                                        else if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(WhoseMoveE.CurPlayerI, EntitiesPool.SelectedIdxE.IdxC.Idx).Contains(EntitiesPool.CurrentIdxE.IdxC.Idx))
                                        {
                                            Rpc.ShiftUnitToMaster(EntitiesPool.SelectedIdxE.IdxC.Idx, EntitiesPool.CurrentIdxE.IdxC.Idx);
                                        }

                                        else
                                        {
                                            if (unit_cur.Have)
                                            {
                                                if (ownUnit_cur.Is(WhoseMoveE.CurPlayerI))
                                                {
                                                    if (unit_cur.Is(UnitTypes.Scout))
                                                    {

                                                    }
                                                    else if (unit_cur.IsMelee)
                                                    {
                                                        SoundE.Sound(ClipTypes.PickMelee).Invoke();
                                                    }
                                                    else
                                                    {
                                                        SoundE.Sound(ClipTypes.PickArcher).Invoke();
                                                    }
                                                }
                                            }
                                        }

                                        EntitiesPool.SelectedIdxE.IdxC.Idx = EntitiesPool.CurrentIdxE.IdxC.Idx;
                                    }

                                    else
                                    {
                                        if (unit_cur.Have)
                                        {
                                            if (ownUnit_cur.Is(WhoseMoveE.CurPlayerI))
                                            {
                                                if (unit_cur.Is(UnitTypes.Scout))
                                                {

                                                }
                                                else if (unit_cur.IsMelee)
                                                {
                                                    SoundE.Sound(ClipTypes.PickMelee).Invoke();
                                                }
                                                else
                                                {
                                                    SoundE.Sound(ClipTypes.PickArcher).Invoke();
                                                }
                                            }
                                        }

                                        EntitiesPool.SelectedIdxE.IdxC.Idx = EntitiesPool.CurrentIdxE.IdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.SetUnit:
                                {
                                    EntityPool.Rpc.SetUniToMaster(EntitiesPool.CurrentIdxE.IdxC.Idx, SelectedUnitE.SelUnit<UnitTC>().Unit);
                                    cellClick.Click = CellClickTypes.SimpleClick;
                                }
                                break;

                            case CellClickTypes.GiveTakeTW:
                                {
                                    if (unit_cur.Is(UnitTypes.Pawn) && ownUnit_cur.Is(WhoseMoveE.CurPlayerI))
                                    {
                                        Rpc.GiveTakeToolWeaponToMaster(SelectedToolWeaponE.SelectedTW<ToolWeaponC>().ToolWeapon, SelectedToolWeaponE.SelectedTW<LevelTC>().Level, EntitiesPool.CurrentIdxE.IdxC.Idx);
                                    }
                                    else
                                    {
                                        cellClick.Click = CellClickTypes.SimpleClick;
                                        EntitiesPool.SelectedIdxE.IdxC.Idx = EntitiesPool.CurrentIdxE.IdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.UpgradeUnit:
                                {
                                    if (unit_cur.Is(new[] { UnitTypes.Pawn, UnitTypes.Archer })
                                        && ownUnit_cur.Is(WhoseMoveE.CurPlayerI)
                                        && !levUnit_cur.Is(LevelTypes.Second))
                                    {
                                        EntityPool.Rpc.UpgradeUnitToMaster(EntitiesPool.CurrentIdxE.IdxC.Idx);
                                    }
                                    else
                                    {
                                        cellClick.Click = CellClickTypes.SimpleClick;
                                        EntitiesPool.SelectedIdxE.IdxC.Idx = EntitiesPool.CurrentIdxE.IdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.GiveScout:
                                {
                                    if (unit_cur.Is(UnitTypes.Pawn)
                                        && ownUnit_cur.Is(WhoseMoveE.CurPlayerI))
                                    {
                                        EntityPool.Rpc.FromNewUnitToMas(UnitTypes.Scout, EntitiesPool.CurrentIdxE.IdxC.Idx);
                                    }

                                    cellClick.Click = CellClickTypes.SimpleClick;
                                    EntitiesPool.SelectedIdxE.IdxC.Idx = EntitiesPool.CurrentIdxE.IdxC.Idx;
                                }
                                break;

                            case CellClickTypes.GiveHero:
                                {
                                    if (InventorUnitsE.HaveHero(WhoseMoveE.CurPlayerI, out var hero))
                                    {
                                        if (hero == UnitTypes.Elfemale || hero == UnitTypes.Snowy)
                                        {
                                            if (EntitiesPool.SelectedIdxE.IdxC.Idx == 0)
                                            {
                                                EntitiesPool.SelectedIdxE.IdxC.Idx = EntitiesPool.CurrentIdxE.IdxC.Idx;

                                                if (unit_cur.Is(UnitTypes.Archer))
                                                {
                                                    SoundE.Sound(ClipTypes.PickArcher).Invoke();
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
                                                            EntityPool.Rpc.FromToNewUnitToMas(hero, EntitiesPool.SelectedIdxE.IdxC.Idx, EntitiesPool.CurrentIdxE.IdxC.Idx);
                                                            cellClick.Click = CellClickTypes.SimpleClick;
                                                        }

                                                        SoundE.Sound(ClipTypes.ClickToTable).Invoke();
                                                    }
                                                    else
                                                    {
                                                        cellClick.Click = CellClickTypes.SimpleClick;
                                                    }

                                                    EntitiesPool.SelectedIdxE.IdxC.Idx = EntitiesPool.CurrentIdxE.IdxC.Idx;
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
                                    if (SelectedUniqueAbilityC.AbilityC.Is(UniqueAbilityTypes.FireArcher))
                                    {
                                        EntityPool.Rpc.FireArcherToMas(EntitiesPool.SelectedIdxE.IdxC.Idx, EntitiesPool.CurrentIdxE.IdxC.Idx);
                                    }

                                    else if (SelectedUniqueAbilityC.AbilityC.Is(UniqueAbilityTypes.ChangeDirectionWind))
                                    {
                                        if (DirectsWindForElfemaleE.IdxsDirects.Contains(EntitiesPool.CurrentIdxE.IdxC.Idx))
                                        {
                                            EntityPool.Rpc.PutOutFireElffToMas(EntitiesPool.SelectedIdxE.IdxC.Idx, EntitiesPool.CurrentIdxE.IdxC.Idx);
                                        }
                                    }

                                    else if (SelectedUniqueAbilityC.AbilityC.Is(UniqueAbilityTypes.StunElfemale))
                                    {
                                        EntityPool.Rpc.StunElfemaleToMas(EntitiesPool.SelectedIdxE.IdxC.Idx, EntitiesPool.CurrentIdxE.IdxC.Idx);
                                    }

                                    else if (SelectedUniqueAbilityC.AbilityC.Is(UniqueAbilityTypes.FreezeDirectEnemy))
                                    {
                                        EntityPool.Rpc.FreezeDirectEnemyToMaster(EntitiesPool.SelectedIdxE.IdxC.Idx, EntitiesPool.CurrentIdxE.IdxC.Idx);
                                    }

                                    cellClick.Click = CellClickTypes.SimpleClick;
                                    EntitiesPool.SelectedIdxE.IdxC.Idx = EntitiesPool.CurrentIdxE.IdxC.Idx;
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
                        if (!unit_cur.Have || !CellUnitVisibleEs.Visible(WhoseMoveE.CurPlayerI, idx_cur).IsVisible)
                        {
                            if (EntitiesPool.CurrentIdxE.IsStartDirectToCell)
                            {
                                PreVisIdx<IdxC>().Idx = EntitiesPool.CurrentIdxE.IdxC.Idx;
                            }
                            else
                            {
                                PreVisIdx<IdxC>().Idx = EntitiesPool.CurrentIdxE.IdxC.Idx;
                            }
                        }
                    }
                }
            }
        }
    }
}