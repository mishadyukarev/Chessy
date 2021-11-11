using Leopotam.Ecs;
using Chessy.Common;
using static Chessy.Game.SelectorC;
using System;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class SelectorS : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelUnitC, OwnerC, VisibleC> _cellUnitFilter = default;
        public void Run()
        {
            UnitC UnitDatCom(byte idxCell) => _cellUnitFilter.Get1(idxCell);
            LevelUnitC LevelUnitC(byte idx) => _cellUnitFilter.Get2(idx);
            OwnerC OwnUnitCom(byte idxCell) => _cellUnitFilter.Get3(idxCell);
            VisibleC VisUnitCom(byte idxCell) => _cellUnitFilter.Get4(idxCell);

            #region else

            #endregion


            if (InputC.IsClicked)
            {
                if (RayCastC.Is(RaycastTypes.UI))
                {
                    SelUnitC.ResetSelUnit();
                }

                else if (RayCastC.Is(RaycastTypes.Cell))
                {
                    if (!WhoseMoveC.IsMyMove)
                    {
                        IdxPreCell = IdxSelCell;
                        IdxSelCell = IdxCurCell;

                        Reset();

                        //if (!IsSelCell)
                        //{
                        //    if (IdxPreCell != IdxSelCell)
                        //    {
                        //        IdxSelCell = IdxCurCell;
                        //    }
                        //    else
                        //    {
                        //        IdxSelCell = default;
                        //    }


                        //    IdxPreCell = IdxSelCell;
                        //}

                        //else
                        //{
                        //    if (IdxSelCell != IdxCurCell)
                        //        IdxPreCell = IdxSelCell;

                        //    IdxSelCell = IdxCurCell;
                        //}
                    }

                    else
                    {
                        if (SelUnitC.IsSelUnit)
                        {
                            RpcSys.SetUniToMaster(IdxCurCell, SelUnitC.SelUnit);
                            SelUnitC.ResetSelUnit();
                        }

                        else if (Is(CellClickTypes.PickFire))
                        {
                            RpcSys.FireArcherToMas(IdxSelCell, IdxCurCell);

                            Reset();
                            IdxSelCell = IdxCurCell;
                        }

                        else if (Is(CellClickTypes.PutOutFireElfemale))
                        {
                            RpcSys.PutOutFireElffToMas(IdxSelCell, IdxCurCell);

                            Reset();
                            IdxSelCell = IdxCurCell;
                        }

                        else if (Is(CellClickTypes.GiveTakeTW))
                        {
                            if (UnitDatCom(IdxCurCell).Is(UnitTypes.Pawn) && OwnUnitCom(IdxCurCell).Is(WhoseMoveC.CurPlayerI))
                            {
                                RpcSys.GiveTakeToolWeapon(TwGiveTakeC.TWTypeForGive, TwGiveTakeC.Level(TwGiveTakeC.TWTypeForGive), IdxCurCell);
                            }
                            else
                            {
                                IdxSelCell = IdxCurCell;
                                Reset();
                                TwGiveTakeC.TWTypeForGive = default;
                            }
                        }

                        else if (Is(CellClickTypes.UpgradeUnit))
                        {
                            if (UnitDatCom(IdxCurCell).Is(new[] { UnitTypes.Pawn, UnitTypes.Archer })
                                && OwnUnitCom(IdxCurCell).Is(WhoseMoveC.CurPlayerI)
                                && !LevelUnitC(IdxCurCell).Is(LevelUnitTypes.Second))
                            {
                                RpcSys.UpgradeUnitToMaster(IdxCurCell);
                            }
                            else
                            {
                                IdxSelCell = IdxCurCell;
                                Reset();
                            }
                        }

                        else if (Is(CellClickTypes.GiveScout))
                        {
                            if (IdxSelCell != IdxCurCell)
                                IdxPreCell = IdxSelCell;
                            IdxSelCell = IdxCurCell;

                            if (UnitDatCom(IdxCurCell).Is(UnitTypes.Pawn)
                                && OwnUnitCom(IdxCurCell).Is(WhoseMoveC.CurPlayerI))
                            {
                                RpcSys.FromNewUnitToMas(UnitTypes.Scout, IdxCurCell);
                                Reset();
                            }
                            else
                            {
                                Reset();
                            }
                        }

                        else if (Is(CellClickTypes.GiveHero))
                        {
                            IdxPreCell = IdxSelCell;
                            IdxSelCell = IdxCurCell;

                            ref var unit_sel = ref _cellUnitFilter.Get1(IdxSelCell);
                            ref var unit_pre = ref _cellUnitFilter.Get1(IdxPreCell);

                            if (unit_sel.Is(UnitTypes.Archer))
                            {
                                if (unit_pre.Is(UnitTypes.Archer))
                                {
                                    RpcSys.FromToNewUnitToMas(InvUnitsC.MyHero, IdxPreCell, IdxSelCell);

                                    Reset();
                                }
                            }
                            else
                            {
                                Reset();
                            }
                        }

                        else if (Is(CellClickTypes.StunElfemale))
                        {
                            IdxPreCell = IdxSelCell;
                            IdxSelCell = IdxCurCell;

                            RpcSys.StunElfemaleToMas(IdxPreCell, IdxSelCell);
                            Reset();
                        }

                        else if (IsSelCell)
                        {
                            if (IdxSelCell != IdxCurCell)
                                IdxPreCell = IdxSelCell;

                            IdxSelCell = IdxCurCell;


                            if (CellsAttackC.FindByIdx(WhoseMoveC.CurPlayerI, IdxPreCell, IdxSelCell) != default)
                            {
                                RpcSys.AttackUnitToMaster(IdxPreCell, IdxSelCell);
                            }

                            if (CellsForShiftCom.HaveIdxCell(WhoseMoveC.CurPlayerI, IdxPreCell, IdxSelCell))
                            {
                                RpcSys.ShiftUnitToMaster(IdxPreCell, IdxSelCell);
                            }

                            else if (UnitDatCom(IdxSelCell).HaveUnit)
                            {
                                if (OwnUnitCom(IdxSelCell).Is(WhoseMoveC.CurPlayerI))
                                {
                                    if (UnitDatCom(IdxSelCell).Is(UnitTypes.Scout))
                                    {

                                    }
                                    else if (UnitDatCom(IdxSelCell).IsMelee)
                                    {
                                        SoundEffectC.Play(ClipGameTypes.PickMelee);
                                    }
                                    else
                                    {
                                        SoundEffectC.Play(ClipGameTypes.PickArcher);
                                    }
                                }
                            }
                        }

                        else
                        {
                            if (IdxPreCell != IdxSelCell || IdxPreCell == 0)
                            {
                                IdxSelCell = IdxCurCell;
                            }
                            else
                            {
                                DefSelectedCell();
                            }

                            if (UnitDatCom(IdxSelCell).HaveUnit)
                            {
                                if (OwnUnitCom(IdxSelCell).Is(WhoseMoveC.CurPlayerI))
                                {
                                    if (UnitDatCom(IdxSelCell).Is(UnitTypes.Scout))
                                    {

                                    }
                                    else if (UnitDatCom(IdxSelCell).IsMelee)
                                    {
                                        SoundEffectC.Play(ClipGameTypes.PickMelee);
                                    }
                                    else
                                    {
                                        SoundEffectC.Play(ClipGameTypes.PickArcher);
                                    }
                                }
                            }



                            IdxPreCell = IdxSelCell;
                        }
                    }
                }

                else
                {
                    SelUnitC.ResetSelUnit();
                    DefSelectedCell();
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
                        if (!UnitDatCom(IdxCurCell).HaveUnit || !VisUnitCom(IdxCurCell).IsVisibled(WhoseMoveC.CurPlayerI))
                        {
                            if (IsStartDirectToCell)
                            {
                                IdxPreVisionCell = IdxCurCell;
                                IdxCurCell = default;
                            }
                            else
                            {
                                IdxPreVisionCell = IdxCurCell;
                            }
                        }
                    }
                }
            }
        }
    }
}