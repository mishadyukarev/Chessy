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
            var idx_cur = eMGame.CellsC.Current;


            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.Alpha1)) eMGame.PlayerInfoE(eMGame.CurPlayerITC.Player).ResourcesC(ResourceTypes.Food).Resources += 0.5f;
                if (Input.GetKey(KeyCode.Alpha2)) eMGame.PlayerInfoE(eMGame.CurPlayerITC.Player).ResourcesC(ResourceTypes.Wood).Resources += 0.5f;
                if (Input.GetKey(KeyCode.Alpha3)) eMGame.PlayerInfoE(eMGame.CurPlayerITC.Player).ResourcesC(ResourceTypes.Ore).Resources += 0.5f;
                if (Input.GetKey(KeyCode.Alpha4)) eMGame.PlayerInfoE(eMGame.CurPlayerITC.Player).ResourcesC(ResourceTypes.Iron).Resources += 1;
                if (Input.GetKey(KeyCode.Alpha5)) eMGame.PlayerInfoE(eMGame.CurPlayerITC.Player).ResourcesC(ResourceTypes.Gold).Resources += 1;
            }


            if (eMGame.IsClicked)
            {
                if (eMGame.RaycastTC.Raycast == RaycastTypes.Cell)
                {
                    eMGame.IsSelectedCity = false;

                    if (!eMGame.CurPlayerITC.Is(eMGame.WhoseMove.Player))
                    {
                        eMGame.CellsC.Selected = eMGame.CellsC.Current;
                    }

                    else
                    {
                        switch (eMGame.CellClickTC.Click)
                        {
                            case CellClickTypes.None: throw new Exception();

                            case CellClickTypes.SimpleClick:
                                {
                                    if (eMGame.CellsC.Selected > 0)
                                    {
                                        var curPlayerI = eMGame.CurPlayerITC.Player;

                                        if (eMGame.UnitTC(eMGame.CellsC.Selected).HaveUnit)
                                        {
                                            if (eMGame.UnitEs(eMGame.CellsC.Selected).ForAttack(AttackTypes.Simple).Contains(eMGame.CellsC.Current)
                                            || eMGame.UnitEs(eMGame.CellsC.Selected).ForAttack(AttackTypes.Unique).Contains(eMGame.CellsC.Current))
                                            {
                                                eMGame.RpcPoolEs.AttackUnitToMaster(eMGame.CellsC.Selected, eMGame.CellsC.Current);
                                            }

                                            else if (eMGame.UnitPlayerTC(eMGame.CellsC.Selected).Is(eMGame.CurPlayerITC.Player)
                                                && eMGame.UnitEs(eMGame.CellsC.Selected).ForShift.Contains(eMGame.CellsC.Current))
                                            {
                                                eMGame.RpcPoolEs.ShiftUnitToMaster(eMGame.CellsC.Selected, eMGame.CellsC.Current);
                                            }

                                            else
                                            {
                                                Sound(eMGame);
                                            }
                                        }

                                        else
                                        {
                                            Sound(eMGame);
                                        }


                                        eMGame.CellsC.PreviousSelected = eMGame.CellsC.Selected;
                                        eMGame.CellsC.Selected = eMGame.CellsC.Current;
                                    }

                                    else
                                    {
                                        Sound(eMGame);

                                        eMGame.CellsC.PreviousSelected = eMGame.CellsC.Selected;
                                        eMGame.CellsC.Selected = eMGame.CellsC.Current;
                                    }
                                }
                                break;

                            case CellClickTypes.SetUnit:
                                {
                                    eMGame.RpcPoolEs.SetUniToMaster(eMGame.CellsC.Current, eMGame.SelectedE.UnitC.UnitTC);
                                    eMGame.CellClickTC.Click = CellClickTypes.SimpleClick;
                                }
                                break;

                            case CellClickTypes.GiveTakeTW:
                                {
                                    eMGame.CellsC.Selected = eMGame.CellsC.Current;

                                    if (eMGame.UnitTC(idx_cur).Is(UnitTypes.Pawn) && eMGame.UnitPlayerTC(idx_cur).Is(eMGame.CurPlayerITC.Player))
                                    {
                                        eMGame.RpcPoolEs.GiveTakeToolWeaponToMaster(eMGame.CellsC.Current, eMGame.SelectedE.ToolWeaponC.ToolWeaponT, eMGame.SelectedE.ToolWeaponC.LevelT);
                                    }
                                    else
                                    {
                                        eMGame.CellClickTC.Click = CellClickTypes.SimpleClick;
                                        eMGame.CellsC.PreviousSelected = eMGame.CellsC.Selected;
                                        eMGame.CellsC.Selected = eMGame.CellsC.Current;
                                    }
                                }
                                break;

                            case CellClickTypes.UniqueAbility:
                                {
                                    switch (eMGame.SelectedE.AbilityTC.Ability)
                                    {
                                        case AbilityTypes.FireArcher:
                                            eMGame.RpcPoolEs.FireArcherToMas(eMGame.CellsC.Selected, eMGame.CellsC.Current);
                                            break;

                                        case AbilityTypes.StunElfemale:
                                            eMGame.RpcPoolEs.StunElfemaleToMas(eMGame.CellsC.Selected, eMGame.CellsC.Current);
                                            break;

                                        case AbilityTypes.ChangeDirectionWind:
                                            {
                                                foreach (var cellE in eMGame.CellEs(eMGame.WeatherE.CloudC.Center).AroundCellEs)
                                                {
                                                    if (cellE.IdxC.Idx == eMGame.CellsC.Current)
                                                    {
                                                        eMGame.RpcPoolEs.PutOutFireElffToMas(eMGame.CellsC.Selected, eMGame.CellsC.Current);
                                                    }
                                                }
                                            }
                                            break;

                                        //case AbilityTypes.DirectWave:
                                        //    e.RpcPoolEs.DirectWaveToMaster(e.CellsC.SelectedIdxC, e.CellsC.CurrentIdxC);
                                        //    break;

                                        case AbilityTypes.Resurrect:
                                            eMGame.RpcPoolEs.ResurrectToMaster(eMGame.CellsC.Selected, eMGame.CellsC.Current);
                                            break;

                                        default: throw new Exception();
                                    }

                                    eMGame.CellClickTC.Click = CellClickTypes.SimpleClick;
                                }
                                break;

                            default: throw new Exception();
                        }
                    }
                }

                else if (eMGame.RaycastTC.Raycast == RaycastTypes.UI)
                {
                    //cellClick.Click = CellClickTypes.SimpleClick;
                    //Es.SelectedIdxE.Reset();
                }

                else if (eMGame.RaycastTC.Raycast == RaycastTypes.Background)
                {
                    eMGame.CellClickTC.Click = CellClickTypes.SimpleClick;
                    eMGame.CellsC.PreviousSelected = eMGame.CellsC.Selected;
                    eMGame.CellsC.Selected = 0;
                }

                //_updateView.Invoke();
                //_updateUI.Invoke();
            }

            else
            {
                if (eMGame.RaycastTC.Raycast == RaycastTypes.Cell)
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


                if (e.CellEs(e.WeatherE.CloudC.Center).IdxsAround.Contains(cell_0) || e.WeatherE.CloudC.Center == cell_0)
                {
                    e.Sound(ClipTypes.ShortRain).Invoke();
                }
            }
        }
    }
}