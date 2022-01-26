using Game.Common;
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
                        CellUnitEs.Stun(idx_0).ForExitStun.Take(2);
                        //EntitiesPool.IceWalls[idx_0].Hp.Take(2);
                    }
                    SystemDataMasterManager.InvokeRun(SystemDataMasterTypes.UpdateMove);
                    EntityPool.Rpc.ActiveMotionZoneToGen(sender);
                }

                else if (GameModeC.IsGameMode(GameModes.WithFriendOff))
                {
                    foreach (byte idx_0 in CellEs.Idxs)
                    {
                        CellUnitEs.Stun(idx_0).ForExitStun.Take();
                        //EntitiesPool.IceWalls[idx_0].Hp.Take();
                    }

                    var curPlayer = Entities.WhoseMoveE.CurPlayerI;
                    var nextPlayer = Entities.WhoseMoveE.NextPlayerFrom(curPlayer);

                    if (nextPlayer == PlayerTypes.First)
                    {
                        SystemDataMasterManager.InvokeRun(SystemDataMasterTypes.UpdateMove);
                        EntityPool.Rpc.ActiveMotionZoneToGen(sender);
                    }

                    Entities.WhoseMoveE.WhoseMove.Player = nextPlayer;


                    curPlayer = Entities.WhoseMoveE.CurPlayerI;

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