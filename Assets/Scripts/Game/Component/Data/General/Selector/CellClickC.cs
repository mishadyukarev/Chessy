using System;

namespace Game.Game
{
    public struct CellClickC
    {
        static CellClickTypes _click;
        public static CellClickTypes Click => _click;
        public static bool Is(params CellClickTypes[] clicks)
        {
            foreach (var click in clicks) 
                if (click == _click) return true;
            return false;
        }


        public CellClickC(CellClickTypes click)
        {
            _click = click;
        }


        public static void Set(CellClickTypes click)
        {
            if (click == CellClickTypes.None) throw new Exception();

            _click = click;
        }
    }
}