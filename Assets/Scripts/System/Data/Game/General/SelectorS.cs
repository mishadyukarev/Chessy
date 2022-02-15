﻿using System;

namespace Game.Game
{
    sealed class SelectorS : SystemAbstract, IEcsRunSystem
    {
        readonly SystemsView _systemV;

        public SelectorS(in Entities ents, in SystemsView systemV) : base(ents)
        {
            _systemV = systemV;
        }

        public void Run()
        {
            var idx_cur = Es.CurrentIdxC.Idx;

            ref var raycastTC = ref Es.RayCastTC;
            ref var cellClick = ref Es.CellClickTC;

            if (Es.IsClickedC.IsClicked)
            {
                if (raycastTC.Is(RaycastTypes.Cell))
                {
                    if (!Es.WhoseMovePlayerTC.IsMyMove)
                    {
                        Es.SelectedIdxC.Set(Es.CurrentIdxC.Idx);
                    }

                    else
                    {
                        switch (cellClick.Click)
                        {
                            case CellClickTypes.None: throw new Exception();

                            case CellClickTypes.SimpleClick:
                                {
                                    if (Es.SelectedIdxC.IsSelectedCell)
                                    {
                                        if (CellsForAttackUnitsEs.CanAttack(Es.SelectedIdxC.Idx, Es.CurrentIdxC.Idx, Es.WhoseMovePlayerTC.CurPlayerI, out var attack))
                                        {
                                            Es.RpcE.AttackUnitToMaster(Es.SelectedIdxC.Idx, Es.CurrentIdxC.Idx);
                                        }

                                        else if (CellsForShiftUnitsEs.CellsForShift<IdxsC>(Es.WhoseMovePlayerTC.CurPlayerI, Es.SelectedIdxC.Idx).Contains(Es.CurrentIdxC.Idx))
                                        {
                                            Es.RpcE.ShiftUnitToMaster(Es.SelectedIdxC.Idx, Es.CurrentIdxC.Idx);
                                        }

                                        else
                                        {
                                            if (Es.UnitTC(idx_cur).HaveUnit)
                                            {
                                                if (Es.UnitPlayerTC(idx_cur).Is(Es.WhoseMovePlayerTC.CurPlayerI))
                                                {
                                                    if (Es.UnitTC(idx_cur).Is(UnitTypes.Scout))
                                                    {

                                                    }
                                                    else if (Es.UnitTC(idx_cur).IsMelee && Es.MainTWE(idx_cur).ToolWeaponTC.IsMelee)
                                                    {
                                                        Es.Sound(ClipTypes.PickMelee).ActionC.Invoke();
                                                    }
                                                    else
                                                    {
                                                        Es.Sound(ClipTypes.PickArcher).ActionC.Invoke();
                                                    }
                                                }
                                            }
                                        }

                                        Es.SelectedIdxC.Set(Es.CurrentIdxC.Idx);
                                    }

                                    else
                                    {
                                        if (Es.UnitTC(idx_cur).HaveUnit)
                                        {
                                            if (Es.UnitPlayerTC(idx_cur).Is(Es.WhoseMovePlayerTC.CurPlayerI))
                                            {
                                                if (Es.UnitTC(idx_cur).Is(UnitTypes.Scout))
                                                {

                                                }
                                                else if (Es.UnitTC(idx_cur).IsMelee && Es.MainTWE(idx_cur).ToolWeaponTC.IsMelee)
                                                {
                                                    Es.Sound(ClipTypes.PickMelee).ActionC.Invoke();
                                                }
                                                else
                                                {
                                                    Es.Sound(ClipTypes.PickArcher).ActionC.Invoke();
                                                }
                                            }
                                        }

                                        Es.SelectedIdxC.Set(Es.CurrentIdxC.Idx);
                                    }
                                }
                                break;

                            case CellClickTypes.SetUnit:
                                {
                                    Es.RpcE.SetUniToMaster(Es.CurrentIdxC.Idx, Es.SelUnitTC.Unit);
                                    cellClick.Click = CellClickTypes.SimpleClick;
                                }
                                break;

                            case CellClickTypes.GiveTakeTW:
                                {
                                    if (Es.UnitTC(idx_cur).Is(UnitTypes.Pawn) && Es.UnitPlayerTC(idx_cur).Is(Es.WhoseMovePlayerTC.CurPlayerI))
                                    {
                                        Es.RpcE.GiveTakeToolWeaponToMaster(Es.CurrentIdxC.Idx, Es.SelectedTWTC.ToolWeapon, Es.SelectedTWLevelTC.Level);
                                    }
                                    else
                                    {
                                        cellClick.Click = CellClickTypes.SimpleClick;
                                        Es.SelectedIdxC.Set(Es.CurrentIdxC.Idx);
                                    }
                                }
                                break;

                            case CellClickTypes.UniqueAbility:
                                {
                                    switch (Es.SelAbilityTC.Ability)
                                    {
                                        case AbilityTypes.FireArcher:
                                            Es.RpcE.FireArcherToMas(Es.SelectedIdxC.Idx, Es.CurrentIdxC.Idx);
                                            break;

                                        case AbilityTypes.StunElfemale:
                                            Es.RpcE.StunElfemaleToMas(Es.SelectedIdxC.Idx, Es.CurrentIdxC.Idx);
                                            break;

                                        case AbilityTypes.ChangeDirectionWind:
                                            {
                                                CellWorker.TryGetIdxAround(Es.CenterCloudIdxC.Idx, out var directs);

                                                foreach (var item in directs)
                                                {
                                                    if (item.Value == Es.CurrentIdxC.Idx)
                                                    {
                                                        Es.RpcE.PutOutFireElffToMas(Es.SelectedIdxC.Idx, Es.CurrentIdxC.Idx);
                                                    }
                                                }
                                            }
                                            break;

                                        case AbilityTypes.DirectWave:
                                            Es.RpcE.DirectWaveToMaster(Es.SelectedIdxC.Idx, Es.CurrentIdxC.Idx);
                                            break;

                                        case AbilityTypes.Resurrect:
                                            Es.RpcE.ResurrectToMaster(Es.SelectedIdxC.Idx, Es.CurrentIdxC.Idx);
                                            break;

                                        default: throw new Exception();
                                    }

                                    cellClick.Click = CellClickTypes.SimpleClick;
                                    Es.SelectedIdxC.Set(Es.CurrentIdxC.Idx);
                                }
                                break;

                            case CellClickTypes.CityBuildBuilding:
                                {
                                    Es.RpcE.CityBuildToMaster(Es.SelectedBuildingTC.Build, Es.SelectedIdxC.Idx, Es.CurrentIdxC.Idx);
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
                    Es.SelectedIdxC.Reset();
                }
            }

            else
            {
                if (raycastTC.Is(RaycastTypes.Cell))
                {
                    if (cellClick.Is(CellClickTypes.SetUnit))
                    {
                        if (!Es.UnitTC(idx_cur).HaveUnit || !Es.UnitEs(idx_cur).VisibleE(Es.WhoseMovePlayerTC.CurPlayerI).IsVisible)
                        {
                            if (Es.CurrentIdxC.IsStartDirectToCell)
                            {
                                Es.PreviousVisionIdxC.Set(Es.CurrentIdxC.Idx);
                            }
                            else
                            {
                                Es.PreviousVisionIdxC.Set(Es.CurrentIdxC.Idx);
                            }
                        }
                    }
                }
            }
        }
    }
}