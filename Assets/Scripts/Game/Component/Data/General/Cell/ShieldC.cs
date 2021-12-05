using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct ShieldC : ITWCellE
    {
        readonly byte _idx;

        internal ShieldC(byte idx) : this() => _idx = idx;


        public void Take(int taking = 1)
        {
            UnitTW<ProtectionC>(_idx).Take(taking);

            if (UnitTW<ProtectionC>(_idx).Protection <= 0)
            {
                UnitTW<ToolWeaponC>(_idx).Reset();
            }
        }
    }
}