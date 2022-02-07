namespace Game.Game
{
    public abstract class SystemAbstract
    {
        protected readonly Entities Es;

        protected CellEsSpaceWorker CellWorker => Es.CellWorker;

        protected CellEs CellEs(in byte idx) => Es.CellEs(idx);

        protected CellUnitEs UnitEs(in byte idx) => Es.UnitEs(idx);
        protected CellUnitStatEs UnitStatEs(in byte idx) => Es.UnitStatEs(idx);
        protected CellUnitEffectEs UnitEffectEs(in byte idx) => Es.UnitEffectEs(idx);

        protected CellBuildEs BuildEs(in byte idx) => Es.BuildEs(idx);
        protected CellEnvironmentEs EnvironmentEs(in byte idx) => CellEs(idx).EnvironmentEs;
        protected CellRiverEs RiverEs(in byte idx) => CellEs(idx).RiverEs;
        protected CellTrailEs TrailEs(in byte idx) => CellEs(idx).TrailEs;
        protected CellEffectEs EffectEs(in byte idx) => CellEs(idx).EffectEs;

        protected SystemAbstract(in Entities ents)
        {
            Es = ents;
        }
    }
}