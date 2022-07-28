using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using Chessy.View.Component;
using Chessy.View.System;

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
                if (_unitWhereViewDataCs[cellIdxCurrent].HaveDataReference)
                {
                    var dataIdxCell = _unitWhereViewDataCs[cellIdxCurrent].DataIdxCellP;

                    if (!AboutGameC.LessonType.HaveLesson() || AboutGameC.LessonType >= LessonTypes.Build1Farms)
                    {
                        if (_unitCs[dataIdxCell].UnitType == UnitTypes.Pawn)
                        {
                            if (_unitCs[dataIdxCell].PlayerType == AboutGameC.CurrentPlayerIType)
                            {
                                _needActive[cellIdxCurrent] = ResourcesInInventoryC(AboutGameC.CurrentPlayerIType).Resources(ResourceTypes.Food) < 1;
                            }
                        }
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needFoodSRCs[cellIdxCurrent].TrySetActiveGO(_needActive[cellIdxCurrent]);
            }
        }
    }
}