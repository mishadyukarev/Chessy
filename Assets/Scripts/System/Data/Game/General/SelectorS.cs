using Game.Common;
using System;
using UnityEngine;
using static Game.Game.CellUnitEs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct SelectorS : IEcsRunSystem
    {
        public void Run()
        {
            var idx_cur = CurrentIdxE.IdxC.Idx;
            var idx_sel = SelectedIdxE.IdxC.Idx;

            ref var unit_cur = ref Unit<UnitTC>(idx_cur);
            ref var levUnit_cur = ref Unit<LevelTC>(idx_cur);
            ref var ownUnit_cur = ref Unit<PlayerTC>(idx_cur);

            ref var unit_sel = ref Unit<UnitTC>(idx_cur);

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
                                            Rpc<RpcC>().AttackUnitToMaster(SelectedIdxE.IdxC.Idx, CurrentIdxE.IdxC.Idx);
                                        }

                                        else if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(WhoseMoveE.CurPlayerI, SelectedIdxE.IdxC.Idx).Contains(CurrentIdxE.IdxC.Idx))
                                        {
                                            Rpc<RpcC>().ShiftUnitToMaster(SelectedIdxE.IdxC.Idx, CurrentIdxE.IdxC.Idx);
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
                                                        EntitySound.Sound(ClipTypes.PickMelee).Invoke();
                                                    }
                                                    else
                                                    {
                                                        EntitySound.Sound(ClipTypes.PickArcher).Invoke();
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
                                                    EntitySound.Sound(ClipTypes.PickMelee).Invoke();
                                                }
                                                else
                                                {
                                                    EntitySound.Sound(ClipTypes.PickArcher).Invoke();
                                                }
                                            }
                                        }

                                        SelectedIdxE.IdxC.Idx = CurrentIdxE.IdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.SetUnit:
                                {
                                    EntityPool.Rpc<RpcC>().SetUniToMaster(CurrentIdxE.IdxC.Idx, SelectedUnitE.SelUnit<UnitTC>().Unit);
                                    cellClick.Click = CellClickTypes.SimpleClick;
                                }
                                break;

                            case CellClickTypes.GiveTakeTW:
                                {
                                    if (unit_cur.Is(UnitTypes.Pawn) && ownUnit_cur.Is(WhoseMoveE.CurPlayerI))
                                    {
                                        Rpc<RpcC>().GiveTakeToolWeapon(SelectedToolWeaponE.SelectedTW<ToolWeaponC>().ToolWeapon, SelectedToolWeaponE.SelectedTW<LevelTC>().Level, CurrentIdxE.IdxC.Idx);
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
                                        EntityPool.Rpc<RpcC>().UpgradeUnitToMaster(CurrentIdxE.IdxC.Idx);
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
                                        EntityPool.Rpc<RpcC>().FromNewUnitToMas(UnitTypes.Scout, CurrentIdxE.IdxC.Idx);
                                    }

                                    cellClick.Click = CellClickTypes.SimpleClick;
                                    SelectedIdxE.IdxC.Idx = CurrentIdxE.IdxC.Idx;
                                }
                                break;

                            case CellClickTypes.GiveHero:
                                {
                                    if (SelectedIdxE.IdxC.Idx == 0)
                                    {
                                        SelectedIdxE.IdxC.Idx = CurrentIdxE.IdxC.Idx;

                                        if (unit_cur.Is(UnitTypes.Archer))
                                        {
                                            EntitySound.Sound(ClipTypes.PickArcher).Invoke();
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
                                                    EntityPool.Rpc<RpcC>().FromToNewUnitToMas(UnitTypes.Elfemale, SelectedIdxE.IdxC.Idx, CurrentIdxE.IdxC.Idx);
                                                    cellClick.Click = CellClickTypes.SimpleClick;
                                                }

                                                EntitySound.Sound(ClipTypes.ClickToTable).Invoke();
                                            }
                                            else
                                            {
                                                cellClick.Click = CellClickTypes.SimpleClick;
                                            }

                                            SelectedIdxE.IdxC.Idx = CurrentIdxE.IdxC.Idx;
                                        }
                                    }
                                }
                                break;

                            case CellClickTypes.UniqueAbility:
                                {
                                    if (SelUniqAbilC.Is(UniqueAbilityTypes.FireArcher))
                                    {
                                        EntityPool.Rpc<RpcC>().FireArcherToMas(SelectedIdxE.IdxC.Idx, CurrentIdxE.IdxC.Idx);
                                    }

                                    else if (SelUniqAbilC.Is(UniqueAbilityTypes.ChangeDirectionWind))
                                    {
                                        if (DirectsWindForElfemaleE.IdxsDirects.Contains(CurrentIdxE.IdxC.Idx))
                                        {
                                            EntityPool.Rpc<RpcC>().PutOutFireElffToMas(SelectedIdxE.IdxC.Idx, CurrentIdxE.IdxC.Idx);
                                        }    
                                    }

                                    else if (SelUniqAbilC.Is(UniqueAbilityTypes.StunElfemale))
                                    {
                                        EntityPool.Rpc<RpcC>().StunElfemaleToMas(SelectedIdxE.IdxC.Idx, CurrentIdxE.IdxC.Idx);
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
                        if (!unit_cur.Have || !CellUnitVisibleEs.Visible<IsVisibleC>(WhoseMoveE.CurPlayerI, idx_cur).IsVisible)
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