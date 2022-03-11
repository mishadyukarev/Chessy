using System.Collections.Generic;

namespace Chessy.Game.Entity
{
    public readonly struct CellE
    {
        readonly Dictionary<PlayerTypes, bool> _isStartedCell;

        public readonly IdxCellC IdxC;
        public readonly XyC XyC;
        public readonly int InstanceIDC;

        public bool IsStartedCell(in PlayerTypes playerT) => _isStartedCell[playerT];

        public CellE(in byte idx, in byte[] xy, in int instanceID)
        {
            var x = xy[0];
            var y = xy[1];

            _isStartedCell = new Dictionary<PlayerTypes, bool>();

            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                _isStartedCell.Add(playerT, false);

                if (playerT == PlayerTypes.First)
                {
                    if (y < 3 && x > 3 && x < 12)
                    {
                        _isStartedCell[playerT] = true;
                    }
                }
                else
                {
                    if (y > 7 && x > 3 && x < 12)
                    {
                        _isStartedCell[playerT] = true;
                    }
                }
            }

            IdxC = new IdxCellC(idx);
            XyC = new XyC(xy);
            InstanceIDC = instanceID;
        }
    }
}