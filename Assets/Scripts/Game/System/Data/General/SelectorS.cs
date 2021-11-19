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
            UnitC UnitDatCom(byte idxCell) => _unitF.Get1(idxCell);
            LevelC LevelUnitC(byte idx) => _unitF.Get2(idx);
            OwnerC OwnUnitCom(byte idxCell) => _unitF.Get3(idxCell);


            if (InputC.IsClicked)
            {
                if (RayCastC.Is(RaycastTypes.Cell))
                {
                    if (!WhoseMoveC.IsMyMove)
                    {
                        Set(CellClickTypes.SelCell);
                        SelIdx.Set(CurIdx.Idx);
                    }

                    else
                    {
                        if (Is(CellClickTypes.SetUnit))
                        {
                            RpcSys.SetUniToMaster(CurIdx.Idx, SelUnitC.Unit);
                            Reset();
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

                            Set(CellClickTypes.SelCell);
                            SelIdx.Set(CurIdx.Idx);
                        }

                        else if (Is(CellClickTypes.GiveTakeTW))
                        {
                            if (UnitDatCom(CurIdx.Idx).Is(UnitTypes.Pawn) && OwnUnitCom(CurIdx.Idx).Is(WhoseMoveC.CurPlayerI))
                            {
                                RpcSys.GiveTakeToolWeapon(TwGiveTakeC.TWTypeForGive, TwGiveTakeC.Level(TwGiveTakeC.TWTypeForGive), CurIdx.Idx);
                            }
                            else
                            {
                                Set(CellClickTypes.SelCell);
                                SelIdx.Set(CurIdx.Idx);
                            }
                        }

                        else if (Is(CellClickTypes.UpgradeUnit))
                        {
                            if (UnitDatCom(CurIdx.Idx).Is(new[] { UnitTypes.Pawn, UnitTypes.Archer })
                                && OwnUnitCom(CurIdx.Idx).Is(WhoseMoveC.CurPlayerI)
                                && !LevelUnitC(CurIdx.Idx).Is(LevelTypes.Second))
                            {
                                RpcSys.UpgradeUnitToMaster(CurIdx.Idx);
                            }
                            else
                            {
                                Set(CellClickTypes.SelCell);
                                SelIdx.Set(CurIdx.Idx);
                            }
                        }

                        else if (Is(CellClickTypes.GiveScout))
                        {
                            Set(CellClickTypes.SelCell);
                            SelIdx.Set(CurIdx.Idx);

                            if (UnitDatCom(CurIdx.Idx).Is(UnitTypes.Pawn)
                                && OwnUnitCom(CurIdx.Idx).Is(WhoseMoveC.CurPlayerI))
                            {
                                RpcSys.FromNewUnitToMas(UnitTypes.Scout, CurIdx.Idx);
                            }
                        }

                        else if (Is(CellClickTypes.GiveHero))
                        {
                            PreIdx.Idx = SelIdx.Idx;
                            SelIdx.Set(CurIdx.Idx);


                            ref var unit_sel = ref _unitF.Get1(SelIdx.Idx);
                            ref var unit_pre = ref _unitF.Get1(PreIdx.Idx);

                            if (unit_sel.Is(UnitTypes.Archer))
                            {
                                if (unit_pre.Is(UnitTypes.Archer))
                                {
                                    RpcSys.FromToNewUnitToMas(InvUnitsC.MyHero, PreIdx.Idx, SelIdx.Idx);
                                    Reset();

                                    NeedSoundEffC.Clip = ClipTypes.PickArcher;
                                }

                                NeedSoundEffC.Clip = ClipTypes.ClickToTable;
                            }
                            else
                            {
                                Reset();
                            }
                        }

                        else if (Is(CellClickTypes.SelCell))
                        {
                            if (SelIdx.Idx != CurIdx.Idx)
                            {
                                PreIdx.Idx = SelIdx.Idx;
                                SelIdx.Set(CurIdx.Idx);
                            }

                            ref var unit_sel = ref _unitF.Get1(SelIdx.Idx);
                            ref var ownUnit_sel = ref _unitF.Get3(SelIdx.Idx);


                            if (AttackCellsC.FindByIdx(WhoseMoveC.CurPlayerI, PreIdx.Idx, SelIdx.Idx) != default)
                            {
                                RpcSys.AttackUnitToMaster(PreIdx.Idx, SelIdx.Idx);
                            }

                            else if (CellsShiftC.HaveIdxCell(WhoseMoveC.CurPlayerI, PreIdx.Idx, SelIdx.Idx))
                            {
                                RpcSys.ShiftUnitToMaster(PreIdx.Idx, SelIdx.Idx);
                            }

                            else
                            {
                                if (unit_sel.HaveUnit)
                                {
                                    if (ownUnit_sel.Is(WhoseMoveC.CurPlayerI))
                                    {
                                        if (unit_sel.Is(UnitTypes.Scout))
                                        {

                                        }
                                        else if (unit_sel.IsMelee)
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
                        }

                        else if (Is(CellClickTypes.None))
                        {
                            SelIdx.Set(CurIdx.Idx);
                            Set(CellClickTypes.SelCell);
                            PreIdx.Idx = SelIdx.Idx;

                            ref var unit_sel = ref _unitF.Get1(SelIdx.Idx);
                            ref var ownUnit_sel = ref _unitF.Get3(SelIdx.Idx);

                            if (unit_sel.HaveUnit)
                            {
                                if (ownUnit_sel.Is(WhoseMoveC.CurPlayerI))
                                {
                                    if (unit_sel.Is(UnitTypes.Scout))
                                    {

                                    }
                                    else if (unit_sel.IsMelee)
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

                        else
                        {
                            throw new Exception();
                        }
                    }
                }

                else if (RayCastC.Is(RaycastTypes.UI))
                {

                }

                else if (RayCastC.Is(RaycastTypes.Background))
                {
                    Reset();
                }

                else
                {
                    throw new Exception();
                }
            }

            else
            {
                if (RayCastC.Is(RaycastTypes.Cell))
                {
                    if (Is(CellClickTypes.SetUnit))
                    {
                        ref var unit_cur = ref _unitF.Get1(CurIdx.Idx);
                        ref var visUnit_cur = ref _unitF.Get4(CurIdx.Idx);

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