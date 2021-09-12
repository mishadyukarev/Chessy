using Assets.Scripts.ECS.Component.UI.Game.General;
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
                    endGameViewUIComp.Text = "You're WINNER!";
                }
                else
                {
                    endGameViewUIComp.Text = "You're loser :(";
                }
            }

            else
            {
                if (endGameDataUIComp.IsBotWinner)
                {
                    endGameViewUIComp.Text = "You're loser :(";
                }
                else
                {
                    endGameViewUIComp.Text = "You're WINNER!";
                }
            }
        }
        else
        {
            endGameViewUIComp.SetActiveZone(false);
        }
    }
}
