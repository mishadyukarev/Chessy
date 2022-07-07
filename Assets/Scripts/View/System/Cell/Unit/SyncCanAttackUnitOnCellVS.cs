using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncCanAttackUnitOnCellVS : SystemViewAbstract
    {
        readonly bool[] _needActive = new bool[IndexCellsValues.CELLS];
        readonly Color[] _needColor = new Color[IndexCellsValues.CELLS];
        readonly SpriteRendererVC[] _maxStepsSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];

        internal SyncCanAttackUnitOnCellVS(in SpriteRendererVC[] maxStepsSRCs, in EntitiesModel eM) : base(eM)
        {
            _maxStepsSRCs = maxStepsSRCs;
        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = false;
                _needColor[cellIdxCurrent] = Color.white;

                if (_e.UnitVisibleC(cellIdxCurrent).IsVisible(_e.CurrentPlayerIT))
                {
                    if (_e.UnitT(cellIdxCurrent).HaveUnit() && !_e.UnitT(cellIdxCurrent).IsAnimal())
                    {
                        _needActive[cellIdxCurrent] = !_e.UnitMainC(cellIdxCurrent).HaveCoolDownForAttackAnyUnit;

                        if (_e.UnitPlayerT(cellIdxCurrent).Is(PlayerTypes.First))
                        {
                            _needColor[cellIdxCurrent] = Color.blue;
                        }
                        else
                        {
                            _needColor[cellIdxCurrent] = Color.red;
                        }
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _maxStepsSRCs[cellIdxCurrent].SetActiveGO(_needActive[cellIdxCurrent]);
                _maxStepsSRCs[cellIdxCurrent].Color = _needColor[cellIdxCurrent];
            }
        }
    }
}
