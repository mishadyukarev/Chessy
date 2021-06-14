using System;
using System.Collections.Generic;

internal struct BuildingsComponent
{
    private Dictionary<bool, bool> _isSettedCityDict;
    private Dictionary<bool, int[]> _xySettedCityDict;
    private Dictionary<bool, int> _amountFarmDict;
    private Dictionary<bool, int> _amountWoodcutterDict;
    private Dictionary<bool, int> _amountMineDict;

    internal Dictionary<bool, bool> IsSettedCityDict => _isSettedCityDict;
    internal Dictionary<bool, int[]> XySettedCityDict => _xySettedCityDict;

    internal void CreateDict()
    {
        _isSettedCityDict = new Dictionary<bool, bool>();
        _xySettedCityDict = new Dictionary<bool, int[]>();
        _amountFarmDict = new Dictionary<bool, int>();
        _amountWoodcutterDict = new Dictionary<bool, int>();
        _amountMineDict = new Dictionary<bool, int>();

        _isSettedCityDict.Add(true, default);
        _isSettedCityDict.Add(false, default);

        _xySettedCityDict.Add(true, default);
        _xySettedCityDict.Add(false, default);

        _amountFarmDict.Add(true, default);
        _amountFarmDict.Add(false, default);

        _amountWoodcutterDict.Add(true, default);
        _amountWoodcutterDict.Add(false, default);

        _amountMineDict.Add(true, default);
        _amountMineDict.Add(false, default);
    }


    internal int AmountBuildings(BuildingTypes buildingType, bool isMaster)
    {
        switch (buildingType)
        {
            case BuildingTypes.None:
                throw new Exception();

            case BuildingTypes.City:
                throw new Exception();

            case BuildingTypes.Farm:
                return _amountFarmDict[isMaster];

            case BuildingTypes.Woodcutter:
                return _amountWoodcutterDict[isMaster];

            case BuildingTypes.Mine:
                return _amountMineDict[isMaster];

            default:
                throw new Exception();
        }
    }

    internal void SetAmountBuildings(BuildingTypes buildingType, bool isMaster, int value)
    {
        switch (buildingType)
        {
            case BuildingTypes.None:
                throw new Exception();

            case BuildingTypes.City:
                throw new Exception();

            case BuildingTypes.Farm:
                _amountFarmDict[isMaster] = value;
                break;

            case BuildingTypes.Woodcutter:
                _amountWoodcutterDict[isMaster] = value;
                break;

            case BuildingTypes.Mine:
                _amountMineDict[isMaster] = value;
                break;

            default:
                throw new Exception();
        }
    }

    internal void AddAmountBuildings(BuildingTypes buildingType, bool isMaster, int adding = 1)
    {
        switch (buildingType)
        {
            case BuildingTypes.None:
                throw new Exception();

            case BuildingTypes.City:
                //throw new Exception();
                break;

            case BuildingTypes.Farm:
                _amountFarmDict[isMaster] += adding;
                break;

            case BuildingTypes.Woodcutter:
                _amountWoodcutterDict[isMaster] += adding;
                break;

            case BuildingTypes.Mine:
                _amountMineDict[isMaster] += adding;
                break;

            default:
                throw new Exception();
        }
    }

    internal void TakeAmountBuildings(BuildingTypes buildingType, bool isMaster, int taking = -1)
    {
        switch (buildingType)
        {
            case BuildingTypes.None:
                throw new Exception();

            case BuildingTypes.City:
                //throw new Exception();
                break;

            case BuildingTypes.Farm:
                _amountFarmDict[isMaster] += taking;
                break;

            case BuildingTypes.Woodcutter:
                _amountWoodcutterDict[isMaster] += taking;
                break;

            case BuildingTypes.Mine:
                _amountMineDict[isMaster] += taking;
                break;

            default:
                throw new Exception();
        }
    }
}