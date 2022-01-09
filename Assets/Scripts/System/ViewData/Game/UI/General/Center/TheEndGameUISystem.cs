namespace Game.Game
{
    sealed class TheEndGameUISystem : IEcsRunSystem
    {
        public void Run()
        {
            if (PlayerWinnerC.PlayerWinner == default)
            {
                EntityUIPool.EndGameCenter<EndGameUIC>().SetActiveZone(false);
            }

            else if (PlayerWinnerC.PlayerWinner == WhoseMoveC.CurPlayerI)
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