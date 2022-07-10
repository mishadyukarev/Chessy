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
                if (_e.SkinInfoUnitC(cellIdxCurrent).HaveDataReference)
                {
                    var dataIdxCell = _e.SkinInfoUnitC(cellIdxCurrent).DataIdxCell;

                    if (_e.UnitVisibleC(dataIdxCell).IsVisible(_e.CurrentPlayerIT))
                    {
                        if (_e.UnitT(dataIdxCell).HaveUnit() && !_e.UnitT(dataIdxCell).IsAnimal())
                        {
                            _needActive[cellIdxCurrent] = _e.WaterUnitC(dataIdxCell).Water <= ValuesChessy.MAX_WATER_FOR_ANY_UNIT * 0.4f;
                        }
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needWaterSRCs[cellIdxCurrent].SetActiveGO(_needActive[cellIdxCurrent]);
            }
        }
    }
}