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
            _tMPC.TextMeshPro.gameObject.SetActive(e.IsActivatedIdxAndXyInfoCells);

            if (e.IsActivatedIdxAndXyInfoCells)
            {
                _tMPC.TextMeshPro.text = _currentCell + "\n " + e.XyCellC(_currentCell).X + "|" + e.XyCellC(_currentCell).Y + "  ";
            }
        }
    }
}