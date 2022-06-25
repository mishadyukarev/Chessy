using Chessy.Model;

namespace Chessy.Model.System.View
{
    sealed class NeedFoodVS : SystemViewCellGameAbs
    {
        bool _needActive;
        readonly SpriteRendererVC _needFoodSRC;

        internal NeedFoodVS(in SpriteRendererVC needFoodSRC, in byte currentCell, in EntitiesModel eMG) : base(currentCell, eMG)
        {
            _needFoodSRC = needFoodSRC;
        }

        internal sealed override void Sync()
        {
            _needActive = false;

            if (!_e.LessonT.HaveLesson() || _e.LessonT >= Enum.LessonTypes.Build3Farms)
            {
                if (_e.UnitT(_currentCell).Is(UnitTypes.Pawn))
                {
                    if (_e.UnitPlayerT(_currentCell).Is(_e.CurPlayerIT))
                    {
                        _needActive = _e.ResourcesInInventory(_e.CurPlayerIT, ResourceTypes.Food) < 1;
                    }
                }
            }

            _needFoodSRC.SetActive(_needActive);
        }
    }
}