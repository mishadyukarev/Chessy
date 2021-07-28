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


        UIMiddleWorker.SetIsReady(InfoFrom.Sender.IsMasterClient, IsReady);

        if (UIMiddleWorker.IsReady(true) && UIMiddleWorker.IsReady(false))
        {
            UIMiddleWorker.IsStartedGame = true;
        }

        else
        {
            UIMiddleWorker.IsStartedGame = false;
        }
    }
}
