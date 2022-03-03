using System;
using UnityEngine;

namespace Chessy.Game
{
    sealed class SelectorS : SystemAbstract, IEcsRunSystem
    {
        readonly Action _updateView;
        readonly Action _updateUI;

        public SelectorS(in EntitiesModel ents, in Action updateView, in Action updateUI) : base(ents)
        {
            _updateView = updateView;
            _updateUI = updateUI;
        }

        public void Run()
        {
            var idx_cur = E.CurrentIdxC.Idx;


            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.Alpha1)) E.PlayerE(E.CurPlayerITC.Player).ResourcesC(ResourceTypes.Food).Resources += 0.5f;
                if (Input.GetKey(KeyCode.Alpha2)) E.PlayerE(E.CurPlayerITC.Player).ResourcesC(ResourceTypes.Wood).Resources += 0.5f;
                if (Input.GetKey(KeyCode.Alpha3)) E.PlayerE(E.CurPlayerITC.Player).ResourcesC(ResourceTypes.Ore).Resources += 0.5f;
                if (Input.GetKey(KeyCode.Alpha4)) E.PlayerE(E.CurPlayerITC.Player).ResourcesC(ResourceTypes.Iron).Resources += 1;
                if (Input.GetKey(KeyCode.Alpha5)) E.PlayerE(E.CurPlayerITC.Player).ResourcesC(ResourceTypes.Gold).Resources += 1;
            }




            if (E.IsClicked)
            {
                if (E.RayCastTC.Is(RaycastTypes.Cell))
                {
                    if (!E.CurPlayerITC.Is(E.WhoseMove.Player))
                    {
                        E.SelectedIdxC.Idx = E.CurrentIdxC.Idx;
                    }

                    else
                    {
                        switch (E.CellClickTC.Click)
                        {
                            case CellClickTypes.None: throw new Exception();

                            case CellClickTypes.SimpleClick:
                                {
                                    if (E.SelectedIdxC.Idx > 0)
                                    {
                                        var curPlayerI = E.CurPlayerITC.Player;

                                        if (E.UnitEs(E.SelectedIdxC.Idx).ForAttack(AttackTypes.Simple).Contains(E.CurrentIdxC.Idx)
                                            || E.UnitEs(E.SelectedIdxC.Idx).ForAttack(AttackTypes.Unique).Contains(E.CurrentIdxC.Idx))
                                        {
                                            E.RpcPoolEs.AttackUnitToMaster(E.SelectedIdxC.Idx, E.CurrentIdxC.Idx);
                                        }

                                        else if (E.UnitEs(E.SelectedIdxC.Idx).ForShift.Contains(E.CurrentIdxC.Idx))
                                        {
                                            E.RpcPoolEs.ShiftUnitToMaster(E.SelectedIdxC.Idx, E.CurrentIdxC.Idx);
                                        }

                                        else
                                        {
                                            if (E.UnitTC(idx_cur).HaveUnit)
                                            {
                                                if (E.UnitPlayerTC(idx_cur).Is(E.CurPlayerITC.Player))
                                                {
                                                    if (E.UnitTC(idx_cur).Is(UnitTypes.Scout))
                                                    {

                                                    }
                                                    else if (E.UnitMainE(idx_cur).IsMelee)
                                                    {
                                                        E.Sound(ClipTypes.PickMelee).Invoke();
                                                    }
                                                    else
                                                    {
                                                        E.Sound(ClipTypes.PickArcher).Invoke();
                                                    }
                                                }
                                            }
                                        }

                                        E.SelectedIdxC.Idx = E.CurrentIdxC.Idx;
                                    }

                                    else
                                    {
                                        if (E.UnitTC(idx_cur).HaveUnit)
                                        {
                                            if (E.UnitPlayerTC(idx_cur).Is(E.CurPlayerITC.Player))
                                            {
                                                if (E.UnitTC(idx_cur).Is(UnitTypes.Scout))
                                                {

                                                }
                                                else if (E.UnitMainE(idx_cur).IsMelee)
                                                {
                                                    E.Sound(ClipTypes.PickMelee).Invoke();
                                                }
                                                else
                                                {
                                                    E.Sound(ClipTypes.PickArcher).Invoke();
                                                }
                                            }
                                        }

                                        E.SelectedIdxC.Idx = E.CurrentIdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.SetUnit:
                                {
                                    E.RpcPoolEs.SetUniToMaster(E.CurrentIdxC.Idx, E.SelectedUnitE.UnitTC.Unit);
                                    E.CellClickTC.Click = CellClickTypes.SimpleClick;
                                }
                                break;

                            case CellClickTypes.GiveTakeTW:
                                {
                                    if (E.UnitTC(idx_cur).Is(UnitTypes.Pawn) && E.UnitPlayerTC(idx_cur).Is(E.CurPlayerITC.Player))
                                    {
                                        E.RpcPoolEs.GiveTakeToolWeaponToMaster(E.CurrentIdxC.Idx, E.SelectedTWE.ToolWeaponTC.ToolWeapon, E.SelectedTWE.LevelTC.Level);
                                    }
                                    else
                                    {
                                        E.CellClickTC.Click = CellClickTypes.SimpleClick;
                                        E.SelectedIdxC.Idx= E.CurrentIdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.UniqueAbility:
                                {
                                    switch (E.SelectedAbilityTC.Ability)
                                    {
                                        case AbilityTypes.FireArcher:
                                            E.RpcPoolEs.FireArcherToMas(E.SelectedIdxC.Idx, E.CurrentIdxC.Idx);
                                            break;

                                        case AbilityTypes.StunElfemale:
                                            E.RpcPoolEs.StunElfemaleToMas(E.SelectedIdxC.Idx, E.CurrentIdxC.Idx);
                                            break;

                                        case AbilityTypes.ChangeDirectionWind:
                                            {
                                                foreach (var cellE in E.CellEs(E.CenterCloudIdxC.Idx).AroundCellEs)
                                                {
                                                    if (cellE.IdxC.Idx == E.CurrentIdxC.Idx)
                                                    {
                                                        E.RpcPoolEs.PutOutFireElffToMas(E.SelectedIdxC.Idx, E.CurrentIdxC.Idx);
                                                    }
                                                }
                                            }
                                            break;

                                        case AbilityTypes.DirectWave:
                                            E.RpcPoolEs.DirectWaveToMaster(E.SelectedIdxC.Idx, E.CurrentIdxC.Idx);
                                            break;

                                        case AbilityTypes.Resurrect:
                                            E.RpcPoolEs.ResurrectToMaster(E.SelectedIdxC.Idx, E.CurrentIdxC.Idx);
                                            break;

                                        default: throw new Exception();
                                    }

                                    E.CellClickTC.Click = CellClickTypes.SimpleClick;
                                    E.SelectedIdxC.Idx = E.CurrentIdxC.Idx;
                                }
                                break;

                            default: throw new Exception();
                        }
                    }
                }

                else if (E.RayCastTC.Is(RaycastTypes.UI))
                {
                    //cellClick.Click = CellClickTypes.SimpleClick;
                    //Es.SelectedIdxE.Reset();
                }

                else if (E.RayCastTC.Is(RaycastTypes.Background))
                {
                    E.CellClickTC.Click = CellClickTypes.SimpleClick;
                    E.SelectedIdxC.Idx = 0;
                }

                _updateView.Invoke();
                _updateUI.Invoke();
            }

            else
            {
                if (E.RayCastTC.Is(RaycastTypes.Cell))
                {
                    if (E.CellClickTC.Is(CellClickTypes.SetUnit))
                    {
                        if (!E.UnitTC(idx_cur).HaveUnit || !E.UnitEs(idx_cur).ForPlayer(E.CurPlayerITC.Player).IsVisible)
                        {
                            if (E.CurrentIdxC.Idx == 0)
                            {
                                E.PreviousVisionIdxC = E.CurrentIdxC;
                            }
                            else
                            {
                                E.PreviousVisionIdxC = E.CurrentIdxC;
                            }
                        }
                    }
                }
            }
        }
    }
}