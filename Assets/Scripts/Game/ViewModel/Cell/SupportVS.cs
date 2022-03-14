using UnityEngine;

namespace Chessy.Game
{
    static class SupportVS
    {
        public static void Sync(in EntitiesModel e, in EntitiesView entsView)
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
                            if (e.CellEs(idx_0).CellE.IsStartedCell(e.CurPlayerITC.Player))
                            {
                                if (!e.UnitTC(idx_0).HaveUnit)
                                {
                                    isActive = true;
                                    color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                                }
                            }
                        }
                        break;

                    case CellClickTypes.GiveTakeTW:
                        {
                            if (e.UnitTC(idx_0).Is(UnitTypes.Pawn))
                            {
                                if (e.UnitEs(idx_0).ForPlayer(e.CurPlayerITC.Player).IsVisible)
                                {
                                    if (e.UnitPlayerTC(idx_0).Is(e.CurPlayerITC.Player))
                                    {
                                        isActive = true;
                                        color = ColorsValues.Color(SupportCellVisionTypes.GivePawnTool);
                                    }
                                }
                            }
                        }
                        break;

                    case CellClickTypes.UniqueAbility:

                        switch (e.SelectedAbilityTC.Ability)
                        {
                            case AbilityTypes.ChangeDirectionWind:
                                if (e.HaveFire(idx_0))
                                {
                                    isActive = true;
                                    color = ColorsValues.Color(e.SelectedAbilityTC.Ability);
                                }
                                break;

                            case AbilityTypes.StunElfemale:
                                if (e.AdultForestC(idx_0).HaveAnyResources)
                                {
                                    isActive = true;
                                    color = ColorsValues.Color(e.SelectedAbilityTC.Ability);
                                }
                                break;
                        }
                        break;

                    default:
                        break;
                }


                SupportCellVEs.Support(idx_0).SetActive(isActive);
                SupportCellVEs.Support(idx_0).SR.color = color;
            }

            SupportCellVEs.Support(e.SelectedIdxC.Idx).Enable();
            SupportCellVEs.Support(e.SelectedIdxC.Idx).SR.color = ColorsValues.Color(SupportCellVisionTypes.Selector);


            if (e.CellClickTC.Is(CellClickTypes.UniqueAbility))
            {
                if (e.CellClickTC.Is(CellClickTypes.UniqueAbility))
                {
                    if (e.SelectedAbilityTC.Is(AbilityTypes.ChangeDirectionWind))
                    {
                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = e.CellEs(e.CenterCloudIdxC.Idx).AroundCellE(dirT).IdxC.Idx;

                            SupportCellVEs.Support(idx_1).Enable();
                            SupportCellVEs.Support(idx_1).SR.color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                        }
                    }
                }

                else if (e.SelectedAbilityTC.Is(AbilityTypes.FireArcher))
                {
                    foreach (var idx in e.UnitEs(e.SelectedIdxC.Idx).ForArson.Idxs)
                    {
                        SupportCellVEs.Support(idx).Enable();
                        SupportCellVEs.Support(idx).SR.color = ColorsValues.Color(SupportCellVisionTypes.FireSelector);
                    }
                }
            }


            else
            {
                if (e.UnitTC(e.SelectedIdxC.Idx).HaveUnit)
                {
                    if (e.UnitPlayerTC(e.SelectedIdxC.Idx).Is(e.CurPlayerITC.Player))
                    {
                        var idxs = e.UnitEs(e.SelectedIdxC.Idx).ForShift.Idxs;

                        foreach (var idx_0 in idxs)
                        {
                            SupportCellVEs.Support(idx_0).Enable();
                            SupportCellVEs.Support(idx_0).SR.color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                        }

                        foreach (var idx_0 in e.UnitEs(e.SelectedIdxC.Idx).ForAttack(AttackTypes.Simple).Idxs)
                        {
                            SupportCellVEs.Support(idx_0).Enable();
                            SupportCellVEs.Support(idx_0).SR.color = ColorsValues.Color(SupportCellVisionTypes.SimpleAttack);
                        }
                        foreach (var idx_0 in e.UnitEs(e.SelectedIdxC.Idx).ForAttack(AttackTypes.Unique).Idxs)
                        {
                            SupportCellVEs.Support(idx_0).Enable();
                            SupportCellVEs.Support(idx_0).SR.color = ColorsValues.Color(SupportCellVisionTypes.UniqueAttack);
                        }
                    }
                }
            }
        }
    }
}