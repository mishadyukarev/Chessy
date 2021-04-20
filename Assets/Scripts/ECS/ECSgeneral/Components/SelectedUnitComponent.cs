public struct SelectedUnitComponent
{
    private UnitTypes _selectedUnitType;

    internal bool IsSelectedUnit => _selectedUnitType != UnitTypes.None;
    internal UnitTypes SelectedUnitType => _selectedUnitType;

    internal void ResetSelectedUnit() => SetSelectedUnit(UnitTypes.None);
    internal void SetSelectedUnit(UnitTypes unitType) => _selectedUnitType = unitType;
}