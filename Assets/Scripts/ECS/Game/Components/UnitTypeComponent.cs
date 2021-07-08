using System;

internal struct UnitTypeComponent
{
    private UnitTypes _unitType;

    internal UnitTypes UnitType => _unitType;
    internal bool HaveUnit => UnitType != UnitTypes.None;
    internal bool IsMelee
    {
        get
        {
            switch (_unitType)
            {
                case UnitTypes.None:
                    return false;

                case UnitTypes.King:
                    return true;

                case UnitTypes.Pawn:
                    return true;

                case UnitTypes.PawnSword:
                    return true;

                case UnitTypes.Rook:
                    return false;

                case UnitTypes.RookCrossbow:
                    return false;

                case UnitTypes.Bishop:
                    return false;

                case UnitTypes.BishopCrossbow:
                    return false;

                default:
                    return false;
            }
        }
    }

    internal void StartFill() => _unitType = default;
    internal void SetUnitType(UnitTypes unitType) => _unitType = unitType;
    internal void ResetUnit() => _unitType = default;
}
