using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.UI.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Leopotam.Ecs;

internal sealed class TheEndGameUISystem : IEcsRunSystem
{
    private EcsFilter<EndGameDataUIComponent, EndGameViewUIComponent> _endGameUIFilter = default;

    public void Run()
    {
        ref var endGameDataUIComp = ref _endGameUIFilter.Get1(0);
        ref var endGameViewUIComp = ref _endGameUIFilter.Get2(0);


        if (endGameDataUIComp.IsEndGame)
        {
            endGameViewUIComp.SetActiveZone(true);

            if (endGameDataUIComp.IsOwnerWinner)
            {
                if (endGameDataUIComp.PlayerWinner.IsLocal)
                {
                    endGameViewUIComp.Text = LanguageComComp.GetText(GameLanguageTypes.YouAreWinner);
                }
                else
                {
                    endGameViewUIComp.Text = LanguageComComp.GetText(GameLanguageTypes.YouAreLoser);
                }
            }

            else
            {
                if (endGameDataUIComp.IsBotWinner)
                {
                    endGameViewUIComp.Text = LanguageComComp.GetText(GameLanguageTypes.YouAreLoser);
                }
                else
                {
                    endGameViewUIComp.Text = LanguageComComp.GetText(GameLanguageTypes.YouAreWinner);
                }
            }
        }
        else
        {
            endGameViewUIComp.SetActiveZone(false);
        }
    }
}
