using Chessy.Common;
using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        internal void TryDone(in Player sender)
        {
            var senderPlayerT = PhotonNetwork.OfflineMode ? _eMG.WhoseMovePlayerT : sender.GetPlayer();

            if (!_eMG.PlayerInfoE(senderPlayerT).KingInfoE.HaveInInventor)
            {
                if (_eMG.PlayerInfoE(senderPlayerT).GodInfoE.UnitTC.HaveUnit)
                {
                    if (_eMG.WhoseMovePlayerT == senderPlayerT)
                    {
                        if (PhotonNetwork.OfflineMode)
                        {
                            _eMG.RpcPoolEs.ActiveMotionZone_ToGeneneral(sender);
                            _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.AfterUpdate);

                            if (_eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline))
                            {
                                UpdateCooldonsStunsAndOther(1);

                                ExecuteUpdateEverythingMS.ExecuteUpdateEverythingM();
                            }

                            else if (_eMG.Common.GameModeTC.Is(GameModeTypes.WithFriendOffline))
                            {
                                UpdateCooldonsStunsAndOther(0.5f);

                                var nextPlayer = _eMG.CurPlayerITC.PlayerT.NextPlayer();

                                if (nextPlayer == PlayerTypes.First)
                                {
                                    ExecuteUpdateEverythingMS.ExecuteUpdateEverythingM();
                                }

                                _eMG.WhoseMovePlayerTC.PlayerT = nextPlayer;
                                _eMG.CurPlayerITC.PlayerT = nextPlayer;

                                _eMG.ZoneInfoC.IsActiveFriend = true;
                            }
                        }
                        else
                        {
                            UpdateCooldonsStunsAndOther(0.5f);

                            _eMG.WhoseMovePlayerTC.PlayerT = senderPlayerT.NextPlayer();

                            if (senderPlayerT == PlayerTypes.Second)
                            {
                                ExecuteUpdateEverythingMS.ExecuteUpdateEverythingM();

                                _eMG.RpcPoolEs.ActiveMotionZone_ToGeneneral(RpcTarget.All);
                                _eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AfterUpdate);
                            }
                        }
                    }
                }
                else
                {
                    _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedGetHero, sender);
                }
            }
            else
            {
                _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedSetKing, sender);
            }
        }


        void UpdateCooldonsStunsAndOther(in float taking)
        {
            for (var playerT = PlayerTypes.First; playerT < PlayerTypes.End; playerT++)
            {
                _eMG.PlayerInfoE(playerT).GodInfoE.CooldownC.Cooldown -= taking;
            }

            for (byte idx = 0; idx < StartValues.CELLS; idx++)
            {
                _eMG.StunUnitC(idx).Stun -= taking;

                for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
                {
                    _eMG.UnitCooldownAbilitiesC(idx).Take(abilityT, taking);
                }
            }
        }
    }
}