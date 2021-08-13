using static Assets.Scripts.Abstractions.ValuesConsts.UnitValues;

internal struct CellBuildDataComponent
{
    internal BuildingTypes BuildingType { get; set; }

    internal bool HaveBuild => BuildingType != BuildingTypes.None;
    internal bool IsBuildType(BuildingTypes buildingType) => BuildingType == buildingType;
    internal void ResetBuildType() => BuildingType = default;

    internal int PowerProtection(UnitTypes unitType)
    {
        var powerProtection = 0;
        switch (BuildingType)
        {
            case BuildingTypes.City:

                switch (unitType)
                {
                    case UnitTypes.King:
                        powerProtection += PROTECTION_CITY_KING;
                        break;

                    case UnitTypes.Pawn_Axe:
                        powerProtection += PROTECTION_CITY_PAWN;
                        break;

                    case UnitTypes.Rook_Bow:
                        powerProtection += PROTECTION_CITY_ROOK;
                        break;

                    case UnitTypes.Bishop_Bow:
                        powerProtection += PROTECTION_CITY_BISHOP;
                        break;
                }

                break;

            case BuildingTypes.Farm:
                powerProtection += 5;
                break;

            case BuildingTypes.Woodcutter:
                powerProtection += 5;
                break;

            case BuildingTypes.Mine:
                break;
        }

        return powerProtection;
    }
}
