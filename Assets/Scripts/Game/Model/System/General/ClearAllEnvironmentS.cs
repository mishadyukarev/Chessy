using Chessy.Game.Entity.Model;

namespace Chessy.Game.Model.System
{
    public sealed class ClearAllEnvironmentS : SystemModelGameAbs
    {
        public ClearAllEnvironmentS(in EntitiesModelGame eMGame) : base(eMGame) { }

        public void Clear(in byte cell_0)
        {
            eMGame.FertilizeC(cell_0).Resources = 0;
            eMGame.AdultForestC(cell_0).Resources = 0;
            eMGame.YoungForestC(cell_0).Resources = 0;
            eMGame.HillC(cell_0).Resources = 0;
            eMGame.MountainC(cell_0).Resources = 0;
        }
    }
}