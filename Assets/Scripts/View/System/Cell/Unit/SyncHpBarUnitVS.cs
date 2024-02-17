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
        readonly bool[] _wasActivated = new bool[IndexCellsValues.CELLS];
        readonly Color[] _needSetColorToBar = new Color[IndexCellsValues.CELLS];

        readonly SpriteRenderer[] _hpBarSRs;
        readonly GameObjectVC[] _goVCs = new GameObjectVC[IndexCellsValues.CELLS];

        PlayerTypes _currentPlayerT;

        readonly Color _whiteColor = Color.white;

        internal SyncHpBarUnitVS(in SpriteRenderer[] hpBarSRCs, in EntitiesModel eMG) : base(eMG)
        {
            _hpBarSRs = hpBarSRCs;

            for (byte currentCellIdx_0 = 0; currentCellIdx_0 < IndexCellsValues.CELLS; currentCellIdx_0++)
            {
                _goVCs[currentCellIdx_0] = new GameObjectVC(hpBarSRCs[currentCellIdx_0].gameObject);
            }
        }

        internal override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needActiveBar[cellIdxCurrent] = false;
                _needSetColorToBar[cellIdxCurrent] = _whiteColor;
            }

            _currentPlayerT = aboutGameC.CurrentPlayerIType;

            for (byte currentCellIdx_0 = 0; currentCellIdx_0 < IndexCellsValues.CELLS; currentCellIdx_0++)
            {
                if (CellC(currentCellIdx_0).IsBorder) continue;


                var unitViewData_0 = UnitViewDataC(currentCellIdx_0);

                if (unitViewData_0.HaveDataReference)
                {
                    var dataIdxCell_1 = unitViewData_0.DataIdxCellP;

                    if (_unitVisibleCs[dataIdxCell_1].IsVisible(_currentPlayerT))
                    {
                        var unitC_1 = UnitC(dataIdxCell_1);

                        if (unitC_1.HaveUnit && !unitC_1.IsAnimal)
                        {
                            _needActiveBar[currentCellIdx_0] = true;

                            var xCordinate = (float)(unitHpCs[dataIdxCell_1].HealthP / HpUnitValues.MAX);
                            _hpBarSRs[currentCellIdx_0].transform.localScale = new Vector3(xCordinate * 0.67f, 0.13f, 1);

                            _needSetColorToBar[currentCellIdx_0] = unitC_1.PlayerType == PlayerTypes.First ? Color.blue : Color.red;
                        }
                    }
                }
            }

            for (byte currentCellIdx_0 = 0; currentCellIdx_0 < IndexCellsValues.CELLS; currentCellIdx_0++)
            {
                var needActive = _needActiveBar[currentCellIdx_0];

                _goVCs[currentCellIdx_0].TrySetActive2(needActive, ref _wasActivated[currentCellIdx_0]);
                if (needActive) _hpBarSRs[currentCellIdx_0].color = _needSetColorToBar[currentCellIdx_0];
            }
        }
    }

    public static class SS
    {
        public static void TrySet(this GameObject gO, in bool needActive, ref bool wasActivated)
        {
            if (needActive != wasActivated) gO.SetActive(needActive);

            wasActivated = needActive;
        }
    }
}