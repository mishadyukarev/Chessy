using System.Collections.Generic;

namespace Chessy.Game
{
    public struct TwGiveTakeC
    {
        private static Dictionary<TWTypes, LevelTypes> _curToolWeap;

        public static TWTypes TWTypeForGive { get; set; }
        public static LevelTypes Level(TWTypes tw) => _curToolWeap[tw];
        public static bool IsSelTW => TWTypeForGive != default;

        static TwGiveTakeC()
        {
            _curToolWeap = new Dictionary<TWTypes, LevelTypes>();

            _curToolWeap.Add(TWTypes.Pick, LevelTypes.Second);
            _curToolWeap.Add(TWTypes.Sword, LevelTypes.Second);
            _curToolWeap.Add(TWTypes.Shield, LevelTypes.First);
        }


        public static void ResetTW() => TWTypeForGive = default;
        public static void SetLevel(TWTypes tw, LevelTypes levelTW) => _curToolWeap[tw] = levelTW;
    }
}