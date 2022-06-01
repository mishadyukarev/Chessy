using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    internal sealed class SyncIdxAndXyInfoVS : SystemViewCellGameAbs
    {
        readonly TMPC _tMPC;

        internal SyncIdxAndXyInfoVS(in TMPC tMPC, in byte currentCell, in EntitiesModelGame eMG) : base(currentCell, eMG)
        {
            _tMPC = tMPC;
        }

        internal override void Sync()
        {
            _tMPC.TextMeshPro.gameObject.SetActive(_e.IsActivatedIdxAndXyInfoCells);

            if (_e.IsActivatedIdxAndXyInfoCells)
            {
                _tMPC.TextMeshPro.text = _currentCell + "\n " + _e.XyCellC(_currentCell).X + "|" + _e.XyCellC(_currentCell).Y + "  ";
            }
        }
    }
}