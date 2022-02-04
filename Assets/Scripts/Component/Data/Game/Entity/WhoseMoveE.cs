using ECS;
using Game.Common;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace Game.Game
{
    public sealed class WhoseMoveE : EntityAbstract
    {
        public ref PlayerTC WhoseMove => ref Ent.Get<PlayerTC>();

        public WhoseMoveE(in PlayerTypes whoseMove, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new PlayerTC(whoseMove));
        }

        public PlayerTypes CurPlayerI
        {
            get
            {
                switch (GameModeC.CurGameMode)
                {
                    case GameModes.TrainingOff: return PlayerTypes.First;
                    case GameModes.WithFriendOff:
                        return WhoseMove.Player;

                    case GameModes.PublicOn:
                        if (PhotonNetwork.IsMasterClient) return PlayerTypes.First;
                        else return PlayerTypes.Second;

                    case GameModes.WithFriendOn:
                        if (PhotonNetwork.IsMasterClient) return PlayerTypes.First;
                        else return PlayerTypes.Second;

                    default: throw new Exception();
                }
            }

        }
        public bool IsMyMove => CurPlayerI == WhoseMove.Player;
        public PlayerTypes NextPlayerFrom(PlayerTypes player)
        {
            if (player == PlayerTypes.First) return PlayerTypes.Second;
            else return PlayerTypes.First;
        }

        public bool Done_Master(in Player sender, in Entities e)
        {
            e.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
            var needUpdateMove = false;

            if (PhotonNetwork.OfflineMode)
            {
                if (GameModeC.IsGameMode(GameModes.TrainingOff))
                {
                    foreach (byte idx_0 in e.CellWorker.Idxs)
                    {
                        e.UnitEffectEs(idx_0).StunE.ExecuteAfterTrainingDoner();
                        //EntitiesPool.IceWalls[idx_0].Hp.Take(2);
                    }
                    needUpdateMove = true;
                    e.Rpc.ActiveMotionZoneToGen(sender);
                }

                else if (GameModeC.IsGameMode(GameModes.WithFriendOff))
                {
                    foreach (byte idx_0 in e.CellWorker.Idxs)
                    {
                        e.UnitEffectEs(idx_0).StunE.ExecuteAfterWithFriendDoner();
                        //EntitiesPool.IceWalls[idx_0].Hp.Take();
                    }

                    var curPlayer = e.WhoseMove.CurPlayerI;
                    var nextPlayer = e.WhoseMove.NextPlayerFrom(curPlayer);

                    if (nextPlayer == PlayerTypes.First)
                    {
                        needUpdateMove = true;
                        e.Rpc.ActiveMotionZoneToGen(sender);
                    }

                    e.WhoseMove.WhoseMove.Player = nextPlayer;


                    curPlayer = e.WhoseMove.CurPlayerI;

                    //ViewDataSC.RotateAll.Invoke();

                    e.FriendZoneE.IsActiveC.IsActive = true;
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

            return needUpdateMove;
        }
    }
}
