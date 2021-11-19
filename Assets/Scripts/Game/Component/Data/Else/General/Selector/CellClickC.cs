using System;

namespace Game.Game
{
    public struct CellClickC
    {
        private static CellClickTypes _click;
        public static CellClickTypes Click => _click;
        public static bool Is(params CellClickTypes[] clicks)
        {
            foreach (var click in clicks) if (click == _click) return true;
            return false;
        }

        public static void Reset() => _click = default;
        public static void Set(CellClickTypes click) => _click = click;



        public CellClickC(CellClickTypes click)
        {
            _click = click;
        }
    }
}