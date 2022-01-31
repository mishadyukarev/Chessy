using Game.Common;
using Photon.Pun;

namespace Game.Game
{
    sealed class DonerMS : SystemAbstract, IEcsRunSystem
    {
        readonly SystemsMaster _systemsMaster;

        public DonerMS(in SystemsMaster systemsM, in Entities ents) : base(ents)
        {
            _systemsMaster = systemsM;
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            Es.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);


            if (PhotonNetwork.OfflineMode)
            {
                if (GameModeC.IsGameMode(GameModes.TrainingOff))
                {
                    foreach (byte idx_0 in CellEs.Idxs)
                    {
                        UnitEs.Stun(idx_0).ExecuteAfterTrainingDoner();
                        //EntitiesPool.IceWalls[idx_0].Hp.Take(2);
                    }
                    _systemsMaster.InvokeRun(SystemDataMasterTypes.UpdateMove);
                    Es.Rpc.ActiveMotionZoneToGen(sender);
                }

                else if (GameModeC.IsGameMode(GameModes.WithFriendOff))
                {
                    foreach (byte idx_0 in CellEs.Idxs)
                    {
                        UnitEs.Stun(idx_0).ExecuteAfterWithFriendDoner();
                        //EntitiesPool.IceWalls[idx_0].Hp.Take();
                    }

                    var curPlayer = Es.WhoseMove.CurPlayerI;
                    var nextPlayer = Es.WhoseMove.NextPlayerFrom(curPlayer);

                    if (nextPlayer == PlayerTypes.First)
                    {
                        _systemsMaster.InvokeRun(SystemDataMasterTypes.UpdateMove);
                        Es.Rpc.ActiveMotionZoneToGen(sender);
                    }

                    Es.WhoseMove.WhoseMove.Player = nextPlayer;


                    curPlayer = Es.WhoseMove.CurPlayerI;

                    //ViewDataSC.RotateAll.Invoke();

                    Es.FriendZoneE.IsActiveC.IsActive = true;
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

                //    //        Ents.Rpc.ActiveMotionZoneToGen(PlayerTypes.First.GetPlayer());
                //    //        Ents.Rpc.ActiveMotionZoneToGen(PlayerTypes.Second.GetPlayer());
                //    //    }

                //    //    WhoseMoveC.SetWhoseMove(WhoseMoveC.NextPlayerFrom(playerSend));
                //    //}
                //}
            }
        }
    }
}