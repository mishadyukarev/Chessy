using static Game.Game.EntityCenterUIPool;

namespace Game.Game
{
    struct TheEndGameUISystem : IEcsRunSystem
    {
        public void Run()
        {
            if (PlayerWinnerC.PlayerWinner == default)
            {
                EndGame<EndGameUIEC>().SetActiveZone(false);
            }

            else if (PlayerWinnerC.PlayerWinner == WhoseMoveC.CurPlayerI)
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