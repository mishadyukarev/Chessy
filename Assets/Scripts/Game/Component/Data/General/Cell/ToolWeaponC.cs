using System;

namespace Game.Game
{
    public struct ToolWeaponC : ITWCellE
    {
        TWTypes _tw;

        public TWTypes TW => _tw;
        public bool Is(TWTypes tW) => TW == tW;
        public bool HaveTW => TW != default;


        public void Set(TWTypes tw)
        {
            _tw = tw;
        }
        public void Set(ToolWeaponC twC)
        {
            _tw = twC.TW;
        }
        public void Reset()
        {
            _tw = default;
        }
        public void Sync(TWTypes tw)
        {
            _tw = tw;
        }
    }
}