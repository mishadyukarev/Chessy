using Chessy.Common;
using Chessy.Game.Extensions;
using Chessy.Game.Values;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        internal void TryExecuteDoneReadyM(in Player sender)
        {
            var senderPlayerT = PhotonNetwork.OfflineMode ? _e.WhoseMovePlayerT : sender.GetPlayer();

            if (!_e.PlayerInfoE(senderPlayerT).KingInfoE.HaveInInventor)
            {
                if (_e.PlayerInfoE(senderPlayerT).GodInfoE.UnitT.HaveUnit())
                {
                    if (_e.WhoseMovePlayerT == senderPlayerT)
                    {
                        if (PhotonNetwork.OfflineMode)
                        {
                            ActiveMotionZoneToGeneneral(sender);
                            ExecuteSoundActionToGeneral(sender, ClipTypes.AfterUpdate);

                            if (_e.Com.GameModeT.Is(GameModeTypes.TrainingOffline))
                            {
                                UpdateCooldonsStunsAndOther(1);

                                _e.ExecuteUpdateEverythingM(this);
                            }

                            else if (_e.Com.GameModeT.Is(GameModeTypes.WithFriendOffline))
                            {
                                UpdateCooldonsStunsAndOther(0.5f);

                                var nextPlayer = _e.CurPlayerIT.NextPlayer();

                                if (nextPlayer == PlayerTypes.First)
                                {
                                    _e.ExecuteUpdateEverythingM(this);
                                }

                                _e.WhoseMovePlayerT = nextPlayer;
                                _e.CurPlayerIT = nextPlayer;

                                _e.ZoneInfoC.IsActiveFriend = true;
                            }
                        }
                        else
                        {
                            UpdateCooldonsStunsAndOther(0.5f);

                            _e.WhoseMovePlayerT = senderPlayerT.NextPlayer();

                            if (senderPlayerT == PlayerTypes.Second)
                            {
                                _e.ExecuteUpdateEverythingM(this);

                                ActiveMotionZoneToGeneneral(RpcTarget.All);
                                ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.AfterUpdate);
                            }
                        }
                    }
                }
                else
                {
                    SimpleMistakeToGeneral(MistakeTypes.NeedGetHero, sender);
                }
            }
            else
            {
                SimpleMistakeToGeneral(MistakeTypes.NeedSetKing, sender);
            }
        }


        void UpdateCooldonsStunsAndOther(in float taking)
        {
            for (var playerT = PlayerTypes.First; playerT < PlayerTypes.End; playerT++)
            {
                _e.PlayerInfoE(playerT).GodInfoE.CooldownC.Cooldown -= taking;
            }

            for (byte idx = 0; idx < StartValues.CELLS; idx++)
            {
                _e.StunUnitC(idx).Stun -= taking;

                for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
                {
                    _e.UnitCooldownAbilitiesC(idx).Take(abilityT, taking);
                }
            }
        }
    }
}