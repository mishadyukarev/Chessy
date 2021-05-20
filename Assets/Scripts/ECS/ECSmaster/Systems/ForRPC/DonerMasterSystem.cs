using Leopotam.Ecs;
using Photon.Pun;
using static MainGame;

internal class DonerMasterSystem : RPCMasterSystemReduction, IEcsRunSystem
{
    private SystemsMasterManager _sMM;
    internal bool isDone => _eGM.GeneralRPCEntActiveComponent.IsActived;
    internal PhotonMessageInfo info => _eGM.GeneralRPCEntFromInfoCom.FromInfo;

    internal DonerMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _sMM = eCSmanager.SystemsMasterManager;
    }

    public void Run()
    {
        if (_eGM.InfoEnt_UnitsInfoCom.IsSettedKingDict[info.Sender.IsMasterClient])
        {
            _photonPunRPC.DoneToGeneral(info.Sender, false, isDone, _eGM.UpdatorEntityAmountComponent.Amount);

            _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[info.Sender.IsMasterClient] = isDone;

            bool isRefreshed = InstanceGame.StartValuesGameConfig.IS_TEST
                || _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[true]
                && _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[false];

            if (isRefreshed)
            {
                _sMM.TryInvokeRunSystem(nameof(UpdateMotionMasterSystem), _sMM.SoloSystems);

                _photonPunRPC.DoneToGeneral(RpcTarget.All, true, false, _eGM.UpdatorEntityAmountComponent.Amount);
            }
        }
        else
        {
            _photonPunRPC.MistakeUnitToGeneral(info.Sender);
        }
    }
}
