using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;

namespace Scripts.Game
{
    internal sealed class DonerMasterSystem : IEcsRunSystem
    {
        private EcsFilter<InfoCom> _infoFilter = default;
        private EcsFilter<InventorUnitsComponent> _invUnitsFilter = default;
        private EcsFilter<FriendZoneDataUICom> _friendUIFilt = default;

        private EcsFilter<CellViewComponent> _cellViewFilter = default;


        private PlayerTypes _playerMotion = PlayerTypes.First;

        public void Run()
        {
            ref var infoMasCom = ref _infoFilter.Get1(0);

            var sender = infoMasCom.FromInfo.Sender;



            RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);

            if (PhotonNetwork.OfflineMode)
            {
                if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                {
                    GameMasterSystemManager.UpdateMotion.Run();
                }

                else if (GameModesCom.IsGameMode(GameModes.WithFriendOff))
                {
                    _friendUIFilt.Get1(0).IsActiveFriendZone = true;

                    if (_playerMotion == PlayerTypes.First)
                    {
                        _playerMotion = PlayerTypes.Second;

                        CameraComComp.SetPosRotClient(false, SpawnInitComSys.Main_GO.transform.position);

                        WhoseMoveCom.WhoseMoveOffline = _playerMotion;

                        foreach (byte curIdxCell in _cellViewFilter)
                            _cellViewFilter.Get1(curIdxCell).SetRotForClient(false);

                    }
                    else
                    {
                        _playerMotion = PlayerTypes.First;

                        CameraComComp.SetPosRotClient(true, SpawnInitComSys.Main_GO.transform.position);

                        WhoseMoveCom.WhoseMoveOffline = _playerMotion;

                        foreach (byte curIdxCell in _cellViewFilter)
                            _cellViewFilter.Get1(curIdxCell).SetRotForClient(true);

                        GameMasterSystemManager.UpdateMotion.Run();
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
                    if (!_invUnitsFilter.Get1(0).HaveUnitInInv(sender.GetPlayerType(), UnitTypes.King, LevelUnitTypes.Wood))
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
                        }
                    }
                }
            }
        }
    }
}