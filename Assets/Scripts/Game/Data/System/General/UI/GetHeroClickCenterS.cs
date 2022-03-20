namespace Chessy.Game.System.Model
{
    public struct GetHeroClickCenterS
    {
        public void Get(in UnitTypes unitT, in EntitiesModel e)
        {
            if (e.CurPlayerITC.Is(e.WhoseMove.Player))
            {
                e.Sound(ClipTypes.Click).Invoke();

                e.RpcPoolEs.GetHeroToMaster(unitT);
            }
            else e.Sound(ClipTypes.Mistake).Action.Invoke();
        }
    }
}