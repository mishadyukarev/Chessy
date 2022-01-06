using System;

namespace Game.Game
{
    public struct CellClickC : IClickerObjectE
    {
        CellClickTypes _click;
        public CellClickTypes Click => _click;
        public bool Is(params CellClickTypes[] clicks)
        {
            foreach (var click in clicks) 
                if (click == _click) return true;
            return false;
        }


        public CellClickC(CellClickTypes click)
        {
            _click = click;
        }


        public void Set(CellClickTypes click)
        {
            if (click == CellClickTypes.None) throw new Exception();

            _click = click;
        }
    }
}