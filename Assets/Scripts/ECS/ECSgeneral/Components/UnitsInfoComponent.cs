using System.Collections.Generic;

internal struct UnitsInfoComponent
{
    private Dictionary<bool, bool> _isSettedKingDict;

    private Dictionary<bool, int> _amountKingDict;
    private Dictionary<bool, int> _amountPawnDict;
    private Dictionary<bool, int> _amountRookDict;
    private Dictionary<bool, int> _amountBishopDict;


    internal Dictionary<bool, bool> IsSettedKingDict => _isSettedKingDict;

    internal Dictionary<bool, int> AmountKingDict => _amountKingDict;
    internal Dictionary<bool, int> AmountPawnDict => _amountPawnDict;
    internal Dictionary<bool, int> AmountRookDict => _amountRookDict;
    internal Dictionary<bool, int> AmountBishopDict => _amountBishopDict;

    internal UnitsInfoComponent(StartValuesGameConfig startValuesGameConfig)
    {
        _isSettedKingDict = new Dictionary<bool, bool>();

        _amountKingDict = new Dictionary<bool, int>();
        _amountPawnDict = new Dictionary<bool, int>();
        _amountRookDict = new Dictionary<bool, int>();
        _amountBishopDict = new Dictionary<bool, int>();

        _isSettedKingDict.Add(true, default);
        _isSettedKingDict.Add(false, default);

        _amountKingDict.Add(true, default);
        _amountKingDict.Add(false, default);

        _amountPawnDict.Add(true, default);
        _amountPawnDict.Add(false, default);

        _amountRookDict.Add(true, default);
        _amountRookDict.Add(false, default);

        _amountBishopDict.Add(true, default);
        _amountBishopDict.Add(false, default);
    }
}
