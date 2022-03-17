using System;
using UnityEngine;

namespace Chessy.Game
{
    public struct SelectorS
    {
        public SelectorS(in RaycastTypes raycastT, ref EntitiesModel e)
        {
            var idx_cur = e.CellsC.Current;


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
                        e.CellsC.Selected = e.CellsC.Current;
                    }

                    else
                    {
                        switch (e.CellClickTC.Click)
                        {
                            case CellClickTypes.None: throw new Exception();

                            case CellClickTypes.SimpleClick:
                                {
                                    if (e.CellsC.Selected > 0)
                                    {
                                        var curPlayerI = e.CurPlayerITC.Player;

                                        if (e.UnitTC(e.CellsC.Selected).HaveUnit)
                                        {
                                            if (e.UnitEs(e.CellsC.Selected).ForAttack(AttackTypes.Simple).Contains(e.CellsC.Current)
                                            || e.UnitEs(e.CellsC.Selected).ForAttack(AttackTypes.Unique).Contains(e.CellsC.Current))
                                            {
                                                e.RpcPoolEs.AttackUnitToMaster(e.CellsC.Selected, e.CellsC.Current);
                                            }

                                            else if (e.UnitEs(e.CellsC.Selected).ForShift.Contains(e.CellsC.Current))
                                            {
                                                e.RpcPoolEs.ShiftUnitToMaster(e.CellsC.Selected, e.CellsC.Current);
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


                                        e.CellsC.PreviousSelected = e.CellsC.Selected;
                                        e.CellsC.Selected = e.CellsC.Current;
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

                                        e.CellsC.PreviousSelected = e.CellsC.Selected;
                                        e.CellsC.Selected = e.CellsC.Current;
                                    }
                                }
                                break;

                            case CellClickTypes.SetUnit:
                                {
                                    e.RpcPoolEs.SetUniToMaster(e.CellsC.Current, e.SelectedE.UnitC.UnitTC);
                                    e.CellClickTC.Click = CellClickTypes.SimpleClick;
                                }
                                break;

                            case CellClickTypes.GiveTakeTW:
                                {
                                    if (e.UnitTC(idx_cur).Is(UnitTypes.Pawn) && e.UnitPlayerTC(idx_cur).Is(e.CurPlayerITC.Player))
                                    {
                                        e.RpcPoolEs.GiveTakeToolWeaponToMaster(e.CellsC.Current, e.SelectedE.ToolWeaponC.ToolWeaponT, e.SelectedE.ToolWeaponC.LevelT);
                                    }
                                    else
                                    {
                                        e.CellClickTC.Click = CellClickTypes.SimpleClick;
                                        e.CellsC.PreviousSelected = e.CellsC.Selected;
                                        e.CellsC.Selected = e.CellsC.Current;
                                    }
                                }
                                break;

                            case CellClickTypes.UniqueAbility:
                                {
                                    switch (e.SelectedE.AbilityTC.Ability)
                                    {
                                        case AbilityTypes.FireArcher:
                                            e.RpcPoolEs.FireArcherToMas(e.CellsC.Selected, e.CellsC.Current);
                                            break;

                                        case AbilityTypes.StunElfemale:
                                            e.RpcPoolEs.StunElfemaleToMas(e.CellsC.Selected, e.CellsC.Current);
                                            break;

                                        case AbilityTypes.ChangeDirectionWind:
                                            {
                                                foreach (var cellE in e.CellEs(e.WeatherE.CloudC.Center).AroundCellEs)
                                                {
                                                    if (cellE.IdxC.Idx == e.CellsC.Current)
                                                    {
                                                        e.RpcPoolEs.PutOutFireElffToMas(e.CellsC.Selected, e.CellsC.Current);
                                                    }
                                                }
                                            }
                                            break;

                                        //case AbilityTypes.DirectWave:
                                        //    e.RpcPoolEs.DirectWaveToMaster(e.CellsC.SelectedIdxC, e.CellsC.CurrentIdxC);
                                        //    break;

                                        case AbilityTypes.Resurrect:
                                            e.RpcPoolEs.ResurrectToMaster(e.CellsC.Selected, e.CellsC.Current);
                                            break;

                                        default: throw new Exception();
                                    }

                                    e.CellClickTC.Click = CellClickTypes.SimpleClick;
                                    e.CellsC.Selected = e.CellsC.Current;
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
                    e.CellsC.PreviousSelected = e.CellsC.Selected;
                    e.CellsC.Selected = 0;
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
                    //        if (e.CellsC.CurrentIdxC == 0)
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