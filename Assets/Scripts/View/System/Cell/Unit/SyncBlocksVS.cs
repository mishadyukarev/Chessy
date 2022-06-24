using Chessy.Model.Model.Entity;
using Chessy.Model.Values.Cell.Unit.Stats;
using UnityEngine;

namespace Chessy.Model
{
    sealed class SyncBlocksVS : SystemViewCellGameAbs
    {
        readonly bool[] _needActive = new bool[(byte)CellBlockTypes.End];
        readonly Color[] _needColor = new Color[(byte)CellBlockTypes.End];

        readonly EntitiesView _vEs;

        internal SyncBlocksVS(in EntitiesView vEs, in byte currentCell, in EntitiesModel eMG) : base(currentCell, eMG)
        {
            _vEs = vEs;
        }

        internal sealed override void Sync()
        {
            for (var i = 0; i < _needActive.Length; i++)
            {
                _needActive[i] = false;
                _needColor[i] = Color.white;
            }

            if (_e.UnitVisibleC(_currentCell).IsVisible(_e.CurPlayerIT))
            {
                if (_e.UnitT(_currentCell).HaveUnit() && !_e.UnitT(_currentCell).IsAnimal())
                {
                    _needActive[(byte)CellBlockTypes.NeedWater] = _e.WaterUnitC(_currentCell).Water <= WaterValues.MAX * 0.4f;
                    _needActive[(byte)CellBlockTypes.MaxSteps] = _e.EnergyUnitC(_currentCell).Energy >= StepValues.MAX;


                    if (_e.UnitConditionT(_currentCell).Is(ConditionUnitTypes.Protected))
                    {
                        _needActive[(byte)CellBlockTypes.Condition] = true;
                        _needColor[(byte)CellBlockTypes.Condition] = Color.yellow;
                    }

                    else if (_e.UnitConditionT(_currentCell).Is(ConditionUnitTypes.Relaxed))
                    {
                        _needActive[(byte)CellBlockTypes.Condition] = true;
                        _needColor[(byte)CellBlockTypes.Condition] = Color.green;
                    }

                    if (_e.UnitPlayerT(_currentCell).Is(PlayerTypes.First))
                    {
                        _needColor[(byte)CellBlockTypes.MaxSteps] = Color.blue;
                    }
                    else
                    {
                        _needColor[(byte)CellBlockTypes.MaxSteps] = Color.red;
                    }
                }
            }

            for (var cellBlockT = (CellBlockTypes)1; cellBlockT < CellBlockTypes.End; cellBlockT++)
            {
                _vEs.CellEs(_currentCell).UnitEs.Block(cellBlockT).SetActive(_needActive[(byte)cellBlockT]);
                _vEs.CellEs(_currentCell).UnitEs.Block(cellBlockT).SR.color = _needColor[(byte)cellBlockT];
            }
        }
    }
}
