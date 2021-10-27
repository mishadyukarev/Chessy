using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    internal struct MistakeDataUIC
    {
        private static Dictionary<ResourceTypes, int> _needRes;

        internal static MistakeTypes MistakeType { get; set; }
        internal static float CurTime { get; set; }

        internal MistakeDataUIC(Dictionary<ResourceTypes, int> needResources) : this()
        {
            _needRes = needResources;

            for (var resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                _needRes.Add(resType, default);
            }
        }

        internal static void ResetMistakeType() => MistakeType = default;

        internal static bool NeedRes(ResourceTypes resType) => _needRes[resType] < 0;
        internal static int NeedResAmount(ResourceTypes resType) => _needRes[resType];
        internal static void AddNeedRes(ResourceTypes resType, int amount) => _needRes[resType] = amount;
        internal static void ClearAllNeeds()
        {
            for (var resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                _needRes[resType] = default;
            }
        }
    }
}
