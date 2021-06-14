using System;
using System.Collections.Generic;

internal struct UpgradeUnitsComponent : IDisposable
{
    private Dictionary<bool, int> _amountUpgradePawnDict;
    private Dictionary<bool, int> _amountUpgradeRookDict;
    private Dictionary<bool, int> _amountUpgradeBishopDict;

    internal void CreateDict()
    {
        _amountUpgradePawnDict = new Dictionary<bool, int>();
        _amountUpgradeRookDict = new Dictionary<bool, int>();
        _amountUpgradeBishopDict = new Dictionary<bool, int>();


        _amountUpgradePawnDict.Add(true, default);
        _amountUpgradePawnDict.Add(false, default);

        _amountUpgradeRookDict.Add(true, default);
        _amountUpgradeRookDict.Add(false, default);

        _amountUpgradeBishopDict.Add(true, default);
        _amountUpgradeBishopDict.Add(false, default);
    }

    internal int AmountUpgrades(UnitTypes unitType, bool isMaster)
    {
        switch (unitType)
        {
            case UnitTypes.None:
                throw new Exception();

            case UnitTypes.King:
                throw new Exception();

            case UnitTypes.Pawn:
                return _amountUpgradePawnDict[isMaster];

            case UnitTypes.Rook:
                return _amountUpgradeRookDict[isMaster];

            case UnitTypes.Bishop:
                return _amountUpgradeBishopDict[isMaster];

            default:
                throw new Exception();
        }
    }

    internal void SetAmountUpgrades(UnitTypes unitType, bool isMaster, int value)
    {
        switch (unitType)
        {
            case UnitTypes.None:
                throw new Exception();

            case UnitTypes.King:
                throw new Exception();

            case UnitTypes.Pawn:
                _amountUpgradePawnDict[isMaster] = value;
                break;

            case UnitTypes.Rook:
                _amountUpgradeRookDict[isMaster] = value;
                break;

            case UnitTypes.Bishop:
                _amountUpgradeBishopDict[isMaster] = value;
                break;

            default:
                throw new Exception();
        }
    }

    internal void AddAmountUpgrades(UnitTypes unitType, bool isMaster, int adding = 1)
    {
        switch (unitType)
        {
            case UnitTypes.None:
                throw new Exception();

            case UnitTypes.King:
                throw new Exception();

            case UnitTypes.Pawn:
                _amountUpgradePawnDict[isMaster] += adding;
                break;

            case UnitTypes.Rook:
                _amountUpgradeRookDict[isMaster] += adding;
                break;

            case UnitTypes.Bishop:
                _amountUpgradeBishopDict[isMaster] += adding;
                break;

            default:
                throw new Exception();
        }
    }

    public void Dispose()
    {

    }
}
