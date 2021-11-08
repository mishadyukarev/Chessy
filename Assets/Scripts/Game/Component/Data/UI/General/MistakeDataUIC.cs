using System.Collections.Generic;

namespace Chessy.Game
{
    public struct MistakeDataUIC
    {
        private static Dictionary<ResTypes, int> _needRes;

        public static MistakeTypes MistakeType { get; set; }
        public static float CurTime { get; set; }

        public MistakeDataUIC(Dictionary<ResTypes, int> needResources) : this()
        {
            _needRes = needResources;

            for (var resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                _needRes.Add(resType, default);
            }
        }

        public static void ResetMistakeType() => MistakeType = default;

        public static bool NeedRes(ResTypes resType) => _needRes[resType] < 0;
        public static int NeedResAmount(ResTypes resType) => _needRes[resType];
        public static void AddNeedRes(ResTypes resType, int amount) => _needRes[resType] = amount;
        public static void ClearAllNeeds()
        {
            for (var resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                _needRes[resType] = default;
            }
        }
    }
}
