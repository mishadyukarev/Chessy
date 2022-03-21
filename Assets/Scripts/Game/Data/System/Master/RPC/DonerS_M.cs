using Chessy.Common;
using Chessy.Game.Values;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.System.Model
{
    public struct DonerS_M
    {
        public DonerS_M(in Player sender, in SystemsModel sMM, in Chessy.Game.Entity.Model.EntitiesModel e)
        {
            if (PhotonNetwork.OfflineMode)
            {
                e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.AfterUpdate);

                if (GameModeC.IsGameMode(GameModes.TrainingOff))
                {
                    for (byte idx = 0; idx < StartValues.CELLS; idx++)
                    {
                        e.UnitEffectStunC(idx).Stun -= 1f;

                        for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
                        {
                            e.UnitEs(idx).CoolDownC(abilityT).Cooldown -= 1;
                        }
                    }

                    for (var playerT = PlayerTypes.First; playerT < PlayerTypes.End; playerT++)
                    {
                        e.PlayerInfoE(playerT).HeroCooldownC.Cooldown -= 1;
                    }

                    sMM.UpdateS_M.UpdateMove(sMM, e);
                    e.RpcPoolEs.ActiveMotionZoneToGen(sender);
                }

                else if (GameModeC.IsGameMode(GameModes.WithFriendOff))
                {
                    for (byte idx = 0; idx < StartValues.CELLS; idx++)
                    {
                        e.UnitEffectStunC(idx).Stun -= 0.5f;

                        for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
                        {
                            e.UnitEs(idx).CoolDownC(abilityT).Cooldown -= 0.5f;
                        }
                    }

                    for (var playerT = PlayerTypes.First; playerT < PlayerTypes.End; playerT++)
                    {
                        e.PlayerInfoE(playerT).HeroCooldownC.Cooldown -= 0.5f;
                    }


                    var curPlayer = e.CurPlayerITC.Player;
                    var nextPlayer = e.NextPlayer(curPlayer).Player;

                    if (nextPlayer == PlayerTypes.First)
                    {
                        sMM.UpdateS_M.UpdateMove(sMM, e);
                        e.RpcPoolEs.ActiveMotionZoneToGen(sender);
                    }

                    e.WhoseMove.Player = nextPlayer;


                    curPlayer = e.CurPlayerITC.Player;

                    //ViewDataSC.RotateAll.Invoke();

                    e.ZoneInfoC.IsActiveFriend = true;
                }
            }
            else
            {
                for (var playerT = PlayerTypes.First; playerT < PlayerTypes.End; playerT++)
                {
                    e.PlayerInfoE(playerT).HeroCooldownC.Cooldown -= 0.5f;
                }

                for (byte idx = 0; idx < StartValues.CELLS; idx++)
                {
                    e.UnitEffectStunC(idx).Stun -= 0.5f;

                    for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
                    {
                        e.UnitEs(idx).CoolDownC(abilityT).Cooldown -= 0.5f;
                    }
                }



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