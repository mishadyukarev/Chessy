internal struct UnitTypeComponent
{
    internal UnitTypes UnitType;

    internal bool HaveUnit => UnitType != UnitTypes.None;
}
