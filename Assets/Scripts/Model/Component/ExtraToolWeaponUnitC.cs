namespace Chessy.Model.Component
{
    public sealed class ExtraToolWeaponUnitC
    {
        internal ToolsWeaponsWarriorTypes ToolWeaponT;
        internal LevelTypes LevelT;
        internal float ProtectionShield;

        public ToolsWeaponsWarriorTypes ToolWeaponType => ToolWeaponT;
        public LevelTypes LevelType => LevelT;
        public float ProtectionShieldP => ProtectionShield;

        public bool HaveAnyProtectionShield => ProtectionShield >= 1;
        public bool HaveToolWeapon => ToolWeaponT.HaveToolWeapon();

        internal void Dispose()
        {
            ToolWeaponT = default;
            LevelT = default;
            ProtectionShield = default;
        }
        internal void Clone(in ExtraToolWeaponUnitC extraToolWeaponUnitC)
        {
            ToolWeaponT = extraToolWeaponUnitC.ToolWeaponT;
            LevelT = extraToolWeaponUnitC.LevelT;
            ProtectionShield = extraToolWeaponUnitC.ProtectionShield;
        }
        internal void Set(in ToolsWeaponsWarriorTypes twT, in LevelTypes levelT, in float protectionShield)
        {
            ToolWeaponT = twT;
            LevelT = levelT;
            ProtectionShield = protectionShield;
        }
    }
}