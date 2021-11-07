using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class SelectorSystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, LevelUnitC, OwnerCom, VisibleC> _cellUnitFilter = default;
        public void Run()
        {
            CellUnitDataC UnitDatCom(byte idxCell) => _cellUnitFilter.Get1(idxCell);
            LevelUnitC LevelUnitC(byte idx) => _cellUnitFilter.Get2(idx);
            OwnerCom OwnUnitCom(byte idxCell) => _cellUnitFilter.Get3(idxCell);
            VisibleC VisUnitCom(byte idxCell) => _cellUnitFilter.Get4(idxCell);


            if (InputC.IsClicked)
            {
                if (SelectorC.Is(RaycastGettedTypes.UI))
                {
                    SelectorC.DefSelectedUnit();
                }

                else if (SelectorC.Is(RaycastGettedTypes.Cell))
                {
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

                    /*else */if (SelectorC.IsSelUnit)
                    {
                        RpcSys.SetUniToMaster(SelectorC.IdxCurCell, SelectorC.SelUnitType);
                        SelectorC.SelUnitType = default;
                    }

                    else if (SelectorC.Is(CellClickTypes.PickFire))
                    {
                        RpcSys.FireToMaster(SelectorC.IdxSelCell, SelectorC.IdxCurCell);

                        SelectorC.CellClickType = default;
                        SelectorC.IdxSelCell = SelectorC.IdxCurCell;
                    }

                    else if (SelectorC.Is(CellClickTypes.GiveTakeTW))
                    {
                        if (UnitDatCom(SelectorC.IdxCurCell).Is(UnitTypes.Pawn) && OwnUnitCom(SelectorC.IdxCurCell).Is(WhoseMoveC.CurPlayerI))
                        {
                            RpcSys.GiveTakeToolWeapon(SelectorC.TWTypeForGive, GiveTakeDataUIC.Level(SelectorC.TWTypeForGive), SelectorC.IdxCurCell);
                        }
                        else
                        {
                            SelectorC.IdxSelCell = SelectorC.IdxCurCell;
                            SelectorC.DefCellClickType();
                            SelectorC.TWTypeForGive = default;
                        }
                    }

                    else if (SelectorC.Is(CellClickTypes.UpgradeUnit))
                    {
                        if (UnitDatCom(SelectorC.IdxCurCell).Is(new[] { UnitTypes.Pawn, UnitTypes.Rook, UnitTypes.Bishop })
                            && OwnUnitCom(SelectorC.IdxCurCell).Is(WhoseMoveC.CurPlayerI)
                            && !LevelUnitC(SelectorC.IdxCurCell).Is(LevelUnitTypes.Iron))
                        {
                            RpcSys.UpgradeUnitToMaster(SelectorC.IdxCurCell);
                        }
                        else
                        {
                            SelectorC.IdxSelCell = SelectorC.IdxCurCell;
                            SelectorC.DefCellClickType();
                        }
                    }

                    else if (SelectorC.Is(CellClickTypes.OldToNewUnit))
                    {
                        if (UnitDatCom(SelectorC.IdxCurCell).Is(UnitTypes.Pawn) && OwnUnitCom(SelectorC.IdxCurCell).Is(WhoseMoveC.CurPlayerI))
                        {
                            RpcSys.OldToNewToMaster(SelectorC.UnitTypeOldToNew, SelectorC.IdxCurCell);
                            SelectorC.DefCellClickType();
                        }
                        else
                        {
                            SelectorC.IdxSelCell = SelectorC.IdxCurCell;
                            SelectorC.DefCellClickType();
                        }
                    }

                    else if (SelectorC.IsSelCell)
                    {
                        if (SelectorC.IdxSelCell != SelectorC.IdxCurCell)
                            SelectorC.IdxPreCell = SelectorC.IdxSelCell;

                        SelectorC.IdxSelCell = SelectorC.IdxCurCell;

                       
                        if (CellsAttackC.FindByIdx(WhoseMoveC.CurPlayerI, SelectorC.IdxPreCell, SelectorC.IdxSelCell) != default)
                        {
                            RpcSys.AttackUnitToMaster(SelectorC.IdxPreCell, SelectorC.IdxSelCell);
                        }

                        if (CellsForShiftCom.HaveIdxCell(WhoseMoveC.CurPlayerI, SelectorC.IdxPreCell, SelectorC.IdxSelCell))
                        {
                            RpcSys.ShiftUnitToMaster(SelectorC.IdxPreCell, SelectorC.IdxSelCell);
                        }

                        else if (UnitDatCom(SelectorC.IdxSelCell).HaveUnit)
                        {
                            if (OwnUnitCom(SelectorC.IdxSelCell).Is(WhoseMoveC.CurPlayerI))
                            {
                                if (UnitDatCom(SelectorC.IdxSelCell).Is(UnitTypes.Scout))
                                {

                                }
                                else if (UnitDatCom(SelectorC.IdxSelCell).IsMelee)
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
                        if (SelectorC.IdxPreCell != SelectorC.IdxSelCell || SelectorC.IdxPreCell == 0)
                        {
                            SelectorC.IdxSelCell = SelectorC.IdxCurCell;
                        }
                        else
                        {
                            SelectorC.DefSelectedCell();
                        }

                        if (UnitDatCom(SelectorC.IdxSelCell).HaveUnit)
                        {
                            if (OwnUnitCom(SelectorC.IdxSelCell).Is(WhoseMoveC.CurPlayerI))
                            {
                                if (UnitDatCom(SelectorC.IdxSelCell).Is(UnitTypes.Scout))
                                {

                                }
                                else if (UnitDatCom(SelectorC.IdxSelCell).IsMelee)
                                {
                                    SoundEffectC.Play(ClipGameTypes.PickMelee);
                                }
                                else
                                {
                                    SoundEffectC.Play(ClipGameTypes.PickArcher);
                                }
                            }
                        }



                        SelectorC.IdxPreCell = SelectorC.IdxSelCell;
                    }

                }

                else
                {
                    SelectorC.DefSelectedUnit();
                    SelectorC.DefSelectedCell();
                    SelectorC.DefCellClickType();
                    SelectorC.TWTypeForGive = default;
                }
            }

            else
            {
                if (SelectorC.Is(RaycastGettedTypes.UI))
                {
                }

                else if (SelectorC.Is(RaycastGettedTypes.Cell))
                {
                    if (SelectorC.IsSelUnit)
                    {
                        if (!UnitDatCom(SelectorC.IdxCurCell).HaveUnit || !VisUnitCom(SelectorC.IdxCurCell).IsVisibled(WhoseMoveC.CurPlayerI))
                        {
                            if (SelectorC.IsStartDirectToCell)
                            {
                                SelectorC.IdxPreVisionCell = SelectorC.IdxCurCell;
                                SelectorC.IdxCurCell = default;
                            }
                            else
                            {
                                SelectorC.IdxPreVisionCell = SelectorC.IdxCurCell;
                            }
                        }
                    }
                }
            }
        }
    }
}