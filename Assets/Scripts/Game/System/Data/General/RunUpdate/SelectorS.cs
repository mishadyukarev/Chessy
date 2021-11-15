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
                        IdxPre.Idx = IdxSel.Idx;
                        IdxSel.Idx = IdxCur.Idx;

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
                                RpcSys.FireArcherToMas(IdxSel.Idx, IdxCur.Idx);

                                Reset();
                                IdxSel.Idx = IdxCur.Idx;
                            }

                            else if (SelUniqAbilC.Is(UniqAbilTypes.PutOutFireElfemale))
                            {
                                RpcSys.PutOutFireElffToMas(IdxSel.Idx, IdxCur.Idx);

                                Reset();
                                IdxSel.Idx = IdxCur.Idx;
                            }

                            else if (SelUniqAbilC.Is(UniqAbilTypes.StunElfemale))
                            {
                                IdxPre.Idx = IdxSel.Idx;
                                IdxSel.Idx = IdxCur.Idx;

                                RpcSys.StunElfemaleToMas(IdxPre.Idx, IdxSel.Idx);
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
                                IdxSel.Idx = IdxCur.Idx;
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
                                IdxSel.Idx = IdxCur.Idx;
                                Reset();
                            }
                        }

                        else if (Is(CellClickTypes.GiveScout))
                        {
                            if (IdxSel.Idx != IdxCur.Idx)
                                IdxPre.Idx = IdxSel.Idx;
                            IdxSel.Idx = IdxCur.Idx;

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
                            IdxPre.Idx = IdxSel.Idx;
                            IdxSel.Idx = IdxCur.Idx;

                            ref var unit_sel = ref _cellUnitFilter.Get1(IdxSel.Idx);
                            ref var unit_pre = ref _cellUnitFilter.Get1(IdxPre.Idx);

                            if (unit_sel.Is(UnitTypes.Archer))
                            {
                                if (unit_pre.Is(UnitTypes.Archer))
                                {
                                    RpcSys.FromToNewUnitToMas(InvUnitsC.MyHero, IdxPre.Idx, IdxSel.Idx);

                                    Reset();
                                }
                            }
                            else
                            {
                                Reset();
                            }
                        }

                        else if (IdxSel.IsSelCell)
                        {
                            if (IdxSel.Idx != IdxCur.Idx)
                                IdxPre.Idx = IdxSel.Idx;

                            IdxSel.Idx = IdxCur.Idx;


                            if (CellsAttackC.FindByIdx(WhoseMoveC.CurPlayerI, IdxPre.Idx, IdxSel.Idx) != default)
                            {
                                RpcSys.AttackUnitToMaster(IdxPre.Idx, IdxSel.Idx);
                            }

                            if (CellsForShiftCom.HaveIdxCell(WhoseMoveC.CurPlayerI, IdxPre.Idx, IdxSel.Idx))
                            {
                                RpcSys.ShiftUnitToMaster(IdxPre.Idx, IdxSel.Idx);
                            }

                            else if (UnitDatCom(IdxSel.Idx).HaveUnit)
                            {
                                if (OwnUnitCom(IdxSel.Idx).Is(WhoseMoveC.CurPlayerI))
                                {
                                    if (UnitDatCom(IdxSel.Idx).Is(UnitTypes.Scout))
                                    {

                                    }
                                    else if (UnitDatCom(IdxSel.Idx).IsMelee)
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
                            if (IdxPre.Idx != IdxSel.Idx || IdxPre.Idx == 0)
                            {
                                IdxSel.Idx = IdxCur.Idx;
                            }
                            else
                            {
                                IdxSel.Reset();
                            }

                            if (UnitDatCom(IdxSel.Idx).HaveUnit)
                            {
                                if (OwnUnitCom(IdxSel.Idx).Is(WhoseMoveC.CurPlayerI))
                                {
                                    if (UnitDatCom(IdxSel.Idx).Is(UnitTypes.Scout))
                                    {

                                    }
                                    else if (UnitDatCom(IdxSel.Idx).IsMelee)
                                    {
                                        //SoundEffectC.Play(ClipGameTypes.PickMelee);
                                    }
                                    else
                                    {
                                        //SoundEffectC.Play(ClipGameTypes.PickArcher);
                                    }
                                }
                            }



                            IdxPre.Idx = IdxSel.Idx;
                        }
                    }
                }

                else
                {
                    IdxSel.Reset();
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