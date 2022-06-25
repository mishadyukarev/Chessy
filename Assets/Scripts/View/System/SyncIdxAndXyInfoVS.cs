using Chessy.Model;

namespace Chessy.Model
{
    internal sealed class SyncIdxAndXyInfoVS : SystemViewCellGameAbs
    {
        readonly TMPC _tMPC;

        internal SyncIdxAndXyInfoVS(in TMPC tMPC, in byte currentCell, in EntitiesModel eMG) : base(currentCell, eMG)
        {
            _tMPC = tMPC;
        }

        internal override void Sync()
        {
            _tMPC.TextMeshPro.gameObject.SetActive(_e.IsActivatedIdxAndXyInfoCells);

            if (_e.IsActivatedIdxAndXyInfoCells)
            {
                _tMPC.TextMeshPro.text = _currentCell + "\n " + _e.XCell(_currentCell) + "|" + _e.YCell(_currentCell) + "  ";
            }
        }
    }
}