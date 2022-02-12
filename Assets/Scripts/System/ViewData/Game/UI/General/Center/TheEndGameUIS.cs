using static Game.Game.EntityCenterUIPool;

namespace Game.Game
{
    sealed class TheEndGameUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal TheEndGameUIS(in Entities ents, in EntitiesUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            if (Es.WinnerE.Winner.Player == default)
            {
                EndGame<EndGameUIEC>().SetActiveZone(false);
            }

            else if (Es.WinnerE.Winner.Player != Es.WhoseMoveE.CurPlayerI)
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