using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    public struct MistakeDataUIC
    {
        private static Dictionary<ResourceTypes, int> _needRes;

        public static MistakeTypes MistakeType { get; set; }
        public static float CurTime { get; set; }

        public MistakeDataUIC(Dictionary<ResourceTypes, int> needResources) : this()
        {
            _needRes = needResources;

            for (var resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                _needRes.Add(resType, default);
            }
        }

        public static void ResetMistakeType() => MistakeType = default;

        public static bool NeedRes(ResourceTypes resType) => _needRes[resType] < 0;
        public static int NeedResAmount(ResourceTypes resType) => _needRes[resType];
        public static void AddNeedRes(ResourceTypes resType, int amount) => _needRes[resType] = amount;
        public static void ClearAllNeeds()
        {
            for (var resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                _needRes[resType] = default;
            }
        }
    }
}
