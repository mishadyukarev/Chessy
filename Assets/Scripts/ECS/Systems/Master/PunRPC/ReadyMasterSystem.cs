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

        UIDownWorker.SetDoned(InfoFrom.Sender.IsMasterClient, IsReady);

        if (UIDownWorker.IsDoned(true)
            && UIDownWorker.IsDoned(false))
            PhotonPunRPC.ReadyToGeneral(RpcTarget.All, false, true);

        else PhotonPunRPC.ReadyToGeneral(InfoFrom.Sender, IsReady, false);
    }
}
