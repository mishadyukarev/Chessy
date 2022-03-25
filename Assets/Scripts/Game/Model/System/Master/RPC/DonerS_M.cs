﻿using Chessy.Common;
using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model.Master;
using Chessy.Game.Values;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.System.Model
{
    public sealed class DonerS_M : SystemModelGameAbs
    {
        readonly UpdateS_M _updateS_M;

        public DonerS_M(in UpdateS_M updateS_M, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _updateS_M = updateS_M;
        }

        public void Done(in GameModeTC gameModeTC, in Player sender)
        {
            if (PhotonNetwork.OfflineMode)
            {
                eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.AfterUpdate);

                if (gameModeTC.Is(GameModes.TrainingOff))
                {
                    for (byte idx = 0; idx < StartValues.CELLS; idx++)
                    {
                        eMGame.UnitEffectStunC(idx).Stun -= 1f;

                        for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
                        {
                            eMGame.UnitEs(idx).CoolDownC(abilityT).Cooldown -= 1;
                        }
                    }

                    for (var playerT = PlayerTypes.First; playerT < PlayerTypes.End; playerT++)
                    {
                        eMGame.PlayerInfoE(playerT).HeroCooldownC.Cooldown -= 1;
                    }

                    _updateS_M.Run(gameModeTC);
                    eMGame.RpcPoolEs.ActiveMotionZoneToGen(sender);
                }

                else if (gameModeTC.Is(GameModes.WithFriendOff))
                {
                    for (byte idx = 0; idx < StartValues.CELLS; idx++)
                    {
                        eMGame.UnitEffectStunC(idx).Stun -= 0.5f;

                        for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
                        {
                            eMGame.UnitEs(idx).CoolDownC(abilityT).Cooldown -= 0.5f;
                        }
                    }

                    for (var playerT = PlayerTypes.First; playerT < PlayerTypes.End; playerT++)
                    {
                        eMGame.PlayerInfoE(playerT).HeroCooldownC.Cooldown -= 0.5f;
                    }


                    var curPlayer = eMGame.CurPlayerITC.Player;
                    var nextPlayer = eMGame.NextPlayer(curPlayer).Player;

                    if (nextPlayer == PlayerTypes.First)
                    {
                        _updateS_M.Run(gameModeTC);
                        eMGame.RpcPoolEs.ActiveMotionZoneToGen(sender);
                    }

                    eMGame.WhoseMove.Player = nextPlayer;


                    curPlayer = eMGame.CurPlayerITC.Player;

                    //ViewDataSC.RotateAll.Invoke();

                    eMGame.ZoneInfoC.IsActiveFriend = true;
                }
            }
            else
            {
                for (var playerT = PlayerTypes.First; playerT < PlayerTypes.End; playerT++)
                {
                    eMGame.PlayerInfoE(playerT).HeroCooldownC.Cooldown -= 0.5f;
                }

                for (byte idx = 0; idx < StartValues.CELLS; idx++)
                {
                    eMGame.UnitEffectStunC(idx).Stun -= 0.5f;

                    for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
                    {
                        eMGame.UnitEs(idx).CoolDownC(abilityT).Cooldown -= 0.5f;
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