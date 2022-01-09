using Game.Common;
using Photon.Pun;

namespace Game.Game
{
    public sealed class DonerMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            RpcS.SoundToGeneral(sender, ClipTypes.ClickToTable);


            if (PhotonNetwork.OfflineMode)
            {
                if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                {
                    DataMastSC.InvokeRun(MastDataSysTypes.Update);
                    RpcS.ActiveMotionZoneToGen(sender);
                }

                else if (GameModesCom.IsGameMode(GameModes.WithFriendOff))
                {
                    var curPlayer = WhoseMoveC.CurPlayerI;
                    var nextPlayer = WhoseMoveC.NextPlayerFrom(curPlayer);

                    if (nextPlayer == PlayerTypes.First)
                    {
                        DataMastSC.InvokeRun(MastDataSysTypes.Update);
                        RpcS.ActiveMotionZoneToGen(sender);
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

                            RpcS.ActiveMotionZoneToGen(PlayerTypes.First.GetPlayer());
                            RpcS.ActiveMotionZoneToGen(PlayerTypes.Second.GetPlayer());
                        }

                        WhoseMoveC.SetWhoseMove(WhoseMoveC.NextPlayerFrom(playerSend));
                    }
                }
            }
        }
    }
}