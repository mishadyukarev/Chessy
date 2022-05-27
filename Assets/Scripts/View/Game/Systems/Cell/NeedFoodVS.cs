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

            if (e.UnitTC(_currentCell).Is(UnitTypes.Pawn))
            {
                if (e.UnitPlayerTC(_currentCell).Is(e.CurPlayerITC.PlayerT))
                {
                    _needActive = e.PlayerInfoE(e.CurPlayerITC.PlayerT).ResourcesC(ResourceTypes.Food).Resources < 1;
                }
            }

            _needFoodSRC.SetActive(_needActive);
        }
    }
}