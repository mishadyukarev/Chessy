using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class ClearAllEnvironmentS_M : SystemModel
    {
        internal ClearAllEnvironmentS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Clear(in byte cellIdx)
        {
            eMG.FertilizeC(cellIdx).Resources = 0;
            eMG.YoungForestC(cellIdx).Resources = 0;
            eMG.AdultForestC(cellIdx).Resources = 0;
            eMG.HillC(cellIdx).Resources = 0;
            eMG.MountainC(cellIdx).Resources = 0;
        }
    }
}