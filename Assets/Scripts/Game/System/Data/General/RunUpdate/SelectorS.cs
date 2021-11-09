using Leopotam.Ecs;
using Chessy.Common;
using static Chessy.Game.SelectorC;
using System;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class SelectorS : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, LevelUnitC, OwnerC, VisibleC> _cellUnitFilter = default;
        public void Run()
        {
            CellUnitDataC UnitDatCom(byte idxCell) => _cellUnitFilter.Get1(idxCell);
            LevelUnitC LevelUnitC(byte idx) => _cellUnitFilter.Get2(idx);
            OwnerC OwnUnitCom(byte idxCell) => _cellUnitFilter.Get3(idxCell);
            VisibleC VisUnitCom(byte idxCell) => _cellUnitFilter.Get4(idxCell);

            #region else
            //if (GameModesCom.IsOnlineMode && !WhoseMoveCom.IsMyOnlineMove)
            //{
            //    if (!SelectorCom.IsSelCell)
            //    {
            //        if (SelectorCom.IdxPreCell != SelectorCom.IdxSelCell)
            //        {
            //            SelectorCom.IdxSelCell = SelectorCom.IdxCurCell;
            //        }
            //        else
            //        {
            //            SelectorCom.IdxSelCell = default;
            //        }


            //        SelectorCom.IdxPreCell = SelectorCom.IdxSelCell;
            //    }

            //    else
            //    {
            //        if (SelectorCom.IdxSelCell != SelectorCom.IdxCurCell)
            //            SelectorCom.IdxPreCell = SelectorCom.IdxSelCell;

            //        SelectorCom.IdxSelCell = SelectorCom.IdxCurCell;
            //    }
            //}

            /*else */
            #endregion


            if (InputC.IsClicked)
            {
                if (RayCastC.Is(RaycastTypes.UI))
                {
                    SelUnitC.ResetSelUnit();
                }

                else if (RayCastC.Is(RaycastTypes.Cell))
                {
                    if (SelUnitC.IsSelUnit)
                    {
                        RpcSys.SetUniToMaster(IdxCurCell, SelUnitC.SelUnit);
                        SelUnitC.ResetSelUnit();
                    }

                    else if (Is(CellClickTypes.PickFire))
                    {
                        RpcSys.FireToMaster(IdxSelCell, IdxCurCell);

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
                        if (UnitDatCom(IdxCurCell).Is(new[] { UnitTypes.Pawn, UnitTypes.Rook, UnitTypes.Bishop })
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

                    else if (Is(CellClickTypes.OldNewUnit))
                    {
                        if (IdxSelCell != IdxCurCell)
                            IdxPreCell = IdxSelCell;
                        IdxSelCell = IdxCurCell;

                        ref var unit_sel = ref _cellUnitFilter.Get1(IdxSelCell);
                        ref var unit_pre = ref _cellUnitFilter.Get1(IdxPreCell);

                        if (OldNewC.Is(UnitTypes.Scout))
                        {
                            if (UnitDatCom(IdxCurCell).Is(UnitTypes.Pawn)
                                && OwnUnitCom(IdxCurCell).Is(WhoseMoveC.CurPlayerI))
                            {
                                RpcSys.FromNewUnitToMas(OldNewC.Unit, IdxCurCell);
                                Reset();
                            }
                            else
                            {

                                Reset();
                            }
                        }

                        else if (OldNewC.Is(UnitTypes.Elfemale))
                        {
                            if (unit_sel.Is(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
                            {
                                if (unit_pre.Is(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
                                {
                                    RpcSys.FromToNewUnitToMas(OldNewC.Unit, IdxPreCell, IdxSelCell);

                                    Reset();
                                }
                            }
                            else
                            {
                                Reset();
                            }
                        }

                        else throw new Exception();
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