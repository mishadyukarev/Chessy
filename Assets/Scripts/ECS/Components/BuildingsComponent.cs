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

    internal void StartFill()
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

}