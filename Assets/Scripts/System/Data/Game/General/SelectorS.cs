using Game.Common;
using System;
using static Game.Game.CellUnitE;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct SelectorS : IEcsRunSystem
    {
        public void Run()
        {
            var idx_cur = CurIdx<IdxC>().Idx;

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
                                        if (Unit<UnitCellEC>(SelIdx<IdxC>().Idx).CanAttack(WhoseMoveE.CurPlayerI, CurIdx<IdxC>().Idx, out var attack))
                                        {
                                            Rpc<RpcC>().AttackUnitToMaster(SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx);
                                        }

                                        else if (Unit<UnitCellEC>(SelIdx<IdxC>().Idx).CanShift(WhoseMoveE.CurPlayerI, CurIdx<IdxC>().Idx))
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
                                                        //NeedSoundEffC.Clip = ClipTypes.PickMelee;
                                                    }
                                                    else
                                                    {
                                                        //NeedSoundEffC.Clip = ClipTypes.PickArcher;
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
                                                    //NeedSoundEffC.Clip = ClipTypes.PickMelee;
                                                }
                                                else
                                                {
                                                    //NeedSoundEffC.Clip = ClipTypes.PickArcher;
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
                                        //EntityPool.Rpc<RpcC>().GiveTakeToolWeapon(TwGiveTakeC.TWTypeForGive, TwGiveTakeC.Level(TwGiveTakeC.TWTypeForGive), CurIdx<IdxC>().Idx);
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
                                    if (unit_cur.Is(UnitTypes.Archer))
                                    {
                                        if (unit_sel.Is(UnitTypes.Archer))
                                        {
                                            EntityPool.Rpc<RpcC>().FromToNewUnitToMas(UnitTypes.Elfemale, SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx);
                                            cellClick.Click = CellClickTypes.SimpleClick;

                                            //NeedSoundEffC.Clip = ClipTypes.PickArcher;
                                        }

                                        //NeedSoundEffC.Clip = ClipTypes.ClickToTable;
                                    }
                                    else
                                    {
                                        cellClick.Click = CellClickTypes.SimpleClick;
                                    }

                                    SelIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
                                }
                                break;

                            case CellClickTypes.UniqAbil:
                                {
                                    if (SelUniqAbilC.Is(UniqueAbilityTypes.FireArcher))
                                    {
                                        EntityPool.Rpc<RpcC>().FireArcherToMas(SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx);
                                    }

                                    else if (SelUniqAbilC.Is(UniqueAbilityTypes.ChangeDirWind))
                                    {
                                        EntityPool.Rpc<RpcC>().PutOutFireElffToMas(SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx);
                                    }

                                    else if (SelUniqAbilC.Is(UniqueAbilityTypes.StunElfemale))
                                    {
                                        EntityPool.Rpc<RpcC>().StunElfemaleToMas(SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx);
                                    }

                                    cellClick.Click = CellClickTypes.SimpleClick;
                                    //cellClick.Set(CellClickTypes.FirstClick);
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
                        if (!unit_cur.Have || !Unit<IsVisibledC>(WhoseMoveE.CurPlayerI, idx_cur).IsVisibled)
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

                //NeedSoundEffC.Clip = default;
            }
        }
    }
}