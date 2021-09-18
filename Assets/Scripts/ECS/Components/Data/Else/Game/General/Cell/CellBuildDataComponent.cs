using System;

internal struct CellBuildDataComponent
{
    internal BuildingTypes BuildType;

    internal bool HaveBuild => BuildType != BuildingTypes.None;
    internal bool IsBuildType(BuildingTypes buildingType) => BuildType == buildingType;
    internal void DefBuildType() => BuildType = default;

    internal int PowerProtectionUnit(UnitTypes unitType)
    {
        var powerProtection = 0;
        switch (BuildType)
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
