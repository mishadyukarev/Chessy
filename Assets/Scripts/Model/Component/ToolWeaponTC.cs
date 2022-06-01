using System;

namespace Chessy.Game
{
    public struct ToolWeaponTC
    {
        public ToolWeaponTypes ToolWeaponT { get; internal set; }

        public bool Is(params ToolWeaponTypes[] tWs)
        {
            if (tWs == default) throw new Exception();

            foreach (var tw in tWs) if (tw == ToolWeaponT) return true;
            return false;
        }
        public bool HaveToolWeapon => !Is(ToolWeaponTypes.None, ToolWeaponTypes.End);
    }
}