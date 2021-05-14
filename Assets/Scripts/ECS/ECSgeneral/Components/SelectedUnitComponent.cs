public struct SelectedUnitComponent
{
    private UnitTypes _selectedUnitType;

    internal bool IsSelectedUnit => _selectedUnitType != UnitTypes.None;
    internal UnitTypes SelectedUnitType
    {
        get => _selectedUnitType;
        set => _selectedUnitType = value;
    }
}