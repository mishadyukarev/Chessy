using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.UI.Game.General.Center;
using Assets.Scripts.Supports;
using Leopotam.Ecs;

internal sealed class DonerMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoFilter = default;
    private EcsFilter<ForDonerMasCom> _donerFilter = default;
    private EcsFilter<MotionsDataUIComponent> _motionsFilter = default;
    private EcsFilter<InventorUnitsComponent> _invUnitsFilter = default;
    private EcsFilter<FriendZoneDataUICom> _friendUIFilt = default;

    private EcsFilter<CellViewComponent> _cellViewFilter = default;


    private PlayerTypes _playerMotion = PlayerTypes.First;

    public void Run()
    {
        ref var infoMasCom = ref _infoFilter.Get1(0);
        ref var forDonerMasCom = ref _donerFilter.Get1(0);

        var sender = infoMasCom.FromInfo.sender;



        RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

        if (PhotonNetwork.offlineMode)
        {
            if (GameModesCom.IsGameMode(GameModes.TrainingOff))
            {
                RpcSys.ActiveAmountMotionUIToGeneral(PhotonTargets.MasterClient);
                GameMasterSystemManager.UpdateMotion.Run();
            }

            else if (GameModesCom.IsGameMode(GameModes.WithFriendOff))
            {
                _friendUIFilt.Get1(0).IsActiveFriendZone = true;

                if (_playerMotion == PlayerTypes.First)
                {
                    _playerMotion = PlayerTypes.Second;

                    CameraComComp.SetPosRotClient(false);

                    WhoseMoveCom.WhoseMoveOffline = _playerMotion;

                    foreach (byte curIdxCell in _cellViewFilter)
                        _cellViewFilter.Get1(curIdxCell).SetRotForClient(false);

                }
                else
                {
                    _playerMotion = PlayerTypes.First;

                    CameraComComp.SetPosRotClient(true);

                    WhoseMoveCom.WhoseMoveOffline = _playerMotion;

                    foreach (byte curIdxCell in _cellViewFilter)
                        _cellViewFilter.Get1(curIdxCell).SetRotForClient(true);

                    GameMasterSystemManager.UpdateMotion.Run();
                    RpcSys.ActiveAmountMotionUIToGeneral(PhotonTargets.MasterClient);
                }
            }

            else
            {
                throw new System.Exception();
            }
        }
        else
        {
            if (sender.GetPlayerType() == _playerMotion)
            {
                if (!_invUnitsFilter.Get1(0).HaveUnitInInv(sender.GetPlayerType(), UnitTypes.King))
                {

                    if (_playerMotion == PlayerTypes.First)
                    {
                        _playerMotion = PlayerTypes.Second;
                        WhoseMoveCom.WhoseMoveOnline = _playerMotion;
                    }
                    else
                    {
                        _playerMotion = PlayerTypes.First;
                        WhoseMoveCom.WhoseMoveOnline = _playerMotion;

                        GameMasterSystemManager.UpdateMotion.Run();
                        RpcSys.ActiveAmountMotionUIToGeneral(PhotonTargets.MasterClient);
                    }
                }
            }
        }
    }
}
