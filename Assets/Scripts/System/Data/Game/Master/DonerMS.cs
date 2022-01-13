using Game.Common;
using Photon.Pun;

namespace Game.Game
{
    struct DonerMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.ClickToTable);


            if (PhotonNetwork.OfflineMode)
            {
                if (GameModeC.IsGameMode(GameModes.TrainingOff))
                {
                    SystemDataMasterManager.InvokeRun(SystemDataMasterTypes.Update);
                    EntityPool.Rpc<RpcC>().ActiveMotionZoneToGen(sender);
                }

                else if (GameModeC.IsGameMode(GameModes.WithFriendOff))
                {
                    //var curPlayer = WhoseMoveC.WhoseMove<WhoseMoveEC>().CurPlayerI;
                    //var nextPlayer = WhoseMoveC.NextPlayerFrom(curPlayer);

                    //if (nextPlayer == PlayerTypes.First)
                    //{
                    //    SystemDataMasterManager.InvokeRun(SystemDataMasterTypes.Update);
                    //    EntityPool.Rpc<RpcC>().ActiveMotionZoneToGen(sender);
                    //}

                    //WhoseMoveC.SetWhoseMove(nextPlayer);


                    //curPlayer = WhoseMoveC.WhoseMove<WhoseMoveEC>().CurPlayerI;

                    //ViewDataSC.RotateAll.Invoke(); 

                    EntityPool.FriendZone<IsActivatedC>().IsActivated = true;
                }
            }
            else
            {
                var playerSend = sender.GetPlayer();

                //if (WhoseMoveC.WhoseMove == playerSend)
                //{
                //    //if (!EntInventorUnits.Have(UnitTypes.King, LevelTypes.First, sender.GetPlayer()))
                //    //{
                //    //    if (playerSend == PlayerTypes.Second)
                //    //    {
                //    //        SystemDataMasterManager.InvokeRun(SystemDataMasterTypes.Update);

                //    //        EntityPool.Rpc<RpcC>().ActiveMotionZoneToGen(PlayerTypes.First.GetPlayer());
                //    //        EntityPool.Rpc<RpcC>().ActiveMotionZoneToGen(PlayerTypes.Second.GetPlayer());
                //    //    }

                //    //    WhoseMoveC.SetWhoseMove(WhoseMoveC.NextPlayerFrom(playerSend));
                //    //}
                //}
            }
        }
    }
}