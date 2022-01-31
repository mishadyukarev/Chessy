namespace Game.Game
{
    public struct ToolWeaponTC
    {
        public ToolWeaponTypes ToolWeapon;
        public bool Is(in ToolWeaponTypes tW) => ToolWeapon == tW;
        public bool IsShield => Is(ToolWeaponTypes.Shield);
        public bool HaveTW => ToolWeapon != default;

        public ToolWeaponTC(in ToolWeaponTypes tw) => ToolWeapon = tw;
    }
}