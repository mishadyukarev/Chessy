internal struct UnitTypeComponent
{
    internal UnitTypes UnitType { get; set; }

    internal void StartFill(UnitTypes unitType = default) => UnitType = unitType;
}
