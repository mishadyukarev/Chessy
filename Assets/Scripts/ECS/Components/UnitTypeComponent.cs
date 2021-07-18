using static Assets.Scripts.CellUnitWorker;

internal struct UnitTypeComponent
{
    private UnitTypes _unitType;

    internal UnitTypes UnitType => _unitType;
    internal bool HaveAnyUnit => _unitType != UnitTypes.None;

    internal bool IsMelee => IsMelee(_unitType);
    internal int SimplePowerDamage => SimplePowerDamage(_unitType);
    internal int UniquePowerDamage => UniquePowerDamage(_unitType);
    internal int MaxAmountHealth => MaxAmountHealth(_unitType);


    internal void StartFill(UnitTypes unitType = default) => _unitType = unitType;
    internal void SetUnitType(UnitTypes unitType) => _unitType = unitType;
    internal bool IsUnit(UnitTypes unitType) => _unitType == unitType;
    internal void ResetUnit() => _unitType = default;
}
