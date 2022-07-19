namespace Chessy.Model.Cell.Unit
{
    public struct ExtraToolWeaponUnitC
    {
        internal ToolsWeaponsWarriorTypes ToolWeaponT;
        internal LevelTypes LevelT;
        internal float ProtectionShield;

        public bool HaveAnyProtectionShield => ProtectionShield >= 1;
    }
}