using Game.Common;
using System;
using UnityEngine;
using static Game.Game.CellUnitEs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    readonly struct SelectorS : IEcsRunSystem
    {
        public void Run()
        {
            var idx_cur = CurrentIdxE.IdxC.Idx;
            var idx_sel = SelectedIdxE.IdxC.Idx;

            ref var unit_cur = ref Unit(idx_cur);
            ref var levUnit_cur = ref EntitiesPool.UnitElse.Level(idx_cur);
            ref var ownUnit_cur = ref EntitiesPool.UnitElse.Owner(idx_cur);

            ref var unit_sel = ref Unit(idx_cur);

            ref var raycast = ref ClickerObject<RayCastC>();
            ref var cellClick = ref ClickerObject<CellClickC>();

            if (Input<IsClickedC>().IsClicked)
            {
                if (raycast.Is(RaycastTypes.Cell))
                {
                    if (!WhoseMoveE.IsMyMove)
                    {
                        SelectedIdxE.IdxC.Idx = CurrentIdxE.IdxC.Idx;
                    }

                    else
                    {
                        switch (cellClick.Click)
                        {
                            case CellClickTypes.None: throw new Exception();

                            case CellClickTypes.SimpleClick:
                                {
                                    if (SelectedIdxE.IsSelCell)
                                    {
                                        if (CellsForAttackUnitsEs.CanAttack(SelectedIdxE.IdxC.Idx, CurrentIdxE.IdxC.Idx, WhoseMoveE.CurPlayerI, out var attack))
                                        {
                                            Rpc.AttackUnitToMaster(SelectedIdxE.IdxC.Idx, CurrentIdxE.IdxC.Idx);
                                        }

                                        else if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(WhoseMoveE.CurPlayerI, SelectedIdxE.IdxC.Idx).Contains(CurrentIdxE.IdxC.Idx))
                                        {
                                            Rpc.ShiftUnitToMaster(SelectedIdxE.IdxC.Idx, CurrentIdxE.IdxC.Idx);
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

                                        SelectedIdxE.IdxC.Idx = CurrentIdxE.IdxC.Idx;
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

                                        SelectedIdxE.IdxC.Idx = CurrentIdxE.IdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.SetUnit:
                                {
                                    EntityPool.Rpc.SetUniToMaster(CurrentIdxE.IdxC.Idx, SelectedUnitE.SelUnit<UnitTC>().Unit);
                                    cellClick.Click = CellClickTypes.SimpleClick;
                                }
                                break;

                            case CellClickTypes.GiveTakeTW:
                                {
                                    if (unit_cur.Is(UnitTypes.Pawn) && ownUnit_cur.Is(WhoseMoveE.CurPlayerI))
                                    {
                                        Rpc.GiveTakeToolWeaponToMaster(SelectedToolWeaponE.SelectedTW<ToolWeaponC>().ToolWeapon, SelectedToolWeaponE.SelectedTW<LevelTC>().Level, CurrentIdxE.IdxC.Idx);
                                    }
                                    else
                                    {
                                        cellClick.Click = CellClickTypes.SimpleClick;
                                        SelectedIdxE.IdxC.Idx = CurrentIdxE.IdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.UpgradeUnit:
                                {
                                    if (unit_cur.Is(new[] { UnitTypes.Pawn, UnitTypes.Archer })
                                        && ownUnit_cur.Is(WhoseMoveE.CurPlayerI)
                                        && !levUnit_cur.Is(LevelTypes.Second))
                                    {
                                        EntityPool.Rpc.UpgradeUnitToMaster(CurrentIdxE.IdxC.Idx);
                                    }
                                    else
                                    {
                                        cellClick.Click = CellClickTypes.SimpleClick;
                                        SelectedIdxE.IdxC.Idx = CurrentIdxE.IdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.GiveScout:
                                {
                                    if (unit_cur.Is(UnitTypes.Pawn)
                                        && ownUnit_cur.Is(WhoseMoveE.CurPlayerI))
                                    {
                                        EntityPool.Rpc.FromNewUnitToMas(UnitTypes.Scout, CurrentIdxE.IdxC.Idx);
                                    }

                                    cellClick.Click = CellClickTypes.SimpleClick;
                                    SelectedIdxE.IdxC.Idx = CurrentIdxE.IdxC.Idx;
                                }
                                break;

                            case CellClickTypes.GiveHero:
                                {
                                    if(InventorUnitsE.HaveHero(WhoseMoveE.CurPlayerI, out var hero))
                                    {
                                        if(hero == UnitTypes.Elfemale || hero == UnitTypes.Snowy)
                                        {
                                            if (SelectedIdxE.IdxC.Idx == 0)
                                            {
                                                SelectedIdxE.IdxC.Idx = CurrentIdxE.IdxC.Idx;

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
                                                            EntityPool.Rpc.FromToNewUnitToMas(hero, SelectedIdxE.IdxC.Idx, CurrentIdxE.IdxC.Idx);
                                                            cellClick.Click = CellClickTypes.SimpleClick;
                                                        }

                                                        SoundE.Sound(ClipTypes.ClickToTable).Invoke();
                                                    }
                                                    else
                                                    {
                                                        cellClick.Click = CellClickTypes.SimpleClick;
                                                    }

                                                    SelectedIdxE.IdxC.Idx = CurrentIdxE.IdxC.Idx;
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
                                        EntityPool.Rpc.FireArcherToMas(SelectedIdxE.IdxC.Idx, CurrentIdxE.IdxC.Idx);
                                    }

                                    else if (SelectedUniqueAbilityC.AbilityC.Is(UniqueAbilityTypes.ChangeDirectionWind))
                                    {
                                        if (DirectsWindForElfemaleE.IdxsDirects.Contains(CurrentIdxE.IdxC.Idx))
                                        {
                                            EntityPool.Rpc.PutOutFireElffToMas(SelectedIdxE.IdxC.Idx, CurrentIdxE.IdxC.Idx);
                                        }    
                                    }

                                    else if (SelectedUniqueAbilityC.AbilityC.Is(UniqueAbilityTypes.StunElfemale))
                                    {
                                        EntityPool.Rpc.StunElfemaleToMas(SelectedIdxE.IdxC.Idx, CurrentIdxE.IdxC.Idx);
                                    }

                                    else if (SelectedUniqueAbilityC.AbilityC.Is(UniqueAbilityTypes.FreezeDirectEnemy))
                                    {
                                        EntityPool.Rpc.FreezeDirectEnemyToMaster(SelectedIdxE.IdxC.Idx, CurrentIdxE.IdxC.Idx);
                                    }

                                    cellClick.Click = CellClickTypes.SimpleClick;
                                    SelectedIdxE.IdxC.Idx = CurrentIdxE.IdxC.Idx;
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
                            if (CurrentIdxE.IsStartDirectToCell)
                            {
                                PreVisIdx<IdxC>().Idx = CurrentIdxE.IdxC.Idx;
                            }
                            else
                            {
                                PreVisIdx<IdxC>().Idx = CurrentIdxE.IdxC.Idx;
                            }
                        }
                    }
                }
            }
        }
    }
}