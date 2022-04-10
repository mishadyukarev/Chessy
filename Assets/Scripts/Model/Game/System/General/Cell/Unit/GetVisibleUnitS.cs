using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class GetVisibleUnitS : SystemModel
    {
        internal GetVisibleUnitS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                eMG.UnitVisibleC(cell_0).Set(playerT, true);
            }

            if (eMG.UnitTC(cell_0).HaveUnit)
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
                            var cell_1 = eMG.AroundCellsE(cell_0).IdxCell(dirT);

                            if (eMG.UnitTC(cell_1).HaveUnit)
                            {
                                if (eMG.UnitPlayerTC(cell_1).Is(PlayerTypes.First)) isVisForFirst = true;
                                if (eMG.UnitPlayerTC(cell_1).Is(PlayerTypes.Second)) isVisForSecond = true;
                            }
                        }
                    }

                    eMG.UnitVisibleC(cell_0).Set(PlayerTypes.First, isVisForFirst);
                    eMG.UnitVisibleC(cell_0).Set(PlayerTypes.Second, isVisForSecond);
                }

                else
                {
                    if (eMG.AdultForestC(cell_0).HaveAnyResources)
                    {
                        var isVisibledNextPlayer = false;

                        foreach (var idx_1 in eMG.AroundCellsE(cell_0).CellsAround)
                        {
                            if (eMG.UnitTC(idx_1).HaveUnit)
                            {
                                if (!eMG.UnitTC(idx_1).IsAnimal)
                                {
                                    if (!eMG.UnitPlayerTC(idx_1).Is(eMG.UnitPlayerT(cell_0)))
                                    {
                                        isVisibledNextPlayer = true;
                                    }
                                }
                            }
                        }

                        eMG.UnitVisibleC(cell_0).Set(eMG.UnitPlayerT(cell_0).NextPlayer(), isVisibledNextPlayer);
                    }
                }
            }
        }
    }
}