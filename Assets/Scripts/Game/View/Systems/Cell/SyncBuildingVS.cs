namespace Chessy.Game
{
    static class SyncBuildingVS
    {
        public static void Sync(in byte idx_0, in EntitiesViewGame vEs, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            var curPlayerI = e.CurPlayerITC.Player;

            var isVisForMe = e.BuildEs(idx_0).IsVisible(curPlayerI);
            var isVisForNext = e.BuildEs(idx_0).IsVisible(e.NextPlayer(curPlayerI).Player);

            for (var build = BuildingTypes.None + 1; build < BuildingTypes.End; build++)
            {
                vEs.BuildingE(idx_0, build).Disable();
            }

            if (e.BuildingTC(idx_0).HaveBuilding)
            {
                if (isVisForMe)
                {
                    vEs.BuildingE(idx_0, e.BuildingTC(idx_0).Building).Enable();
                }
            }
        }
    }

}
