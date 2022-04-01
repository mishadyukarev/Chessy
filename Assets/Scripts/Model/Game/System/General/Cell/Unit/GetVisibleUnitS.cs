using Chessy.Game.Entity.Model;
using Chessy.Game.Extensions;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;

namespace Chessy.Game.Model.System
{
    sealed class GetVisibleUnitS : SystemModelGameAbs
    {
        internal GetVisibleUnitS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            if (eMG.UnitTC(cell_0).HaveUnit)
            {
                if (eMG.UnitPlayerTC(cell_0).Is(PlayerTypes.None))
                {
                    if (eMG.UnitTC(cell_0).IsAnimal)
                    {
                        var isVisForFirst = true;
                        var isVisForSecond = true;

                        if (eMG.AdultForestC(cell_0).HaveAnyResources)
                        {
                            isVisForFirst = false;
                            isVisForSecond = false;

                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                var cell_1 = eMG.CellEs(cell_0).AroundCellsEs.AroundCellE(dirT).IdxC.Idx;

                                if (eMG.UnitTC(cell_1).HaveUnit)
                                {
                                    if (eMG.UnitPlayerTC(cell_1).Is(PlayerTypes.First)) isVisForFirst = true;
                                    if (eMG.UnitPlayerTC(cell_1).Is(PlayerTypes.Second)) isVisForSecond = true;
                                }
                            }
                        }

                        eMG.UnitEs(cell_0).ForPlayer(PlayerTypes.First).IsVisible = isVisForFirst;
                        eMG.UnitEs(cell_0).ForPlayer(PlayerTypes.Second).IsVisible = isVisForSecond;
                    }
                }
                else
                {
                    eMG.UnitEs(cell_0).ForPlayer(eMG.UnitPlayerTC(cell_0).PlayerT).IsVisible = true;

                    if (eMG.AdultForestC(cell_0).HaveAnyResources)
                    {
                        var isVisibledNextPlayer = false;

                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = eMG.CellEs(cell_0).AroundCellsEs.AroundCellE(dirT).IdxC.Idx;

                            if (eMG.UnitTC(idx_1).HaveUnit)
                            {
                                if (!eMG.UnitPlayerTC(idx_1).Is(eMG.UnitPlayerTC(cell_0).PlayerT))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }

                        eMG.UnitEs(cell_0).ForPlayer(eMG.UnitPlayerTC(cell_0).PlayerT.NextPlayer()).IsVisible = isVisibledNextPlayer;
                    }
                    else
                    {
                        eMG.UnitEs(cell_0).ForPlayer(eMG.UnitPlayerTC(cell_0).PlayerT.NextPlayer()).IsVisible = true;
                    }
                }
            }
        }
    }
}