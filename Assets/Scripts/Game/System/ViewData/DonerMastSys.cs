using Leopotam.Ecs;
using Photon.Pun;
using Game.Common;

namespace Game.Game
{
    public sealed class DonerMastSys : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            RpcSys.SoundToGeneral(sender, ClipTypes.ClickToTable);


            if (PhotonNetwork.OfflineMode)
            {
                if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                {
                    DataMastSC.InvokeRun(MastDataSysTypes.Update);
                    RpcSys.ActiveMotionZoneToGen(sender);
                }

                else if (GameModesCom.IsGameMode(GameModes.WithFriendOff))
                {
                    var curPlayer = WhoseMoveC.CurPlayerI;
                    var nextPlayer = WhoseMoveC.NextPlayerFrom(curPlayer);

                    if(nextPlayer == PlayerTypes.First)
                    {
                        DataMastSC.InvokeRun(MastDataSysTypes.Update);
                        RpcSys.ActiveMotionZoneToGen(sender);
                    }

                    WhoseMoveC.SetWhoseMove(nextPlayer);


                    curPlayer = WhoseMoveC.CurPlayerI;

                    DataViewSC.RotateAll.Invoke(); 

                    FriendC.IsActiveFriendZone = true;     
                }
            }
            else
            {
                var playerSend = sender.GetPlayerType();

                if (WhoseMoveC.WhoseMove == playerSend)
                {
                    if (!InvUnitsC.Have(UnitTypes.King, LevelTypes.First, sender.GetPlayerType()))
                    {
                        if (playerSend == PlayerTypes.Second)
                        {
                            DataMastSC.InvokeRun(MastDataSysTypes.Update);

                            RpcSys.ActiveMotionZoneToGen(PlayerTypes.First.GetPlayerType());
                            RpcSys.ActiveMotionZoneToGen(PlayerTypes.Second.GetPlayerType());
                        }

                        WhoseMoveC.SetWhoseMove(WhoseMoveC.NextPlayerFrom(playerSend));
                    }
                }
            }
        }
    }
}