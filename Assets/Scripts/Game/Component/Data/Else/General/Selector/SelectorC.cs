using System;

namespace Chessy.Game
{
    public struct SelectorC
    {
        private static CellClickTypes _click;
        public static CellClickTypes CellClick => _click;
        public static bool Is(params CellClickTypes[] clicks)
        {
            foreach (var click in clicks) if (click == _click) return true;
            return false;
        }

        public static void Reset() => _click = default;
        public static void Set(CellClickTypes cellClick) => _click = cellClick;


        public static byte IdxCurCell { get; set; }
        public static byte IdxSelCell { get; set; }
        public static byte IdxPreCell { get; set; }
        public static byte IdxPreVisionCell { get; set; }

        public static bool IsSelCell => IdxSelCell != default;
        public static bool IsStartDirectToCell => IdxCurCell == default;
        public static void DefSelectedCell() => IdxSelCell = 0;


        public SelectorC(bool isStart)
        {
            if (isStart)
            {
                _click = default;
                IdxSelCell = default;
            }

            else throw new Exception();
        }
    }
}