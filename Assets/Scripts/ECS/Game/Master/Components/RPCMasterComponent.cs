using Assets.Scripts.Abstractions.Enums;

internal struct RPCMasterComponent
{
    internal UniqueAbilitiesPawnTypes UniqueAbilitiesPawnType;
    internal int[] XyCell;
    internal BuildingTypes BuildingType;
    internal int[] XySelected;
    internal int[] XyPrevious;
    internal UnitTypes UnitType;
    internal UpgradeModTypes UpgradeModType;
}
