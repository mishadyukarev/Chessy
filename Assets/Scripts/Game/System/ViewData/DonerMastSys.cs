using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;

namespace Scripts.Game
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
                    MastDataSysC.Run(MastDataSysTypes.Update);
                }

                else if (GameModesCom.IsGameMode(GameModes.WithFriendOff))
                {
                    var curPlayer = WhoseMoveC.CurPlayerI;
                    var nextPlayer = WhoseMoveC.NextPlayerFrom(curPlayer);

                    if(nextPlayer == PlayerTypes.First)
                    {
                        MastDataSysC.Run(MastDataSysTypes.Update);
                    }

                    WhoseMoveC.SetWhoseMove(nextPlayer);


                    curPlayer = WhoseMoveC.CurPlayerI;

                    GenViewSysC.RotateAll.Invoke(); 

                    FriendZoneDataUIC.IsActiveFriendZone = true;     
                }
            }
            else
            {
                var playerSend = sender.GetPlayerType();

                if (WhoseMoveC.WhoseMove == playerSend)
                {
                    if (!InvUnitsC.HaveUnitInInv(sender.GetPlayerType(), UnitTypes.King, LevelUnitTypes.Wood))
                    {                   
                        if (playerSend == PlayerTypes.Second)
                        {
                            MastDataSysC.Run(MastDataSysTypes.Update);
                        }

                        WhoseMoveC.SetWhoseMove(WhoseMoveC.NextPlayerFrom(playerSend));
                    }
                }
            }
        }
    }
}