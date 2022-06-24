namespace Chessy.Model.Model.Entity.Cell.Unit
{
    public struct ExtraToolWeaponUnitC
    {
        public ToolWeaponTypes ToolWeaponT { get; internal set; }
        public LevelTypes LevelT { get; internal set; }
        public float ProtectionShield { get; internal set; }

        public bool HaveAnyProtectionShield => ProtectionShield >= 1;
    }
}