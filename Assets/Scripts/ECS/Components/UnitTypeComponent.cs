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

    internal bool IsKing => _unitType == UnitTypes.King;
    internal bool IsPawn => _unitType == UnitTypes.Pawn;
    internal bool IsPawnSword => _unitType == UnitTypes.PawnSword;
    internal bool IsRook => _unitType == UnitTypes.Rook;
    internal bool IsRookCrossbow => _unitType == UnitTypes.RookCrossbow;
    internal bool IsBishop => _unitType == UnitTypes.Bishop;
    internal bool IsBishopCrossbow => _unitType == UnitTypes.BishopCrossbow;

    internal void StartFill() => _unitType = default;
    internal void SetUnitType(UnitTypes unitType) => _unitType = unitType;
    internal void ResetUnit() => _unitType = default;
}
