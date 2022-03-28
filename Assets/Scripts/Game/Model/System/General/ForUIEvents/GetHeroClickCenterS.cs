using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class GetHeroClickCenterS : SystemModelGameAbs
    {
        public GetHeroClickCenterS(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Get(in UnitTypes unitT)
        {
            if (e.CurPlayerITC.Is(e.WhoseMove.Player))
            {
                e.Sound(ClipTypes.Click).Invoke();

                e.RpcPoolEs.GetHeroToMaster(unitT);
            }
            else e.Sound(ClipTypes.Mistake).Action.Invoke();

            e.NeedUpdateView = true;
        }
    }
}