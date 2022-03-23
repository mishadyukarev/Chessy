namespace Chessy.Game.System.View
{
    public struct NeedFoodS
    {
        bool _needActive;

        public void Sync(in byte idx_0, in SpriteRendererVC needFoodSR, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            _needActive = false;

            if (e.UnitTC(idx_0).Is(UnitTypes.Pawn))
            {
                if (e.UnitPlayerTC(idx_0).Is(e.CurPlayerITC.Player))
                {
                    _needActive = e.PlayerInfoE(e.CurPlayerITC.Player).ResourcesC(ResourceTypes.Food).Resources < 1;
                }
            }

            needFoodSR.SetActive(_needActive);
        }
    }
}