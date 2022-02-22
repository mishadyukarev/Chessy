using Game.Common;
using Photon.Pun;
using System;

namespace Game.Game
{
    sealed class DonerMS : SystemAbstract, IEcsRunSystem
    {
        readonly Action _updateMove;

        internal DonerMS(in Action updateMove, in EntitiesModel ents) : base(ents)
        {
            _updateMove = updateMove;
        }

        public void Run()
        {

            var sender = E.RpcPoolEs.SenderC.Player;

            E.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);

            if (PhotonNetwork.OfflineMode)
            {
                if (GameModeC.IsGameMode(GameModes.TrainingOff))
                {
                    for (byte idx = 0; idx < Start_Values.ALL_CELLS_AMOUNT; idx++)
                    {
                        E.UnitEffectStunC(idx).Stun -= 2;
                        //EntitiesPool.IceWalls[idx_0].Hp.Take(2);
                    }
                    _updateMove.Invoke();
                    E.RpcPoolEs.ActiveMotionZoneToGen(sender);
                }

                else if (GameModeC.IsGameMode(GameModes.WithFriendOff))
                {
                    for (byte idx = 0; idx < Start_Values.ALL_CELLS_AMOUNT; idx++)
                    {
                        E.UnitEffectStunC(idx).Stun -= 2;
                        //EntitiesPool.IceWalls[idx_0].Hp.Take();
                    }

                    var curPlayer = E.CurPlayerI.Player;
                    var nextPlayer = E.NextPlayer(curPlayer).Player;

                    if (nextPlayer == PlayerTypes.First)
                    {
                        _updateMove.Invoke();
                        E.RpcPoolEs.ActiveMotionZoneToGen(sender);
                    }

                    E.WhoseMove.Player = nextPlayer;


                    curPlayer = E.CurPlayerI.Player;

                    //ViewDataSC.RotateAll.Invoke();

                    E.FriendIsActive = true;
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