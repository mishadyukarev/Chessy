using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game
{
    sealed class SyncRedCircularVS : SystemViewCellGameAbs
    {
        bool _needActive;
        SpriteRendererVC _redCircularSRC;

        internal SyncRedCircularVS(in SpriteRendererVC redCircularSRC, in byte currentCell, in EntitiesModelGame eMG) : base(currentCell, eMG)
        {
            _redCircularSRC = redCircularSRC;
        }

        internal override void Sync()
        {
            _needActive = false;

            if (_e.LessonT == LessonTypes.ShiftPawnForSeedingHere)
            {
                if (StartValues.CELL_FOR_SHIFT_PAWN_FOR_SEEDING_LESSON == _currentCell)
                {
                    _needActive = true;
                }
            }
            else if (_e.LessonT == LessonTypes.ShiftPawnHere)
            {
                if (StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON == _currentCell)
                {
                    _needActive = true;
                }
            }
            else if (_e.LessonT == LessonTypes.ShiftPawnForFireForestHere)
            {
                if (StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST == _currentCell)
                {
                    _needActive = true;
                }
            }


            if (!_e.LessonTC.HaveLesson)
            {
                if (_e.Common.GameModeT == Common.GameModeTypes.TrainingOffline)
                {
                    if (_e.UnitT(_currentCell) == UnitTypes.King)
                    {
                        if (_e.UnitPlayerT(_currentCell) == PlayerTypes.Second)
                        {
                            _needActive = true;
                        }
                    }
                }
            }



            _redCircularSRC.SetActive(_needActive);
        }
    }
}