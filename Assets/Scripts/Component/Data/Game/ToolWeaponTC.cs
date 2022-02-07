using System;

namespace Game.Game
{
    public struct ToolWeaponTC
    {
        public ToolWeaponTypes ToolWeapon;
        public bool Is(params ToolWeaponTypes[] tWs)
        {
            if (tWs == default) throw new Exception();

            foreach (var tw in tWs) if (tw == ToolWeapon) return true;
            return false;
        }
        public bool IsShield => Is(ToolWeaponTypes.Shield);
        public bool HaveTW => ToolWeapon != default;

        public ToolWeaponTC(in ToolWeaponTypes tw) => ToolWeapon = tw;
    }
}