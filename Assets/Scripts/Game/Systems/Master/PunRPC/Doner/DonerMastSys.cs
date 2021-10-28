using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;

namespace Scripts.Game
{
    internal sealed class DonerMastSys : IEcsRunSystem
    {
        private EcsFilter<CellViewComponent> _cellViewFilter = default;

        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);

            RpcSys.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);


            if (PhotonNetwork.OfflineMode)
            {
                if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                {
                    GameMasSysDataM.UpdateMotion.Run();
                }

                else if (GameModesCom.IsGameMode(GameModes.WithFriendOff))
                {
                    var curPlayer = WhoseMoveC.CurPlayer;
                    var nextPlayer = WhoseMoveC.NextPlayerFrom(curPlayer);

                    WhoseMoveC.SetWhoseMove(nextPlayer);


                    curPlayer = WhoseMoveC.CurPlayer;

                    CameraC.SetPosRotClient(curPlayer, SpawnInitComSys.Main_GO.transform.position);
                    foreach (byte curIdxCell in _cellViewFilter)
                        _cellViewFilter.Get1(curIdxCell).SetRotForClient(curPlayer);



                    FriendZoneDataUIC.IsActiveFriendZone = true;

                    GameMasSysDataM.UpdateMotion.Run();
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