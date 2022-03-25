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
            if (eMGame.CurPlayerITC.Is(eMGame.WhoseMove.Player))
            {
                eMGame.Sound(ClipTypes.Click).Invoke();

                eMGame.RpcPoolEs.GetHeroToMaster(unitT);
            }
            else eMGame.Sound(ClipTypes.Mistake).Action.Invoke();
        }
    }
}