using Leopotam.Ecs;
using Game.Common;

namespace Game.Game
{
    public sealed class TheEndGameUISystem : IEcsRunSystem
    {
        public void Run()
        {
            if (PlyerWinnerC.PlayerWinner == default)
            {
                EntityUIPool.EndGameCenter<EndGameUIC>().SetActiveZone(false);
            }

            else if (PlyerWinnerC.PlayerWinner == WhoseMoveC.CurPlayerI)
            {
                EntityUIPool.EndGameCenter<EndGameUIC>().SetText(true);
                EntityUIPool.EndGameCenter<EndGameUIC>().SetActiveZone(true);
            }
            else
            {
                EntityUIPool.EndGameCenter<EndGameUIC>().SetText(false);
                EntityUIPool.EndGameCenter<EndGameUIC>().SetActiveZone(true);
            }
        }
    }
}