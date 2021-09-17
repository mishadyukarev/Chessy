using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;

internal sealed class DonerMasterSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoFilter = default;
    private EcsFilter<ForDonerMasCom> _donerFilter = default;
    private EcsFilter<MotionsDataUIComponent> _motionsFilter = default;
    private EcsFilter<DonerDataUIComponent> _donerDataUIFilter = default;
    private EcsFilter<InventorUnitsComponent> _invUnitsFilter = default;

    private EcsFilter<CellViewComponent> _cellViewFilter = default;
    private EcsFilter<WhoseMoveCom> _whoseMoveFilter = default;


    private Dictionary<bool, bool> _doneOrNotFromStartAnyUpdate = new Dictionary<bool, bool>();

    private bool _isMasterMotion = true;

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

        ref var whoseMoveCom = ref _whoseMoveFilter.Get1(0);

        if (!_invUnitsFilter.Get1(0).HaveUnitInInventor(UnitTypes.King, sender.IsMasterClient))
        {
            RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

            if (PhotonNetwork.OfflineMode)
            {
                if (_isMasterMotion)
                {
                    _isMasterMotion = !_isMasterMotion;

                    CameraComComp.SetPosRotClient(_isMasterMotion);

                    whoseMoveCom.IsMainMove = _isMasterMotion;

                    foreach (byte curIdxCell in _cellViewFilter) 
                        _cellViewFilter.Get1(curIdxCell).SetRotForClient(_isMasterMotion);

                }
                else
                {
                    _isMasterMotion = !_isMasterMotion;

                    CameraComComp.SetPosRotClient(_isMasterMotion);

                    whoseMoveCom.IsMainMove = _isMasterMotion;

                    foreach (byte curIdxCell in _cellViewFilter)
                        _cellViewFilter.Get1(curIdxCell).SetRotForClient(_isMasterMotion);

                    GameMasterSystemManager.UpdateMotion.Run();
                    RpcSys.ActiveAmountMotionUIToGeneral(RpcTarget.MasterClient);
                }
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

                        _doneOrNotFromStartAnyUpdate[false] = true;
                        donerDataUICom.SetDoned(false, true);
                    }
                }
            }
        }
    }
}
