﻿using System.Collections.Generic;

internal struct BuildingsInfoComponent
{
    internal Dictionary<bool, bool> IsSettedCityDict;
    internal Dictionary<bool, int[]> XySettedCityDict;

    internal Dictionary<bool, int> AmountFarmDict;
    internal Dictionary<bool, int> AmountWoodcutterDict;
    internal Dictionary<bool, int> AmountMineDict;
}
