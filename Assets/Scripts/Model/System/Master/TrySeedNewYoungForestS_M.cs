﻿using Chessy.Model.Values;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void TrySeedNewYoungForestOnCell(in byte cellIdx)
        {
            if (UnityEngine.Random.Range(0f, 1f) < ValuesChessy.PERCENT_SPAWN_FOR_YOUNG_FOREST_AFTER_FIRE)
                _e.YoungForestC(cellIdx).Resources = ValuesChessy.MAX_RESOURCES_ENVIRONMENT;
        }
    }
}