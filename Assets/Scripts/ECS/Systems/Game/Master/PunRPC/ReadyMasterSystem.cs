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


        MiddleVisUIWorker.SetIsReady(InfoFrom.Sender.IsMasterClient, IsReady);

        if (MiddleVisUIWorker.IsReady(true) && MiddleVisUIWorker.IsReady(false))
        {
            MiddleVisUIWorker.IsStartedGame = true;
        }

        else
        {
            MiddleVisUIWorker.IsStartedGame = false;
        }
    }
}
