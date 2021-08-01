using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Common;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Game.UI;
using Photon.Pun;
using System;
using System.Collections.Generic;

internal sealed class DonerMasterSystem : SystemMasterReduction
{
    private Dictionary<bool, bool> _doneOrNotFromStartAnyUpdate = new Dictionary<bool, bool>();

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

        if (InfoUnitsContainer.IsSettedKing(RpcWorker.InfoFrom.Sender.IsMasterClient))
        {
            PhotonPunRPC.SoundToGeneral(RpcWorker.InfoFrom.Sender, SoundEffectTypes.ClickToTable);

            if (PhotonNetwork.OfflineMode)
            {
                SystemsGameMasterManager.UpdateMotion.Run();

                PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, MiddleViewUIWorker.AmountMotions);
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
                        DownDonerUIWorker.SetDoned(RpcWorker.InfoFrom.Sender.IsMasterClient, true);
                        _doneOrNotFromStartAnyUpdate[RpcWorker.InfoFrom.Sender.IsMasterClient] = false;

                        if (RpcWorker.InfoFrom.Sender.IsMasterClient)
                        {
                            if (_doneOrNotFromStartAnyUpdate[false] == true)
                            {
                                DownDonerUIWorker.SetDoned(false, false);
                            }
                            else
                            {
                                SystemsGameMasterManager.UpdateMotion.Run();

                                PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, Main.Instance.ECSmanager.EntGameGeneralUIViewManager.MotionEnt_AmountCom.AmountMotions);
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
                                SystemsGameMasterManager.UpdateMotion.Run();

                                PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, Main.Instance.ECSmanager.EntGameGeneralUIViewManager.MotionEnt_AmountCom.AmountMotions);
                                PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);

                                DownDonerUIWorker.SetDoned(true, default);
                                DownDonerUIWorker.SetDoned(false, default);

                                _doneOrNotFromStartAnyUpdate[false] = true;
                                DownDonerUIWorker.SetDoned(false, true);
                            }
                        }
                        break;

                    case StepModeTypes.Together:
                        DownDonerUIWorker.SetDoned(RpcWorker.InfoFrom.Sender.IsMasterClient, NeedDoneOrNot);

                        bool needUpdate = PhotonNetwork.OfflineMode
                            || DownDonerUIWorker.IsDoned(true)
                            && DownDonerUIWorker.IsDoned(false);

                        if (needUpdate)
                        {
                            SystemsGameMasterManager.UpdateMotion.Run();

                            PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, Main.Instance.ECSmanager.EntGameGeneralUIViewManager.MotionEnt_AmountCom.AmountMotions);
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
            PhotonPunRPC.MistakeUnitToGeneral(RpcWorker.InfoFrom.Sender);
        }
    }
}
