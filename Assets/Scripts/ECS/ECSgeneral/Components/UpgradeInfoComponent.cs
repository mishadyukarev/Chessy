using System.Collections.Generic;

internal struct UpgradeInfoComponent
{
    private Dictionary<bool, int> _amountUpgradePawnDict;
    private Dictionary<bool, int> _amountUpgradeRookDict;
    private Dictionary<bool, int> _amountUpgradeBishopDict;

    private Dictionary<bool, int> _amountUpgradeFarmDict;
    private Dictionary<bool, int> _amountUpgradeWoodcutterDict;
    private Dictionary<bool, int> _amountUpgradeMineDict;


    internal Dictionary<bool, int> AmountUpgradePawnDict => _amountUpgradePawnDict;
    internal Dictionary<bool, int> AmountUpgradeRookDict => _amountUpgradeRookDict;
    internal Dictionary<bool, int> AmountUpgradeBishopDict => _amountUpgradeBishopDict;

    internal Dictionary<bool, int> AmountUpgradeFarmDict => _amountUpgradeFarmDict;
    internal Dictionary<bool, int> AmountUpgradeWoodcutterDict => _amountUpgradeWoodcutterDict;
    internal Dictionary<bool, int> AmountUpgradeMineDict => _amountUpgradeMineDict;

    internal UpgradeInfoComponent(StartValuesGameConfig startValuesGameConfig)
    {
        _amountUpgradePawnDict = new Dictionary<bool, int>();
        _amountUpgradeRookDict = new Dictionary<bool, int>();
        _amountUpgradeBishopDict = new Dictionary<bool, int>();

        _amountUpgradeFarmDict = new Dictionary<bool, int>();
        _amountUpgradeWoodcutterDict = new Dictionary<bool, int>();
        _amountUpgradeMineDict = new Dictionary<bool, int>();


        _amountUpgradePawnDict.Add(true, default);
        _amountUpgradePawnDict.Add(false, default);

        _amountUpgradeRookDict.Add(true, default);
        _amountUpgradeRookDict.Add(false, default);

        _amountUpgradeBishopDict.Add(true, default);
        _amountUpgradeBishopDict.Add(false, default);


        _amountUpgradeFarmDict.Add(true, default);
        _amountUpgradeFarmDict.Add(false, default);

        _amountUpgradeWoodcutterDict.Add(true, default);
        _amountUpgradeWoodcutterDict.Add(false, default);

        _amountUpgradeMineDict.Add(true, default);
        _amountUpgradeMineDict.Add(false, default);
    }
}
