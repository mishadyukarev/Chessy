using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;
using static Assets.Scripts.Main;

internal sealed class DonerMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

    internal bool isDone => _eGM.RpcGeneralEnt_RPCCom.NeedActiveSomething;

    public override void Run()
    {
        base.Run();

        if (_eGM.UnitInfoEnt_UnitInventorCom.IsSettedKing(InfoFrom.Sender.IsMasterClient))
        {
            _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

            _photonPunRPC.DoneToGeneral(InfoFrom.Sender, false, isDone, _eGM.MotionEnt_AmountCom.Amount);

            _eGM.DonerEnt_IsActivatedDictCom.SetActivated(InfoFrom.Sender.IsMasterClient, isDone);

            bool isRefreshed = PhotonNetwork.OfflineMode/*Instance.GameModeType == GameModTypes.WithBot*/
                || _eGM.DonerEnt_IsActivatedDictCom.IsActivated(true)
                && _eGM.DonerEnt_IsActivatedDictCom.IsActivated(false);

            if (isRefreshed)
            {            

                _sMM.TryInvokeRunSystem(nameof(UpdateMotionMasterSystem), _sMM.RPCSystems);

                _photonPunRPC.DoneToGeneral(RpcTarget.All, true, false, _eGM.MotionEnt_AmountCom.Amount);
            }
        }
        else
        {
            _photonPunRPC.MistakeUnitToGeneral(InfoFrom.Sender);
        }
    }
}
