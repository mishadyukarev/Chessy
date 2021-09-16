using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;
using Photon.Pun;
using System;
using System.Collections.Generic;

internal sealed class DonerMasterSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoFilter = default;
    private EcsFilter<ForDonerMasCom> _donerFilter = default;
    private EcsFilter<MotionsDataUIComponent> _motionsFilter = default;
    private EcsFilter<DonerDataUIComponent> _donerDataUIFilter = default;
    private EcsFilter<InventorUnitsComponent> _inventUnitsFilter = default;

    private Dictionary<bool, bool> _doneOrNotFromStartAnyUpdate = new Dictionary<bool, bool>();


    public void Init()
    {
        _doneOrNotFromStartAnyUpdate.Add(true, false);
        _doneOrNotFromStartAnyUpdate.Add(false, true);
    }

    public void Run()
    {
        ref var infoMasCom = ref _infoFilter.Get1(0);
        ref var forDonerMasCom = ref _donerFilter.Get1(0);
        ref var donerDataUICom = ref _donerDataUIFilter.Get1(0);

        var sender = infoMasCom.FromInfo.Sender;

        if (!_inventUnitsFilter.Get1(0).HaveUnitInInventor(UnitTypes.King, sender.IsMasterClient))
        {
            RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

            if (PhotonNetwork.OfflineMode)
            {
                GameMasterSystemManager.UpdateMotion.Run();

                RpcSys.ActiveAmountMotionUIToGeneral(RpcTarget.All);
                donerDataUICom.SetDoned(true, default);
                donerDataUICom.SetDoned(false, default);
            }
            else
            {
                donerDataUICom.SetDoned(sender.IsMasterClient, true);
                _doneOrNotFromStartAnyUpdate[sender.IsMasterClient] = false;

                if (sender.IsMasterClient)
                {
                    if (_doneOrNotFromStartAnyUpdate[false] == true)
                    {
                        donerDataUICom.SetDoned(false, false);
                    }
                    else
                    {
                        GameMasterSystemManager.UpdateMotion.Run();

                        RpcSys.ActiveAmountMotionUIToGeneral(RpcTarget.All);

                        donerDataUICom.SetDoned(true, default);
                        donerDataUICom.SetDoned(false, default);

                        _doneOrNotFromStartAnyUpdate[true] = true;
                        donerDataUICom.SetDoned(true, true);
                    }
                }
                else
                {
                    if (_doneOrNotFromStartAnyUpdate[true] == true)
                    {
                        donerDataUICom.SetDoned(true, false);
                    }
                    else
                    {
                        GameMasterSystemManager.UpdateMotion.Run();
                        RpcSys.ActiveAmountMotionUIToGeneral(RpcTarget.All);

                        donerDataUICom.SetDoned(true, default);
                        donerDataUICom.SetDoned(false, default);

                        _doneOrNotFromStartAnyUpdate[false] = true;
                        donerDataUICom.SetDoned(false, true);
                    }
                }
            }
        }
        else
        {
            //RpcGameSystem.SimpleMistakeToGeneral(MistakeTypes.) MistakeUnitToGeneral(sender);
        }
    }
}
