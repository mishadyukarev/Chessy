using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Common;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Game.UI;
using Photon.Pun;
using System;
using System.Collections.Generic;

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

        if (InfoAmountUnitsWorker.IsSettedKing(InfoFrom.Sender.IsMasterClient))
        {
            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);

            if (PhotonNetwork.OfflineMode)
            {
                _sMM.UpdateMotion.Run();

                PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, _eGGUIM.MotionEnt_AmountCom.AmountMotions);
                PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);
                DownDonerUIWorker.SetDoned(true, default);
                DownDonerUIWorker.SetDoned(false, default);
            }
            else
            {
                switch (SaverComWorker.StepModeType)
                {
                    case StepModeTypes.None:
                        throw new Exception();

                    case StepModeTypes.ByQueue:
                        DownDonerUIWorker.SetDoned(InfoFrom.Sender.IsMasterClient, true);
                        _doneOrNotFromStartAnyUpdate[InfoFrom.Sender.IsMasterClient] = false;

                        if (InfoFrom.Sender.IsMasterClient)
                        {
                            if (_doneOrNotFromStartAnyUpdate[false] == true)
                            {
                                DownDonerUIWorker.SetDoned(false, false);
                            }
                            else
                            {
                                _sMM.UpdateMotion.Run();

                                PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, _eGGUIM.MotionEnt_AmountCom.AmountMotions);
                                PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);

                                DownDonerUIWorker.SetDoned(true, default);
                                DownDonerUIWorker.SetDoned(false, default);

                                _doneOrNotFromStartAnyUpdate[true] = true;
                                DownDonerUIWorker.SetDoned(true, true);
                            }
                        }
                        else
                        {
                            if (_doneOrNotFromStartAnyUpdate[true] == true)
                            {
                                DownDonerUIWorker.SetDoned(true, false);
                            }
                            else
                            {
                                _sMM.UpdateMotion.Run();

                                PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, _eGGUIM.MotionEnt_AmountCom.AmountMotions);
                                PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);

                                DownDonerUIWorker.SetDoned(true, default);
                                DownDonerUIWorker.SetDoned(false, default);

                                _doneOrNotFromStartAnyUpdate[false] = true;
                                DownDonerUIWorker.SetDoned(false, true);
                            }
                        }


                        _lasterWhoDoneForUpdate = InfoFrom.Sender.IsMasterClient;
                        break;

                    case StepModeTypes.Together:
                        //PhotonPunRPC.SetDonerActiveToGeneral(InfoFrom.Sender, NeedDoneOrNot);
                        DownDonerUIWorker.SetDoned(InfoFrom.Sender.IsMasterClient, NeedDoneOrNot);

                        DownDonerUIWorker.SetDoned(InfoFrom.Sender.IsMasterClient, NeedDoneOrNot);

                        bool needUpdate = PhotonNetwork.OfflineMode
                            || DownDonerUIWorker.IsDoned(true)
                            && DownDonerUIWorker.IsDoned(false);

                        if (needUpdate)
                        {
                            _sMM.UpdateMotion.Run();

                            PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, _eGGUIM.MotionEnt_AmountCom.AmountMotions);
                            PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);

                            DownDonerUIWorker.SetDoned(true, default);
                            DownDonerUIWorker.SetDoned(false, default);
                        }
                        break;

                    default:
                        throw new Exception();
                }
            }
        }
        else
        {
            PhotonPunRPC.MistakeUnitToGeneral(InfoFrom.Sender);
        }
    }
}
