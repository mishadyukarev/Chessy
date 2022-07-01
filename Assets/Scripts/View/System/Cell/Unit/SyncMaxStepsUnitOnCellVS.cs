using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncMaxStepsUnitOnCellVS : SystemViewAbstract
    {
        readonly bool[] _needActive = new bool[StartValues.CELLS];
        readonly Color[] _needColor = new Color[StartValues.CELLS];
        readonly SpriteRendererVC[] _maxStepsSRCs = new SpriteRendererVC[StartValues.CELLS];

        internal SyncMaxStepsUnitOnCellVS(in SpriteRendererVC[] maxStepsSRCs, in EntitiesModel eM) : base(eM)
        {
            _maxStepsSRCs = maxStepsSRCs;
        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = false;
                _needColor[cellIdxCurrent] = Color.white;

                if (_e.UnitVisibleC(cellIdxCurrent).IsVisible(_e.CurrentPlayerIT))
                {
                    if (_e.UnitT(cellIdxCurrent).HaveUnit() && !_e.UnitT(cellIdxCurrent).IsAnimal())
                    {
                        _needActive[(byte)CellBlockTypes.MaxSteps] = _e.EnergyUnitC(cellIdxCurrent).Energy >= StepValues.MAX;

                        if (_e.UnitPlayerT(cellIdxCurrent).Is(PlayerTypes.First))
                        {
                            _needColor[(byte)CellBlockTypes.MaxSteps] = Color.blue;
                        }
                        else
                        {
                            _needColor[(byte)CellBlockTypes.MaxSteps] = Color.red;
                        }
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _maxStepsSRCs[cellIdxCurrent].SetActiveGO(_needActive[cellIdxCurrent]);
                _maxStepsSRCs[cellIdxCurrent].Color = _needColor[cellIdxCurrent];
            }
        }
    }
}
