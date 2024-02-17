using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncConditionOnCellVS : SystemViewAbstract
    {
        readonly bool[] _needActive = new bool[IndexCellsValues.CELLS];
        readonly bool[] _wasActivated = new bool[IndexCellsValues.CELLS];

        readonly SpriteRendererVC[] _conditionSRCs;
        readonly GameObjectVC[] _conditionGOCs = new GameObjectVC[IndexCellsValues.CELLS];
        readonly Color[] _needColor = new Color[IndexCellsValues.CELLS];

        readonly Color _whiteColor = Color.white;

        internal SyncConditionOnCellVS(in SpriteRendererVC[] conditionSRCs, in EntitiesModel eM) : base(eM)
        {
            _conditionSRCs = conditionSRCs;

            for (var currentCellIdx_0 = 0; currentCellIdx_0 < IndexCellsValues.CELLS; currentCellIdx_0++)
            {
                _conditionGOCs[currentCellIdx_0] = new GameObjectVC(_conditionSRCs[currentCellIdx_0].GO);
            }
        }

        internal override void Sync()
        {
            var currentPlayerT = aboutGameC.CurrentPlayerIType;

            for (byte currentCellIdx_0 = 0; currentCellIdx_0 < IndexCellsValues.CELLS; currentCellIdx_0++)
            {
                if (CellC(currentCellIdx_0).IsBorder) continue;

                _needActive[currentCellIdx_0] = false;
                _needColor[currentCellIdx_0] = _whiteColor;

                if (UnitViewDataC(currentCellIdx_0).HaveDataReference)
                {
                    var dataIdxCell_1 = UnitViewDataC(currentCellIdx_0).DataIdxCellP;

                    if (_unitVisibleCs[dataIdxCell_1].IsVisible(currentPlayerT))
                    {
                        var unitC_1 = UnitC(dataIdxCell_1);

                        if (unitC_1.HaveUnit && !unitC_1.IsAnimal)
                        {
                            if (unitC_1.ConditionType == ConditionUnitTypes.Protected)
                            {
                                _needActive[currentCellIdx_0] = true;
                                _needColor[currentCellIdx_0] = Color.yellow;
                            }

                            else if (unitC_1.ConditionType == ConditionUnitTypes.Relaxed)
                            {
                                _needActive[currentCellIdx_0] = true;
                                _needColor[currentCellIdx_0] = Color.green;
                            }
                        }
                    }
                }
            }

            for (byte currentCellIdx_0 = 0; currentCellIdx_0 < IndexCellsValues.CELLS; currentCellIdx_0++)
            {
                if (CellC(currentCellIdx_0).IsBorder) continue;

                var needActive = _needActive[currentCellIdx_0];

                _conditionGOCs[currentCellIdx_0].TrySetActive2(needActive, ref _wasActivated[currentCellIdx_0]);
                if (needActive) _conditionSRCs[currentCellIdx_0].Color = _needColor[currentCellIdx_0];
            }
        }
    }
}