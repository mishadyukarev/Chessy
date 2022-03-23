using Chessy.Common.Entity.View;
using Chessy.Game.Entity.Model;
using Chessy.Game.System.View;
using Chessy.Game.Values;

namespace Chessy.Game
{
    public readonly struct UpdateViewS
    {
        public void Run(in SystemsViewGame sVGame, in EntitiesViewGame eVGame, in EntitiesModelGame eMGame, in EntitiesViewCommon eVCommon)
        {
            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                SyncUnitVS.Sync(idx_0, eVGame, eMGame);
                SyncBuildingVS.Sync(idx_0, eVGame, eMGame);
                sVGame.SyncStatsS.Sync(idx_0, eVGame, eMGame);
                sVGame.SyncEnvironmentS.Run(idx_0, eVGame, eMGame);
                sVGame.SyncFireS.Sync(idx_0, eVGame, eMGame);
                sVGame.SyncRiverS.Sync(idx_0, eVGame, eMGame);
                sVGame.SyncBarsEnvironmentS.Sync(idx_0, eVGame, eMGame);
                sVGame.SyncTrailS.Sync(idx_0, eVGame, eMGame);
                sVGame.SyncNoneVisionS.Sync(idx_0, eVGame.CellEs(idx_0).SupportCellEs.NoneSRC, eMGame);
                sVGame.SyncNeedFoodS.Sync(idx_0, eVGame.CellEs(idx_0).UnitVEs.NeedFoodSRC, eMGame);
                sVGame.SyncBuildingFlagS.Sync(eVGame.BuildingEs(idx_0).FlagSRC, idx_0, eMGame);


                eVGame.CellEs(idx_0).UnitVEs.EffectVEs.SyncVision(eMGame.UnitEs(idx_0), idx_0 == eMGame.CellsC.Selected, eMGame);
                SyncStunVS.Sync(idx_0, eVGame, eMGame);
                ShieldVS.Run(idx_0, eVGame, eMGame);
            }

            SoundVS.Sync(eVGame);
            SupportVS.Sync(eMGame, eVGame);
            CloudVS.Run(eVGame, eMGame);
            RotateAllVS.Rotate(eVGame, eMGame, eVCommon);
        }
    }
}