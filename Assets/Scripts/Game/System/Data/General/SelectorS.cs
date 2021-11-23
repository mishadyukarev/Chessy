using Leopotam.Ecs;
using System;
using static Game.Game.CellClickC;

namespace Game.Game
{
    public sealed class SelectorS : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC, VisibleC> _unitF = default;

        public void Run()
        {
            ref var unit_cur = ref _unitF.Get1(CurIdx.Idx);
            ref var levUnit_cur = ref _unitF.Get2(CurIdx.Idx);
            ref var ownUnit_cur = ref _unitF.Get3(CurIdx.Idx);
            ref var visUnit_cur = ref _unitF.Get4(CurIdx.Idx);

            ref var unit_sel = ref _unitF.Get1(SelIdx.Idx);


            if (InputC.IsClicked)
            {
                if (RayCastC.Is(RaycastTypes.Cell))
                {
                    if (!WhoseMoveC.IsMyMove)
                    {
                        SelIdx.Set(CurIdx.Idx);
                    }

                    else
                    {
                        if (Is(CellClickTypes.SetUnit))
                        {
                            RpcSys.SetUniToMaster(CurIdx.Idx, SelUnitC.Unit);
                            Set(CellClickTypes.Firstlick);
                        }

                        else if (Is(CellClickTypes.UniqAbil))
                        {
                            if (SelUniqAbilC.Is(UniqAbilTypes.FireArcher))
                            {
                                RpcSys.FireArcherToMas(SelIdx.Idx, CurIdx.Idx);
                            }

                            else if (SelUniqAbilC.Is(UniqAbilTypes.ChangeDirWind))
                            {
                                RpcSys.PutOutFireElffToMas(SelIdx.Idx, CurIdx.Idx);
                            }

                            else if (SelUniqAbilC.Is(UniqAbilTypes.StunElfemale))
                            {
                                RpcSys.StunElfemaleToMas(SelIdx.Idx, CurIdx.Idx);
                            }

                            Set(CellClickTypes.Firstlick);
                            SelIdx.Set(CurIdx.Idx);
                        }

                        else if (Is(CellClickTypes.GiveTakeTW))
                        {
                            if (unit_cur.Is(UnitTypes.Pawn) && ownUnit_cur.Is(WhoseMoveC.CurPlayerI))
                            {
                                RpcSys.GiveTakeToolWeapon(TwGiveTakeC.TWTypeForGive, TwGiveTakeC.Level(TwGiveTakeC.TWTypeForGive), CurIdx.Idx);
                            }
                            else
                            {
                                Set(CellClickTypes.Firstlick);
                                SelIdx.Set(CurIdx.Idx);
                            }
                        }

                        else if (Is(CellClickTypes.UpgradeUnit))
                        {
                            if (unit_cur.Is(new[] { UnitTypes.Pawn, UnitTypes.Archer })
                                && ownUnit_cur.Is(WhoseMoveC.CurPlayerI)
                                && !levUnit_cur.Is(LevelTypes.Second))
                            {
                                RpcSys.UpgradeUnitToMaster(CurIdx.Idx);
                            }
                            else
                            {
                                Set(CellClickTypes.Firstlick);
                                SelIdx.Set(CurIdx.Idx);
                            }
                        }

                        else if (Is(CellClickTypes.GiveScout))
                        {
                            if (unit_cur.Is(UnitTypes.Pawn)
                                && ownUnit_cur.Is(WhoseMoveC.CurPlayerI))
                            {
                                RpcSys.FromNewUnitToMas(UnitTypes.Scout, CurIdx.Idx);
                            }

                            Set(CellClickTypes.Firstlick);
                            SelIdx.Set(CurIdx.Idx);
                        }

                        else if (Is(CellClickTypes.GiveHero))
                        {


                            if (unit_cur.Is(UnitTypes.Archer))
                            {
                                if (unit_sel.Is(UnitTypes.Archer))
                                {
                                    RpcSys.FromToNewUnitToMas(UnitTypes.Elfemale, SelIdx.Idx, CurIdx.Idx);
                                    Set(CellClickTypes.Firstlick);

                                    NeedSoundEffC.Clip = ClipTypes.PickArcher;
                                }

                                NeedSoundEffC.Clip = ClipTypes.ClickToTable;
                            }
                            else
                            {
                                Set(CellClickTypes.Firstlick);
                            }

                            SelIdx.Set(CurIdx.Idx);
                        }

                        else if (SelIdx.IsSelCell)
                        {
                            if (AttackCellsC.WhichAttack(WhoseMoveC.CurPlayerI, SelIdx.Idx, CurIdx.Idx) != default)
                            {
                                RpcSys.AttackUnitToMaster(SelIdx.Idx, CurIdx.Idx);
                            }

                            else if (ShiftCellsC.HaveIdxCell(WhoseMoveC.CurPlayerI, SelIdx.Idx, CurIdx.Idx))
                            {
                                RpcSys.ShiftUnitToMaster(SelIdx.Idx, CurIdx.Idx);
                            }

                            else
                            {
                                

                                if (unit_cur.HaveUnit)
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

                            SelIdx.Set(CurIdx.Idx);
                        }

                        else if (Is(CellClickTypes.Firstlick))
                        {
                            if (unit_cur.HaveUnit)
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

                            SelIdx.Set(CurIdx.Idx);
                        }
                    }
                }

                else if (RayCastC.Is(RaycastTypes.UI))
                {

                }

                else if (RayCastC.Is(RaycastTypes.Background))
                {
                    Set(CellClickTypes.Firstlick);
                    SelIdx.Reset();
                }
            }

            else
            {
                if (RayCastC.Is(RaycastTypes.Cell))
                {
                    if (Is(CellClickTypes.SetUnit))
                    {
                        if (!unit_cur.HaveUnit || !visUnit_cur.IsVisibled(WhoseMoveC.CurPlayerI))
                        {
                            if (CurIdx.IsStartDirectToCell)
                            {
                                IdxPreVis.Idx = CurIdx.Idx;
                            }
                            else
                            {
                                IdxPreVis.Idx = CurIdx.Idx;
                            }
                        }
                    }
                }

                NeedSoundEffC.Clip = default;
            }
        }
    }
}