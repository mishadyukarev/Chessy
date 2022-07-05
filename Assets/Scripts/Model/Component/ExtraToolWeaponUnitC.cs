namespace Chessy.Model.Cell.Unit
{
    public struct ExtraToolWeaponUnitC
    {
        public ToolsWeaponsWarriorTypes ToolWeaponT { get; internal set; }
        public LevelTypes LevelT { get; internal set; }
        public float ProtectionShield { get; internal set; }

        public bool HaveAnyProtectionShield => ProtectionShield >= 1;
    }
}