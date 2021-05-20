using Leopotam.Ecs;
using Photon.Pun;

internal class ReadyMasterSystem : RPCMasterSystemReduction, IEcsRunSystem
{
    internal bool isReady => _eGM.GeneralRPCEntActiveComponent.IsActived;
    internal PhotonMessageInfo Info => _eGM.GeneralRPCEntFromInfoCom.FromInfo;

    internal ReadyMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {

    }

    public void Run()
    {
        _eGM.ReadyEntIsActivatedDictCom.IsActivatedDictionary[Info.Sender.IsMasterClient] = isReady;

        if (_eGM.ReadyEntIsActivatedDictCom.IsActivatedDictionary[true]
            && _eGM.ReadyEntIsActivatedDictCom.IsActivatedDictionary[false])
            _photonPunRPC.ReadyToGeneral(RpcTarget.All, true, true);

        else _photonPunRPC.ReadyToGeneral(Info.Sender, isReady, false);
    }
}
