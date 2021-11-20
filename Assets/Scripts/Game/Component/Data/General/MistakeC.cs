using System.Collections.Generic;

namespace Game.Game
{
    public struct MistakeC
    {
        private static Dictionary<ResTypes, int> _needRes;

        public static MistakeTypes MistakeType { get; set; }
        public static float CurTime { get; set; }

        public MistakeC(Dictionary<ResTypes, int> needResources) : this()
        {
            _needRes = needResources;

            for (var resType = ResTypes.First; resType < ResTypes.End; resType++)
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
            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                _needRes[res] = default;
            }
        }
    }
}
