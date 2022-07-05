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
        readonly SpriteRendererVC[] _conditionSRCs;
        readonly Color[] _needColor = new Color[IndexCellsValues.CELLS];

        internal SyncConditionOnCellVS(in SpriteRendererVC[] conditionSRCs, in EntitiesModel eM) : base(eM)
        {
            _conditionSRCs = conditionSRCs;
        }

        internal override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = false;
                _needColor[cellIdxCurrent] = Color.white;

                if (_e.UnitVisibleC(cellIdxCurrent).IsVisible(_e.CurrentPlayerIT))
                {
                    if (_e.UnitT(cellIdxCurrent).HaveUnit() && !_e.UnitT(cellIdxCurrent).IsAnimal())
                    {
                        if (_e.UnitConditionT(cellIdxCurrent) == ConditionUnitTypes.Protected)
                        {
                            _needActive[cellIdxCurrent] = true;
                            _needColor[cellIdxCurrent] = Color.yellow;
                        }

                        else if (_e.UnitConditionT(cellIdxCurrent) == ConditionUnitTypes.Relaxed)
                        {
                            _needActive[cellIdxCurrent] = true;
                            _needColor[cellIdxCurrent] = Color.green;
                        }
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _conditionSRCs[cellIdxCurrent].SetActiveGO(_needActive[cellIdxCurrent]);
                _conditionSRCs[cellIdxCurrent].Color = _needColor[cellIdxCurrent];
            }
        }
    }
}