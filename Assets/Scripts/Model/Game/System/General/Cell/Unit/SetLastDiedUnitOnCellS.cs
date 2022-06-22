namespace Chessy.Game.Model.System
{
    sealed partial class UnitSystems
    {
        internal void SetLastDiedUnitOnCell(in UnitTypes unitT, in LevelTypes levelT, in PlayerTypes playerT, in byte cell_0)
        {
            _e.SetLastDiedUnitT(cell_0, unitT);
            _e.SetLastDiedLevelT(cell_0, levelT);
            _e.SetLastDiedPlayerT(cell_0, playerT);
        }

        internal void SetLastDiedUnitOnCell(in byte cell_from, in byte cell_to)
        {
            _e.SetLastDiedUnitT(cell_to, _e.LastDiedUnitT(cell_from));
            _e.SetLastDiedPlayerT(cell_to, _e.LastDiedPlayerT(cell_from));
            _e.SetLastDiedLevelT(cell_to, _e.LastDiedLevelT(cell_from));
        }
        internal void SetLastDiedUnitOnCell(in byte cell_0)
        {
            _e.SetLastDiedUnitT(cell_0, _e.UnitT(cell_0));
            _e.SetLastDiedPlayerT(cell_0, _e.UnitPlayerT(cell_0));
            _e.SetLastDiedLevelT(cell_0, _e.UnitLevelT(cell_0));
        }
    }
}