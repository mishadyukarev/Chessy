namespace Game.Game
{
    public abstract class SystemAbstract
    {
        protected readonly Entities Es;

        protected CellSpaceWorker CellWorker => Es.CellSpaceWorker;

        protected CellEs CellEs(in byte idx) => Es.CellEs(idx);

        protected CellUnitEs UnitEs(in byte idx) => Es.UnitEs(idx);

        protected CellBuildingEs BuildEs(in byte idx) => Es.BuildingEs(idx);
        protected CellEnvironmentEs EnvironmentEs(in byte idx) => CellEs(idx).EnvironmentEs;
        protected CellTrailEs TrailEs(in byte idx) => CellEs(idx).TrailEs;

        protected SystemAbstract(in Entities ents)
        {
            Es = ents;
        }
    }
}