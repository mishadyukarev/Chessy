using System;
using UnityEngine;

namespace Game.Game
{
    sealed class SelectorS : SystemAbstract, IEcsRunSystem
    {
        readonly Action _updateView;
        readonly Action _updateUI;

        public SelectorS(in Entities ents, in Action updateView, in Action updateUI) : base(ents)
        {
            _updateView = updateView;
            _updateUI = updateUI;
        }

        public void Run()
        {
            var idx_cur = Es.CurrentIdxC.Idx;


            if (Input.GetKey(KeyCode.Space))
            {
                if (Input.GetKeyDown(KeyCode.Alpha1)) Es.PlayerE(Es.CurPlayerI.Player).ResourcesC(ResourceTypes.Food).Resources += 0.5f;
                if (Input.GetKeyDown(KeyCode.Alpha2)) Es.PlayerE(Es.CurPlayerI.Player).ResourcesC(ResourceTypes.Wood).Resources += 0.5f;
                if (Input.GetKeyDown(KeyCode.Alpha3)) Es.PlayerE(Es.CurPlayerI.Player).ResourcesC(ResourceTypes.Ore).Resources += 0.5f;
                if (Input.GetKeyDown(KeyCode.Alpha4)) Es.PlayerE(Es.CurPlayerI.Player).ResourcesC(ResourceTypes.Iron).Resources += 1;
                if (Input.GetKeyDown(KeyCode.Alpha5)) Es.PlayerE(Es.CurPlayerI.Player).ResourcesC(ResourceTypes.Gold).Resources += 1;
            }

            if (Es.IsClicked)
            {
                if (Es.RayCastTC.Is(RaycastTypes.Cell))
                {
                    if (!Es.IsMyMove)
                    {
                        Es.SelectedIdxC.Idx = Es.CurrentIdxC.Idx;
                    }

                    else
                    {
                        switch (Es.CellClickTC.Click)
                        {
                            case CellClickTypes.None: throw new Exception();

                            case CellClickTypes.SimpleClick:
                                {
                                    if (Es.SelectedIdxC.Idx > 0)
                                    {
                                        var curPlayerI = Es.CurPlayerI.Player;

                                        if (Es.UnitEs(Es.SelectedIdxC.Idx).ForAttack(AttackTypes.Simple).Contains(Es.CurrentIdxC.Idx)
                                            || Es.UnitEs(Es.SelectedIdxC.Idx).ForAttack(AttackTypes.Unique).Contains(Es.CurrentIdxC.Idx))
                                        {
                                            Es.RpcE.AttackUnitToMaster(Es.SelectedIdxC.Idx, Es.CurrentIdxC.Idx);
                                        }

                                        else if (Es.UnitEs(Es.SelectedIdxC.Idx).ForShift.Contains(Es.CurrentIdxC.Idx))
                                        {
                                            Es.RpcE.ShiftUnitToMaster(Es.SelectedIdxC.Idx, Es.CurrentIdxC.Idx);
                                        }

                                        else
                                        {
                                            if (Es.UnitTC(idx_cur).HaveUnit)
                                            {
                                                if (Es.UnitPlayerTC(idx_cur).Is(Es.CurPlayerI.Player))
                                                {
                                                    if (Es.UnitTC(idx_cur).Is(UnitTypes.Scout))
                                                    {

                                                    }
                                                    else if (Es.UnitEs(idx_cur).IsMelee)
                                                    {
                                                        Es.Sound(ClipTypes.PickMelee).Invoke();
                                                    }
                                                    else
                                                    {
                                                        Es.Sound(ClipTypes.PickArcher).Invoke();
                                                    }
                                                }
                                            }
                                        }

                                        Es.SelectedIdxC.Idx = Es.CurrentIdxC.Idx;
                                    }

                                    else
                                    {
                                        if (Es.UnitTC(idx_cur).HaveUnit)
                                        {
                                            if (Es.UnitPlayerTC(idx_cur).Is(Es.CurPlayerI.Player))
                                            {
                                                if (Es.UnitTC(idx_cur).Is(UnitTypes.Scout))
                                                {

                                                }
                                                else if (Es.UnitEs(idx_cur).IsMelee)
                                                {
                                                    Es.Sound(ClipTypes.PickMelee).Invoke();
                                                }
                                                else
                                                {
                                                    Es.Sound(ClipTypes.PickArcher).Invoke();
                                                }
                                            }
                                        }

                                        Es.SelectedIdxC.Idx = Es.CurrentIdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.SetUnit:
                                {
                                    Es.RpcE.SetUniToMaster(Es.CurrentIdxC.Idx, Es.SelectedUnitE.UnitTC.Unit);
                                    Es.CellClickTC.Click = CellClickTypes.SimpleClick;
                                }
                                break;

                            case CellClickTypes.GiveTakeTW:
                                {
                                    if (Es.UnitTC(idx_cur).Is(UnitTypes.Pawn) && Es.UnitPlayerTC(idx_cur).Is(Es.CurPlayerI.Player))
                                    {
                                        Es.RpcE.GiveTakeToolWeaponToMaster(Es.CurrentIdxC.Idx, Es.SelectedTWE.ToolWeaponTC.ToolWeapon, Es.SelectedTWE.LevelTC.Level);
                                    }
                                    else
                                    {
                                        Es.CellClickTC.Click = CellClickTypes.SimpleClick;
                                        Es.SelectedIdxC.Idx= Es.CurrentIdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.UniqueAbility:
                                {
                                    switch (Es.SelectedAbilityTC.Ability)
                                    {
                                        case AbilityTypes.FireArcher:
                                            Es.RpcE.FireArcherToMas(Es.SelectedIdxC.Idx, Es.CurrentIdxC.Idx);
                                            break;

                                        case AbilityTypes.StunElfemale:
                                            Es.RpcE.StunElfemaleToMas(Es.SelectedIdxC.Idx, Es.CurrentIdxC.Idx);
                                            break;

                                        case AbilityTypes.ChangeDirectionWind:
                                            {
                                                foreach (var cellE in Es.CellEs(Es.CenterCloudIdxC.Idx).AroundCellEs)
                                                {
                                                    if (cellE.IdxC.Idx == Es.CurrentIdxC.Idx)
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

                                    Es.CellClickTC.Click = CellClickTypes.SimpleClick;
                                    Es.SelectedIdxC.Idx = Es.CurrentIdxC.Idx;
                                }
                                break;

                            case CellClickTypes.CityBuildBuilding:
                                {
                                    Es.RpcE.CityBuildToMaster(Es.SelectedBuildingTC.Build, Es.SelectedIdxC.Idx, Es.CurrentIdxC.Idx);
                                    Es.CellClickTC.Click = CellClickTypes.SimpleClick;
                                }
                                break;

                            default: throw new Exception();
                        }
                    }
                }

                else if (Es.RayCastTC.Is(RaycastTypes.UI))
                {
                    //cellClick.Click = CellClickTypes.SimpleClick;
                    //Es.SelectedIdxE.Reset();
                }

                else if (Es.RayCastTC.Is(RaycastTypes.Background))
                {
                    Es.CellClickTC.Click = CellClickTypes.SimpleClick;
                    Es.SelectedIdxC.Idx = 0;
                }

                _updateView.Invoke();
                _updateUI.Invoke();
            }

            else
            {
                if (Es.RayCastTC.Is(RaycastTypes.Cell))
                {
                    if (Es.CellClickTC.Is(CellClickTypes.SetUnit))
                    {
                        if (!Es.UnitTC(idx_cur).HaveUnit || !Es.UnitEs(idx_cur).ForPlayer(Es.CurPlayerI.Player).IsVisibleC)
                        {
                            if (Es.CurrentIdxC.Idx == 0)
                            {
                                Es.PreviousVisionIdxC = Es.CurrentIdxC;
                            }
                            else
                            {
                                Es.PreviousVisionIdxC = Es.CurrentIdxC;
                            }
                        }
                    }
                }
            }
        }
    }
}