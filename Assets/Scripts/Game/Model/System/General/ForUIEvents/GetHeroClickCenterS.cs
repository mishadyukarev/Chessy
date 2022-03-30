using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class GetHeroClickCenterS : SystemModelGameAbs
    {
        internal GetHeroClickCenterS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

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