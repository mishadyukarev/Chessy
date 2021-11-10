namespace Chessy.Game
{
    public struct SelectorC
    {
        private static CellClickTypes _cellClick;
        public static CellClickTypes CellClick => _cellClick;
        public static bool Is(CellClickTypes cellClickType) => cellClickType == _cellClick;

        public static void Reset() => _cellClick = default;
        public static void Set(CellClickTypes cellClick) => _cellClick = cellClick;


        public static byte IdxCurCell { get; set; }
        public static byte IdxSelCell { get; set; }
        public static byte IdxPreCell { get; set; }
        public static byte IdxPreVisionCell { get; set; }

        public static bool IsSelCell => IdxSelCell != default;
        public static bool IsStartDirectToCell => IdxCurCell == default;
        public static void DefSelectedCell() => IdxSelCell = 0;
    }
}