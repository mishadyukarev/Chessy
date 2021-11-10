﻿using Leopotam.Ecs;
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
                }

                else if (GameModesCom.IsGameMode(GameModes.WithFriendOff))
                {
                    var curPlayer = WhoseMoveC.CurPlayerI;
                    var nextPlayer = WhoseMoveC.NextPlayerFrom(curPlayer);

                    if(nextPlayer == PlayerTypes.First)
                    {
                        DataMastC.InvokeRun(MastDataSysTypes.Update);
                    }

                    WhoseMoveC.SetWhoseMove(nextPlayer);


                    curPlayer = WhoseMoveC.CurPlayerI;

                    GameGenSysDataViewC.RotateAll.Invoke(); 

                    FriendZoneDataUIC.IsActiveFriendZone = true;     
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
                        }

                        WhoseMoveC.SetWhoseMove(WhoseMoveC.NextPlayerFrom(playerSend));
                    }
                }
            }
        }
    }
}