using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncHpBarUnitVS : SystemViewAbstract
    {
        readonly bool[] _needActiveBar = new bool[StartValues.CELLS];
        readonly Color[] _needSetColorToBar = new Color[StartValues.CELLS];

        readonly SpriteRendererVC[] _hpBarSRC;

        internal SyncHpBarUnitVS(in SpriteRendererVC[] hpBarSRCs, in EntitiesModel eMG) : base(eMG)
        {
            _hpBarSRC = hpBarSRCs;
        }

        internal override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _needActiveBar[cellIdxCurrent] = false;
                _needSetColorToBar[cellIdxCurrent] = Color.white;
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.UnitVisibleC(cellIdxCurrent).IsVisible(_e.CurrentPlayerIT))
                {
                    if (_e.UnitT(cellIdxCurrent).HaveUnit() && !_e.UnitT(cellIdxCurrent).IsAnimal())
                    {
                        _needActiveBar[cellIdxCurrent] = true;

                        var xCordinate = (float)(_e.HpUnit(cellIdxCurrent) / HpValues.MAX);
                        _hpBarSRC[cellIdxCurrent].Transform.localScale = new Vector3(xCordinate * 0.67f, 0.13f, 1);

                        _needSetColorToBar[cellIdxCurrent] = _e.UnitPlayerT(cellIdxCurrent) == PlayerTypes.First ? Color.blue : Color.red;
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _hpBarSRC[cellIdxCurrent].SetActiveGO(_needActiveBar[cellIdxCurrent]);
                _hpBarSRC[cellIdxCurrent].SR.color = _needSetColorToBar[cellIdxCurrent];
            }
        }
    }
}