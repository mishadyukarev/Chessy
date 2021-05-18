using System.Collections.Generic;
using static MainGame;

internal struct BuildingsInfoComponent
{
    internal Dictionary<bool, bool> IsBuildedCityDictionary;
    internal Dictionary<bool, int[]> XYsettedCityDictionary;

    internal Dictionary<bool, int> AmountFarmDictionary;
    internal Dictionary<bool, int> AmountWoodcutterDictionary;
    internal Dictionary<bool, int> AmountMineDictionary;
}
