using static Game.Game.EntityCenterUIPool;

namespace Game.Game
{
    struct TheEndGameUISystem : IEcsRunSystem
    {
        public void Run()
        {
            if (EntityPool.Winner<PlayerC>().Player == default)
            {
                EndGame<EndGameUIEC>().SetActiveZone(false);
            }

            else if (EntityPool.Winner<PlayerC>().Player == EntWhoseMove.CurPlayerI)
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