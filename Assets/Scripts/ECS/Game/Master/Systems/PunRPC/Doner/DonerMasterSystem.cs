using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Info;
using Photon.Pun;
using System.Collections.Generic;
using static Assets.Scripts.Main;

internal sealed class DonerMasterSystem : RPCMasterSystemReduction
{
    private Dictionary<bool, bool> _doneOrNotFromStartAnyUpdate = new Dictionary<bool, bool>();
    private bool _lasterWhoDoneForUpdate;

    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

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

                PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, _eGGUIM.MotionEnt_AmountCom.Amount);
                PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);
                _eGGUIM.DonerUIEnt_IsActivatedDictCom.ResetAll();
            }
            else
            {
                switch (Instance.EntComM.SaverEnt_StepModeTypeCom.StepModeType)
                {
                    case StepModeTypes.None:
                        throw new System.Exception();

                    case StepModeTypes.ByQueue:
                        _eGGUIM.DonerUIEnt_IsActivatedDictCom.SetActivated(InfoFrom.Sender.IsMasterClient, true);
                        _doneOrNotFromStartAnyUpdate[InfoFrom.Sender.IsMasterClient] = false;

                        if (InfoFrom.Sender.IsMasterClient)
                        {
                            if (_doneOrNotFromStartAnyUpdate[false] == true)
                            {
                                _eGGUIM.DonerUIEnt_IsActivatedDictCom.SetActivated(false, false);
                                //PhotonPunRPC.SetDonerActiveToGeneral(PhotonNetwork.PlayerList[1], false);
                            }
                            else
                            {
                                _sMM.TryInvokeRunSystem(nameof(UpdateMotionMasterSystem), _sMM.RpcSystems);

                                PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, _eGGUIM.MotionEnt_AmountCom.Amount);
                                PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);
                                _eGGUIM.DonerUIEnt_IsActivatedDictCom.ResetAll();

                                _eGGUIM.DonerUIEnt_IsActivatedDictCom.ResetAll();

                                _doneOrNotFromStartAnyUpdate[true] = true;
                                _eGGUIM.DonerUIEnt_IsActivatedDictCom.SetActivated(true, true);
                            }
                        }
                        else
                        {
                            if (_doneOrNotFromStartAnyUpdate[true] == true)
                            {
                                _eGGUIM.DonerUIEnt_IsActivatedDictCom.SetActivated(true, false);
                            }
                            else
                            {
                                _sMM.TryInvokeRunSystem(nameof(UpdateMotionMasterSystem), _sMM.RpcSystems);

                                PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, _eGGUIM.MotionEnt_AmountCom.Amount);
                                PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);
                                _eGGUIM.DonerUIEnt_IsActivatedDictCom.ResetAll();

                                _doneOrNotFromStartAnyUpdate[false] = true;
                                _eGGUIM.DonerUIEnt_IsActivatedDictCom.SetActivated(false, true);
                            }
                        }


                        _lasterWhoDoneForUpdate = InfoFrom.Sender.IsMasterClient;
                        break;

                    case StepModeTypes.Together:
                        //PhotonPunRPC.SetDonerActiveToGeneral(InfoFrom.Sender, NeedDoneOrNot);
                        _eGGUIM.DonerUIEnt_IsActivatedDictCom.SetActivated(InfoFrom.Sender.IsMasterClient, NeedDoneOrNot);

                        _eGGUIM.DonerUIEnt_IsActivatedDictCom.SetActivated(InfoFrom.Sender.IsMasterClient, NeedDoneOrNot);

                        bool needUpdate = PhotonNetwork.OfflineMode
                            || _eGGUIM.DonerUIEnt_IsActivatedDictCom.IsActivated(true)
                            && _eGGUIM.DonerUIEnt_IsActivatedDictCom.IsActivated(false);

                        if (needUpdate)
                        {
                            _sMM.TryInvokeRunSystem(nameof(UpdateMotionMasterSystem), _sMM.RpcSystems);

                            PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, _eGGUIM.MotionEnt_AmountCom.Amount);
                            PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);
                            _eGGUIM.DonerUIEnt_IsActivatedDictCom.ResetAll();
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
