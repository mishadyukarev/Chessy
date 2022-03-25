using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    sealed class TakeAdultForestResourcesS : SystemModelGameAbs
    {
        internal TakeAdultForestResourcesS(in EntitiesModelGame eMGame) : base(eMGame) { }

        internal void Take(in float extract, in byte idx)
        {
            if (eMGame.AdultForestC(idx).HaveAnyResources)
            {
                eMGame.AdultForestC(idx).Resources -= extract;

                if (!eMGame.AdultForestC(idx).HaveAnyResources)
                {
                    eMGame.AdultForestC(idx).Resources = 0;

                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        eMGame.CellEs(idx).TrailHealthC(dirT).Health = 0;
                    }
                }
            }
        }
    }
}