using Chessy.Common.Entity.View;
using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.View
{
    public sealed class SystemsViewGame
    {
        public readonly UpdateViewS UpdateS;

        public readonly SyncNoneVisionS SyncNoneVisionS;
        public readonly NeedFoodS SyncNeedFoodS;
        public readonly BuildingFlagVS SyncBuildingFlagS;
        public readonly SyncTrailVS SyncTrailS;
        public readonly SyncBarsEnvironmentVS SyncBarsEnvironmentS;
        public readonly SyncRiverVS SyncRiverS;
        public readonly SyncFireVS SyncFireS;
        public readonly SyncEnvironmentVS SyncEnvironmentS;
        public readonly SyncStatsVS SyncStatsS;

        public SystemsViewGame(in EntitiesViewGame eVGame, in EntitiesModelGame eMGame, in EntitiesViewCommon eVCommon)
        {
            UpdateS = new UpdateViewS(this, eVGame, eMGame, eVCommon);
        }

    }
}