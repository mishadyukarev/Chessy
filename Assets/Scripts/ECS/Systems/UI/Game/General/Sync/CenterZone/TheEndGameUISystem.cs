using Assets.Scripts.ECS.Component.UI.Game.General;
using Leopotam.Ecs;

internal sealed class TheEndGameUISystem : IEcsRunSystem
{
    private EcsFilter<EndGameDataUIComponent, EndGameViewUIComponent> _endGameUIFilter = default;

    public void Run()
    {
        if (_endGameUIFilter.Get1(0).IsEndGame)
        {
            _endGameUIFilter.Get2(0).SetActiveZone(true);
            if (_endGameUIFilter.Get1(0).PlayerWinner.IsLocal) _endGameUIFilter.Get2(0).Text = "You're WINNER!";
            else _endGameUIFilter.Get2(0).Text = "You're loser :(";
        }
        else
        {
            _endGameUIFilter.Get2(0).SetActiveZone(false);
        }
    }
}
