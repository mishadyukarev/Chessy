using System;

namespace Game.Game
{
    sealed class SelectorS : SystemCellAbstract, IEcsRunSystem
    {
        readonly SystemsView _systemV;

        public SelectorS(in Entities ents, in SystemsView systemV) : base(ents)
        {
            _systemV = systemV;
        }

        public void Run()
        {
            var idx_cur = Es.CurrentIdxE.IdxC.Idx;
            var idx_sel = Es.SelectedIdxE.IdxC.Idx;

            var unit_cur = UnitEs(idx_cur).MainE.UnitTC;
            var ownUnit_cur = UnitEs(idx_cur).OwnerE.OwnerC;

            var unit_sel = UnitEs(idx_cur).MainE.UnitTC;

            ref var raycastTC = ref Es.ClickerObject.RayCastTC;
            ref var cellClick = ref Es.ClickerObject.CellClickC;

            if (Es.InputE.IsClickedC.IsClicked)
            {
                if (raycastTC.Is(RaycastTypes.Cell))
                {
                    if (!Es.WhoseMove.IsMyMove)
                    {
                        Es.SelectedIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;
                    }

                    else
                    {
                        switch (cellClick.Click)
                        {
                            case CellClickTypes.None: throw new Exception();

                            case CellClickTypes.SimpleClick:
                                {
                                    if (Es.SelectedIdxE.IsSelCell)
                                    {
                                        if (CellsForAttackUnitsEs.CanAttack(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx, Es.WhoseMove.CurPlayerI, out var attack))
                                        {
                                            Es.Rpc.AttackUnitToMaster(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                        }

                                        else if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(Es.WhoseMove.CurPlayerI, Es.SelectedIdxE.IdxC.Idx).Contains(Es.CurrentIdxE.IdxC.Idx))
                                        {
                                            Es.Rpc.ShiftUnitToMaster(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                        }

                                        else
                                        {
                                            if (UnitEs(idx_cur).MainE.HaveUnit)
                                            {
                                                if (ownUnit_cur.Is(Es.WhoseMove.CurPlayerI))
                                                {
                                                    if (unit_cur.Is(UnitTypes.Scout))
                                                    {

                                                    }
                                                    else if (unit_cur.IsMelee)
                                                    {
                                                        Es.Sound(ClipTypes.PickMelee).Sound.Invoke();
                                                    }
                                                    else
                                                    {
                                                        Es.Sound(ClipTypes.PickArcher).Sound.Invoke();
                                                    }
                                                }
                                            }
                                        }

                                        Es.SelectedIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;
                                    }

                                    else
                                    {
                                        if (UnitEs(idx_cur).MainE.HaveUnit)
                                        {
                                            if (ownUnit_cur.Is(Es.WhoseMove.CurPlayerI))
                                            {
                                                if (unit_cur.Is(UnitTypes.Scout))
                                                {

                                                }
                                                else if (unit_cur.IsMelee)
                                                {
                                                    Es.Sound(ClipTypes.PickMelee).Sound.Invoke();
                                                }
                                                else
                                                {
                                                    Es.Sound(ClipTypes.PickArcher).Sound.Invoke();
                                                }
                                            }
                                        }

                                        Es.SelectedIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.SetUnit:
                                {
                                    Es.Rpc.SetUniToMaster(Es.CurrentIdxE.IdxC.Idx, Es.SelectedUnitE.UnitTC.Unit);
                                    cellClick.Click = CellClickTypes.SimpleClick;
                                }
                                break;

                            case CellClickTypes.GiveTakeTW:
                                {
                                    if (unit_cur.Is(UnitTypes.Pawn) && ownUnit_cur.Is(Es.WhoseMove.CurPlayerI))
                                    {
                                        Es.Rpc.GiveTakeToolWeaponToMaster(Es.CurrentIdxE.IdxC.Idx, Es.SelectedToolWeaponE.ToolWeaponTC.ToolWeapon, Es.SelectedToolWeaponE.LevelTC.Level);
                                    }
                                    else
                                    {
                                        cellClick.Click = CellClickTypes.SimpleClick;
                                        Es.SelectedIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.UpgradeUnit:
                                {
                                    if (unit_cur.Is(new[] { UnitTypes.Pawn, UnitTypes.Archer })
                                        && ownUnit_cur.Is(Es.WhoseMove.CurPlayerI)
                                        && !UnitEs(idx_cur).LevelE.LevelTC.Is(LevelTypes.Second))
                                    {
                                        Es.Rpc.UpgradeUnitToMaster(Es.CurrentIdxE.IdxC.Idx);
                                    }
                                    else
                                    {
                                        cellClick.Click = CellClickTypes.SimpleClick;
                                        Es.SelectedIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.UniqueAbility:
                                {
                                    switch (Es.SelectedUniqueAbilityE.AbilityTC.Ability)
                                    {
                                        case AbilityTypes.FireArcher:
                                            Es.Rpc.FireArcherToMas(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                            break;

                                        case AbilityTypes.StunElfemale:
                                            Es.Rpc.StunElfemaleToMas(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                            break;

                                        case AbilityTypes.ChangeDirectionWind:
                                            {
                                                CellWorker.TryGetIdxAround(Es.WindE.CenterCloud.Idx, out var directs);

                                                foreach (var item in directs)
                                                {
                                                    if (item.Value == Es.CurrentIdxE.IdxC.Idx)
                                                    {
                                                        Es.Rpc.PutOutFireElffToMas(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                                    }
                                                }
                                            }
                                            break;

                                        case AbilityTypes.DirectWave:
                                            Es.Rpc.DirectWaveToMaster(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                            break;

                                        case AbilityTypes.Resurrect:
                                            Es.Rpc.ResurrectToMaster(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                            break;

                                        default: throw new Exception();
                                    }

                                    cellClick.Click = CellClickTypes.SimpleClick;
                                    Es.SelectedIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;
                                }
                                break;

                            default: throw new Exception();
                        }
                    }
                }

                else if (raycastTC.Is(RaycastTypes.UI))
                {
                    cellClick.Click = CellClickTypes.SimpleClick;
                }

                else if (raycastTC.Is(RaycastTypes.Background))
                {
                    cellClick.Click = CellClickTypes.SimpleClick;
                }
            }

            else
            {
                if (raycastTC.Is(RaycastTypes.Cell))
                {
                    if (cellClick.Is(CellClickTypes.SetUnit))
                    {
                        if (!UnitEs(idx_cur).MainE.HaveUnit || !UnitEs(idx_cur).VisibleE(Es.WhoseMove.CurPlayerI).IsVisibleC.IsVisible)
                        {
                            if (Es.CurrentIdxE.IsStartDirectToCell)
                            {
                                Es.PreviousVisionIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;
                            }
                            else
                            {
                                Es.PreviousVisionIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;
                            }
                        }
                    }
                }
            }
        }
    }
}