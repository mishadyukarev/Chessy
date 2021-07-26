using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Game.UI;
using Assets.Scripts.Workers.Info;
using Photon.Pun;
using System.Collections.Generic;
using static Assets.Scripts.Main;

internal sealed class DonerMasterSystem : RPCMasterSystemReduction
{
    private Dictionary<bool, bool> _doneOrNotFromStartAnyUpdate = new Dictionary<bool, bool>();
    private bool _lasterWhoDoneForUpdate;

    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.FromInfo;

    internal bool NeedDoneOrNot => _eMM.DonerEnt_IsActivatedCom.IsActivated;

    public override void Init()
    {
        base.Init();

        _doneOrNotFromStartAnyUpdate.Add(true, false);
        _doneOrNotFromStartAnyUpdate.Add(false, true);
    }

    public override void Run()
    {
        base.Run();

        if (InfoUnitsWorker.IsSettedKing(InfoFrom.Sender.IsMasterClient))
        {
            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

            if (PhotonNetwork.OfflineMode)
            {
                _sMM.TryInvokeRunSystem(nameof(UpdateMotionMasterSystem), _sMM.RpcSystems);

                PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, _eGGUIM.MotionEnt_AmountCom.AmountMotions);
                PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);
                UIDownWorker.SetDoned(true, default);
                UIDownWorker.SetDoned(false, default);
            }
            else
            {
                switch (Instance.EntComM.SaverEnt_StepModeTypeCom.StepModeType)
                {
                    case StepModeTypes.None:
                        throw new System.Exception();

                    case StepModeTypes.ByQueue:
                        UIDownWorker.SetDoned(InfoFrom.Sender.IsMasterClient, true);
                        _doneOrNotFromStartAnyUpdate[InfoFrom.Sender.IsMasterClient] = false;

                        if (InfoFrom.Sender.IsMasterClient)
                        {
                            if (_doneOrNotFromStartAnyUpdate[false] == true)
                            {
                                UIDownWorker.SetDoned(false, false);
                            }
                            else
                            {
                                _sMM.TryInvokeRunSystem(nameof(UpdateMotionMasterSystem), _sMM.RpcSystems);

                                PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, _eGGUIM.MotionEnt_AmountCom.AmountMotions);
                                PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);

                                UIDownWorker.SetDoned(true, default);
                                UIDownWorker.SetDoned(false, default);

                                _doneOrNotFromStartAnyUpdate[true] = true;
                                UIDownWorker.SetDoned(true, true);
                            }
                        }
                        else
                        {
                            if (_doneOrNotFromStartAnyUpdate[true] == true)
                            {
                                UIDownWorker.SetDoned(true, false);
                            }
                            else
                            {
                                _sMM.TryInvokeRunSystem(nameof(UpdateMotionMasterSystem), _sMM.RpcSystems);

                                PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, _eGGUIM.MotionEnt_AmountCom.AmountMotions);
                                PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);

                                UIDownWorker.SetDoned(true, default);
                                UIDownWorker.SetDoned(false, default);

                                _doneOrNotFromStartAnyUpdate[false] = true;
                                UIDownWorker.SetDoned(false, true);
                            }
                        }


                        _lasterWhoDoneForUpdate = InfoFrom.Sender.IsMasterClient;
                        break;

                    case StepModeTypes.Together:
                        //PhotonPunRPC.SetDonerActiveToGeneral(InfoFrom.Sender, NeedDoneOrNot);
                        UIDownWorker.SetDoned(InfoFrom.Sender.IsMasterClient, NeedDoneOrNot);

                        UIDownWorker.SetDoned(InfoFrom.Sender.IsMasterClient, NeedDoneOrNot);

                        bool needUpdate = PhotonNetwork.OfflineMode
                            || UIDownWorker.IsDoned(true)
                            && UIDownWorker.IsDoned(false);

                        if (needUpdate)
                        {
                            _sMM.TryInvokeRunSystem(nameof(UpdateMotionMasterSystem), _sMM.RpcSystems);

                            PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, _eGGUIM.MotionEnt_AmountCom.AmountMotions);
                            PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);

                            UIDownWorker.SetDoned(true, default);
                            UIDownWorker.SetDoned(false, default);
                        }
                        break;

                    default:
                        throw new System.Exception();
                }
            }
        }
        else
        {
            PhotonPunRPC.MistakeUnitToGeneral(InfoFrom.Sender);
        }
    }
}
