﻿using Game.Common;
using Photon.Pun;

namespace Game.Game
{
    struct DonerMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);


            if (PhotonNetwork.OfflineMode)
            {
                if (GameModeC.IsGameMode(GameModes.TrainingOff))
                {
                    foreach (byte idx_0 in CellEs.Idxs)
                    {
                        CellUnitStunEs.ForExitStun(idx_0).Take(2);
                        CellIceWallEs.Hp(idx_0).Take(2);
                    }
                    SystemDataMasterManager.InvokeRun(SystemDataMasterTypes.UpdateMove);
                    EntityPool.Rpc.ActiveMotionZoneToGen(sender);
                }

                else if (GameModeC.IsGameMode(GameModes.WithFriendOff))
                {
                    foreach (byte idx_0 in CellEs.Idxs)
                    {
                        CellUnitStunEs.ForExitStun(idx_0).Take();
                        CellIceWallEs.Hp(idx_0).Take();
                    }

                    var curPlayer = WhoseMoveE.CurPlayerI;
                    var nextPlayer = WhoseMoveE.NextPlayerFrom(curPlayer);

                    if (nextPlayer == PlayerTypes.First)
                    {
                        SystemDataMasterManager.InvokeRun(SystemDataMasterTypes.UpdateMove);
                        EntityPool.Rpc.ActiveMotionZoneToGen(sender);
                    }

                    WhoseMoveE.WhoseMove.Player = nextPlayer;


                    curPlayer = WhoseMoveE.CurPlayerI;

                    //ViewDataSC.RotateAll.Invoke();

                    EntityPool.FriendZone<IsActiveC>().IsActive = true;
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

                //    //        EntityPool.Rpc.ActiveMotionZoneToGen(PlayerTypes.First.GetPlayer());
                //    //        EntityPool.Rpc.ActiveMotionZoneToGen(PlayerTypes.Second.GetPlayer());
                //    //    }

                //    //    WhoseMoveC.SetWhoseMove(WhoseMoveC.NextPlayerFrom(playerSend));
                //    //}
                //}
            }
        }
    }
}