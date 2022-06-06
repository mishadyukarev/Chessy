using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    internal sealed class TrySeedNewYoungForestS_M : SystemModel
    {
        internal TrySeedNewYoungForestS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void TrySeed(in byte cellIdx)
        {
            if (UnityEngine.Random.Range(0f, 1f) < EnvironmentValues.PERCENT_SPAWN_FOR_YOUNG_FOREST_AFTER_FIRE)
                eMG.YoungForestC(cellIdx).Resources = EnvironmentValues.MAX_RESOURCES;
        }
    }
}