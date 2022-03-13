using UnityEngine;

namespace Chessy.Game
{
    sealed class CellSupportVS : SystemViewAbstract, IEcsRunSystem
    {
        bool _isActive;
        Color _color;

        internal CellSupportVS(in EntitiesModel ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            ref var cellClick = ref E.CellClickTC;
            var curPlayer = E.CurPlayerITC.Player;

            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                _isActive = false;
                _color = default;


                switch (E.CellClickTC.Click)
                {
                    case CellClickTypes.SimpleClick:
                        {

                        }
                        break;

                    case CellClickTypes.SetUnit:
                        {
                            if (E.CellEs(idx_0).CellE.IsStartedCell(E.CurPlayerITC.Player))
                            {
                                if (!E.UnitTC(idx_0).HaveUnit)
                                {
                                    _isActive = true;
                                    _color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                                }
                            }
                        }
                        break;

                    case CellClickTypes.GiveTakeTW:
                        {
                            if (E.UnitTC(idx_0).Is(UnitTypes.Pawn))
                            {
                                if (E.UnitEs(idx_0).ForPlayer(curPlayer).IsVisible)
                                {
                                    if (E.UnitPlayerTC(idx_0).Is(curPlayer))
                                    {
                                        _isActive = true;
                                        _color = ColorsValues.Color(SupportCellVisionTypes.GivePawnTool);
                                    }
                                }
                            }
                        }
                        break;

                    case CellClickTypes.UniqueAbility:

                        switch (E.SelectedAbilityTC.Ability)
                        {
                            case AbilityTypes.ChangeDirectionWind:
                                if (E.HaveFire(idx_0))
                                {
                                    _isActive = true;
                                    _color = ColorsValues.Color(E.SelectedAbilityTC.Ability);
                                }
                                break;

                            case AbilityTypes.StunElfemale:
                                if (E.AdultForestC(idx_0).HaveAnyResources)
                                {
                                    _isActive = true;
                                    _color = ColorsValues.Color(E.SelectedAbilityTC.Ability);
                                }
                                break;
                        }
                        break;

                    default:
                        break;
                }


                SupportCellVEs.Support(idx_0).SetActive(_isActive);
                SupportCellVEs.Support(idx_0).Color = _color;
            }

            SupportCellVEs.Support(E.SelectedIdxC.Idx).Enable();
            SupportCellVEs.Support(E.SelectedIdxC.Idx).Color = ColorsValues.Color(SupportCellVisionTypes.Selector);


            if (cellClick.Is(CellClickTypes.UniqueAbility))
            {
                if (cellClick.Is(CellClickTypes.UniqueAbility))
                {
                    if (E.SelectedAbilityTC.Is(AbilityTypes.ChangeDirectionWind))
                    {
                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = E.CellEs(E.CenterCloudIdxC.Idx).AroundCellE(dirT).IdxC.Idx;

                            SupportCellVEs.Support(idx_1).Enable();
                            SupportCellVEs.Support(idx_1).Color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                        }
                    }
                }

                else if (E.SelectedAbilityTC.Is(AbilityTypes.FireArcher))
                {
                    foreach (var idx in E.UnitEs(E.SelectedIdxC.Idx).ForArson.Idxs)
                    {
                        SupportCellVEs.Support(idx).Enable();
                        SupportCellVEs.Support(idx).Color = ColorsValues.Color(SupportCellVisionTypes.FireSelector);
                    }
                }
            }


            else
            {
                if (E.UnitTC(E.SelectedIdxC.Idx).HaveUnit)
                {
                    if (E.UnitPlayerTC(E.SelectedIdxC.Idx).Is(E.CurPlayerITC.Player))
                    {
                        var idxs = E.UnitEs(E.SelectedIdxC.Idx).ForShift.Idxs;

                        foreach (var idx_0 in idxs)
                        {
                            SupportCellVEs.Support(idx_0).Enable();
                            SupportCellVEs.Support(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                        }

                        foreach (var idx_0 in E.UnitEs(E.SelectedIdxC.Idx).ForAttack(AttackTypes.Simple).Idxs)
                        {
                            SupportCellVEs.Support(idx_0).Enable();
                            SupportCellVEs.Support(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.SimpleAttack);
                        }
                        foreach (var idx_0 in E.UnitEs(E.SelectedIdxC.Idx).ForAttack(AttackTypes.Unique).Idxs)
                        {
                            SupportCellVEs.Support(idx_0).Enable();
                            SupportCellVEs.Support(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.UniqueAttack);
                        }
                    }
                }
            }
        }
    }
}