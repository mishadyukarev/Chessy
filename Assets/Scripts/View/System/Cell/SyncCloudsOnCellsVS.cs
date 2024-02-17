using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;

namespace Chessy.View.System
{
    sealed class SyncCloudsOnCellsVS : SystemViewAbstract
    {
        readonly bool[] _needActive = new bool[IndexCellsValues.CELLS];
        readonly SpriteRendererVC[] _cloudSRCs;

        internal SyncCloudsOnCellsVS(in SpriteRendererVC[] cloudSRCs, in EntitiesModel eM) : base(eM)
        {
            _cloudSRCs = cloudSRCs;
        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = false;
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (CloudC(cellIdxCurrent).IsCenterP)
                {
                    _needActive[CloudViewDataC(cellIdxCurrent).ViewIdxCellP] = true;

                    foreach (var item in IdxsAroundCellC(cellIdxCurrent).IdxCellsAroundArray)
                    {
                        _needActive[CloudViewDataC(item).ViewIdxCellP] = true;
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _cloudSRCs[cellIdxCurrent].TrySetActiveGO(_needActive[cellIdxCurrent]);
            }
        }
    }
}