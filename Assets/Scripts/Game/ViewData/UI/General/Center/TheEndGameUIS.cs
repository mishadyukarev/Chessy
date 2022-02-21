using static Game.Game.EntityCenterUIPool;

namespace Game.Game
{
    sealed class TheEndGameUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal TheEndGameUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            if (E.WinnerC.Player == default)
            {
                EndGame<EndGameUIEC>().SetActiveZone(false);
            }

            else if (E.WinnerC.Player != E.CurPlayerI.Player)
            {
                EndGame<EndGameUIEC>().SetText(false);
                EndGame<EndGameUIEC>().SetActiveZone(true);
            }
            else
            {
                EndGame<EndGameUIEC>().SetText(true);
                EndGame<EndGameUIEC>().SetActiveZone(true);
            }
        }
    }
}