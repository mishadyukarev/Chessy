using System.Collections.Generic;

namespace Assets.Scripts.ECS.Components
{
    internal struct IsVisibleDictComponent
    {
        internal Dictionary<bool, bool> IsVisibleDict { get; private set; }

        internal IsVisibleDictComponent(Dictionary<bool, bool> isVisibleDict)
        {
            IsVisibleDict = isVisibleDict;

            IsVisibleDict.Add(true, default);
            IsVisibleDict.Add(false, default);
        }
    }
}
