using Photon.Pun;

internal sealed class ReadyMasterSystem : RPCMasterSystemReduction
{
    private bool IsReady => _eGM.RpcGeneralEnt_FromInfoCom.IsActived;
    private PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;

    public override void Run()
    {
        base.Run();

        _eGM.ReadyEnt_ReadyCom.IsActivatedDictionary[Info.Sender.IsMasterClient] = IsReady;

        if (_eGM.ReadyEnt_ReadyCom.IsActivatedDictionary[true]
            && _eGM.ReadyEnt_ReadyCom.IsActivatedDictionary[false])
            _photonPunRPC.ReadyToGeneral(RpcTarget.All, false, true);

        else _photonPunRPC.ReadyToGeneral(Info.Sender, IsReady, false);
    }
}
