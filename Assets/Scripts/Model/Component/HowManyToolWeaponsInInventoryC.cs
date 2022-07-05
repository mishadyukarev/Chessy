using System.Collections.Generic;
namespace Chessy.Model
{
    public struct HowManyToolWeaponsInInventoryC
    {
        readonly Dictionary<ToolsWeaponsWarriorTypes, int[]> _toolWeapons;

        public ref int ToolWeapons(in ToolsWeaponsWarriorTypes twT, LevelTypes levelT) => ref _toolWeapons[twT][(byte)levelT];

        public HowManyToolWeaponsInInventoryC(in bool def)
        {
            _toolWeapons = new Dictionary<ToolsWeaponsWarriorTypes, int[]>();
            for (var twT = (ToolsWeaponsWarriorTypes)0; twT < ToolsWeaponsWarriorTypes.End; twT++)
            {
                _toolWeapons.Add(twT, new int[(byte)LevelTypes.End]);
            }
        }

        internal void Set(in ToolsWeaponsWarriorTypes twT, LevelTypes levelT, in int amount) => _toolWeapons[twT][(byte)levelT] = amount;
        internal void Subtract(in ToolsWeaponsWarriorTypes twT, LevelTypes levelT, in int subtraction = 1) => _toolWeapons[twT][(byte)levelT] -= subtraction;
        internal void Add(in ToolsWeaponsWarriorTypes twT, LevelTypes levelT, in int adding = 1) => _toolWeapons[twT][(byte)levelT] += adding;
    }
}