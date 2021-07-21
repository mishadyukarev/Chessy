using static Assets.Scripts.CellUnitWorker;

internal struct UnitTypeComponent
{
    internal UnitTypes UnitType { get; set; }
    internal bool HaveAnyUnit => UnitType != UnitTypes.None;

    internal bool IsMelee => IsMelee(UnitType);
    internal int SimplePowerDamage => SimplePowerDamage(UnitType);
    internal int UniquePowerDamage => UniquePowerDamage(UnitType);
    internal int MaxAmountHealth => MaxAmountHealth(UnitType);


    internal void StartFill(UnitTypes unitType = default) => UnitType = unitType;

    internal bool IsUnit(UnitTypes unitType) => UnitType == unitType;
    internal void ResetUnit() => UnitType = default;
}
