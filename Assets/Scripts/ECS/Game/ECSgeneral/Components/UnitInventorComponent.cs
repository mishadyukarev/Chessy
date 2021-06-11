using System;
using System.Collections.Generic;

internal struct UnitInventorComponent
{
    private Dictionary<bool, bool> _isSettedKingDict;

    private Dictionary<bool, int> _amountKingDict;
    private Dictionary<bool, int> _amountPawnDict;
    private Dictionary<bool, int> _amountRookDict;
    private Dictionary<bool, int> _amountBishopDict;

    internal void Fill()
    {
        _isSettedKingDict = new Dictionary<bool, bool>();

        _amountKingDict = new Dictionary<bool, int>();
        _amountPawnDict = new Dictionary<bool, int>();
        _amountRookDict = new Dictionary<bool, int>();
        _amountBishopDict = new Dictionary<bool, int>();

        _isSettedKingDict.Add(true, default);
        _isSettedKingDict.Add(false, default);

        _amountKingDict.Add(true, default);
        _amountKingDict.Add(false, default);

        _amountPawnDict.Add(true, default);
        _amountPawnDict.Add(false, default);

        _amountRookDict.Add(true, default);
        _amountRookDict.Add(false, default);

        _amountBishopDict.Add(true, default);
        _amountBishopDict.Add(false, default);
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

            case UnitTypes.Rook:
                return _amountRookDict[isMaster];

            case UnitTypes.Bishop:
                return _amountBishopDict[isMaster];

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

            case UnitTypes.Rook:
                _amountRookDict[isMaster] = value;
                break;

            case UnitTypes.Bishop:
                _amountBishopDict[isMaster] = value;
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

            case UnitTypes.Rook:
                _amountRookDict[isMaster] += adding;
                break;

            case UnitTypes.Bishop:
                _amountBishopDict[isMaster] += adding;
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

            case UnitTypes.Rook:
                _amountRookDict[isMaster] += taking;
                break;

            case UnitTypes.Bishop:
                _amountBishopDict[isMaster] += taking;
                break;

            default:
                throw new Exception();
        }
    }
}
