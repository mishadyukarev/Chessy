using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct CellEs
    {
        CellParenE[] _cellParents;
        CellE[] _cells;
        HashSet<byte> _idxs;

        public CellParenE ParentE(in byte idx) => _cellParents[idx];
        public CellE CellE(in byte idx) => _cells[idx];


        public CellBuildEs BuildEs { get; private set; }
        public CellTrailEs TrailEs { get; private set; }
        public CellUnitEs UnitEs { get; private set; }
        public CellEnvironmentEs EnvironmentEs { get; private set; }
        public CellFireEs FireEs { get; private set; }
        public CellRiverEs RiverEs { get; private set; }


        public byte IdxCell(in byte[] xy)
        {
            for (byte idx = 0; idx < _cells.Length; idx++)
            {
                if (CellE(idx).XyC.Xy.Compare(xy))
                {
                    return idx;
                }
            }
            throw new Exception();
        }
        public HashSet<byte> Idxs
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


            BuildEs = new CellBuildEs(gameW);
            TrailEs = new CellTrailEs(gameW);
            UnitEs = new CellUnitEs(gameW);
            EnvironmentEs = new CellEnvironmentEs(gameW);
            FireEs = new CellFireEs(gameW);
            RiverEs = new CellRiverEs(gameW);
        }
    }
}