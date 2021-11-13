using Leopotam.Ecs;
using Photon.Pun;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class DonerMastSys : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            RpcSys.SoundToGeneral(sender, ClipGameTypes.ClickToTable);


            if (PhotonNetwork.OfflineMode)
            {
                if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                {
                    DataMastC.InvokeRun(MastDataSysTypes.Update);
                    RpcSys.ActiveMotionZoneToGen(sender);
                }

                else if (GameModesCom.IsGameMode(GameModes.WithFriendOff))
                {
                    var curPlayer = WhoseMoveC.CurPlayerI;
                    var nextPlayer = WhoseMoveC.NextPlayerFrom(curPlayer);

                    if(nextPlayer == PlayerTypes.First)
                    {
                        DataMastC.InvokeRun(MastDataSysTypes.Update);
                        RpcSys.ActiveMotionZoneToGen(sender);
                    }

                    WhoseMoveC.SetWhoseMove(nextPlayer);


                    curPlayer = WhoseMoveC.CurPlayerI;

                    GameGenSysDataViewC.RotateAll.Invoke(); 

                    FriendC.IsActiveFriendZone = true;     
                }
            }
            else
            {
                var playerSend = sender.GetPlayerType();

                if (WhoseMoveC.WhoseMove == playerSend)
                {
                    if (!InvUnitsC.Have(sender.GetPlayerType(), UnitTypes.King, LevelUnitTypes.First))
                    {
                        if (playerSend == PlayerTypes.Second)
                        {
                            DataMastC.InvokeRun(MastDataSysTypes.Update);

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