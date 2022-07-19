using Chessy.Model.Entity;
using Chessy.Model.Enum;
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
                if (_e.IsCenterCloud(cellIdxCurrent))
                {
                    _needActive[_e.CloudWhereViewDataOnCellC(cellIdxCurrent).ViewIdxCellP] = true;

                    foreach (var item in _e.IdxsCellsAround(cellIdxCurrent, DistanceFromCellTypes.First))
                    {
                        _needActive[_e.CloudWhereViewDataOnCellC(item).ViewIdxCellP] = true;
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