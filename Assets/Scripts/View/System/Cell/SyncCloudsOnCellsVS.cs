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
                if (_cloudCs[cellIdxCurrent].IsCenterP)
                {
                    _needActive[_cloudWhereViewDataCs[cellIdxCurrent].ViewIdxCellP] = true;

                    foreach (var item in _idxsAroundCellCs[cellIdxCurrent].IdxCellsAroundArray)
                    {
                        _needActive[_cloudWhereViewDataCs[item].ViewIdxCellP] = true;
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