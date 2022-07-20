namespace Chessy.Model.Cell.Unit
{
    public sealed class ExtraToolWeaponUnitC
    {
        internal ToolsWeaponsWarriorTypes ToolWeaponT;
        internal LevelTypes LevelT;
        internal float ProtectionShield;

        public bool HaveAnyProtectionShield => ProtectionShield >= 1;

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