using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class DonerMastSys : IEcsRunSystem
    {
        private EcsFilter<CellViewC> _cellViewFilter = default;
        private EcsFilter<CellRiverViewC> _cellRiverFilt = default;
        private EcsFilter<CellTrailViewC> _cellTrailFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);

            RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);


            if (PhotonNetwork.OfflineMode)
            {
                if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                {
                    MastDataSysContainer.Run(MastDataSysTypes.Update);
                }

                else if (GameModesCom.IsGameMode(GameModes.WithFriendOff))
                {
                    var curPlayer = WhoseMoveC.CurPlayer;
                    var nextPlayer = WhoseMoveC.NextPlayerFrom(curPlayer);

                    if(nextPlayer == PlayerTypes.First)
                    {
                        MastDataSysContainer.Run(MastDataSysTypes.Update);
                    }

                    WhoseMoveC.SetWhoseMove(nextPlayer);


                    curPlayer = WhoseMoveC.CurPlayer;

                    CameraC.SetPosRotClient(curPlayer, SpawnInitComSys.Main_GO.transform.position);
                    foreach (byte curIdxCell in _cellViewFilter)
                    {
                        _cellViewFilter.Get1(curIdxCell).SetRotForClient(curPlayer);
                        _cellRiverFilt.Get1(curIdxCell).Rotate();
                        _cellTrailFilt.Get1(curIdxCell).Rotate();
                    }
                        



                    FriendZoneDataUIC.IsActiveFriendZone = true;

                    
                }
            }
            else
            {
                //if (Wh sender.GetPlayerType() == _playerMotion)
                //{
                //    if (!InventorUnitsC.HaveUnitInInv(sender.GetPlayerType(), UnitTypes.King, LevelUnitTypes.Wood))
                //    {
                //        if (_playerMotion == PlayerTypes.First)
                //        {
                //            _playerMotion = PlayerTypes.Second;
                //            WhoseMoveCom.WhoseMoveOnline = _playerMotion;
                //        }
                //        else
                //        {
                //            _playerMotion = PlayerTypes.First;
                //            WhoseMoveCom.WhoseMoveOnline = _playerMotion;

                //            GameMasterSystemManager.UpdateMotion.Run();
                //        }
                //    }
                //}
            }
        }
    }
}