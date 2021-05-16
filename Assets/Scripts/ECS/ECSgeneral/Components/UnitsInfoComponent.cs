using static MainGame;

internal struct UnitsInfoComponent
{
    internal int AmountKingMaster;
    internal int AmountKingOther;

    internal bool IsSettedKingMaster;
    internal bool IsSettedKingOther;

    internal int AmountUnitPawnMaster;
    internal int AmountUnitPawnOther;

    internal int AmountRookMaster;
    internal int AmountRookOther;

    internal int AmountBishopMaster;
    internal int AmountBishopOther;


    internal UnitsInfoComponent(StartValuesGameConfig startValues)
    {
        AmountKingMaster = startValues.AMOUNT_KING_MASTER;
        AmountKingOther = startValues.AMOUNT_KING_OTHER;

        AmountUnitPawnMaster = startValues.AMOUNT_PAWN_MASTER;
        AmountUnitPawnOther = startValues.AMOUNT_PAWN_OTHER;

        AmountRookMaster = startValues.AMOUNT_ROOK_MASTER;
        AmountRookOther = startValues.AMOUNT_ROOK_OTHER;

        AmountBishopMaster = startValues.AMOUNT_BISHOP_MASTER;
        AmountBishopOther = startValues.AMOUNT_BISHOP_OTHER;

        IsSettedKingMaster = false;
        IsSettedKingOther = false;
    }

    internal bool IsSettedCurrentKing
    {
        get
        {
            if (InstanceGame.IsMasterClient) return IsSettedKingMaster;
            else return IsSettedKingOther;
        }
        set
        {
            if (InstanceGame.IsMasterClient) IsSettedKingMaster = value;
            else IsSettedKingOther = value;
        }
    }
}
