using System;
using System.Collections.Generic;

internal struct UnitInventorComponent
{
    private Dictionary<bool, bool> _isSettedKingDict;

    private Dictionary<bool, int> _amountKingDict;
    private Dictionary<bool, int> _amountPawnDict;
    private Dictionary<bool, int> _amountPawnSwordDict;
    private Dictionary<bool, int> _amountRookDict;
    private Dictionary<bool, int> _amountRookCrossbowDict;
    private Dictionary<bool, int> _amountBishopDict;
    private Dictionary<bool, int> _amountBishopCrossbowDict;

    internal void StartFill()
    {
        _isSettedKingDict = new Dictionary<bool, bool>();

        _amountKingDict = new Dictionary<bool, int>();
        _amountPawnDict = new Dictionary<bool, int>();
        _amountPawnSwordDict = new Dictionary<bool, int>();
        _amountRookDict = new Dictionary<bool, int>();
        _amountRookCrossbowDict = new Dictionary<bool, int>();
        _amountBishopDict = new Dictionary<bool, int>();
        _amountBishopCrossbowDict = new Dictionary<bool, int>();

        _isSettedKingDict.Add(true, default);
        _isSettedKingDict.Add(false, default);

        _amountKingDict.Add(true, default);
        _amountKingDict.Add(false, default);

        _amountPawnDict.Add(true, default);
        _amountPawnDict.Add(false, default);

        _amountPawnSwordDict.Add(true, default);
        _amountPawnSwordDict.Add(false, default);

        _amountRookDict.Add(true, default);
        _amountRookDict.Add(false, default);

        _amountRookCrossbowDict.Add(true, default);
        _amountRookCrossbowDict.Add(false, default);

        _amountBishopDict.Add(true, default);
        _amountBishopDict.Add(false, default);

        _amountBishopCrossbowDict.Add(true, default);
        _amountBishopCrossbowDict.Add(false, default);
    }

    internal bool IsSettedKing(bool isMaster) => _isSettedKingDict[isMaster];
    internal void SetSettedKing(bool isMaster, bool isSetted) => _isSettedKingDict[isMaster] = isSetted;

    internal int AmountUnits(UnitTypes unitType, bool isMaster)
    {
        switch (unitType)
        {
            case UnitTypes.None:
                throw new Exception();

            case UnitTypes.King:
                return _amountKingDict[isMaster];

            case UnitTypes.Pawn:
                return _amountPawnDict[isMaster];

            case UnitTypes.PawnSword:
                return _amountPawnSwordDict[isMaster];

            case UnitTypes.Rook:
                return _amountRookDict[isMaster];

            case UnitTypes.RookCrossbow:
                return _amountRookCrossbowDict[isMaster];

            case UnitTypes.Bishop:
                return _amountBishopDict[isMaster];

            case UnitTypes.BishopCrossbow:
                return _amountBishopCrossbowDict[isMaster];

            default:
                throw new Exception();
        }
    }

    internal bool HaveUnit(UnitTypes unitType, bool isMaster)
    {
        switch (unitType)
        {
            case UnitTypes.None:
                throw new Exception();

            case UnitTypes.King:
                return _amountKingDict[isMaster] >= 1;

            case UnitTypes.Pawn:
                return _amountPawnDict[isMaster] >= 1;

            case UnitTypes.PawnSword:
                return _amountPawnSwordDict[isMaster] >= 1;

            case UnitTypes.Rook:
                return _amountRookDict[isMaster] >= 1;

            case UnitTypes.RookCrossbow:
                return _amountRookCrossbowDict[isMaster] >= 1;

            case UnitTypes.Bishop:
                return _amountBishopDict[isMaster] >= 1;

            case UnitTypes.BishopCrossbow:
                return _amountBishopCrossbowDict[isMaster] >= 1;

            default:
                throw new Exception();
        }
    }

    internal void SetAmountUnits(UnitTypes unitType, bool isMaster, int value)
    {
        switch (unitType)
        {
            case UnitTypes.None:
                throw new Exception();

            case UnitTypes.King:
                _amountKingDict[isMaster] = value;
                break;

            case UnitTypes.Pawn:
                _amountPawnDict[isMaster] = value;
                break;

            case UnitTypes.PawnSword:
                _amountPawnSwordDict[isMaster] = value;
                break;

            case UnitTypes.Rook:
                _amountRookDict[isMaster] = value;
                break;

            case UnitTypes.RookCrossbow:
                _amountRookCrossbowDict[isMaster] = value;
                break;

            case UnitTypes.Bishop:
                _amountBishopDict[isMaster] = value;
                break;

            case UnitTypes.BishopCrossbow:
                _amountBishopCrossbowDict[isMaster] = value;
                break;

            default:
                throw new Exception();
        }
    }

    internal void AddAmountUnits(UnitTypes unitType, bool isMaster, int adding = 1)
    {
        switch (unitType)
        {
            case UnitTypes.None:
                throw new Exception();

            case UnitTypes.King:
                _amountKingDict[isMaster] += adding;
                break;

            case UnitTypes.Pawn:
                _amountPawnDict[isMaster] += adding;
                break;

            case UnitTypes.PawnSword:
                _amountPawnSwordDict[isMaster] += adding;
                break;

            case UnitTypes.Rook:
                _amountRookDict[isMaster] += adding;
                break;

            case UnitTypes.RookCrossbow:
                _amountRookCrossbowDict[isMaster] += adding;
                break;

            case UnitTypes.Bishop:
                _amountBishopDict[isMaster] += adding;
                break;

            case UnitTypes.BishopCrossbow:
                _amountBishopCrossbowDict[isMaster] += adding;
                break;

            default:
                throw new Exception();
        }
    }

    internal void TakeAmountUnits(UnitTypes unitType, bool isMaster, int taking = -1)
    {
        switch (unitType)
        {
            case UnitTypes.None:
                throw new Exception();

            case UnitTypes.King:
                _amountKingDict[isMaster] += taking;
                break;

            case UnitTypes.Pawn:
                _amountPawnDict[isMaster] += taking;
                break;

            case UnitTypes.PawnSword:
                _amountPawnSwordDict[isMaster] += taking;
                break;

            case UnitTypes.Rook:
                _amountRookDict[isMaster] += taking;
                break;

            case UnitTypes.RookCrossbow:
                _amountRookCrossbowDict[isMaster] += taking;
                break;

            case UnitTypes.Bishop:
                _amountBishopDict[isMaster] += taking;
                break;

            case UnitTypes.BishopCrossbow:
                _amountBishopCrossbowDict[isMaster] += taking;
                break;

            default:
                throw new Exception();
        }
    }
}
