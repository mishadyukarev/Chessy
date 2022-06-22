using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        internal void TrySeedNewYoungForestOnCell(in byte cellIdx)
        {
            if (UnityEngine.Random.Range(0f, 1f) < EnvironmentValues.PERCENT_SPAWN_FOR_YOUNG_FOREST_AFTER_FIRE)
                _e.YoungForestC(cellIdx).Resources = EnvironmentValues.MAX_RESOURCES;
        }
    }
}