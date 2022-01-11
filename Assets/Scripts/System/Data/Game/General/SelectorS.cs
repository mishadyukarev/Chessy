using System;
using static Game.Game.EntityCellUnitPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct SelectorS : IEcsRunSystem
    {
        public void Run()
        {
            ref var unit_cur = ref Unit<UnitC>(CurIdx<IdxC>().Idx);
            ref var levUnit_cur = ref Unit<LevelC>(CurIdx<IdxC>().Idx);
            ref var ownUnit_cur = ref Unit<OwnerC>(CurIdx<IdxC>().Idx);

            ref var unit_sel = ref Unit<UnitC>(SelIdx<IdxC>().Idx);

            ref var raycast = ref ClickerObject<RayCastC>();
            ref var cellClick = ref ClickerObject<CellClickC>();


            if (Input<ClickC>().IsClicked)
            {
                if (raycast.Is(RaycastTypes.Cell))
                {
                    if (!WhoseMoveC.IsMyMove)
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
                                    if (SelIdx<SelIdxC>().IsSelCell)
                                    {
                                        if (Unit<UnitCellEC>(SelIdx<IdxC>().Idx).CanAttack(WhoseMoveC.CurPlayerI, CurIdx<IdxC>().Idx, out var attack))
                                        {
                                            EntityPool.Rpc<RpcC>().AttackUnitToMaster(SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx);
                                        }

                                        else if (Unit<UnitCellEC>(SelIdx<IdxC>().Idx).CanShift(WhoseMoveC.CurPlayerI, CurIdx<IdxC>().Idx))
                                        {
                                            EntityPool.Rpc<RpcC>().ShiftUnitToMaster(SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx);
                                        }

                                        else
                                        {
                                            if (unit_cur.Have)
                                            {
                                                if (ownUnit_cur.Is(WhoseMoveC.CurPlayerI))
                                                {
                                                    if (unit_cur.Is(UnitTypes.Scout))
                                                    {

                                                    }
                                                    else if (unit_cur.IsMelee)
                                                    {
                                                        NeedSoundEffC.Clip = ClipTypes.PickMelee;
                                                    }
                                                    else
                                                    {
                                                        NeedSoundEffC.Clip = ClipTypes.PickArcher;
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
                                            if (ownUnit_cur.Is(WhoseMoveC.CurPlayerI))
                                            {
                                                if (unit_cur.Is(UnitTypes.Scout))
                                                {

                                                }
                                                else if (unit_cur.IsMelee)
                                                {
                                                    NeedSoundEffC.Clip = ClipTypes.PickMelee;
                                                }
                                                else
                                                {
                                                    NeedSoundEffC.Clip = ClipTypes.PickArcher;
                                                }
                                            }
                                        }

                                        SelIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.SetUnit:
                                {
                                    EntityPool.Rpc<RpcC>().SetUniToMaster(CurIdx<IdxC>().Idx, SelUnitC.Unit);
                                    cellClick.Set(CellClickTypes.SimpleClick);
                                }
                                break;

                            case CellClickTypes.GiveTakeTW:
                                {
                                    if (unit_cur.Is(UnitTypes.Pawn) && ownUnit_cur.Is(WhoseMoveC.CurPlayerI))
                                    {
                                        EntityPool.Rpc<RpcC>().GiveTakeToolWeapon(TwGiveTakeC.TWTypeForGive, TwGiveTakeC.Level(TwGiveTakeC.TWTypeForGive), CurIdx<IdxC>().Idx);
                                    }
                                    else
                                    {
                                        cellClick.Set(CellClickTypes.SimpleClick);
                                        SelIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.UpgradeUnit:
                                {
                                    if (unit_cur.Is(new[] { UnitTypes.Pawn, UnitTypes.Archer })
                                        && ownUnit_cur.Is(WhoseMoveC.CurPlayerI)
                                        && !levUnit_cur.Is(LevelTypes.Second))
                                    {
                                        EntityPool.Rpc<RpcC>().UpgradeUnitToMaster(CurIdx<IdxC>().Idx);
                                    }
                                    else
                                    {
                                        cellClick.Set(CellClickTypes.SimpleClick);
                                        SelIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.GiveScout:
                                {
                                    if (unit_cur.Is(UnitTypes.Pawn)
                                        && ownUnit_cur.Is(WhoseMoveC.CurPlayerI))
                                    {
                                        EntityPool.Rpc<RpcC>().FromNewUnitToMas(UnitTypes.Scout, CurIdx<IdxC>().Idx);
                                    }

                                    cellClick.Set(CellClickTypes.SimpleClick);
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
                                            cellClick.Set(CellClickTypes.SimpleClick);

                                            NeedSoundEffC.Clip = ClipTypes.PickArcher;
                                        }

                                        NeedSoundEffC.Clip = ClipTypes.ClickToTable;
                                    }
                                    else
                                    {
                                        cellClick.Set(CellClickTypes.SimpleClick);
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

                                    cellClick.Set(CellClickTypes.SimpleClick);
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

                }

                else if (raycast.Is(RaycastTypes.Background))
                {
                    //CellClicker<CellClickC>().Set(CellClickTypes.FirstClick);
                    SelIdx<IdxC>().Reset();
                }
            }

            else
            {
                if (raycast.Is(RaycastTypes.Cell))
                {
                    if (ClickerObject<CellClickC>().Is(CellClickTypes.SetUnit))
                    {
                        if (!unit_cur.Have || !Unit<VisibledC>(WhoseMoveC.CurPlayerI, CurIdx<IdxC>().Idx).IsVisibled)
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

                NeedSoundEffC.Clip = default;
            }
        }
    }
}