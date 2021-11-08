using System.Collections.Generic;

namespace Chessy.Game
{
    public struct TwGiveTakeC
    {
        private static Dictionary<ToolWeaponTypes, LevelTWTypes> _curToolWeap;

        public static ToolWeaponTypes TWTypeForGive { get; set; }
        public static LevelTWTypes Level(ToolWeaponTypes tw) => _curToolWeap[tw];
        public static bool IsSelTW => TWTypeForGive != default;

        static TwGiveTakeC()
        {
            _curToolWeap = new Dictionary<ToolWeaponTypes, LevelTWTypes>();

            _curToolWeap.Add(ToolWeaponTypes.Pick, LevelTWTypes.Iron);
            _curToolWeap.Add(ToolWeaponTypes.Sword, LevelTWTypes.Iron);
            _curToolWeap.Add(ToolWeaponTypes.Shield, LevelTWTypes.Wood);
        }


        public static void ResetTW() => TWTypeForGive = default;
        public static void SetLevel(ToolWeaponTypes tw, LevelTWTypes levelTW) => _curToolWeap[tw] = levelTW;
    }
}