using static Game.Game.EntityCenterUIPool;

namespace Game.Game
{
    sealed class TheEndGameUIS : SystemViewAbstract, IEcsRunSystem
    {
        public TheEndGameUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            if (Es.WinnerE.Winner.Player == default)
            {
                EndGame<EndGameUIEC>().SetActiveZone(false);
            }

            else if (Es.WinnerE.Winner.Player != Es.WhoseMove.CurPlayerI)
            {
                EndGame<EndGameUIEC>().SetText(true);
                EndGame<EndGameUIEC>().SetActiveZone(true);
            }
            else
            {
                EndGame<EndGameUIEC>().SetText(false);
                EndGame<EndGameUIEC>().SetActiveZone(true);
            }
        }
    }
}