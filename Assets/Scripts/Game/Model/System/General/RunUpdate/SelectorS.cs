using Chessy.Game.Entity.Model;
using System;
using System.Linq;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class SelectorS : SystemModelGameAbs, IEcsRunSystem
    {
        public SelectorS(in EntitiesModelGame eMGame) : base(eMGame)
        {

        }

        public void Run()
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


            if (e.IsClicked)
            {
                e.NeedUpdateView = true;

                


                if (e.RaycastTC.Raycast == RaycastTypes.Cell)
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
                                    if (e.LessonTC.HaveLesson) return;

                                    if (e.CellsC.Selected > 0)
                                    {
                                        var curPlayerI = e.CurPlayerITC.Player;

                                        if (e.UnitTC(e.CellsC.Selected).HaveUnit)
                                        {
                                            if (e.UnitEs(e.CellsC.Selected).SimpleAttack.Contains(e.CellsC.Current)
                                            || e.UnitEs(e.CellsC.Selected).UniqueAttack.Contains(e.CellsC.Current))
                                            {
                                                e.RpcPoolEs.AttackUnitToMaster(e.CellsC.Selected, e.CellsC.Current);
                                            }

                                            else if (e.UnitPlayerTC(e.CellsC.Selected).Is(e.CurPlayerITC.Player)
                                                && e.UnitEs(e.CellsC.Selected).ForShift.Contains(e.CellsC.Current))
                                            {
                                                e.RpcPoolEs.ShiftUnitToMaster(e.CellsC.Selected, e.CellsC.Current);
                                            }

                                            else
                                            {
                                                Sound(e);
                                            }
                                        }

                                        else
                                        {
                                            Sound(e);
                                        }


                                        e.CellsC.PreviousSelected = e.CellsC.Selected;
                                        e.CellsC.Selected = e.CellsC.Current;
                                    }

                                    else
                                    {
                                        Sound(e);

                                        e.CellsC.PreviousSelected = e.CellsC.Selected;
                                        e.CellsC.Selected = e.CellsC.Current;
                                    }
                                }
                                break;

                            case CellClickTypes.SetUnit:
                                {
                                    e.RpcPoolEs.SetUniToMaster(e.CellsC.Current, e.SelectedUnitE.UnitTC.Unit);
                                    e.CellClickTC.Click = CellClickTypes.SimpleClick;
                                }
                                break;

                            case CellClickTypes.GiveTakeTW:
                                {
                                    e.CellsC.Selected = e.CellsC.Current;

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
                                                foreach (var cellE in e.CellEs(e.WeatherE.CloudC.Center).AroundCellsEs.AroundCellEs)
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
                                }
                                break;

                            default: throw new Exception();
                        }
                    }
                }

                else if (e.RaycastTC.Raycast == RaycastTypes.UI)
                {
                    //cellClick.Click = CellClickTypes.SimpleClick;
                    //Es.SelectedIdxE.Reset();
                }

                else if (e.RaycastTC.Raycast == RaycastTypes.Background)
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
                if (e.RaycastTC.Raycast == RaycastTypes.Cell)
                {

                }
            }
        }


        void Sound(in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            var cell_0 = e.CellsC.Current;

            if (e.UnitTC(cell_0).HaveUnit
                && e.UnitEs(cell_0).ForPlayer(e.CurPlayerITC.Player).IsVisible && !e.UnitTC(cell_0).Is(UnitTypes.Wolf))
            {
                if (e.UnitTC(cell_0).Is(UnitTypes.Tree))
                {
                    e.Sound(ClipTypes.Leaf).Invoke();
                }

                else if (e.UnitTC(cell_0).IsMelee(e.UnitMainTWTC(cell_0).ToolWeapon))
                {
                    e.Sound(ClipTypes.PickMelee).Invoke();
                }
                else
                {
                    e.Sound(ClipTypes.PickArcher).Invoke();
                }
            }
            else
            {
                if (e.AdultForestC(cell_0).HaveAnyResources)
                {
                    e.Sound(ClipTypes.Leaf).Invoke();
                }
                else if (e.HillC(cell_0).HaveAnyResources)
                {
                    e.Sound(ClipTypes.Rock).Invoke();
                }
                else if (e.MountainC(cell_0).HaveAnyResources)
                {
                    e.Sound(ClipTypes.ShortWind).Invoke();
                }
                else
                {
                    e.Sound(ClipTypes.KickGround).Invoke();
                }


                if (e.CellEs(e.WeatherE.CloudC.Center).AroundCellsEs.IdxsAround.Contains(cell_0) || e.WeatherE.CloudC.Center == cell_0)
                {
                    e.Sound(ClipTypes.ShortRain).Invoke();
                }
            }
        }
    }
}