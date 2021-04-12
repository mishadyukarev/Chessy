using Photon.Realtime;

public struct SelectedUnitComponent
{
    private UnitTypes _selectedUnitType;

    internal bool IsSelectedUnit => _selectedUnitType != UnitTypes.None;
    internal UnitTypes SelectedUnitType => _selectedUnitType;


    internal void SetReset(UnitTypes unitType)
    {
        _selectedUnitType = unitType;
    }
}