using Chessy.Game.Extensions;

namespace Chessy.Game
{
    static class SyncBuildingVS
    {
        public static void Sync(in byte idx_0, in EntitiesViewGame vEs, in Chessy.Game.Model.Entity.EntitiesModelGame e)
        {
            var curPlayerI = e.CurPlayerITC.PlayerT;

            var isVisForMe = e.BuildingVisibleC(idx_0).IsVisible(curPlayerI);
            var isVisForNext = e.BuildingVisibleC(idx_0).IsVisible(curPlayerI.NextPlayer());

            for (var build = BuildingTypes.None + 1; build < BuildingTypes.End; build++)
            {
                vEs.BuildingE(idx_0, build).Disable();
            }

            if (e.BuildingTC(idx_0).HaveBuilding)
            {
                if (isVisForMe)
                {
                    vEs.BuildingE(idx_0, e.BuildingTC(idx_0).BuildingT).Enable();
                }
            }
        }
    }

}
