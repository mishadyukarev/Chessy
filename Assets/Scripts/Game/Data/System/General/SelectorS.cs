using System;
using UnityEngine;

namespace Chessy.Game
{
    public struct SelectorS
    {
        public SelectorS(in RaycastTypes raycastT, ref EntitiesModel e)
        {
            var idx_cur = e.CurrentIdxC.Idx;


            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.Alpha1)) e.PlayerInfoE(e.CurPlayerITC.Player).ResourcesC(ResourceTypes.Food).Resources += 0.5f;
                if (Input.GetKey(KeyCode.Alpha2)) e.PlayerInfoE(e.CurPlayerITC.Player).ResourcesC(ResourceTypes.Wood).Resources += 0.5f;
                if (Input.GetKey(KeyCode.Alpha3)) e.PlayerInfoE(e.CurPlayerITC.Player).ResourcesC(ResourceTypes.Ore).Resources += 0.5f;
                if (Input.GetKey(KeyCode.Alpha4)) e.PlayerInfoE(e.CurPlayerITC.Player).ResourcesC(ResourceTypes.Iron).Resources += 1;
                if (Input.GetKey(KeyCode.Alpha5)) e.PlayerInfoE(e.CurPlayerITC.Player).ResourcesC(ResourceTypes.Gold).Resources += 1;
            }




            if (Input.GetMouseButtonDown(0))
            {
                if (raycastT == RaycastTypes.Cell)
                {
                    e.IsSelectedCity = false;

                    if (!e.CurPlayerITC.Is(e.WhoseMove.Player))
                    {
                        e.SelectedIdxC.Idx = e.CurrentIdxC.Idx;
                    }

                    else
                    {
                        switch (e.CellClickTC.Click)
                        {
                            case CellClickTypes.None: throw new Exception();

                            case CellClickTypes.SimpleClick:
                                {
                                    if (e.SelectedIdxC.Idx > 0)
                                    {
                                        var curPlayerI = e.CurPlayerITC.Player;

                                        if (e.UnitTC(e.SelectedIdxC.Idx).HaveUnit)
                                        {
                                            if (e.UnitEs(e.SelectedIdxC.Idx).ForAttack(AttackTypes.Simple).Contains(e.CurrentIdxC.Idx)
                                            || e.UnitEs(e.SelectedIdxC.Idx).ForAttack(AttackTypes.Unique).Contains(e.CurrentIdxC.Idx))
                                            {
                                                e.RpcPoolEs.AttackUnitToMaster(e.SelectedIdxC.Idx, e.CurrentIdxC.Idx);
                                            }

                                            else if (e.UnitEs(e.SelectedIdxC.Idx).ForShift.Contains(e.CurrentIdxC.Idx))
                                            {
                                                e.RpcPoolEs.ShiftUnitToMaster(e.SelectedIdxC.Idx, e.CurrentIdxC.Idx);
                                            }

                                            else
                                            {
                                                if (e.UnitTC(idx_cur).HaveUnit)
                                                {
                                                    if (e.UnitPlayerTC(idx_cur).Is(e.CurPlayerITC.Player))
                                                    {
                                                        if (e.IsMelee(idx_cur))
                                                        {
                                                            e.Sound(ClipTypes.PickMelee).Invoke();
                                                        }
                                                        else
                                                        {
                                                            e.Sound(ClipTypes.PickArcher).Invoke();
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                        else
                                        {
                                            if (e.UnitTC(idx_cur).HaveUnit)
                                            {
                                                if (e.UnitPlayerTC(idx_cur).Is(e.CurPlayerITC.Player))
                                                {
                                                    if (e.IsMelee(idx_cur))
                                                    {
                                                        e.Sound(ClipTypes.PickMelee).Invoke();
                                                    }
                                                    else
                                                    {
                                                        e.Sound(ClipTypes.PickArcher).Invoke();
                                                    }
                                                }
                                            }
                                        }


                                        e.PreviousSelectedIdxC = e.SelectedIdxC;
                                        e.SelectedIdxC = e.CurrentIdxC;
                                    }

                                    else
                                    {
                                        if (e.UnitTC(idx_cur).HaveUnit)
                                        {
                                            if (e.UnitPlayerTC(idx_cur).Is(e.CurPlayerITC.Player))
                                            {
                                                if (e.IsMelee(idx_cur))
                                                {
                                                    e.Sound(ClipTypes.PickMelee).Invoke();
                                                }
                                                else
                                                {
                                                    e.Sound(ClipTypes.PickArcher).Invoke();
                                                }
                                            }
                                        }

                                        e.PreviousSelectedIdxC = e.SelectedIdxC;
                                        e.SelectedIdxC.Idx = e.CurrentIdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.SetUnit:
                                {
                                    e.RpcPoolEs.SetUniToMaster(e.CurrentIdxC.Idx, e.SelectedUnitE.UnitTC.Unit);
                                    e.CellClickTC.Click = CellClickTypes.SimpleClick;
                                }
                                break;

                            case CellClickTypes.GiveTakeTW:
                                {
                                    if (e.UnitTC(idx_cur).Is(UnitTypes.Pawn) && e.UnitPlayerTC(idx_cur).Is(e.CurPlayerITC.Player))
                                    {
                                        e.RpcPoolEs.GiveTakeToolWeaponToMaster(e.CurrentIdxC.Idx, e.SelectedTWE.ToolWeaponTC.ToolWeapon, e.SelectedTWE.LevelTC.Level);
                                    }
                                    else
                                    {
                                        e.CellClickTC.Click = CellClickTypes.SimpleClick;
                                        e.PreviousSelectedIdxC = e.SelectedIdxC;
                                        e.SelectedIdxC.Idx = e.CurrentIdxC.Idx;
                                    }
                                }
                                break;

                            case CellClickTypes.UniqueAbility:
                                {
                                    switch (e.SelectedAbilityTC.Ability)
                                    {
                                        case AbilityTypes.FireArcher:
                                            e.RpcPoolEs.FireArcherToMas(e.SelectedIdxC.Idx, e.CurrentIdxC.Idx);
                                            break;

                                        case AbilityTypes.StunElfemale:
                                            e.RpcPoolEs.StunElfemaleToMas(e.SelectedIdxC.Idx, e.CurrentIdxC.Idx);
                                            break;

                                        case AbilityTypes.ChangeDirectionWind:
                                            {
                                                foreach (var cellE in e.CellEs(e.CenterCloudIdxC.Idx).AroundCellEs)
                                                {
                                                    if (cellE.IdxC.Idx == e.CurrentIdxC.Idx)
                                                    {
                                                        e.RpcPoolEs.PutOutFireElffToMas(e.SelectedIdxC.Idx, e.CurrentIdxC.Idx);
                                                    }
                                                }
                                            }
                                            break;

                                        case AbilityTypes.DirectWave:
                                            e.RpcPoolEs.DirectWaveToMaster(e.SelectedIdxC.Idx, e.CurrentIdxC.Idx);
                                            break;

                                        case AbilityTypes.Resurrect:
                                            e.RpcPoolEs.ResurrectToMaster(e.SelectedIdxC.Idx, e.CurrentIdxC.Idx);
                                            break;

                                        default: throw new Exception();
                                    }

                                    e.CellClickTC.Click = CellClickTypes.SimpleClick;
                                    e.SelectedIdxC.Idx = e.CurrentIdxC.Idx;
                                }
                                break;

                            default: throw new Exception();
                        }
                    }
                }

                else if (raycastT == RaycastTypes.UI)
                {
                    //cellClick.Click = CellClickTypes.SimpleClick;
                    //Es.SelectedIdxE.Reset();
                }

                else if (raycastT == RaycastTypes.Background)
                {
                    e.CellClickTC.Click = CellClickTypes.SimpleClick;
                    e.PreviousSelectedIdxC = e.SelectedIdxC;
                    e.SelectedIdxC.Idx = 0;
                }

                //_updateView.Invoke();
                //_updateUI.Invoke();
            }

            else
            {
                if (raycastT == RaycastTypes.Cell)
                {





                    //if (e.CellClickTC.Is(CellClickTypes.SetUnit))
                    //{
                    //    if (!e.UnitTC(idx_cur).HaveUnit || !e.UnitEs(idx_cur).ForPlayer(e.CurPlayerITC.Player).IsVisible)
                    //    {
                    //        if (e.CurrentIdxC.Idx == 0)
                    //        {
                    //            e.PreviousVisionIdxC = e.CurrentIdxC;
                    //        }
                    //        else
                    //        {
                    //            e.PreviousVisionIdxC = e.CurrentIdxC;
                    //        }
                    //    }
                    //}
                }
            }
        }
    }
}