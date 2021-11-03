using System.Collections.Generic;

namespace Scripts.Game
{
    public struct GiveTakeDataUIC
    {
        private static Dictionary<ToolWeaponTypes, LevelTWTypes> _curToolWeap;

        public GiveTakeDataUIC(bool needNew) : this()
        {
            if (needNew)
            {
                _curToolWeap = new Dictionary<ToolWeaponTypes, LevelTWTypes>();

                _curToolWeap.Add(ToolWeaponTypes.Pick, LevelTWTypes.Iron);
                _curToolWeap.Add(ToolWeaponTypes.Sword, LevelTWTypes.Iron);
                _curToolWeap.Add(ToolWeaponTypes.Shield, LevelTWTypes.Wood);
            }
        }

        public static LevelTWTypes Level(ToolWeaponTypes tw) => _curToolWeap[tw];
        public static void SetLevel(ToolWeaponTypes tw, LevelTWTypes levelTW) => _curToolWeap[tw] = levelTW;
    }
}