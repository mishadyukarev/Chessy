using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class TakeAdultForestResourcesS : SystemModel
    {
        internal TakeAdultForestResourcesS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Take(in float extract, in byte cell)
        {
            if (eMG.AdultForestC(cell).HaveAnyResources)
            {
                eMG.AdultForestC(cell).Resources -= extract;

                if (!eMG.AdultForestC(cell).HaveAnyResources)
                {
                    sMG.DestroyAdultForestS.Destroy(cell);
                }
            }
        }
    }
}