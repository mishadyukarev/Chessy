using System;

namespace Scripts.Game
{
    internal struct CellBuildDataComponent
    {
        internal BuildingTypes BuildType;

        internal bool HaveBuild => BuildType != BuildingTypes.None;
        internal bool IsBuildType(BuildingTypes buildingType) => BuildType == buildingType;
        internal void DefBuildType() => BuildType = default;

        internal int PowerProtectionUnit(UnitTypes unitType, float simPowerDamage)
        {
            float powerProtection = 0;
            switch (BuildType)
            {
                case BuildingTypes.City:

                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            powerProtection += simPowerDamage * 0.3f;
                            break;

                        case UnitTypes.Pawn:
                            powerProtection += simPowerDamage * 0.5f;
                            break;

                        case UnitTypes.Rook:
                            powerProtection += simPowerDamage * 0.5f;
                            break;

                        case UnitTypes.Bishop:
                            powerProtection += simPowerDamage * 0.5f;
                            break;

                        default:
                            throw new Exception();
                    }

                    break;

                case BuildingTypes.Farm:

                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            powerProtection += simPowerDamage * 0.3f;
                            break;

                        case UnitTypes.Pawn:
                            powerProtection += simPowerDamage * 0.3f;
                            break;

                        case UnitTypes.Rook:
                            powerProtection += simPowerDamage * 0.3f;
                            break;

                        case UnitTypes.Bishop:
                            powerProtection += simPowerDamage * 0.3f;
                            break;

                        default:
                            throw new Exception();
                    }

                    break;

                case BuildingTypes.Woodcutter:

                    switch (unitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            powerProtection += simPowerDamage * 0.1f;
                            break;

                        case UnitTypes.Pawn:
                            powerProtection += simPowerDamage * 0.3f;
                            break;

                        case UnitTypes.Rook:
                            powerProtection += simPowerDamage * 0.3f;
                            break;

                        case UnitTypes.Bishop:
                            powerProtection += simPowerDamage * 0.3f;
                            break;

                        default:
                            throw new Exception();
                    }

                    break;

                case BuildingTypes.Mine:
                    break;
            }

            return (int)powerProtection;
        }
    }
}