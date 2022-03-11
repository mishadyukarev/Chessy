using System;

namespace Chessy.Game
{
    public readonly struct SystemsView
    {
        public SystemsView(ref ActionC update, in EntitiesModel ents, in EntitiesView entsView)
        {
            update.Action +=
                (Action)
            new CellUnitVS(ents, entsView).Run
            + new CellUnitSelectedVS(ents, entsView).Run
            + new CellSupportVS(ents, entsView).Run
            + new UnitStatCellSyncS(ents, entsView).Run
            + new BuildCellVS(ents, entsView).Run
            + new EnvCellVS(ents, entsView).Run
            + new CellFireVS(ents, entsView).Run
            + new CellCloudVS(ents, entsView).Run
            + new RiverCellVS(ents, entsView).Run
            + new CellBarsEnvVS(ents, entsView).Run
            + new CellTrailVS(ents, entsView).Run
            + new CellUnitEffectFrozenArrawVS(ents, entsView).Run


            + new CellUnitEffectStunVS(ents, entsView).Run
            + new CellUnitEffectShieldVS(ents, entsView).Run

            + new RotateAllVS(ents, entsView).Run
            + new SoundVS(ents, entsView).Run;
        }
    }
}