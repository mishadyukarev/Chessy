using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.System.Data.Common;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Common;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;
using System;
using System.Collections.Generic;

internal sealed class DonerMasterSystem : SystemMasterReduction
{
    private EcsFilter<XyUnitsComponent> _xyUnitsFilter;

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

        ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);

        if (xyUnitsCom.IsSettedKing(RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient))
        {
            PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.ClickToTable);

            if (PhotonNetwork.OfflineMode)
            {
                SysDataGameMasterManager.UpdateMotion.Run();

                PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, MiddleUIDataContainer.AmountMotions);
                PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);
                DownDonerUIDataContainer.SetDoned(true, default);
                DownDonerUIDataContainer.SetDoned(false, default);
            }
            else
            {
                switch (MainDataCommSys.CommonZoneEnt_SaverCom.StepModeType)
                {
                    case StepModeTypes.None:
                        throw new Exception();

                    case StepModeTypes.ByQueue:
                        DownDonerUIDataContainer.SetDoned(RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, true);
                        _doneOrNotFromStartAnyUpdate[RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient] = false;

                        if (RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient)
                        {
                            if (_doneOrNotFromStartAnyUpdate[false] == true)
                            {
                                DownDonerUIDataContainer.SetDoned(false, false);
                            }
                            else
                            {
                                SysDataGameMasterManager.UpdateMotion.Run();

                                PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, Main.Instance.ECSmanager.SysViewGameGeneralUIManager.MotionEnt_AmountCom.AmountMotions);
                                PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);

                                DownDonerUIDataContainer.SetDoned(true, default);
                                DownDonerUIDataContainer.SetDoned(false, default);

                                _doneOrNotFromStartAnyUpdate[true] = true;
                                DownDonerUIDataContainer.SetDoned(true, true);
                            }
                        }
                        else
                        {
                            if (_doneOrNotFromStartAnyUpdate[true] == true)
                            {
                                DownDonerUIDataContainer.SetDoned(true, false);
                            }
                            else
                            {
                                SysDataGameMasterManager.UpdateMotion.Run();

                                PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, Main.Instance.ECSmanager.SysViewGameGeneralUIManager.MotionEnt_AmountCom.AmountMotions);
                                PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);

                                DownDonerUIDataContainer.SetDoned(true, default);
                                DownDonerUIDataContainer.SetDoned(false, default);

                                _doneOrNotFromStartAnyUpdate[false] = true;
                                DownDonerUIDataContainer.SetDoned(false, true);
                            }
                        }
                        break;

                    case StepModeTypes.Together:
                        DownDonerUIDataContainer.SetDoned(RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, NeedDoneOrNot);

                        bool needUpdate = PhotonNetwork.OfflineMode
                            || DownDonerUIDataContainer.IsDoned(true)
                            && DownDonerUIDataContainer.IsDoned(false);

                        if (needUpdate)
                        {
                            SysDataGameMasterManager.UpdateMotion.Run();

                            PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, Main.Instance.ECSmanager.SysViewGameGeneralUIManager.MotionEnt_AmountCom.AmountMotions);
                            PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);

                            DownDonerUIDataContainer.SetDoned(true, default);
                            DownDonerUIDataContainer.SetDoned(false, default);
                        }
                        break;

                    default:
                        throw new Exception();
                }
            }
        }
        else
        {
            PhotonPunRPC.MistakeUnitToGeneral(RpcMasterDataContainer.InfoFrom.Sender);
        }
    }
}
