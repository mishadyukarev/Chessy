using Leopotam.Ecs;
using Chessy.Common;
using static Chessy.Game.CellClickC;
using System;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class SelectorS : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC, VisibleC> _cellUnitFilter = default;
        public void Run()
        {
            UnitC UnitDatCom(byte idxCell) => _cellUnitFilter.Get1(idxCell);
            LevelC LevelUnitC(byte idx) => _cellUnitFilter.Get2(idx);
            OwnerC OwnUnitCom(byte idxCell) => _cellUnitFilter.Get3(idxCell);
            VisibleC VisUnitCom(byte idxCell) => _cellUnitFilter.Get4(idxCell);


            if (InputC.IsClicked)
            {
                if (RayCastC.Is(RaycastTypes.UI))
                {
                    Reset();
                }

                else if (RayCastC.Is(RaycastTypes.Cell))
                {
                    if (!WhoseMoveC.IsMyMove)
                    {
                        IdxPre.Idx = SelIdx.Idx;
                        SelIdx.Idx = IdxCur.Idx;

                        Reset();
                    }

                    else
                    {
                        if (Is(CellClickTypes.SetUnit))
                        {
                            if (SelUnitC.IsSelUnit)
                            {
                                RpcSys.SetUniToMaster(IdxCur.Idx, SelUnitC.SelUnit);
                                Reset();
                            }
                        }

                        else if (Is(CellClickTypes.UniqAbil))
                        {
                            if (SelUniqAbilC.Is(UniqAbilTypes.FireArcher))
                            {
                                RpcSys.FireArcherToMas(SelIdx.Idx, IdxCur.Idx);

                                Reset();
                                SelIdx.Idx = IdxCur.Idx;
                            }

                            else if (SelUniqAbilC.Is(UniqAbilTypes.ChangeDirWind))
                            {
                                RpcSys.PutOutFireElffToMas(SelIdx.Idx, IdxCur.Idx);

                                Reset();
                                SelIdx.Idx = IdxCur.Idx;
                            }

                            else if (SelUniqAbilC.Is(UniqAbilTypes.StunElfemale))
                            {
                                IdxPre.Idx = SelIdx.Idx;
                                SelIdx.Idx = IdxCur.Idx;

                                RpcSys.StunElfemaleToMas(IdxPre.Idx, SelIdx.Idx);
                                Reset();
                            }
                        }

                        else if (Is(CellClickTypes.GiveTakeTW))
                        {
                            if (UnitDatCom(IdxCur.Idx).Is(UnitTypes.Pawn) && OwnUnitCom(IdxCur.Idx).Is(WhoseMoveC.CurPlayerI))
                            {
                                RpcSys.GiveTakeToolWeapon(TwGiveTakeC.TWTypeForGive, TwGiveTakeC.Level(TwGiveTakeC.TWTypeForGive), IdxCur.Idx);
                            }
                            else
                            {
                                SelIdx.Idx = IdxCur.Idx;
                                Reset();
                                TwGiveTakeC.TWTypeForGive = default;
                            }
                        }

                        else if (Is(CellClickTypes.UpgradeUnit))
                        {
                            if (UnitDatCom(IdxCur.Idx).Is(new[] { UnitTypes.Pawn, UnitTypes.Archer })
                                && OwnUnitCom(IdxCur.Idx).Is(WhoseMoveC.CurPlayerI)
                                && !LevelUnitC(IdxCur.Idx).Is(LevelUnitTypes.Second))
                            {
                                RpcSys.UpgradeUnitToMaster(IdxCur.Idx);
                            }
                            else
                            {
                                SelIdx.Idx = IdxCur.Idx;
                                Reset();
                            }
                        }

                        else if (Is(CellClickTypes.GiveScout))
                        {
                            if (SelIdx.Idx != IdxCur.Idx)
                                IdxPre.Idx = SelIdx.Idx;
                            SelIdx.Idx = IdxCur.Idx;

                            if (UnitDatCom(IdxCur.Idx).Is(UnitTypes.Pawn)
                                && OwnUnitCom(IdxCur.Idx).Is(WhoseMoveC.CurPlayerI))
                            {
                                RpcSys.FromNewUnitToMas(UnitTypes.Scout, IdxCur.Idx);
                                Reset();
                            }
                            else
                            {
                                Reset();
                            }
                        }

                        else if (Is(CellClickTypes.GiveHero))
                        {
                            IdxPre.Idx = SelIdx.Idx;
                            SelIdx.Idx = IdxCur.Idx;

                            ref var unit_sel = ref _cellUnitFilter.Get1(SelIdx.Idx);
                            ref var unit_pre = ref _cellUnitFilter.Get1(IdxPre.Idx);

                            if (unit_sel.Is(UnitTypes.Archer))
                            {
                                if (unit_pre.Is(UnitTypes.Archer))
                                {
                                    RpcSys.FromToNewUnitToMas(InvUnitsC.MyHero, IdxPre.Idx, SelIdx.Idx);

                                    Reset();
                                }
                            }
                            else
                            {
                                Reset();
                            }
                        }

                        else if (SelIdx.IsSelCell)
                        {
                            if (SelIdx.Idx != IdxCur.Idx)
                                IdxPre.Idx = SelIdx.Idx;

                            SelIdx.Idx = IdxCur.Idx;


                            if (AttackCellsC.FindByIdx(WhoseMoveC.CurPlayerI, IdxPre.Idx, SelIdx.Idx) != default)
                            {
                                RpcSys.AttackUnitToMaster(IdxPre.Idx, SelIdx.Idx);
                            }

                            if (CellsForShiftCom.HaveIdxCell(WhoseMoveC.CurPlayerI, IdxPre.Idx, SelIdx.Idx))
                            {
                                RpcSys.ShiftUnitToMaster(IdxPre.Idx, SelIdx.Idx);
                            }

                            else if (UnitDatCom(SelIdx.Idx).HaveUnit)
                            {
                                if (OwnUnitCom(SelIdx.Idx).Is(WhoseMoveC.CurPlayerI))
                                {
                                    if (UnitDatCom(SelIdx.Idx).Is(UnitTypes.Scout))
                                    {

                                    }
                                    else if (UnitDatCom(SelIdx.Idx).IsMelee)
                                    {
                                        //SoundEffectC.Play(ClipGameTypes.PickMelee);
                                    }
                                    else
                                    {
                                        //SoundEffectC.Play(ClipGameTypes.PickArcher);
                                    }
                                }
                            }
                        }

                        else
                        {
                            if (IdxPre.Idx != SelIdx.Idx || IdxPre.Idx == 0)
                            {
                                SelIdx.Idx = IdxCur.Idx;
                            }
                            else
                            {
                                SelIdx.Reset();
                            }

                            if (UnitDatCom(SelIdx.Idx).HaveUnit)
                            {
                                if (OwnUnitCom(SelIdx.Idx).Is(WhoseMoveC.CurPlayerI))
                                {
                                    if (UnitDatCom(SelIdx.Idx).Is(UnitTypes.Scout))
                                    {

                                    }
                                    else if (UnitDatCom(SelIdx.Idx).IsMelee)
                                    {
                                        //SoundEffectC.Play(ClipGameTypes.PickMelee);
                                    }
                                    else
                                    {
                                        //SoundEffectC.Play(ClipGameTypes.PickArcher);
                                    }
                                }
                            }



                            IdxPre.Idx = SelIdx.Idx;
                        }
                    }
                }

                else
                {
                    SelIdx.Reset();
                    Reset();
                    TwGiveTakeC.TWTypeForGive = default;
                }
            }

            else
            {
                if (RayCastC.Is(RaycastTypes.Cell))
                {
                    if (SelUnitC.IsSelUnit)
                    {
                        if (!UnitDatCom(IdxCur.Idx).HaveUnit || !VisUnitCom(IdxCur.Idx).IsVisibled(WhoseMoveC.CurPlayerI))
                        {
                            if (IdxCur.IsStartDirectToCell)
                            {
                                IdxPreVis.Idx = IdxCur.Idx;
                                IdxCur.Idx = default;
                            }
                            else
                            {
                                IdxPreVis.Idx = IdxCur.Idx;
                            }
                        }
                    }
                }
            }
        }
    }
}