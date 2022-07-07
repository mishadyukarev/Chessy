using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void TryExecuteDoneReadyM(in Player sender)
        {
            //var senderPlayerT = PhotonNetwork.OfflineMode ? _e.WhoseMovePlayerT : sender.GetPlayer();

            //if (!_e.PlayerInfoE(senderPlayerT).PlayerInfoC.HaveKingInInventor)
            //{
            //    if (_e.PlayerInfoE(senderPlayerT).GodInfoC.UnitT.HaveUnit())
            //    {
            //        if (_e.WhoseMovePlayerT == senderPlayerT)
            //        {
            //            if (PhotonNetwork.OfflineMode)
            //            {
            //                RpcSs.ActiveMotionZoneToGeneneral(sender);
            //                RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.AfterUpdate);

            //                if (_e.GameModeT.Is(GameModeTypes.TrainingOffline))
            //                {
            //                    UpdateCooldonsStunsAndOther(1);

            //                    ExecuteUpdateEverythingMS.Execute();
            //                }

            //                else if (_e.GameModeT.Is(GameModeTypes.WithFriendOffline))
            //                {
            //                    UpdateCooldonsStunsAndOther(0.5f);

            //                    var nextPlayer = _e.CurrentPlayerIT.NextPlayer();

            //                    if (nextPlayer == PlayerTypes.First)
            //                    {
            //                        ExecuteUpdateEverythingMS.Execute();
            //                    }

            //                    _e.WhoseMovePlayerT = nextPlayer;
            //                    _e.CurrentPlayerIT = nextPlayer;

            //                    _e.ZoneInfoC.IsActiveFriend = true;
            //                }
            //            }
            //            else
            //            {
            //                UpdateCooldonsStunsAndOther(0.5f);

            //                _e.WhoseMovePlayerT = senderPlayerT.NextPlayer();

            //                if (senderPlayerT == PlayerTypes.Second)
            //                {
            //                    ExecuteUpdateEverythingMS.Execute();

            //                    RpcSs.ActiveMotionZoneToGeneneral(RpcTarget.All);
            //                    RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.AfterUpdate);
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedGetHero, sender);
            //    }
            //}
            //else
            //{
            //    RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedSetKing, sender);
            //}
        }


        //void UpdateCooldonsStunsAndOther(in float taking)
        //{
        //    for (var playerT = PlayerTypes.First; playerT < PlayerTypes.End; playerT++)
        //    {
        //        _e.PlayerInfoE(playerT).GodInfoC.Cooldown -= taking;
        //    }

        //    for (byte idx = 0; idx < IndexCellsValues.CELLS; idx++)
        //    {
        //        _e.UnitEffectsC(idx).StunHowManyUpdatesNeedStay -= taking;

        //        for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
        //        {
        //            _e.UnitCooldownAbilitiesC(idx).Take(abilityT, taking);
        //        }
        //    }
        //}
    }
}