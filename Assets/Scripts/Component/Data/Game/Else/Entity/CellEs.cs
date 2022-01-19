﻿using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellEs
    {
        static Entity[] _cellParents;
        static Entity[] _cells;
        static HashSet<byte> _idxs;

        public static ref C CellParent<C>(in byte idx) where C : struct => ref _cellParents[idx].Get<C>();
        public static ref C Cell<C>(in byte idx) where C : struct, ICell => ref _cells[idx].Get<C>();

        public static byte IdxCell(in byte[] xy)
        {
            for (byte idx = 0; idx < _cells.Length; idx++)
            {
                if (Cell<XyC>(idx).Xy.Compare(xy))
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
            _cellParents = new Entity[isActiveParentCells.Length];
            _cells = new Entity[isActiveParentCells.Length];

            _idxs = new HashSet<byte>();

            byte idx = 0;
            for (idx = 0; idx < _cellParents.Length; idx++) _idxs.Add(idx);
            idx = 0;

            for (byte x = 0; x < CellStartValues.X_AMOUNT; x++)
                for (byte y = 0; y < CellStartValues.Y_AMOUNT; y++)
                {
                    _cellParents[idx] = gameW.NewEntity()
                        .Add(new IsActiveC(isActiveParentCells[idx]));

                    _cells[idx] = gameW.NewEntity()
                        .Add(new XyC(new byte[] { x, y }))
                        .Add(new InstanceIDC(idCells[idx]));

                    ++idx;
                }
        }


    }
    public interface ICell { }
    public interface IUnitCellE { }
    public interface IUnitUniqueCellE { }
    public interface IBuildPlayerCellE { }
    public interface ITrailVisibledCellE { }
}