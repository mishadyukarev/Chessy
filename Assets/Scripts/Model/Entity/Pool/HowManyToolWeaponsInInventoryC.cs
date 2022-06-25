using System.Collections.Generic;

namespace Chessy.Model
{
    public struct HowManyToolWeaponsInInventoryC
    {
        readonly Dictionary<ToolWeaponTypes, int[]> _toolWeapons;

        public ref int ToolWeapons(in ToolWeaponTypes twT, LevelTypes levelT) => ref _toolWeapons[twT][(byte)levelT];

        public HowManyToolWeaponsInInventoryC(in bool def)
        {
            _toolWeapons = new Dictionary<ToolWeaponTypes, int[]>();
            for (var twT = (ToolWeaponTypes)0; twT < ToolWeaponTypes.End; twT++)
            {
                _toolWeapons.Add(twT, new int[(byte)LevelTypes.End]);
            }
        }

        internal void Set(in ToolWeaponTypes twT, LevelTypes levelT, in int amount) => _toolWeapons[twT][(byte)levelT] = amount;
        internal void Subtract(in ToolWeaponTypes twT, LevelTypes levelT, in int subtraction = 1) => _toolWeapons[twT][(byte)levelT] -= subtraction;
        internal void Add(in ToolWeaponTypes twT, LevelTypes levelT, in int adding = 1) => _toolWeapons[twT][(byte)levelT] += adding;
    }
}