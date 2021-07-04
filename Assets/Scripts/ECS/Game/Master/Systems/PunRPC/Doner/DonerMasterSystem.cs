using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;
using static Assets.Scripts.Main;

internal sealed class DonerMasterSystem : RPCMasterSystemReduction
{
    internal bool isDone => _eGM.RpcGeneralEnt_RPCCom.NeedActiveSomething;
    internal PhotonMessageInfo info => _eGM.RpcGeneralEnt_RPCCom.FromInfo;


    public override void Run()
    {
        base.Run();

        if (_eGM.UnitInfoEnt_UnitInventorCom.IsSettedKing(info.Sender.IsMasterClient))
        {
            _photonPunRPC.DoneToGeneral(info.Sender, false, isDone, _eGM.MotionEnt_AmountCom.Amount);

            _eGM.DonerEnt_IsActivatedDictCom.SetIsActivated(info.Sender.IsMasterClient, isDone);

            bool isRefreshed = Instance.GameModeType == GameModTypes.WithBot
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
            _photonPunRPC.MistakeUnitToGeneral(info.Sender);
        }
    }
}
