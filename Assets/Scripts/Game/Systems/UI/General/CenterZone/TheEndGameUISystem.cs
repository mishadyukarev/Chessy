using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    internal sealed class TheEndGameUISystem : IEcsRunSystem
    {
        private EcsFilter<EndGameDataUIComponent, EndGameViewUIComponent> _endGameUIFilter = default;

        public void Run()
        {
            ref var endGameDataUIComp = ref _endGameUIFilter.Get1(0);
            ref var endGameViewUIComp = ref _endGameUIFilter.Get2(0);

            if (endGameDataUIComp.PlayerWinner == default)
            {
                endGameViewUIComp.SetActiveZone(false);
            }

            else if (endGameDataUIComp.PlayerWinner == WhoseMoveCom.CurPlayer)
            {
                endGameViewUIComp.Text = LanguageComCom.GetText(GameLanguageTypes.YouAreWinner);
                endGameViewUIComp.SetActiveZone(true);
            }
            else
            {
                endGameViewUIComp.Text = LanguageComCom.GetText(GameLanguageTypes.YouAreLoser);
                endGameViewUIComp.SetActiveZone(true);
            }
        }
    }
}