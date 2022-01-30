namespace Game.Game
{
    public abstract class SystemCellAbstract : SystemAbstract
    {
        protected readonly CellEs CellEs;
        protected readonly CellUnitEs UnitEs;
        protected readonly CellEnvironmentEs EnvironmentEs;
        protected readonly CellRiverEs RiverEs;
        protected readonly CellTrailEs TrailEs;

        protected SystemCellAbstract(in Entities ents) : base(ents)
        {
            CellEs = ents.CellEs;
            UnitEs = CellEs.UnitEs; 
            EnvironmentEs = CellEs.EnvironmentEs;
            RiverEs = CellEs.RiverEs;
            TrailEs = CellEs.TrailEs;
        }
    }
}