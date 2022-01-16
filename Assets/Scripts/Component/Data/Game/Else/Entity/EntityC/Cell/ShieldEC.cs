using System;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellUnitTWE;

namespace Game.Game
{
    public struct ShieldEC : ITWCellE
    {
        readonly byte _idx;

        internal ShieldEC(in byte idx) : this() => _idx = idx;


        public void Take(in int taking = 1)
        {
            ref var tw = ref UnitTW<ToolWeaponC>(_idx);
            ref var prot = ref UnitTW<ProtectionC>(_idx);

            if (!tw.IsShield) throw new Exception();
            if (!prot.Have) throw new Exception();

            prot.Take(taking);

            if (!prot.Have) tw.Reset();
        }
    }
}