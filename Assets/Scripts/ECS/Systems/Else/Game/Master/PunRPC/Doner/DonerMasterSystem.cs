using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Common;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;
using System;
using System.Collections.Generic;

internal sealed class DonerMasterSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _currentGameWorld;
    private EcsFilter<InfoMasCom> _infoFilter;
    private EcsFilter<DonerMasCom, NeedActiveSomethingMasCom> _donerFilter;
    private EcsFilter<XyUnitsComponent> _xyUnitsFilter;

    private Dictionary<bool, bool> _doneOrNotFromStartAnyUpdate = new Dictionary<bool, bool>();


    public void Init()
    {
        _currentGameWorld.NewEntity()
            .Replace(new DonerMasCom())
            .Replace(new NeedActiveSomethingMasCom());

        _doneOrNotFromStartAnyUpdate.Add(true, false);
        _doneOrNotFromStartAnyUpdate.Add(false, true);
    }

    public void Run()
    {
        var sender = _infoFilter.Get1(0).FromInfo.Sender;
        var needActiveDoner = _donerFilter.Get2(0).NeedActiveSomething;

        ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);

        if (xyUnitsCom.IsSettedKing(sender.IsMasterClient))
        {
            RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

            if (PhotonNetwork.OfflineMode)
            {
                GameMasterSystemManager.UpdateMotion.Run();

                RPCGameSystem.SetAmountMotionToOther(RpcTarget.All, MiddleUIDataContainer.AmountMotions);
                RPCGameSystem.ActiveAmountMotionUIToGeneral(RpcTarget.All);
                DownDonerUIDataContainer.SetDoned(true, default);
                DownDonerUIDataContainer.SetDoned(false, default);
            }
            else
            {
                switch (SaverComponent.StepModeType)
                {
                    case StepModeTypes.None:
                        throw new Exception();

                    case StepModeTypes.ByQueue:
                        DownDonerUIDataContainer.SetDoned(sender.IsMasterClient, true);
                        _doneOrNotFromStartAnyUpdate[sender.IsMasterClient] = false;

                        if (sender.IsMasterClient)
                        {
                            if (_doneOrNotFromStartAnyUpdate[false] == true)
                            {
                                DownDonerUIDataContainer.SetDoned(false, false);
                            }
                            else
                            {
                                GameMasterSystemManager.UpdateMotion.Run();

                                RPCGameSystem.SetAmountMotionToOther(RpcTarget.All, MainGameSystem.MotionEnt_AmountCom.AmountMotions);
                                RPCGameSystem.ActiveAmountMotionUIToGeneral(RpcTarget.All);

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
                                GameMasterSystemManager.UpdateMotion.Run();

                                RPCGameSystem.SetAmountMotionToOther(RpcTarget.All, MainGameSystem.MotionEnt_AmountCom.AmountMotions);
                                RPCGameSystem.ActiveAmountMotionUIToGeneral(RpcTarget.All);

                                DownDonerUIDataContainer.SetDoned(true, default);
                                DownDonerUIDataContainer.SetDoned(false, default);

                                _doneOrNotFromStartAnyUpdate[false] = true;
                                DownDonerUIDataContainer.SetDoned(false, true);
                            }
                        }
                        break;

                    case StepModeTypes.Together:
                        DownDonerUIDataContainer.SetDoned(sender.IsMasterClient, needActiveDoner);

                        bool needUpdate = PhotonNetwork.OfflineMode
                            || DownDonerUIDataContainer.IsDoned(true)
                            && DownDonerUIDataContainer.IsDoned(false);

                        if (needUpdate)
                        {
                            GameMasterSystemManager.UpdateMotion.Run();

                            RPCGameSystem.SetAmountMotionToOther(RpcTarget.All, MainGameSystem.MotionEnt_AmountCom.AmountMotions);
                            RPCGameSystem.ActiveAmountMotionUIToGeneral(RpcTarget.All);

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
            RPCGameSystem.MistakeUnitToGeneral(sender);
        }
    }
}
