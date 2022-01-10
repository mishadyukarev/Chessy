﻿using Game.Common;
using Photon.Pun;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class DonerMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.ClickToTable);


            if (PhotonNetwork.OfflineMode)
            {
                if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                {
                    DataMastSC.InvokeRun(MastDataSysTypes.Update);
                    EntityPool.Rpc<RpcC>().ActiveMotionZoneToGen(sender);
                }

                else if (GameModesCom.IsGameMode(GameModes.WithFriendOff))
                {
                    var curPlayer = WhoseMoveC.CurPlayerI;
                    var nextPlayer = WhoseMoveC.NextPlayerFrom(curPlayer);

                    if (nextPlayer == PlayerTypes.First)
                    {
                        DataMastSC.InvokeRun(MastDataSysTypes.Update);
                        EntityPool.Rpc<RpcC>().ActiveMotionZoneToGen(sender);
                    }

                    WhoseMoveC.SetWhoseMove(nextPlayer);


                    //curPlayer = WhoseMoveC.CurPlayerI;

                    //ViewDataSC.RotateAll.Invoke(); 

                    FriendC.IsActiveFriendZone = true;
                }
            }
            else
            {
                var playerSend = sender.GetPlayer();

                if (WhoseMoveC.WhoseMove == playerSend)
                {
                    if (!InvUnitsC.Have(UnitTypes.King, LevelTypes.First, sender.GetPlayer()))
                    {
                        if (playerSend == PlayerTypes.Second)
                        {
                            DataMastSC.InvokeRun(MastDataSysTypes.Update);

                            EntityPool.Rpc<RpcC>().ActiveMotionZoneToGen(PlayerTypes.First.GetPlayer());
                            EntityPool.Rpc<RpcC>().ActiveMotionZoneToGen(PlayerTypes.Second.GetPlayer());
                        }

                        WhoseMoveC.SetWhoseMove(WhoseMoveC.NextPlayerFrom(playerSend));
                    }
                }
            }
        }
    }
}