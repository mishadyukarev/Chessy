using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    sealed class TakeAdultForestResourcesS : SystemModelGameAbs
    {
        internal TakeAdultForestResourcesS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Take(in float extract, in byte idx)
        {
            if (e.AdultForestC(idx).HaveAnyResources)
            {
                e.AdultForestC(idx).Resources -= extract;

                if (!e.AdultForestC(idx).HaveAnyResources)
                {
                    e.AdultForestC(idx).Resources = 0;

                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        e.CellEs(idx).TrailHealthC(dirT).Health = 0;
                    }
                }
            }
        }
    }
}