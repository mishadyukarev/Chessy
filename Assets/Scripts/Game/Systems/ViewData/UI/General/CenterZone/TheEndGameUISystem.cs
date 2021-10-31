using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class TheEndGameUISystem : IEcsRunSystem
    {
        public void Run()
        {
            if (EndGameDataUIC.PlayerWinner == default)
            {
                EndGameViewUIC.SetActiveZone(false);
            }

            else if (EndGameDataUIC.PlayerWinner == WhoseMoveC.CurPlayer)
            {
                EndGameViewUIC.Text = LanguageComCom.GetText(GameLanguageTypes.YouAreWinner);
                EndGameViewUIC.SetActiveZone(true);
            }
            else
            {
                EndGameViewUIC.Text = LanguageComCom.GetText(GameLanguageTypes.YouAreLoser);
                EndGameViewUIC.SetActiveZone(true);
            }
        }
    }
}