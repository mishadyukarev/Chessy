using Chessy.Game.Entity;
using Chessy.Game.Values;
using UnityEngine;

namespace Chessy.Game
{
    static class SupportVS
    {
        public static void Sync(in Chessy.Game.Entity.Model.EntitiesModelGame e, in EntitiesViewGame eV)
        {
            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                var isActive = false;
                Color color = default;


                switch (e.CellClickTC.Click)
                {
                    case CellClickTypes.SimpleClick:
                        {

                        }
                        break;

                    case CellClickTypes.SetUnit:
                        {
                            //if (e.CellEs(idx_0).CellE.IsStartedCell(e.CurPlayerITC.Player))
                            //{
                            //    if (!e.UnitTC(idx_0).HaveUnit)
                            //    {
                            //        isActive = true;
                            //        color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                            //    }
                            //}
                        }
                        break;

                    case CellClickTypes.GiveTakeTW:
                        {
                            //if (e.UnitTC(idx_0).Is(UnitTypes.Pawn))
                            //{
                            //    if (e.UnitEs(idx_0).ForPlayer(e.CurPlayerITC.Player).IsVisible)
                            //    {
                            //        if (e.UnitPlayerTC(idx_0).Is(e.CurPlayerITC.Player))
                            //        {
                            //            isActive = true;
                            //            color = ColorsValues.Color(SupportCellVisionTypes.GiveTakeToolWeapon);
                            //        }
                            //    }
                            //}
                        }
                        break;

                    case CellClickTypes.UniqueAbility:

                        switch (e.SelectedE.AbilityTC.Ability)
                        {
                            case AbilityTypes.ChangeDirectionWind:
                                if (e.HaveFire(idx_0))
                                {
                                    isActive = true;
                                    color = ColorsValues.Color(e.SelectedE.AbilityTC.Ability);
                                }
                                break;

                            case AbilityTypes.StunElfemale:
                                //if (e.AdultForestC(idx_0).HaveAnyResources)
                                //{
                                //    isActive = true;
                                //    color = ColorsValues.Color(e.SelectedE.AbilityTC.Ability);
                                //}
                                break;
                        }
                        break;

                    default:
                        break;
                }


                eV.CellEs(idx_0).SupportCellEs.Support.SetActive(isActive);
                eV.CellEs(idx_0).SupportCellEs.Support.SR.color = color;
            }

            eV.CellEs(e.CellsC.Selected).SupportCellEs.Support.Enable();
            eV.CellEs(e.CellsC.Selected).SupportCellEs.Support.SR.color = ColorsValues.Color(SupportCellVisionTypes.Selector);


            if (e.CellClickTC.Is(CellClickTypes.UniqueAbility))
            {

                if (e.SelectedE.AbilityTC.Is(AbilityTypes.ChangeDirectionWind))
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = e.CellEs(e.WeatherE.CloudC.Center).AroundCellsEs.AroundCellE(dirT).IdxC.Idx;

                        eV.CellEs(idx_1).SupportCellEs.Support.Enable();
                        eV.CellEs(idx_1).SupportCellEs.Support.SR.color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                    }
                }

                else if (e.SelectedE.AbilityTC.Is(AbilityTypes.FireArcher))
                {
                    foreach (var idx in e.UnitEs(e.CellsC.Selected).ForArson.Idxs)
                    {
                        eV.CellEs(idx).SupportCellEs.Support.Enable();
                        eV.CellEs(idx).SupportCellEs.Support.SR.color = ColorsValues.Color(AbilityTypes.FireArcher);
                    }
                }
            }


            else
            {
                if (e.UnitTC(e.CellsC.Selected).HaveUnit)
                {
                    if (e.UnitPlayerTC(e.CellsC.Selected).Is(e.CurPlayerITC.Player))
                    {
                        var idxs = e.UnitEs(e.CellsC.Selected).ForShift.Idxs;

                        if (!e.CellClickTC.Is(CellClickTypes.GiveTakeTW))
                        {
                            foreach (var idx_0 in idxs)
                            {
                                eV.CellEs(idx_0).SupportCellEs.Support.Enable();
                                eV.CellEs(idx_0).SupportCellEs.Support.SR.color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                            }

                            foreach (var idx_0 in e.UnitEs(e.CellsC.Selected).SimpleAttack.Idxs)
                            {
                                eV.CellEs(idx_0).SupportCellEs.Support.Enable();
                                eV.CellEs(idx_0).SupportCellEs.Support.SR.color = ColorsValues.Color(SupportCellVisionTypes.SimpleAttack);
                            }
                            foreach (var idx_0 in e.UnitEs(e.CellsC.Selected).UniqueAttack.Idxs)
                            {
                                eV.CellEs(idx_0).SupportCellEs.Support.Enable();
                                eV.CellEs(idx_0).SupportCellEs.Support.SR.color = ColorsValues.Color(SupportCellVisionTypes.UniqueAttack);
                            }
                        }
                    }
                }
            }

            //switch (e.CellClickTC.Click)
            //{
            //    case CellClickTypes.UniqueAbility:
            //        {
            //            switch (e.SelectedAbilityTC.Ability)
            //            {
            //                case AbilityTypes.FireArcher:
            //                    foreach (var idx_0 in e.UnitEs(e.CellsC.SelectedIdxC).ForArson.Idxs)
            //                    {
            //                        SupportCellVEs.Support(idx_0).Enable();
            //                        SupportCellVEs.Support(idx_0).SR.color = ColorsValues.Color(SupportCellVisionTypes.FireSelector);
            //                    }
            //                    break;
            //            }
            //        }
            //        break;
            //}



        }
    }
}