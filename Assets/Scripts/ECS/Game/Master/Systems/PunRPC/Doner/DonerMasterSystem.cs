using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;
using static Assets.Scripts.Main;

internal sealed class DonerMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

    internal bool NeedDoneOrNot => _eGM.RpcGeneralEnt_RPCCom.NeedActiveSomething;

    public override void Run()
    {
        base.Run();

        if (_eGM.UnitInfoEnt_UnitInventorCom.IsSettedKing(InfoFrom.Sender.IsMasterClient))
        {
            _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

            //_photonPunRPC.DoneToGeneral(InfoFrom.Sender, false, NeedDoneOrNot, _eGM.MotionEnt_AmountCom.Amount);

            _photonPunRPC.SetDonerActiveToGeneral(InfoFrom.Sender, NeedDoneOrNot);

            _eGM.DonerUIEnt_IsActivatedDictCom.SetActivated(InfoFrom.Sender.IsMasterClient, NeedDoneOrNot);

            bool isRefreshed = PhotonNetwork.OfflineMode
                || _eGM.DonerUIEnt_IsActivatedDictCom.IsActivated(true)
                && _eGM.DonerUIEnt_IsActivatedDictCom.IsActivated(false);

            if (isRefreshed)
            {            
                _sMM.TryInvokeRunSystem(nameof(UpdateMotionMasterSystem), _sMM.RpcSystems);

                _photonPunRPC.SetAmountMotionToGeneral(RpcTarget.All, _eGM.MotionEnt_AmountCom.Amount);
                _photonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);
                _photonPunRPC.SetDonerActiveToGeneral(RpcTarget.All, false);
            }
        }
        else
        {
            _photonPunRPC.MistakeUnitToGeneral(InfoFrom.Sender);
        }
    }
}
