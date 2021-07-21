using Assets.Scripts;
using Photon.Pun;

internal sealed class ReadyMasterSystem : RPCMasterSystemReduction
{
    private PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

    private bool IsReady => _eMM.ReadyEnt_IsActivatedCom.IsActivated;

    public override void Run()
    {
        base.Run();

        _eGM.ReadyEnt_ActivatedDictCom.SetActivated(InfoFrom.Sender.IsMasterClient, IsReady);

        if (_eGM.ReadyEnt_ActivatedDictCom.IsActivatedAll)
            PhotonPunRPC.ReadyToGeneral(RpcTarget.All, false, true);

        else PhotonPunRPC.ReadyToGeneral(InfoFrom.Sender, IsReady, false);
    }
}
