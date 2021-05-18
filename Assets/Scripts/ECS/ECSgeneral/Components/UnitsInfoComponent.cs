﻿using System.Collections.Generic;
using static MainGame;

internal struct UnitsInfoComponent
{
    internal Dictionary<bool, bool> IsSettedKingDict;

    internal Dictionary<bool, int> AmountKingDict;
    internal Dictionary<bool, int> AmountPawnDict;
    internal Dictionary<bool, int> AmountRookDict;
    internal Dictionary<bool, int> AmountBishopDict;
}
