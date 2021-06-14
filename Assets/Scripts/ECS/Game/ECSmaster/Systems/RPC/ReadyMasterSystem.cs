using Photon.Pun;

internal sealed class ReadyMasterSystem : RPCMasterSystemReduction
{
    private bool IsReady => _eGM.RpcGeneralEnt_FromInfoCom.IsActived;
    private PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;

    public override void Run()
    {
        base.Run();

        _eGM.ReadyEnt_ActivatedDictCom.SetIsActivated(Info.Sender.IsMasterClient, IsReady);

        if (_eGM.ReadyEnt_ActivatedDictCom.IsActivatedAll)
            _photonPunRPC.ReadyToGeneral(RpcTarget.All, false, true);

        else _photonPunRPC.ReadyToGeneral(Info.Sender, IsReady, false);
    }
}
