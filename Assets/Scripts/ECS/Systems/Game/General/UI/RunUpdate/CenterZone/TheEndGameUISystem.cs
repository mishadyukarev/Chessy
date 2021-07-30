using Assets.Scripts.Workers.Game.UI.Middle;
using Leopotam.Ecs;

internal sealed class TheEndGameUISystem : IEcsRunSystem
{
    public void Run()
    {
        if (EndGameUIWorker.IsEndGame)
        {
            EndGameUIWorker.SetActiveParent(true);
            if (EndGameUIWorker.IsLocalWinnet) EndGameUIWorker.Text = "You're WINNER!";
            else EndGameUIWorker.Text = "You're loser :(";
        }
        else
        {
            EndGameUIWorker.SetActiveParent(false);
        }
    }
}
