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
        _eGM.ReadyEntityIsActivatedDictionaryComponent.IsActivatedDictionary[Info.Sender.IsMasterClient] = isReady;

        if (_eGM.ReadyEntityIsActivatedDictionaryComponent.IsActivatedDictionary[true]
            && _eGM.ReadyEntityIsActivatedDictionaryComponent.IsActivatedDictionary[false])
            _photonPunRPC.ReadyToGeneral(RpcTarget.All, true, true);

        else _photonPunRPC.ReadyToGeneral(Info.Sender, isReady, false);
    }
}
