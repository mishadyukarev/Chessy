using Assets.Scripts;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.UI;

internal sealed class ReadyMasterSystem : SystemMasterReduction
{
    private bool IsReady => _eMM.ReadyEnt_IsActivatedCom.IsActivated;

    public override void Run()
    {
        base.Run();


        MiddleViewUIWorker.SetIsReady(RpcWorker.InfoFrom.Sender.IsMasterClient, IsReady);

        if (MiddleViewUIWorker.IsReady(true) && MiddleViewUIWorker.IsReady(false))
        {
            MiddleViewUIWorker.IsStartedGame = true;
        }

        else
        {
            MiddleViewUIWorker.IsStartedGame = false;
        }
    }
}
