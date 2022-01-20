using static Game.Game.EntityCenterUIPool;

namespace Game.Game
{
    struct TheEndGameUIS : IEcsRunSystem
    {
        public void Run()
        {
            if (EntityPool.Winner.Player == default)
            {
                EndGame<EndGameUIEC>().SetActiveZone(false);
            }

            else if (EntityPool.Winner.Player != WhoseMoveE.CurPlayerI)
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