using System;

namespace Game.Game
{
    public class ToolWeaponTC
    {
        public ToolWeaponTypes ToolWeapon;
        public bool Is(params ToolWeaponTypes[] tWs)
        {
            if (tWs == default) throw new Exception();

            foreach (var tw in tWs) if (tw == ToolWeapon) return true;
            return false;
        }
        public bool HaveToolWeapon => !Is(ToolWeaponTypes.None, ToolWeaponTypes.End);

        public ToolWeaponTC() { }
        public ToolWeaponTC(in ToolWeaponTypes tw) => ToolWeapon = tw;
    }
}