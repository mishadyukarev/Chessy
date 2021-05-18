using System.Collections.Generic;
using static MainGame;

internal struct UnitsInfoComponent
{
    internal Dictionary<bool, bool> IsSettedKingDictionary;

    internal Dictionary<bool, int> AmountKingDictionary;
    internal Dictionary<bool, int> AmountPawnDictionary;
    internal Dictionary<bool, int> AmountRookDictionary;
    internal Dictionary<bool, int> AmountBishopDictionary;
}
