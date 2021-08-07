using Assets.Scripts.ECS.Component.UI.Game.General;
using Leopotam.Ecs;

internal sealed class TheEndGameUISystem : IEcsRunSystem
{
    private EcsFilter<EndGameDataUIComponent, EndGameViewUIComponent> _endGameFilter;

    public void Run()
    {
        if (_endGameFilter.Get1(0).IsEndGame)
        {
            _endGameFilter.Get2(0).SetActiveZone(true);
            if (_endGameFilter.Get1(0).PlayerWinner.IsLocal) _endGameFilter.Get2(0).Text = "You're WINNER!";
            else _endGameFilter.Get2(0).Text = "You're loser :(";
        }
        else
        {
            _endGameFilter.Get2(0).SetActiveZone(false);
        }
    }
}
