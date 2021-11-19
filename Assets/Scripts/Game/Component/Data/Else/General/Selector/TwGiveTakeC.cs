using System.Collections.Generic;

namespace Game.Game
{
    public struct TwGiveTakeC
    {
        private static TWTypes _tWType;
        private static Dictionary<TWTypes, LevelTypes> _curTW;

        public static TWTypes TWTypeForGive => _tWType;
        public static LevelTypes Level(TWTypes tw) => _curTW[tw];


        static TwGiveTakeC()
        {
            _curTW = new Dictionary<TWTypes, LevelTypes>();

            _curTW.Add(TWTypes.Pick, LevelTypes.Second);
            _curTW.Add(TWTypes.Sword, LevelTypes.Second);
            _curTW.Add(TWTypes.Shield, LevelTypes.First);
        }


        public static void SetInDown(TWTypes tw, LevelTypes levelTW) => _curTW[tw] = levelTW;
        public static void Set(TWTypes tw)
        {
            _tWType = tw;
        }
    }
}