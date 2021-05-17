public struct SelectedUnitComponent
{
    internal UnitTypes SelectedUnitType;

    internal bool IsSelectedUnit => SelectedUnitType != UnitTypes.None;
}