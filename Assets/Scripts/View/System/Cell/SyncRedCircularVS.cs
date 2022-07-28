using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using Chessy.View.System;
using UnityEngine;

namespace Chessy.Model
{
    sealed class SyncRedCircularVS : SystemViewAbstract
    {
        readonly bool[] _needActive = new bool[IndexCellsValues.CELLS];
        readonly bool[] _wasActivated = new bool[IndexCellsValues.CELLS];
        readonly SpriteRenderer[] _redCircularSRCs;

        internal SyncRedCircularVS(in SpriteRenderer[] redCircularSRCs, in EntitiesModel eMG) : base(eMG)
        {
            _redCircularSRCs = redCircularSRCs;
        }

        internal override void Sync()
        {
            var currentLessonT = AboutGameC.LessonType;

            for (var currentIdxCell = 0; currentIdxCell < IndexCellsValues.CELLS; currentIdxCell++)
            {
                _needActive[currentIdxCell] = false;
            }

            for (byte currentIdxCell = 0; currentIdxCell < IndexCellsValues.CELLS; currentIdxCell++)
            {
                if (_cellCs[currentIdxCell].IsBorder) continue;

                var currentUnitT_0 = _unitCs[currentIdxCell].UnitType;
                ref var needActiveRef = ref _needActive[currentIdxCell];

                if (currentLessonT == LessonTypes.StepAwayFromWoodcutter)
                {
                    if (KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_FOR_StepAwayFromWoodcutter == currentIdxCell)
                    {
                        needActiveRef = true;
                    }
                }
                else if (currentLessonT == LessonTypes.ShiftPawnHere)
                {
                    if (KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON == currentIdxCell)
                    {
                        needActiveRef = true;
                    }
                }


                if (!currentLessonT.HaveLesson())
                {
                    if (AboutGameC.GameModeType == GameModeTypes.TrainingOffline)
                    {
                        if (currentUnitT_0 == UnitTypes.King)
                        {
                            if (_unitCs[currentIdxCell].PlayerType == PlayerTypes.Second)
                            {
                                needActiveRef = true;
                            }
                        }
                    }
                }


                ref var wasActivatedRef = ref _wasActivated[currentIdxCell];

                if (needActiveRef != wasActivatedRef) _redCircularSRCs[currentIdxCell].gameObject.SetActive(needActiveRef);

                wasActivatedRef = needActiveRef;
            }
        }
    }
}