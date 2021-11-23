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
                EndGameUIC.SetActiveZone(false);
            }

            else if (PlyerWinnerC.PlayerWinner == WhoseMoveC.CurPlayerI)
            {
                EndGameUIC.Text = LanguageC.GetText(GameLanguageTypes.YouAreWinner);
                EndGameUIC.SetActiveZone(true);
            }
            else
            {
                EndGameUIC.Text = LanguageC.GetText(GameLanguageTypes.YouAreLoser);
                EndGameUIC.SetActiveZone(true);
            }
        }
    }
}