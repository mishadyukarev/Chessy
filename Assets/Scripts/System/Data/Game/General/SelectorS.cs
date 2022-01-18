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
            var idx_cur = CurIdx<IdxC>().Idx;
            var idx_sel = SelIdx<IdxC>().Idx;

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
                        SelIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
                    }

                    else
                    {
                        switch (cellClick.Click)
                        {
                            case CellClickTypes.None: throw new Exception();

                            case CellClickTypes.SimpleClick:
                                {
                                    if (SelIdx<SelIdxEC>().IsSelCell)
                                    {
                                        if (CellsForAttackUnitsEs.CanAttack(SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx, WhoseMoveE.CurPlayerI, out var attack))
                                        {
                                            Rpc<RpcC>().AttackUnitToMaster(SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx);
                                        }

                                        else if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(WhoseMoveE.CurPlayerI, SelIdx<IdxC>().Idx).Contains(CurIdx<IdxC>().Idx))
                                        {
                                            Rpc<RpcC>().ShiftUnitToMaster(SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx);
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
                                                        EntitySound.Sound<ActionC>(ClipTypes.PickMelee).Invoke();
                                                    }
                                                    else
                                                    {
                                                        EntitySound.Sound<ActionC>(ClipTypes.PickArcher).Invoke();
                                                    }
                                                }
                                            }
                                        }

                                        SelIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
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
                                                    EntitySound.Sound<ActionC>(ClipTypes.PickMelee).Invoke();
                                                }
                                                else
                                                {
                                                    EntitySound.Sound<ActionC>(ClipTypes.PickArcher).Invoke();
                                                }
                                            }
                                        }

                                        SelIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.SetUnit:
                                {
                                    EntityPool.Rpc<RpcC>().SetUniToMaster(CurIdx<IdxC>().Idx, SelectedUnitE.SelUnit<UnitTC>().Unit);
                                    cellClick.Click = CellClickTypes.SimpleClick;
                                }
                                break;

                            case CellClickTypes.GiveTakeTW:
                                {
                                    if (unit_cur.Is(UnitTypes.Pawn) && ownUnit_cur.Is(WhoseMoveE.CurPlayerI))
                                    {
                                        Rpc<RpcC>().GiveTakeToolWeapon(SelectedToolWeaponE.SelectedTW<ToolWeaponC>().ToolWeapon, SelectedToolWeaponE.SelectedTW<LevelTC>().Level, CurIdx<IdxC>().Idx);
                                    }
                                    else
                                    {
                                        cellClick.Click = CellClickTypes.SimpleClick;
                                        SelIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.UpgradeUnit:
                                {
                                    if (unit_cur.Is(new[] { UnitTypes.Pawn, UnitTypes.Archer })
                                        && ownUnit_cur.Is(WhoseMoveE.CurPlayerI)
                                        && !levUnit_cur.Is(LevelTypes.Second))
                                    {
                                        EntityPool.Rpc<RpcC>().UpgradeUnitToMaster(CurIdx<IdxC>().Idx);
                                    }
                                    else
                                    {
                                        cellClick.Click = CellClickTypes.SimpleClick;
                                        SelIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.GiveScout:
                                {
                                    if (unit_cur.Is(UnitTypes.Pawn)
                                        && ownUnit_cur.Is(WhoseMoveE.CurPlayerI))
                                    {
                                        EntityPool.Rpc<RpcC>().FromNewUnitToMas(UnitTypes.Scout, CurIdx<IdxC>().Idx);
                                    }

                                    cellClick.Click = CellClickTypes.SimpleClick;
                                    SelIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
                                }
                                break;

                            case CellClickTypes.GiveHero:
                                {
                                    if (SelIdx<IdxC>().Idx == 0)
                                    {
                                        SelIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;

                                        if (unit_cur.Is(UnitTypes.Archer))
                                        {
                                            EntitySound.Sound<ActionC>(ClipTypes.PickArcher).Invoke();
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
                                                    EntityPool.Rpc<RpcC>().FromToNewUnitToMas(UnitTypes.Elfemale, SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx);
                                                    cellClick.Click = CellClickTypes.SimpleClick;
                                                }

                                                EntitySound.Sound<ActionC>(ClipTypes.ClickToTable).Invoke();
                                            }
                                            else
                                            {
                                                cellClick.Click = CellClickTypes.SimpleClick;
                                            }

                                            SelIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
                                        }
                                    }
                                }
                                break;

                            case CellClickTypes.UniqueAbility:
                                {
                                    if (SelUniqAbilC.Is(UniqueAbilityTypes.FireArcher))
                                    {
                                        EntityPool.Rpc<RpcC>().FireArcherToMas(SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx);
                                    }

                                    else if (SelUniqAbilC.Is(UniqueAbilityTypes.ChangeDirectionWind))
                                    {
                                        if (DirectsWindForElfemaleE.IdxsDirects.Contains(CurIdx<IdxC>().Idx))
                                        {
                                            EntityPool.Rpc<RpcC>().PutOutFireElffToMas(SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx);
                                        }    
                                    }

                                    else if (SelUniqAbilC.Is(UniqueAbilityTypes.StunElfemale))
                                    {
                                        EntityPool.Rpc<RpcC>().StunElfemaleToMas(SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx);
                                    }

                                    cellClick.Click = CellClickTypes.SimpleClick;
                                    SelIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
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
                        if (!unit_cur.Have || !Unit<IsVisibleC>(WhoseMoveE.CurPlayerI, idx_cur).IsVisible)
                        {
                            if (CurIdx<CurIdxC>().IsStartDirectToCell)
                            {
                                PreVisIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
                            }
                            else
                            {
                                PreVisIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
                            }
                        }
                    }
                }
            }
        }
    }
}