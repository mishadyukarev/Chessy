using static Game.Game.EntityCenterUIPool;

namespace Game.Game
{
    sealed class TheEndGameUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal TheEndGameUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            if (Es.WinnerC.Player == default)
            {
                EndGame<EndGameUIEC>().SetActiveZone(false);
            }

            else if (Es.WinnerC.Player != Es.CurPlayerI.Player)
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