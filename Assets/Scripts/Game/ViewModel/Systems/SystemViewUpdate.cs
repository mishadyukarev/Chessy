using Chessy.Game.System.View;
using Chessy.Game.Values;

namespace Chessy.Game
{
    public readonly struct SystemViewUpdate
    {
        public void Run(in SystemsView systems, in EntitiesView eV, in EntitiesModel e)
        {
            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                SyncUnitVS.Sync(idx_0, eV, e);
                SyncBuildingVS.Sync(idx_0, eV, e);
                SyncStatsVS.Sync(idx_0, eV, e);
                SyncEnvironmentVS.Run(idx_0, eV, e);
                SyncFireVS.Sync(idx_0, eV, e);
                SyncRiverVS.Sync(idx_0, eV, e);
                SyncBarsEnvironmentVS.Sync(idx_0, eV, e);
                SyncTrailVS.Sync(idx_0, eV, e);
                systems.SyncNoneVisionS.Sync(idx_0, eV.CellEs(idx_0).SupportCellEs.NoneSRC, e);


                eV.CellEs(idx_0).UnitVEs.EffectVEs.SyncVision(e.UnitEs(idx_0), idx_0 == e.CellsC.Selected, e);
                SyncStunVS.Sync(idx_0, eV, e);
                ShieldVS.Run(idx_0, eV, e);
            }

            SoundVS.Sync(eV);
            SupportVS.Sync(e, eV);
            CloudVS.Run(eV, e);
            RotateAllVS.Rotate(eV, e);
        }
    }
}