namespace Game.Game
{
    public struct ToolWeaponC : ITWCellE
    {
        public TWTypes ToolWeapon { get; internal set; }
        public bool Is(in TWTypes tW) => ToolWeapon == tW;
        public bool IsShield => Is(TWTypes.Shield);
        public bool HaveTW => ToolWeapon != default;


        internal void Set(ToolWeaponC twC) => ToolWeapon = twC.ToolWeapon;
        internal void Reset() => ToolWeapon = default;
    }
}