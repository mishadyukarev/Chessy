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

                    case UnitTypes.Pawn:
                        powerProtection += PROTECTION_CITY_PAWN;
                        break;

                    case UnitTypes.Rook:
                        powerProtection += PROTECTION_CITY_ROOK;
                        break;

                    case UnitTypes.RookCrossbow:
                        powerProtection += PROTECTION_CITY_ROOK_CROSSBOW;
                        break;

                    case UnitTypes.Bishop:
                        powerProtection += PROTECTION_CITY_BISHOP;
                        break;

                    case UnitTypes.BishopCrossbow:
                        powerProtection += PROTECTION_CITY_BISHOP_CROSSBOW;
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
