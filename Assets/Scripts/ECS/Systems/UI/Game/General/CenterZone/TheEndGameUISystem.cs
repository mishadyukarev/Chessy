using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.UI.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.Supports;
using Leopotam.Ecs;

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
