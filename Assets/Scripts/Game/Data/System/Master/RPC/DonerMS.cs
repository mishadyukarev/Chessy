using Chessy.Common;
using Chessy.Game.System.Model.Master;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game
{
    public struct DonerMS
    {
        public DonerMS(in Player sender, in EntitiesModel e)
        {
            if (PhotonNetwork.OfflineMode)
            {
                e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.AfterUpdate);

                if (GameModeC.IsGameMode(GameModes.TrainingOff))
                {
                    for (byte idx = 0; idx < StartValues.CELLS; idx++)
                    {
                        e.UnitEffectStunC(idx).Stun -= 2;
                        //EntitiesPool.IceWalls[idx_0].Hp.Take(2);
                    }

                    e.UpdateMove();
                    e.RpcPoolEs.ActiveMotionZoneToGen(sender);
                }

                else if (GameModeC.IsGameMode(GameModes.WithFriendOff))
                {
                    for (byte idx = 0; idx < StartValues.CELLS; idx++)
                    {
                        e.UnitEffectStunC(idx).Stun -= 2;
                        //EntitiesPool.IceWalls[idx_0].Hp.Take();
                    }

                    var curPlayer = e.CurPlayerITC.Player;
                    var nextPlayer = e.NextPlayer(curPlayer).Player;

                    if (nextPlayer == PlayerTypes.First)
                    {
                        e.UpdateMove();
                        e.RpcPoolEs.ActiveMotionZoneToGen(sender);
                    }

                    e.WhoseMove.Player = nextPlayer;


                    curPlayer = e.CurPlayerITC.Player;

                    //ViewDataSC.RotateAll.Invoke();

                    e.FriendIsActive = true;
                }
            }
            else
            {
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