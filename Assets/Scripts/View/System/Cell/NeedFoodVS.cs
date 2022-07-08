using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.View.System;
using Chessy.View.Component;
using Chessy.Model.Values;

namespace Chessy.View.UI.System
{
    sealed class NeedFoodVS : SystemViewAbstract
    {
        bool[] _needActive = new bool[IndexCellsValues.CELLS];
        readonly SpriteRendererVC[] _needFoodSRCs;

        internal NeedFoodVS(in SpriteRendererVC[] needFoodSRCs, in EntitiesModel eMG) : base(eMG)
        {
            _needFoodSRCs = needFoodSRCs;
        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = false;
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.SkinInfoUnitC(cellIdxCurrent).HaveData)
                {
                    var dataIdxCell = _e.SkinInfoUnitC(cellIdxCurrent).DataIdxCell;

                    if (!_e.LessonT.HaveLesson() || _e.LessonT >= LessonTypes.Build3Farms)
                    {
                        if (_e.UnitT(dataIdxCell).Is(UnitTypes.Pawn))
                        {
                            if (_e.UnitPlayerT(dataIdxCell).Is(_e.CurrentPlayerIT))
                            {
                                _needActive[cellIdxCurrent] = _e.ResourcesInInventory(_e.CurrentPlayerIT, ResourceTypes.Food) < 1;
                            }
                        }
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needFoodSRCs[cellIdxCurrent].SetActiveGO(_needActive[cellIdxCurrent]);
            }
        }
    }
}