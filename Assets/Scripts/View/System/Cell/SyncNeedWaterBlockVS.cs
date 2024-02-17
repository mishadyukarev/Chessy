using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;

namespace Chessy.View.System
{
    sealed class SyncNeedWaterBlockVS : SystemViewAbstract
    {
        readonly bool[] _needActive = new bool[IndexCellsValues.CELLS];
        readonly SpriteRendererVC[] _needWaterSRCs;

        internal SyncNeedWaterBlockVS(in SpriteRendererVC[] needWaterSRCs, in EntitiesModel eM) : base(eM)
        {
            _needWaterSRCs = needWaterSRCs;
        }

        internal override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = false;
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_unitWhereViewDataCs[cellIdxCurrent].HaveDataReference)
                {
                    var dataIdxCell = _unitWhereViewDataCs[cellIdxCurrent].DataIdxCellP;

                    if (_unitVisibleCs[dataIdxCell].IsVisible(aboutGameC.CurrentPlayerIType))
                    {
                        if (unitCs[dataIdxCell].HaveUnit && !unitCs[dataIdxCell].UnitType.IsAnimal())
                        {
                            _needActive[cellIdxCurrent] = _unitWaterCs[dataIdxCell].WaterP <= ValuesChessy.MAX_WATER_FOR_ANY_UNIT * 0.4f;
                        }
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needWaterSRCs[cellIdxCurrent].TrySetActiveGO(_needActive[cellIdxCurrent]);
            }
        }
    }
}