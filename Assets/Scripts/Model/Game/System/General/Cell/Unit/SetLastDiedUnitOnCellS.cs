namespace Chessy.Game.Model.System
{
    sealed partial class UnitSystems
    {
        internal void SetLastDiedUnitOnCell(in UnitTypes unitT, in LevelTypes levelT, in PlayerTypes playerT, in byte cell_0)
        {
            _eMG.LastDiedUnitTC(cell_0).UnitT = unitT;
            _eMG.LastDiedLevelTC(cell_0).LevelT = levelT;
            _eMG.LastDiedPlayerTC(cell_0).PlayerT = playerT;
        }

        internal void SetLastDiedUnitOnCell(in byte cell_from, in byte cell_to)
        {
            _eMG.LastDiedUnitTC(cell_to) = _eMG.LastDiedUnitTC(cell_from);
            _eMG.LastDiedPlayerTC(cell_to) = _eMG.LastDiedPlayerTC(cell_from);
            _eMG.LastDiedLevelTC(cell_to) = _eMG.LastDiedLevelTC(cell_from);
        }
        internal void SetLastDiedUnitOnCell(in byte cell_0)
        {
            _eMG.LastDiedUnitTC(cell_0) = _eMG.UnitTC(cell_0);
            _eMG.LastDiedPlayerTC(cell_0) = _eMG.UnitPlayerTC(cell_0);
            _eMG.LastDiedLevelTC(cell_0) = _eMG.UnitLevelTC(cell_0);
        }
    }
}