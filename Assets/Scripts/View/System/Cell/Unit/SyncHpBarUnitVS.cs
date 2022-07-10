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
                if (_e.SkinInfoUnitC(cellIdxCurrent).HaveDataReference)
                {
                    var dataIdxCell = _e.SkinInfoUnitC(cellIdxCurrent).DataIdxCell;

                    if (_e.UnitVisibleC(dataIdxCell).IsVisible(_e.CurrentPlayerIT))
                    {
                        if (_e.UnitT(dataIdxCell).HaveUnit() && !_e.UnitT(dataIdxCell).IsAnimal())
                        {
                            _needActiveBar[cellIdxCurrent] = true;

                            var xCordinate = (float)(_e.HpUnit(dataIdxCell) / HpValues.MAX);
                            _hpBarSRC[cellIdxCurrent].Transform.localScale = new Vector3(xCordinate * 0.67f, 0.13f, 1);

                            _needSetColorToBar[cellIdxCurrent] = _e.UnitPlayerT(dataIdxCell) == PlayerTypes.First ? Color.blue : Color.red;
                        }
                    }
                }


            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _hpBarSRC[cellIdxCurrent].SetActiveGO(_needActiveBar[cellIdxCurrent]);
                _hpBarSRC[cellIdxCurrent].SetColor(_needSetColorToBar[cellIdxCurrent]);
            }
        }
    }
}