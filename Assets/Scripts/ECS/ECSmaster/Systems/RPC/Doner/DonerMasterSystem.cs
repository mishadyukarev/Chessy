using Photon.Pun;
using static Main;

internal sealed class DonerMasterSystem : RPCMasterSystemReduction
{
    internal bool isDone => _eGM.RpcGeneralEnt_FromInfoCom.IsActived;
    internal PhotonMessageInfo info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;


    public override void Run()
    {
        base.Run();

        if (_eGM.InfoEnt_UnitsInfoCom.IsSettedKingDict[info.Sender.IsMasterClient])
        {
            _photonPunRPC.DoneToGeneral(info.Sender, false, isDone, _eGM.InfoEnt_UpdatorCom.AmountMotions);

            _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[info.Sender.IsMasterClient] = isDone;

            bool isRefreshed = Instance.TestType == TestTypes.Standart
                || _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[true]
                && _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[false];

            if (isRefreshed)
            {
                _sMM.TryInvokeRunSystem(nameof(UpdateMotionMasterSystem), _sMM.RPCSystems);

                _photonPunRPC.DoneToGeneral(RpcTarget.All, true, false, _eGM.InfoEnt_UpdatorCom.AmountMotions);
            }
        }
        else
        {
            _photonPunRPC.MistakeUnitToGeneral(info.Sender);
        }
    }
}
