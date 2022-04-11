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
    sealed class TryExecuteDoneS_M : SystemModel
    {
        internal TryExecuteDoneS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void TryDone(in GameModeTC gameModeTC, in Player sender)
        {
            var senderPlayerT = PhotonNetwork.OfflineMode ? eMG.WhoseMovePlayerT : sender.GetPlayer();

            if (!eMG.PlayerInfoE(senderPlayerT).KingInfoE.HaveInInventor)
            {
                if (eMG.PlayerInfoE(senderPlayerT).GodInfoE.UnitTC.HaveUnit)
                {
                    if(eMG.WhoseMovePlayerT == senderPlayerT)
                    {
                        if (PhotonNetwork.OfflineMode)
                        {
                            eMG.RpcPoolEs.ActiveMotionZone_ToGeneneral(sender);
                            eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.AfterUpdate);

                            if (gameModeTC.Is(GameModeTypes.TrainingOff))
                            {
                                UpdateCooldonsStunsAndOther(1);

                                sMG.MasterSs.UpdateS_M.Run(gameModeTC);
                            }

                            else if (gameModeTC.Is(GameModeTypes.WithFriendOff))
                            {
                                UpdateCooldonsStunsAndOther(0.5f);

                                var nextPlayer = eMG.CurPlayerITC.PlayerT.NextPlayer();

                                if (nextPlayer == PlayerTypes.First)
                                {
                                    sMG.MasterSs.UpdateS_M.Run(gameModeTC);
                                }

                                eMG.WhoseMovePlayerTC.PlayerT = nextPlayer;
                                eMG.CurPlayerITC.PlayerT = nextPlayer;

                                eMG.ZoneInfoC.IsActiveFriend = true;
                            }
                        }
                        else
                        {
                            UpdateCooldonsStunsAndOther(0.5f);

                            eMG.WhoseMovePlayerTC.PlayerT = senderPlayerT.NextPlayer();

                            if(senderPlayerT == PlayerTypes.Second)
                            {
                                sMG.MasterSs.UpdateS_M.Run(gameModeTC);

                                eMG.RpcPoolEs.ActiveMotionZone_ToGeneneral(RpcTarget.All);
                                eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AfterUpdate);
                            }
                        }
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
                eMG.StunUnitC(idx).Stun -= taking;

                for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
                {
                    eMG.UnitCooldownAbilitiesC(idx).Take(abilityT, taking);
                }
            }
        }
    }
}