using Assets.Scripts;
using Assets.Scripts.Workers.Game.UI;
using Photon.Pun;

internal sealed class ReadyMasterSystem : RPCMasterSystemReduction
{
    private PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.FromInfo;

    private bool IsReady => _eMM.ReadyEnt_IsActivatedCom.IsActivated;

    public override void Run()
    {
        base.Run();


        MiddleViewUIWorker.SetIsReady(InfoFrom.Sender.IsMasterClient, IsReady);

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
