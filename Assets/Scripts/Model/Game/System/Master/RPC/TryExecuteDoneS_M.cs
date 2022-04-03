using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Extensions;
using Chessy.Game.Values;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class TryExecuteDoneS_M : SystemModelGameAbs
    {
        internal TryExecuteDoneS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void TryDone(in GameModeTC gameModeTC, in Player sender, in PlayerTypes senderPlayerT)
        {
            if (!eMG.PlayerInfoE(senderPlayerT).KingInfoE.HaveInInventor)
            {
                if (eMG.PlayerInfoE(senderPlayerT).GodInfoE.UnitTC.HaveUnit)
                {
                    if (PhotonNetwork.OfflineMode)
                    {
                        eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.AfterUpdate);

                        if (gameModeTC.Is(GameModes.TrainingOff))
                        {
                            UpdateCooldonsStunsAndOther(1);

                            sMG.MasterSs.UpdateS_M.Run(gameModeTC);
                            eMG.RpcPoolEs.ActiveMotionZone_ToGeneneral(sender);
                        }

                        else if (gameModeTC.Is(GameModes.WithFriendOff))
                        {
                            UpdateCooldonsStunsAndOther(0.5f);

                            var nextPlayer = eMG.CurPlayerITC.PlayerT.NextPlayer();

                            if (nextPlayer == PlayerTypes.First)
                            {
                                sMG.MasterSs.UpdateS_M.Run(gameModeTC);
                                eMG.RpcPoolEs.ActiveMotionZone_ToGeneneral(sender);
                            }

                            eMG.WhoseMove.PlayerT = nextPlayer;
                            eMG.CurPlayerITC.PlayerT = nextPlayer;

                            eMG.ZoneInfoC.IsActiveFriend = true;
                        }
                    }
                    else
                    {
                        UpdateCooldonsStunsAndOther(0.5f);

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
                else
                {
                    eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedGetHero, sender);
                }
            }
            else
            {
                eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedSetKing, sender);
            }
        }


        void UpdateCooldonsStunsAndOther(in float taking)
        {
            for (var playerT = PlayerTypes.First; playerT < PlayerTypes.End; playerT++)
            {
                eMG.PlayerInfoE(playerT).GodInfoE.CooldownC.Cooldown -= taking;
            }

            for (byte idx = 0; idx < StartValues.CELLS; idx++)
            {
                eMG.UnitEffectStunC(idx).Stun -= taking;

                for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
                {
                    eMG.UnitAbilityE(idx).Cooldown(abilityT) -= taking;
                }
            }
        }
    }
}