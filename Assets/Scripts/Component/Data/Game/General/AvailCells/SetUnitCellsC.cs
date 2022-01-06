using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct SetUnitCellsC
    {
        private static Dictionary<PlayerTypes, List<byte>> _cells;

        public static Dictionary<PlayerTypes, List<byte>> Cells
        {
            get
            {
                var dict = new Dictionary<PlayerTypes, List<byte>>();
                foreach (var item in _cells) dict.Add(item.Key, new List<byte>());
                return dict;
            }
        }
        public static List<byte> List(PlayerTypes playerType)
        {
            if (playerType == default) throw new Exception();
            return _cells[playerType].Copy();
        }
        public static bool HaveIdxCell(PlayerTypes playerType, byte idxCell)
        {
            if (playerType == default) throw new Exception();
            return _cells[playerType].Contains(idxCell);
        }


        static SetUnitCellsC()
        {
            _cells = new Dictionary<PlayerTypes, List<byte>>();

            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                _cells.Add(player, new List<byte>());
            }
        }
        public SetUnitCellsC(bool needReset) : this()
        {
            if (needReset) foreach (var item in Cells) Clear(item.Key);
            else throw new Exception();
        }


        public static bool RemoveIdxCell(PlayerTypes playerType, byte idxCell)
        {
            if (playerType == default) throw new Exception();
            return _cells[playerType].Remove(idxCell);
        }
        public static void AddIdxCell(PlayerTypes playerType, byte idxCellValue)
        {
            if (playerType == default) throw new Exception();
            _cells[playerType].Add(idxCellValue);
        }
        public static void Clear(PlayerTypes playerType)
        {
            if (playerType == default) throw new Exception();
            _cells[playerType].Clear();
        }
    }
}
