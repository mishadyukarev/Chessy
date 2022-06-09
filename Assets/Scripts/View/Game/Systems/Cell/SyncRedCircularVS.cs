using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.View.System;

namespace Chessy.Game
{
    sealed class SyncRedCircularVS : SystemViewGameAbs
    {
        bool[] _needActive = new bool[StartValues.CELLS];
        SpriteRendererVC[] _redCircularSRCs;

        internal SyncRedCircularVS(in SpriteRendererVC[] redCircularSRCs, in EntitiesModelGame eMG) : base(eMG)
        {
            _redCircularSRCs = redCircularSRCs;
        }

        internal override void Sync()
        {
            for (var currentIdxCell = 0; currentIdxCell < StartValues.CELLS; currentIdxCell++)
            {
                _needActive[currentIdxCell] = false;
            }

            for (byte currentIdxCell = 0; currentIdxCell < StartValues.CELLS; currentIdxCell++)
            {
                if (_e.LessonT == LessonTypes.ShiftPawnForSeedingHere)
                {
                    if (StartValues.CELL_FOR_SHIFT_PAWN_FOR_SEEDING_LESSON == currentIdxCell)
                    {
                        _needActive[currentIdxCell] = true;
                    }
                }
                else if (_e.LessonT == LessonTypes.StepAwayFromWoodcutter)
                {
                    if (StartValues.CELL_FOR_SHIFT_PAWN_FOR_SEEDING_LESSON == currentIdxCell)
                    {
                        _needActive[currentIdxCell] = true;
                    }
                }
                else if (_e.LessonT == LessonTypes.ShiftPawnHere)
                {
                    if (StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON == currentIdxCell)
                    {
                        _needActive[currentIdxCell] = true;
                    }
                }
                else if (_e.LessonT == LessonTypes.ShiftPawnForFireForestHere)
                {
                    if (StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST == currentIdxCell)
                    {
                        _needActive[currentIdxCell] = true;
                    }
                }



                if (!_e.LessonTC.HaveLesson)
                {
                    if (_e.Common.GameModeT == Common.GameModeTypes.TrainingOffline)
                    {
                        if (_e.UnitT(currentIdxCell) == UnitTypes.King)
                        {
                            if (_e.UnitPlayerT(currentIdxCell) == PlayerTypes.Second)
                            {
                                _needActive[currentIdxCell] = true;
                            }
                        }
                    }
                }

                _redCircularSRCs[currentIdxCell].SetActive(_needActive[currentIdxCell]);
            }
        }
    }
}