using System.Collections.Generic;
using static MainGame;

internal struct BuildingsInfoComponent
{
    internal Dictionary<bool, bool> IsBuildedCityDictionary;
    internal Dictionary<bool, int[]> XYsettedCityDictionary;

    internal Dictionary<bool, int> AmountFarmDict;
    internal Dictionary<bool, int> AmountWoodcutterDict;
    internal Dictionary<bool, int> AmountMineDict;
}
