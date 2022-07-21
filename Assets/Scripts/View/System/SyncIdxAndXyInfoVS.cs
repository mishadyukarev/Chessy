using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;

namespace Chessy.View.System
{
    internal sealed class SyncIdxAndXyInfoVS : SystemViewAbstract
    {
        readonly bool[] _needActive = new bool[IndexCellsValues.CELLS];
        readonly TMPC[] _tMPCs;

        internal SyncIdxAndXyInfoVS(in TMPC[] tMPCs, in EntitiesModel eM) : base(eM)
        {
            _tMPCs = tMPCs;
        }

        internal override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = _e.IsActivatedIdxAndXyInfoCells;
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _tMPCs[cellIdxCurrent].TextMeshPro.gameObject.SetActive(_needActive[cellIdxCurrent]);

                if (_needActive[cellIdxCurrent])
                {
                    _tMPCs[cellIdxCurrent].TextMeshPro.text = cellIdxCurrent + "\n " + _xyCellsCs[cellIdxCurrent].X + "|" + _xyCellsCs[cellIdxCurrent].Y + "  ";
                }
            }
        }
    }
}