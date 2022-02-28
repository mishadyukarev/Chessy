﻿namespace Chessy.Game
{
    public struct CellClickC
    {
        public CellClickTypes Click;
        public bool Is(params CellClickTypes[] clicks)
        {
            foreach (var click in clicks)
                if (click == Click) return true;
            return false;
        }

        public CellClickC(CellClickTypes click) => Click = click;
    }
}