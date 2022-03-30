using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;

namespace Chessy.Game.Model.System
{
    sealed class ClearAllEnvironmentS : SystemModelGameAbs
    {
        internal ClearAllEnvironmentS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Clear(in byte cell_0)
        {
            e.FertilizeC(cell_0).Resources = 0;
            e.AdultForestC(cell_0).Resources = 0;
            e.YoungForestC(cell_0).Resources = 0;
            e.HillC(cell_0).Resources = 0;
            e.MountainC(cell_0).Resources = 0;
        }
    }
}