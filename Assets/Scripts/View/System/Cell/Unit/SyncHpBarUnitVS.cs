using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncHpBarUnitVS : SystemViewAbstract
    {
        readonly bool[] _needActiveBar = new bool[IndexCellsValues.CELLS];
        readonly Color[] _needSetColorToBar = new Color[IndexCellsValues.CELLS];

        readonly SpriteRendererVC[] _hpBarSRC;

        internal SyncHpBarUnitVS(in SpriteRendererVC[] hpBarSRCs, in EntitiesModel eMG) : base(eMG)
        {
            _hpBarSRC = hpBarSRCs;
        }

        internal override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needActiveBar[cellIdxCurrent] = false;
                _needSetColorToBar[cellIdxCurrent] = Color.white;
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_unitWhereViewDataCs[cellIdxCurrent].HaveDataReference)
                {
                    var dataIdxCell = _unitWhereViewDataCs[cellIdxCurrent].DataIdxCellP;

                    if (_unitVisibleCs[dataIdxCell].IsVisible(_aboutGameC.CurrentPlayerIType))
                    {
                        if (_unitCs[dataIdxCell].HaveUnit && !_unitCs[dataIdxCell].UnitType.IsAnimal())
                        {
                            _needActiveBar[cellIdxCurrent] = true;

                            var xCordinate = (float)(_hpUnitCs[dataIdxCell].HealthP / HpUnitValues.MAX);
                            _hpBarSRC[cellIdxCurrent].Transform.localScale = new Vector3(xCordinate * 0.67f, 0.13f, 1);

                            _needSetColorToBar[cellIdxCurrent] = _unitCs[dataIdxCell].PlayerType == PlayerTypes.First ? Color.blue : Color.red;
                        }
                    }
                }


            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _hpBarSRC[cellIdxCurrent].TrySetActiveGO(_needActiveBar[cellIdxCurrent]);
                _hpBarSRC[cellIdxCurrent].Color = _needSetColorToBar[cellIdxCurrent];
            }
        }
    }
}