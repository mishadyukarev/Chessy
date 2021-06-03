using System.Collections.Generic;

internal struct ReadyComponent
{
    private Dictionary<bool, bool> _isActivatedDict;

    internal bool IsSkipped;

    internal Dictionary<bool, bool> IsActivatedDictionary => _isActivatedDict;

    internal ReadyComponent(StartValuesGameConfig startValuesGameConfig)
    {
        IsSkipped = default;

        _isActivatedDict = new Dictionary<bool, bool>();
        _isActivatedDict.Add(true, default);
        _isActivatedDict.Add(false, default);
    }
}
