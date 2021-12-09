using Leopotam.Ecs;
using static Game.Game.CellClickC;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class SelectorS : IEcsRunSystem
    {


        private EcsFilter<UnitC, LevelC, OwnerC, VisibleC> _unitF = default;

        public void Run()
        {
            ref var unit_cur = ref _unitF.Get1(CurIdx<IdxC>().Idx);
            ref var levUnit_cur = ref _unitF.Get2(CurIdx<IdxC>().Idx);
            ref var ownUnit_cur = ref _unitF.Get3(CurIdx<IdxC>().Idx);
            ref var visUnit_cur = ref _unitF.Get4(CurIdx<IdxC>().Idx);

            ref var unit_sel = ref _unitF.Get1(SelIdx<IdxC>().Idx);


            if (InputC.IsClicked)
            {
                if (RayCastC.Is(RaycastTypes.Cell))
                {
                    if (!WhoseMoveC.IsMyMove)
                    {
                        SelIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
                    }

                    else
                    {
                        if (Is(CellClickTypes.SetUnit))
                        {
                            RpcSys.SetUniToMaster(CurIdx<IdxC>().Idx, SelUnitC.Unit);
                            Set(CellClickTypes.Firstlick);
                        }

                        else if (Is(CellClickTypes.UniqAbil))
                        {
                            if (SelUniqAbilC.Is(UniqueAbilTypes.FireArcher))
                            {
                                RpcSys.FireArcherToMas(SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx);
                            }

                            else if (SelUniqAbilC.Is(UniqueAbilTypes.ChangeDirWind))
                            {
                                RpcSys.PutOutFireElffToMas(SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx);
                            }

                            else if (SelUniqAbilC.Is(UniqueAbilTypes.StunElfemale))
                            {
                                RpcSys.StunElfemaleToMas(SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx);
                            }

                            Set(CellClickTypes.Firstlick);
                            SelIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
                        }

                        else if (Is(CellClickTypes.GiveTakeTW))
                        {
                            if (unit_cur.Is(UnitTypes.Pawn) && ownUnit_cur.Is(WhoseMoveC.CurPlayerI))
                            {
                                RpcSys.GiveTakeToolWeapon(TwGiveTakeC.TWTypeForGive, TwGiveTakeC.Level(TwGiveTakeC.TWTypeForGive), CurIdx<IdxC>().Idx);
                            }
                            else
                            {
                                Set(CellClickTypes.Firstlick);
                                SelIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
                            }
                        }

                        else if (Is(CellClickTypes.UpgradeUnit))
                        {
                            if (unit_cur.Is(new[] { UnitTypes.Pawn, UnitTypes.Archer })
                                && ownUnit_cur.Is(WhoseMoveC.CurPlayerI)
                                && !levUnit_cur.Is(LevelTypes.Second))
                            {
                                RpcSys.UpgradeUnitToMaster(CurIdx<IdxC>().Idx);
                            }
                            else
                            {
                                Set(CellClickTypes.Firstlick);
                                SelIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
                            }
                        }

                        else if (Is(CellClickTypes.GiveScout))
                        {
                            if (unit_cur.Is(UnitTypes.Pawn)
                                && ownUnit_cur.Is(WhoseMoveC.CurPlayerI))
                            {
                                RpcSys.FromNewUnitToMas(UnitTypes.Scout, CurIdx<IdxC>().Idx);
                            }

                            Set(CellClickTypes.Firstlick);
                            SelIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
                        }

                        else if (Is(CellClickTypes.GiveHero))
                        {


                            if (unit_cur.Is(UnitTypes.Archer))
                            {
                                if (unit_sel.Is(UnitTypes.Archer))
                                {
                                    RpcSys.FromToNewUnitToMas(UnitTypes.Elfemale, SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx);
                                    Set(CellClickTypes.Firstlick);

                                    NeedSoundEffC.Clip = ClipTypes.PickArcher;
                                }

                                NeedSoundEffC.Clip = ClipTypes.ClickToTable;
                            }
                            else
                            {
                                Set(CellClickTypes.Firstlick);
                            }

                            SelIdx<IdxC>().Idx = CurIdx<IdxC>().Idx;
                        }

                        else if (SelIdx<SelIdxC>().IsSelCell)
                        {
                            if (AttackCellsC.WhichAttack(WhoseMoveC.CurPlayerI, SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx) != default)
                            {
                                RpcSys.AttackUnitToMaster(SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx);
                            }

                            else if (Unit<UnitCellWC>(SelIdx<IdxC>().Idx).CanShift(CurIdx<IdxC>().Idx))
                            {
                                RpcSys.ShiftUnitToMaster(SelIdx<IdxC>().Idx, CurIdx<IdxC>().Idx);
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

                        else if (Is(CellClickTypes.Firstlick))
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
                }

                else if (RayCastC.Is(RaycastTypes.UI))
                {

                }

                else if (RayCastC.Is(RaycastTypes.Background))
                {
                    Set(CellClickTypes.Firstlick);
                    SelIdx<IdxC>().Reset();
                }
            }

            else
            {
                if (RayCastC.Is(RaycastTypes.Cell))
                {
                    if (Is(CellClickTypes.SetUnit))
                    {
                        if (!unit_cur.Have || !visUnit_cur.IsVisibled(WhoseMoveC.CurPlayerI))
                        {
                            if (CurIdxC.IsStartDirectToCell)
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