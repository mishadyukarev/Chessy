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

            var unit_cur = Es.UnitEs(idx_cur).UnitE.UnitTC;
            var ownUnit_cur = Es.UnitE(idx_cur).OwnerC;

            ref var raycastTC = ref Es.ClickerObjectE.RayCastTC;
            ref var cellClick = ref Es.ClickerObjectE.CellClickCRef;

            if (Es.InputE.IsClickedC.IsClicked)
            {
                if (raycastTC.Is(RaycastTypes.Cell))
                {
                    if (!Es.WhoseMoveE.IsMyMove)
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
                                        if (CellsForAttackUnitsEs.CanAttack(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx, Es.WhoseMoveE.CurPlayerI, out var attack))
                                        {
                                            Es.RpcE.AttackUnitToMaster(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                        }

                                        else if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(Es.WhoseMoveE.CurPlayerI, Es.SelectedIdxE.IdxC.Idx).Contains(Es.CurrentIdxE.IdxC.Idx))
                                        {
                                            Es.RpcE.ShiftUnitToMaster(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                        }

                                        else
                                        {
                                            if (UnitEs(idx_cur).UnitE.HaveUnit)
                                            {
                                                if (ownUnit_cur.Is(Es.WhoseMoveE.CurPlayerI))
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
                                        if (UnitEs(idx_cur).UnitE.HaveUnit)
                                        {
                                            if (ownUnit_cur.Is(Es.WhoseMoveE.CurPlayerI))
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
                                    Es.RpcE.SetUniToMaster(Es.CurrentIdxE.IdxC.Idx, Es.SelectedUnitE.UnitTC.Unit);
                                    cellClick.Click = CellClickTypes.SimpleClick;
                                }
                                break;

                            case CellClickTypes.GiveTakeTW:
                                {
                                    if (unit_cur.Is(UnitTypes.Pawn) && ownUnit_cur.Is(Es.WhoseMoveE.CurPlayerI))
                                    {
                                        Es.RpcE.GiveTakeToolWeaponToMaster(Es.CurrentIdxE.IdxC.Idx, Es.SelectedToolWeaponE.ToolWeaponT, Es.SelectedToolWeaponE.LevelT);
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
                                    if (unit_cur.Is(UnitTypes.Pawn)
                                        && ownUnit_cur.Is(Es.WhoseMoveE.CurPlayerI)
                                        && !Es.UnitE(idx_cur).Is(LevelTypes.Second))
                                    {
                                        Es.RpcE.UpgradeUnitToMaster(Es.CurrentIdxE.IdxC.Idx);
                                    }
                                    else
                                    {
                                        cellClick.Click = CellClickTypes.SimpleClick;
                                        Es.SelectedIdxE.IdxC.Idx = Es.CurrentIdxE.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.UniqueAbility:
                                {
                                    switch (Es.SelectedAbilityE.AbilityTC.Ability)
                                    {
                                        case AbilityTypes.FireArcher:
                                            Es.RpcE.FireArcherToMas(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                            break;

                                        case AbilityTypes.StunElfemale:
                                            Es.RpcE.StunElfemaleToMas(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                            break;

                                        case AbilityTypes.ChangeDirectionWind:
                                            {
                                                CellWorker.TryGetIdxAround(Es.WindCloudE.CenterCloud.Idx, out var directs);

                                                foreach (var item in directs)
                                                {
                                                    if (item.Value == Es.CurrentIdxE.IdxC.Idx)
                                                    {
                                                        Es.RpcE.PutOutFireElffToMas(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                                    }
                                                }
                                            }
                                            break;

                                        case AbilityTypes.DirectWave:
                                            Es.RpcE.DirectWaveToMaster(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                            break;

                                        case AbilityTypes.Resurrect:
                                            Es.RpcE.ResurrectToMaster(Es.SelectedIdxE.IdxC.Idx, Es.CurrentIdxE.IdxC.Idx);
                                            break;

                                        default: throw new Exception();
                                    }

                                    cellClick.Click = CellClickTypes.SimpleClick;
                                    Es.SelectedIdxE.IdxC.Idx = Es.CurrentIdxE.IdxC.Idx;
                                }
                                break;

                            case CellClickTypes.CityBuildBuilding:
                                {
                                    Es.RpcE.CityBuildToMaster(Es.SelectedBuildingE.BuildT, Es.SelectedIdxE.Idx, Es.CurrentIdxE.Idx);
                                    cellClick.Click = CellClickTypes.SimpleClick;
                                }
                                break;

                            default: throw new Exception();
                        }
                    }
                }

                else if (raycastTC.Is(RaycastTypes.UI))
                {
                    //cellClick.Click = CellClickTypes.SimpleClick;
                    //Es.SelectedIdxE.Reset();
                }

                else if (raycastTC.Is(RaycastTypes.Background))
                {
                    cellClick.Click = CellClickTypes.SimpleClick;
                    Es.SelectedIdxE.Reset();
                }
            }

            else
            {
                if (raycastTC.Is(RaycastTypes.Cell))
                {
                    if (cellClick.Is(CellClickTypes.SetUnit))
                    {
                        if (!UnitEs(idx_cur).UnitE.HaveUnit || !UnitEs(idx_cur).VisibleE(Es.WhoseMoveE.CurPlayerI).IsVisibleC.IsVisible)
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