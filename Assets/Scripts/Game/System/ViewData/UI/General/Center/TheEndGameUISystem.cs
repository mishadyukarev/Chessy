using Leopotam.Ecs;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class TheEndGameUISystem : IEcsRunSystem
    {
        public void Run()
        {
            if (PlyerWinnerC.PlayerWinner == default)
            {
                EndGameViewUIC.SetActiveZone(false);
            }

            else if (PlyerWinnerC.PlayerWinner == WhoseMoveC.CurPlayerI)
            {
                EndGameViewUIC.Text = LanguageComC.GetText(GameLanguageTypes.YouAreWinner);
                EndGameViewUIC.SetActiveZone(true);
            }
            else
            {
                EndGameViewUIC.Text = LanguageComC.GetText(GameLanguageTypes.YouAreLoser);
                EndGameViewUIC.SetActiveZone(true);
            }
        }
    }
}