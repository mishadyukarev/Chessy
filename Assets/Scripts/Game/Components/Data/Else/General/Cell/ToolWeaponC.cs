using System;

namespace Scripts.Game
{
    public struct ToolWeaponC
    {
        public ToolWeaponTypes TWExtraType;
        public bool IsTWExtraType(ToolWeaponTypes toolWeaponType) => TWExtraType == toolWeaponType;
        public bool HaveExtraTW => TWExtraType != default;
        public bool HaveShield => TWExtraType == ToolWeaponTypes.Shield;

        public LevelTWTypes LevelTWType;
        public bool IsLevelTWType(LevelTWTypes levelTWType) => LevelTWType == levelTWType;

        public int ShieldProtection;
        public void AddShieldProtect(LevelTWTypes levelTWType)
        {
            switch (levelTWType)
            {
                case LevelTWTypes.None: throw new Exception();
                case LevelTWTypes.Wood: ShieldProtection = 1; return;
                case LevelTWTypes.Iron: ShieldProtection = 3; return;
                default: throw new Exception();
            }
        }
        public void TakeShieldProtect(int taking = 1)
        {
            ShieldProtection -= taking;
            if (ShieldProtection <= 0)
            {
                TWExtraType = ToolWeaponTypes.None;
            }
        }
    }
}