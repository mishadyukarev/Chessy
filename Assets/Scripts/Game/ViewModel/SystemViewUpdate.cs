namespace Chessy.Game
{
    public static class SystemViewUpdate
    {
        public static void Run(in EntitiesView eV, in EntitiesModel e)
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

                eV.CellEs(idx_0).UnitVEs.EffectVEs.SyncVision(e.UnitEs(idx_0), idx_0 == e.SelectedIdxC.Idx);
                SyncStunVS.Sync(idx_0, eV, e);
                ShieldVS.Run(idx_0, eV, e);
            }

            SoundVS.Sync(eV);
            SupportVS.Sync(e, eV);
            CloudVS.Run(eV, e);
            RotateAllVS.Run(eV, e);
        }
    }
}