using System.Collections.Generic;

internal struct BuildingsInfoComponent
{
    private Dictionary<bool, bool> _isSettedCityDict;
    private Dictionary<bool, int[]> _xySettedCityDict;
    private Dictionary<bool, int> _amountFarmDict;
    private Dictionary<bool, int> _amountWoodcutterDict;
    private Dictionary<bool, int> _amountMineDict;

    internal Dictionary<bool, bool> IsSettedCityDict => _isSettedCityDict;
    internal Dictionary<bool, int[]> XySettedCityDict => _xySettedCityDict;

    internal Dictionary<bool, int> AmountFarmDict => _amountFarmDict;
    internal Dictionary<bool, int> AmountWoodcutterDict => _amountWoodcutterDict;
    internal Dictionary<bool, int> AmountMineDict => _amountMineDict;


    internal BuildingsInfoComponent(StartValuesGameConfig startValuesGameConfig)
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