namespace Game.Game
{
    public abstract class SystemAbstract
    {
        protected readonly Entities Es;

        protected CellEs CellEs => Es.CellEs;

        protected CellUnitEs UnitEs => CellEs.UnitEs;
        protected CellUnitStatEs UnitStatEs => UnitEs.StatEs;

        protected CellBuildEs BuildEs => CellEs.BuildEs;
        protected CellEnvironmentEs EnvironmentEs => CellEs.EnvironmentEs;
        protected CellRiverEs RiverEs => CellEs.RiverEs;
        protected CellTrailEs TrailEs => CellEs.TrailEs;

        protected SystemAbstract(in Entities ents)
        {
            Es = ents;
        }
    }
}