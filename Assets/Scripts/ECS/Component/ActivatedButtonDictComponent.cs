using System.Collections.Generic;

internal struct ActivatedButtonDictComponent
{
    internal Dictionary<bool, bool> IsActivatedButtonDict { get; private set; }

    internal ActivatedButtonDictComponent(Dictionary<bool, bool> dict)
    {
        IsActivatedButtonDict = dict;

        IsActivatedButtonDict.Add(true, default);
        IsActivatedButtonDict.Add(false, default);
    }
}