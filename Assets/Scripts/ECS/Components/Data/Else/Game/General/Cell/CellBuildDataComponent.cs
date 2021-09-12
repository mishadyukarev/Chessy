using System;

internal struct CellBuildDataComponent
{
    internal BuildingTypes BuildingType;

    internal bool HaveBuild => BuildingType != BuildingTypes.None;
    internal bool IsBuildType(BuildingTypes buildingType) => BuildingType == buildingType;
    internal void DefBuildType() => BuildingType = default;

    internal int PowerProtectionUnit(UnitTypes unitType)
    {
        var powerProtection = 0;
        switch (BuildingType)
        {
            case BuildingTypes.City:

                switch (unitType)
                {
                    case UnitTypes.None:
                        throw new Exception();

                    case UnitTypes.King:
                        powerProtection += 10;
                        break;

                    case UnitTypes.Pawn:
                        powerProtection += 10;
                        break;

                    case UnitTypes.Rook:
                        powerProtection += 10;
                        break;

                    case UnitTypes.Bishop:
                        powerProtection += 10;
                        break;

                    default:
                        throw new Exception();
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
