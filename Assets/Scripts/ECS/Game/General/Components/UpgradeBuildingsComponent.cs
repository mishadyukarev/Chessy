using System;
using System.Collections.Generic;

internal struct UpgradeBuildingsComponent
{
    private Dictionary<bool, int> _amountUpgradeFarmDict;
    private Dictionary<bool, int> _amountUpgradeWoodcutterDict;
    private Dictionary<bool, int> _amountUpgradeMineDict;

    internal void CreateDict()
    {
        _amountUpgradeFarmDict = new Dictionary<bool, int>();
        _amountUpgradeWoodcutterDict = new Dictionary<bool, int>();
        _amountUpgradeMineDict = new Dictionary<bool, int>();


        _amountUpgradeFarmDict.Add(true, default);
        _amountUpgradeFarmDict.Add(false, default);

        _amountUpgradeWoodcutterDict.Add(true, default);
        _amountUpgradeWoodcutterDict.Add(false, default);

        _amountUpgradeMineDict.Add(true, default);
        _amountUpgradeMineDict.Add(false, default);


        _amountUpgradeFarmDict[true] += 1;
        _amountUpgradeFarmDict[false] += 1;

        _amountUpgradeWoodcutterDict[true] += 1;
        _amountUpgradeWoodcutterDict[false] += 1;

        _amountUpgradeMineDict[true] += 1;
        _amountUpgradeMineDict[false] += 1;
    }


    internal int AmountUpgrades(BuildingTypes buildingType, bool isMaster)
    {
        switch (buildingType)
        {
            case BuildingTypes.None:
                throw new Exception();

            case BuildingTypes.City:
                throw new Exception();

            case BuildingTypes.Farm:
                return _amountUpgradeFarmDict[isMaster];

            case BuildingTypes.Woodcutter:
                return _amountUpgradeWoodcutterDict[isMaster];

            case BuildingTypes.Mine:
                return _amountUpgradeMineDict[isMaster];

            default:
                throw new Exception();
        }
    }

    internal void SetAmountUpgrades(BuildingTypes buildingType, bool isMaster, int value)
    {
        switch (buildingType)
        {
            case BuildingTypes.None:
                throw new Exception();

            case BuildingTypes.City:
                throw new Exception();

            case BuildingTypes.Farm:
                _amountUpgradeFarmDict[isMaster] = value;
                break;

            case BuildingTypes.Woodcutter:
                _amountUpgradeWoodcutterDict[isMaster] = value;
                break;

            case BuildingTypes.Mine:
                _amountUpgradeMineDict[isMaster] = value;
                break;

            default:
                throw new Exception();
        }
    }

    internal void AddAmountUpgrades(BuildingTypes buildingType, bool isMaster, int adding = 1)
    {
        switch (buildingType)
        {
            case BuildingTypes.None:
                throw new Exception();

            case BuildingTypes.City:
                throw new Exception();

            case BuildingTypes.Farm:
                _amountUpgradeFarmDict[isMaster] += adding;
                break;

            case BuildingTypes.Woodcutter:
                _amountUpgradeWoodcutterDict[isMaster] += adding;
                break;

            case BuildingTypes.Mine:
                _amountUpgradeMineDict[isMaster] += adding;
                break;

            default:
                throw new Exception();
        }
    }
}