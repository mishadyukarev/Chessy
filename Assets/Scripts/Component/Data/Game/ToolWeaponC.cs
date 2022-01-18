namespace Game.Game
{
    public struct ToolWeaponC : ITWCellE
    {
        public ToolWeaponTypes ToolWeapon;
        public bool Is(in ToolWeaponTypes tW) => ToolWeapon == tW;
        public bool IsShield => Is(ToolWeaponTypes.Shield);
        public bool HaveTW => ToolWeapon != default;

        public ToolWeaponC(in ToolWeaponTypes tw) => ToolWeapon = tw;

        public void Set(ToolWeaponC twC) => ToolWeapon = twC.ToolWeapon;
        public void Reset() => ToolWeapon = default;
    }
}