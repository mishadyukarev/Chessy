using Leopotam.Ecs;
using Photon.Pun;
using static MainGame;

internal class DonerMasterSystem : RPCMasterSystemReduction
{
    internal bool isDone => _eGM.RpcGeneralEnt_FromInfoCom.IsActived;
    internal PhotonMessageInfo info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;

    internal DonerMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public override void Run()
    {
        base.Run();

        if (_eGM.InfoEnt_UnitsInfoCom.IsSettedKingDict[info.Sender.IsMasterClient])
        {
            _photonPunRPC.DoneToGeneral(info.Sender, false, isDone, _eGM.UpdatorEntityAmountComponent.Amount);

            _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[info.Sender.IsMasterClient] = isDone;

            bool isRefreshed = Instance.StartValuesGameConfig.IS_TEST
                || _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[true]
                && _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[false];

            if (isRefreshed)
            {
                _sMM.TryInvokeRunSystem(nameof(UpdateMotionMasterSystem), _sMM.RPCSystems);

                _photonPunRPC.DoneToGeneral(RpcTarget.All, true, false, _eGM.UpdatorEntityAmountComponent.Amount);
            }
        }
        else
        {
            _photonPunRPC.MistakeUnitToGeneral(info.Sender);
        }
    }
}
