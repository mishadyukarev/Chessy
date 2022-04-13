using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;
using UnityEngine;

namespace Chessy.Game
{
    sealed class SyncBlocksVS : SystemViewCellGameAbs
    {
        readonly bool[] _needActive = new bool[(byte)CellBlockTypes.End];
        readonly Color[] _needColor = new Color[(byte)CellBlockTypes.End];

        readonly EntitiesViewGame _vEs;

        internal SyncBlocksVS(in EntitiesViewGame vEs, in byte currentCell, in EntitiesModelGame eMG) : base(currentCell, eMG)
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

            if (e.UnitVisibleC(_currentCell).IsVisible(e.CurPlayerIT))
            {
                if (e.UnitTC(_currentCell).HaveUnit && !e.UnitTC(_currentCell).IsAnimal)
                {
                    _needActive[(byte)CellBlockTypes.NeedWater] = e.WaterUnitC(_currentCell).Water <= WaterValues.MAX * 0.4f;
                    _needActive[(byte)CellBlockTypes.MaxSteps] = e.StepUnitC(_currentCell).Steps >= StepValues.MAX;


                    if (e.UnitConditionTC(_currentCell).Is(ConditionUnitTypes.Protected))
                    {
                        _needActive[(byte)CellBlockTypes.Condition] = true;
                        _needColor[(byte)CellBlockTypes.Condition] = Color.yellow;
                    }

                    else if (e.UnitConditionTC(_currentCell).Is(ConditionUnitTypes.Relaxed))
                    {
                        _needActive[(byte)CellBlockTypes.Condition] = true;
                        _needColor[(byte)CellBlockTypes.Condition] = Color.green;
                    }

                    if (e.UnitPlayerTC(_currentCell).Is(PlayerTypes.First))
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
