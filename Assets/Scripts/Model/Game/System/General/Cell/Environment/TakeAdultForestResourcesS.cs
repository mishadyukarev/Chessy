using Chessy.Game.Entity.Model;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;

namespace Chessy.Game.Model.System
{
    sealed class TakeAdultForestResourcesS : SystemModelGameAbs
    {
        internal TakeAdultForestResourcesS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Take(in float extract, in byte idx)
        {
            if (eMG.AdultForestC(idx).HaveAnyResources)
            {
                eMG.AdultForestC(idx).Resources -= extract;

                if (!eMG.AdultForestC(idx).HaveAnyResources)
                {
                    eMG.AdultForestC(idx).Resources = 0;

                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        eMG.CellEs(idx).TrailHealthC(dirT).Health = 0;
                    }
                }
            }
        }
    }
}