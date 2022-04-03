using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class GetVisibleUnitS : SystemModelGameAbs
    {
        internal GetVisibleUnitS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Get(in byte cell_0)
        {

            if (eMG.UnitTC(cell_0).HaveUnit)
            {
                for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                {
                    eMG.UnitVisibleC(cell_0).Set(playerT, true);
                }

                //eMG.UnitVisibleC(cell_0).Set(PlayerTypes.First, true);
                //eMG.UnitVisibleC(cell_0).Set(PlayerTypes.Second, true);



                if (eMG.UnitPlayerTC(cell_0).Is(PlayerTypes.First))
                {
                    if (eMG.UnitTC(cell_0).Is(UnitTypes.Elfemale))
                    {

                    }
                    else if (eMG.UnitTC(cell_0).Is(UnitTypes.King))
                    {

                    }
                    else if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn))
                    {

                    }
                }

                if (eMG.UnitTC(cell_0).IsAnimal)
                {

                }

                else
                {
                    if (eMG.AdultForestC(cell_0).HaveAnyResources)
                    {
                        var isVisibledNextPlayer = false;

                        //foreach (var idx_1 in eMG.AroundCellsE(cell_0).CellsAround)
                        //{
                        //    if (eMG.UnitTC(idx_1).HaveUnit)
                        //    {
                        //        if (!eMG.UnitPlayerTC(idx_1).Is(eMG.UnitPlayerT(cell_0)))
                        //        {
                        //            isVisibledNextPlayer = true;
                        //        }
                        //    }
                        //}



                        var nextPlayer = eMG.UnitPlayerT(cell_0).NextPlayer();

                        eMG.UnitVisibleC(cell_0).Set(nextPlayer, isVisibledNextPlayer);

                        var v = eMG.UnitVisibleC(cell_0).IsVisible(nextPlayer);
                    }
                }


                //if (eMG.UnitPlayerTC(cell_0).Is(PlayerTypes.None))
                //{
                //    //if (eMG.UnitTC(cell_0).IsAnimal)
                //    //{
                //    //    var isVisForFirst = true;
                //    //    var isVisForSecond = true;

                //    //    if (eMG.AdultForestC(cell_0).HaveAnyResources)
                //    //    {
                //    //        isVisForFirst = false;
                //    //        isVisForSecond = false;

                //    //        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                //    //        {
                //    //            var cell_1 = eMG.AroundCellsE(cell_0).IdxCell(dirT);

                //    //            if (eMG.UnitTC(cell_1).HaveUnit)
                //    //            {
                //    //                if (eMG.UnitPlayerTC(cell_1).Is(PlayerTypes.First)) isVisForFirst = true;
                //    //                if (eMG.UnitPlayerTC(cell_1).Is(PlayerTypes.Second)) isVisForSecond = true;
                //    //            }
                //    //        }
                //    //    }

                //    //    eMG.UnitVisibleC(cell_0).Set(PlayerTypes.First, isVisForFirst);
                //    //    eMG.UnitVisibleC(cell_0).Set(PlayerTypes.Second, isVisForSecond);
                //    //}
                //}
                //else
                //{
                //    eMG.UnitVisibleC(cell_0).Set(eMG.UnitPlayerTC(cell_0).PlayerT, true);

                //    if (eMG.AdultForestC(cell_0).HaveAnyResources)
                //    {
                //        var isVisibledNextPlayer = false;

                //        foreach (var idx_1 in eMG.AroundCellsE(cell_0).CellsAround)
                //        {
                //            if (eMG.UnitTC(idx_1).HaveUnit)
                //            {
                //                //if (!eMG.UnitTC(idx_1).IsAnimal)
                //                //{
                //                    if (!eMG.UnitPlayerTC(idx_1).Is(eMG.UnitPlayerT(cell_0)))
                //                    {
                //                        isVisibledNextPlayer = true;
                //                    }
                //                //}
                //            }
                //        }

                //        var nextPlayer = eMG.UnitPlayerTC(cell_0).PlayerT.NextPlayer();

                //        eMG.UnitVisibleC(cell_0).Set(nextPlayer, isVisibledNextPlayer);
                //    }
                //    else
                //    {
                //        eMG.UnitVisibleC(cell_0).Set(eMG.UnitPlayerT(cell_0).NextPlayer(), true);
                //    }
                //}
            }
        }
    }
}