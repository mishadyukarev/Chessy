using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;

namespace Chessy.View.System
{
    sealed class SyncNeedWaterBlockVS : SystemViewAbstract
    {
        readonly bool[] _needActive = new bool[StartValues.CELLS];
        readonly SpriteRendererVC[] _needWaterSRCs;

        internal SyncNeedWaterBlockVS(in SpriteRendererVC[] needWaterSRCs, in EntitiesModel eM) : base(eM)
        {
            _needWaterSRCs = needWaterSRCs;
        }

        internal override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = false;
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitVisibleC(cellIdxCurrent).IsVisible(_e.CurrentPlayerIT))
                {
                    if (_e.UnitT(cellIdxCurrent).HaveUnit() && !_e.UnitT(cellIdxCurrent).IsAnimal())
                    {
                        _needActive[cellIdxCurrent] = _e.WaterUnitC(cellIdxCurrent).Water <= WaterValues.MAX * 0.4f;
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _needWaterSRCs[cellIdxCurrent].SetActiveGO(_needActive[cellIdxCurrent]);
            }
        }
    }
}