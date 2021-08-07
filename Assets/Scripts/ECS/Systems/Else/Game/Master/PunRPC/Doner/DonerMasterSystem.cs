using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Leopotam.Ecs;
using Photon.Pun;
using System;
using System.Collections.Generic;

internal sealed class DonerMasterSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _currentGameWorld = default;
    private EcsFilter<InfoMasCom> _infoFilter = default;
    private EcsFilter<DonerMasCom, NeedActiveSomethingMasCom> _donerFilter = default;
    private EcsFilter<XyUnitsComponent> _xyUnitsFilter = default;
    private EcsFilter<MotionsDataUIComponent> _motionsFilter = default;
    private EcsFilter<DonerDataUIComponent, DonerViewUIComponent> _donerUIFilter = default;

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

                RPCGameSystem.SetAmountMotionToOther(RpcTarget.All, _motionsFilter.Get1(0).AmountMotions);
                RPCGameSystem.ActiveAmountMotionUIToGeneral(RpcTarget.All);
                _donerUIFilter.Get1(0).SetDoned(true, default);
                _donerUIFilter.Get1(0).SetDoned(false, default);
            }
            else
            {
                switch (SaverComponent.StepModeType)
                {
                    case StepModeTypes.None:
                        throw new Exception();

                    case StepModeTypes.ByQueue:
                        _donerUIFilter.Get1(0).SetDoned(sender.IsMasterClient, true);
                        _doneOrNotFromStartAnyUpdate[sender.IsMasterClient] = false;

                        if (sender.IsMasterClient)
                        {
                            if (_doneOrNotFromStartAnyUpdate[false] == true)
                            {
                                _donerUIFilter.Get1(0).SetDoned(false, false);
                            }
                            else
                            {
                                GameMasterSystemManager.UpdateMotion.Run();

                                RPCGameSystem.SetAmountMotionToOther(RpcTarget.All, _motionsFilter.Get1(0).AmountMotions);
                                RPCGameSystem.ActiveAmountMotionUIToGeneral(RpcTarget.All);

                                _donerUIFilter.Get1(0).SetDoned(true, default);
                                _donerUIFilter.Get1(0).SetDoned(false, default);

                                _doneOrNotFromStartAnyUpdate[true] = true;
                                _donerUIFilter.Get1(0).SetDoned(true, true);
                            }
                        }
                        else
                        {
                            if (_doneOrNotFromStartAnyUpdate[true] == true)
                            {
                                _donerUIFilter.Get1(0).SetDoned(true, false);
                            }
                            else
                            {
                                GameMasterSystemManager.UpdateMotion.Run();

                                RPCGameSystem.SetAmountMotionToOther(RpcTarget.All, _motionsFilter.Get1(0).AmountMotions);
                                RPCGameSystem.ActiveAmountMotionUIToGeneral(RpcTarget.All);

                                _donerUIFilter.Get1(0).SetDoned(true, default);
                                _donerUIFilter.Get1(0).SetDoned(false, default);

                                _doneOrNotFromStartAnyUpdate[false] = true;
                                _donerUIFilter.Get1(0).SetDoned(false, true);
                            }
                        }
                        break;

                    case StepModeTypes.Together:
                        _donerUIFilter.Get1(0).SetDoned(sender.IsMasterClient, needActiveDoner);

                        bool needUpdate = PhotonNetwork.OfflineMode
                            || _donerUIFilter.Get1(0).IsDoned(true)
                            && _donerUIFilter.Get1(0).IsDoned(false);

                        if (needUpdate)
                        {
                            GameMasterSystemManager.UpdateMotion.Run();

                            RPCGameSystem.SetAmountMotionToOther(RpcTarget.All, _motionsFilter.Get1(0).AmountMotions);
                            RPCGameSystem.ActiveAmountMotionUIToGeneral(RpcTarget.All);

                            _donerUIFilter.Get1(0).SetDoned(true, default);
                            _donerUIFilter.Get1(0).SetDoned(false, default);
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
