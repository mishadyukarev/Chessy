internal struct UnitTypeComponent
{
    internal UnitTypes UnitType;

    internal bool HaveUnit => UnitType != UnitTypes.None;


    internal bool IsMelee
    {
        get
        {
            switch (UnitType)
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

    internal void StartFill()
    {
        UnitType = UnitTypes.None;
    }
}
