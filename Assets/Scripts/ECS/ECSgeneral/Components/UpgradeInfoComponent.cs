using System.Collections.Generic;

internal struct UpgradeInfoComponent
{
    internal Dictionary<bool, int> AmountUpgradePawnDict;
    internal Dictionary<bool, int> AmountUpgradeRookDict;
    internal Dictionary<bool, int> AmountUpgradeBishopDict;

    internal Dictionary<bool, int> AmountUpgradeFarmDict;
    internal Dictionary<bool, int> AmountUpgradeWoodcutterDict;
    internal Dictionary<bool, int> AmountUpgradeMineDict;
}
