using Chessy.Game.Model.Entity;

namespace Chessy.Game.System.View
{
    sealed class NeedFoodVS : SystemViewCellGameAbs
    {
        bool _needActive;
        readonly SpriteRendererVC _needFoodSRC;

        internal NeedFoodVS(in SpriteRendererVC needFoodSRC, in byte currentCell, in EntitiesModelGame eMG) : base(currentCell, eMG)
        {
            _needFoodSRC = needFoodSRC;
        }

        internal sealed override void Sync()
        {
            _needActive = false;

            if (_e.UnitTC(_currentCell).Is(UnitTypes.Pawn))
            {
                if (_e.UnitPlayerTC(_currentCell).Is(_e.CurPlayerITC.PlayerT))
                {
                    _needActive = _e.PlayerInfoE(_e.CurPlayerITC.PlayerT).ResourcesC(ResourceTypes.Food).Resources < 1;
                }
            }

            _needFoodSRC.SetActive(_needActive);
        }
    }
}