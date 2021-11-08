using Leopotam.Ecs;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class TheEndGameUISystem : IEcsRunSystem
    {
        public void Run()
        {
            if (EndGameDataUIC.PlayerWinner == default)
            {
                EndGameViewUIC.SetActiveZone(false);
            }

            else if (EndGameDataUIC.PlayerWinner == WhoseMoveC.CurPlayerI)
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