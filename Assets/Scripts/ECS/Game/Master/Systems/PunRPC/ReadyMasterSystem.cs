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
            _photonPunRPC.ReadyToGeneral(RpcTarget.All, false, true);

        else _photonPunRPC.ReadyToGeneral(InfoFrom.Sender, IsReady, false);
    }
}
