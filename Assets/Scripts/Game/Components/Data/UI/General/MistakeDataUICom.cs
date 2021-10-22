using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    internal struct MistakeDataUICom
    {
        private Dictionary<ResourceTypes, int> _needRes;

        internal MistakeTypes MistakeType { get; set; }
        internal float CurTime { get; set; }

        internal MistakeDataUICom(Dictionary<ResourceTypes, int> needResources) : this()
        {
            _needRes = needResources;

            for (var resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                _needRes.Add(resType, default);
            }
        }

        internal void ResetMistakeType() => MistakeType = default;

        internal bool NeedRes(ResourceTypes resType) => _needRes[resType] < 0;
        internal int NeedResAmount(ResourceTypes resType) => _needRes[resType];
        internal void AddNeedRes(ResourceTypes resType, int amount) => _needRes[resType] = amount;
        internal void ClearAllNeeds()
        {
            for (var resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                _needRes[resType] = default;
            }
        }
    }
}
