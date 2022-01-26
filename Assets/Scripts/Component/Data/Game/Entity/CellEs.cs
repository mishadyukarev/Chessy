using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellEs
    {
        static CellParenE[] _cellParents;
        static CellE[] _cells;
        static HashSet<byte> _idxs;

        public static CellParenE Parent(in byte idx) => _cellParents[idx];
        public static CellE Cell(in byte idx) => _cells[idx];

        public static byte IdxCell(in byte[] xy)
        {
            for (byte idx = 0; idx < _cells.Length; idx++)
            {
                if (Cell(idx).XyC.Xy.Compare(xy))
                {
                    return idx;
                }
            }
            throw new Exception();
        }
        public static HashSet<byte> Idxs
        {
            get
            {
                var hash = new HashSet<byte>();
                foreach (var item in _idxs) hash.Add(item);
                return hash;
            }
        }

        public CellEs(in EcsWorld gameW, in bool[] isActiveParentCells, in int[] idCells)
        {
            _cellParents = new CellParenE[isActiveParentCells.Length];
            _cells = new CellE[isActiveParentCells.Length];

            _idxs = new HashSet<byte>();

            byte idx = 0;
            for (idx = 0; idx < _cellParents.Length; idx++) _idxs.Add(idx);
            idx = 0;

            for (byte x = 0; x < CellStartValues.X_AMOUNT; x++)
                for (byte y = 0; y < CellStartValues.Y_AMOUNT; y++)
                {
                    _cellParents[idx] = new CellParenE(gameW, isActiveParentCells[idx]);
                    _cells[idx] = new CellE(gameW, new byte[] { x, y }, idCells[idx]);

                    ++idx;
                }
        }


    }
    public interface IUnitCellE { }
}