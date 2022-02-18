using Game.Common;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public sealed class RpcS : MonoBehaviour
    {
        static Entities _e;
        static Systems _systems;
        int _idx_cur;

        public static List<string> NamesMethods
        {
            get
            {
                var list = new List<string>();
                list.Add(nameof(MasterRPC));
                list.Add(nameof(GeneralRpc));
                list.Add(nameof(OtherRpc));
                return list;
            }
        }

        public RpcS GiveData(in Entities ents, in Systems systems)
        {
            _e = ents;
            _systems = systems;
            return this;
        }


        [PunRPC]
        void MasterRPC(object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;

            var sender = infoFrom.Sender;

            var obj = objects[_idx_cur++];

            var whoseMove = _e.WhoseMove.Player;

            if (obj is AbilityTypes ability)
            {
                switch (ability)
                {
                    case AbilityTypes.CircularAttack:
                        _e.UnitEs((byte)objects[_idx_cur++]).Ability(ability).CircularAttack_Master(sender, _e);
                        break;

                    case AbilityTypes.BonusNear:
                        _e.UnitEs((byte)objects[_idx_cur++]).Ability(ability).BonusNear_Master(sender, _e);
                        break;

                    case AbilityTypes.FirePawn:
                        _e.UnitEs((byte)objects[_idx_cur++]).Ability(ability).FirePawn_Master(sender, _e);
                        break;

                    case AbilityTypes.PutOutFirePawn:
                        _e.UnitEs((byte)objects[_idx_cur++]).Ability(ability).PutOut_Master(sender, _e);
                        break;

                    case AbilityTypes.Seed:
                        _e.UnitEs((byte)objects[_idx_cur++]).Ability(ability).Seed_Master(sender, _e);
                        break;

                    case AbilityTypes.SetFarm:
                        _e.UnitEs((byte)objects[_idx_cur++]).Ability(ability).BuildFarm_Master(sender, _e);
                        break;

                    case AbilityTypes.SetCity:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.NeedForAbility(ability))
                            {
                                if (!_e.AdultForestC(idx_0).HaveAny)
                                {
                                    bool haveNearBorder = false;

                                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                                    {
                                        var idx_1 = _e.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                                        if (!_e.CellEs(idx_1).IsActiveParentSelf)
                                        {
                                            haveNearBorder = true;
                                            break;
                                        }
                                    }

                                    if (!haveNearBorder)
                                    {
                                        _e.RpcE.SoundToGeneral(sender, ClipTypes.Building);
                                        _e.RpcE.SoundToGeneral(sender, ClipTypes.AfterBuildTown);


                                        _e.BuildTC(idx_0).Build = BuildingTypes.City;
                                        _e.BuildPlayerTC(idx_0).Player = whoseMove;
                                        _e.BuildHpC(idx_0).Health = CellBuildingValues.MaxAmountHealth(BuildingTypes.City);
                                        _e.BuildLevelTC(idx_0).Level = LevelTypes.First;

                                        _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.NeedForAbility(ability);

                                        _e.HaveFire(idx_0) = false;

                                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                                        {
                                            _e.CellEs(idx_0).TrailHealthC(dirT).Health = 0;
                                        }

                                        _e.AdultForestC(idx_0).Resources = 0;
                                        _e.FertilizeC(idx_0).Resources = 0;
                                        _e.YoungForestC(idx_0).Resources = 0;
                                    }

                                    else
                                    {
                                        _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NearBorder, sender);
                                    }
                                }
                                else
                                {
                                    _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                }
                            }
                            else
                            {
                                _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        
                        break;

                    case AbilityTypes.DestroyBuilding:
                        _e.UnitEs((byte)objects[_idx_cur++]).Ability(ability).Destroy_Master(sender, ref _e);
                        break;

                    case AbilityTypes.FireArcher:
                        _e.UnitEs((byte)objects[_idx_cur++]).Ability(ability).FireArcher_Master((byte)objects[_idx_cur++], sender, _e);
                        break;

                    case AbilityTypes.GrowAdultForest:
                        _e.UnitEs((byte)objects[_idx_cur++]).Ability(ability).GrowElfemale_Master(sender, _e);
                        break;

                    case AbilityTypes.StunElfemale:
                        _e.UnitEs((byte)objects[_idx_cur++]).Ability(ability).StunElfemale_Master((byte)objects[_idx_cur++], sender, _e);
                        break;

                    case AbilityTypes.ChangeDirectionWind:
                        _e.UnitEs((byte)objects[_idx_cur++]).Ability(ability).ChangeDirectionWind_Master((byte)objects[_idx_cur++], sender, ref _e);
                        break;

                    case AbilityTypes.ChangeCornerArcher:
                        _e.UnitEs((byte)objects[_idx_cur++]).Ability(ability).ChangeCornerArcher_Master(sender, _e);
                        break;

                    case AbilityTypes.IceWall:
                        _e.UnitEs((byte)objects[_idx_cur++]).Ability(ability).SetIceWallSnowy_Master(_e);
                        break;

                    case AbilityTypes.ActiveAroundBonusSnowy:
                        _e.UnitEs((byte)objects[_idx_cur++]).Ability(ability).ActiveSnowyAround_Master(sender, _e);
                        break;

                    case AbilityTypes.DirectWave:
                        _e.UnitEs((byte)objects[_idx_cur++]).Ability(ability).DirectWaveSnowy_Master((byte)objects[_idx_cur++], sender, _e);
                        break;

                    case AbilityTypes.Resurrect:
                        _e.UnitEs((byte)objects[_idx_cur++]).Ability(ability).ResurrectUnit_Master(sender, (byte)objects[_idx_cur++], _e);
                        break;

                    case AbilityTypes.SetTeleport:
                        _e.UnitEs((byte)objects[_idx_cur++]).Ability(ability).SetTeleport_Master(ref _e);
                        break;

                    case AbilityTypes.Teleport:
                        //var idx_0 = (byte)objects[_idx_cur++];

                        //if (_ents.UnitStepC(idx_0).Have(CellUnitStatStepValues.NeedForAbility(ability)))
                        //{
                        //    if (_ents.BuildTC(idx_0).Is(BuildingTypes.Teleport))
                        //    {
                        //        var idx_start = _ents.StartTeleportIdxC;
                        //        var idx_end = _ents.EndTeleportIdxC.Idx;

                        //        if (_ents.EndTeleportIdxC.Idx.HaveEnd && idx_start == idx_0)
                        //        {
                        //            if (!_ents.UnitTC(idx_end).HaveUnit)
                        //            {
                        //                _ents.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(ability));

                        //                Teleport(idx_end, ents);
                        //            }
                        //        }
                        //        else if (_ents.StartTeleportIdxC.HaveStart && idx_end == idx_0)
                        //        {
                        //            if (!_ents.UnitTC(idx_start).HaveUnit)
                        //            {
                        //                _ents.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(ability));

                        //                Teleport(idx_start, _ents);
                        //            }
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    _ents.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                        //}
                        break;

                    case AbilityTypes.InvokeSkeletons:
                        //_ents.UnitE((byte)objects[_idx_cur++]).InvokeSkeletons_Master(ability, sender, _ents);
                        break;

                    default: throw new Exception();
                }
            }

            else if (obj is BuildingTypes buildT)
            {
                //_ents.BuildingE((byte)objects[_idx_cur++]).Build_Master((byte)objects[_idx_cur++], buildT, sender, _ents);
            }

            else if (obj is MarketBuyTypes marketBuy)
            {
                //_e.InventorResourcesEs.TryBuyResourcesFromMarket_Master(marketBuy, sender, _e);
            }

            else if (obj is RpcMasterTypes rpcT)
            {
                byte idx_0;

                switch (rpcT)
                {
                    case RpcMasterTypes.None:
                        throw new Exception();

                    case RpcMasterTypes.Ready:
                        {
                            var playerSend = sender.GetPlayer();

                            _e.PlayerE(playerSend).IsReadyC = !_e.PlayerE(playerSend).IsReadyC;

                            if (_e.PlayerE(PlayerTypes.First).IsReadyC
                                && _e.PlayerE(PlayerTypes.Second).IsReadyC)
                            {
                                _e.IsStartedGame = true;
                            }

                            else
                            {
                                _e.IsStartedGame = false;
                            }
                        }       
                        break;

                    case RpcMasterTypes.Done:
                        {
                            _e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);

                            if (PhotonNetwork.OfflineMode)
                            {
                                if (GameModeC.IsGameMode(GameModes.TrainingOff))
                                {
                                    for (byte idx = 0; idx < StartValues.ALL_CELLS_AMOUNT; idx++)
                                    {
                                        _e.UnitStunC(idx).Stun -= 2;
                                        //EntitiesPool.IceWalls[idx_0].Hp.Take(2);
                                    }
                                    _systems.SystemsMaster.InvokeRun(SystemDataMasterTypes.UpdateMove);
                                    _e.RpcE.ActiveMotionZoneToGen(sender);
                                }

                                else if (GameModeC.IsGameMode(GameModes.WithFriendOff))
                                {
                                    for (byte idx = 0; idx < StartValues.ALL_CELLS_AMOUNT; idx++)
                                    {
                                        _e.UnitStunC(idx).Stun -= 2;
                                        //EntitiesPool.IceWalls[idx_0].Hp.Take();
                                    }

                                    var curPlayer = _e.CurPlayerI.Player;
                                    var nextPlayer = _e.WhoseMove.NextPlayerFrom(curPlayer);

                                    if (nextPlayer == PlayerTypes.First)
                                    {
                                        _systems.SystemsMaster.InvokeRun(SystemDataMasterTypes.UpdateMove);
                                        _e.RpcE.ActiveMotionZoneToGen(sender);
                                    }

                                    _e.WhoseMove.Player = nextPlayer;


                                    curPlayer = _e.CurPlayerI.Player;

                                    //ViewDataSC.RotateAll.Invoke();

                                    _e.FriendIsActive = true;
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
                        break;

                    case RpcMasterTypes.Shift:
                        {
                            var idx_from = (byte)objects[_idx_cur++];
                            var idx_to = (byte)objects[_idx_cur++];

                            if (_e.UnitEs(idx_from).ForPlayer(whoseMove).ForShift.Contains(idx_to))
                            {
                                _e.UnitStepC(idx_from).Steps -= _e.UnitEs(idx_from).ForPlayer(whoseMove).NeedStepsForShift[idx_to];

                                _e.UnitEs(idx_to).Set(_e.UnitEs(idx_from));
                                _e.UnitConditionTC(idx_to).Condition = ConditionUnitTypes.None;

                                _e.UnitTC(idx_from).Unit = UnitTypes.None;


                                var direct = _e.CellEs(idx_from).Direct(idx_to);

                                if (!_e.UnitTC(idx_to).Is(UnitTypes.Undead))
                                {
                                    if (_e.AdultForestC(idx_from).HaveAny)
                                    {
                                        _e.CellEs(idx_from).TrailHealthC(direct).Health = CellTrail_Values.HEALTH_TRAIL;
                                    }
                                    if (_e.AdultForestC(idx_to).HaveAny)
                                    {
                                        _e.CellEs(idx_to).TrailHealthC(direct.Invert()).Health = CellTrail_Values.HEALTH_TRAIL;
                                    }

                                    if (_e.RiverEs(idx_to).RiverE.RiverTC.HaveRiverNear)
                                    {
                                        _e.UnitWaterC(idx_to).Water = CellUnitStatWater_Values.MAX_WATER;
                                    }
                                }

                                if (_e.UnitTC(idx_to).Is(UnitTypes.Hell))
                                {
                                    if (_e.AdultForestC(idx_to).HaveAny)
                                    {
                                        _e.EffectEs(idx_to).HaveFire = true;
                                    }
                                }

                                if (_e.BuildTC(idx_to).HaveBuilding && !_e.BuildTC(idx_to).Is(BuildingTypes.City))
                                {
                                    if (!_e.BuildPlayerTC(idx_to).Is(_e.UnitPlayerTC(idx_to).Player))
                                    {
                                        _e.BuildTC(idx_to).Build = BuildingTypes.None;
                                    }
                                }

                                _e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                            }
                        }
                        break;

                    case RpcMasterTypes.Attack:
                        {
                            var idx_from = (byte)objects[_idx_cur++];
                            var idx_to = (byte)objects[_idx_cur++];

                            var canAttack = _e.UnitEs(idx_from).ForPlayer(whoseMove).ForAttack(AttackTypes.Unique).Contains(idx_to)
                                || _e.UnitEs(idx_from).ForPlayer(whoseMove).ForAttack(AttackTypes.Simple).Contains(idx_to);

                            if (canAttack)
                            {
                                _e.UnitStepC(idx_from).Steps = 0;
                                _e.UnitConditionTC(idx_from).Condition = ConditionUnitTypes.None;

                                if (_e.UnitEs(idx_from).IsMelee)
                                    _e.RpcE.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                                else _e.RpcE.SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);


                                float powerDam_from = _e.UnitEs(idx_to).DamageAttackC.Damage;
                                float powerDam_to = _e.UnitEs(idx_to).DamageAttackC.Damage;


                                if (_e.UnitEs(idx_from).ForPlayer(whoseMove).ForAttack(AttackTypes.Unique).Contains(idx_to))
                                {
                                    powerDam_from *= CellUnitDamage_Values.UNIQUE_PERCENT_DAMAGE;
                                }

                                var dirAttack = _e.CellEs(idx_from).Direct(idx_to);

                                if (_e.SunSideTC.IsAcitveSun)
                                {
                                    var isSunnedUnit = true;

                                    foreach (var dir in _e.SunSideTC.RaysSun)
                                    {
                                        if (dirAttack == dir) isSunnedUnit = false;
                                    }

                                    if (isSunnedUnit)
                                    {
                                        powerDam_from *= 0.9f;
                                    }
                                }






                                float min_limit = 0;
                                float max_limit = 0;
                                float minus_to = 0;
                                float minus_from = 0;

                                var maxDamage = CellUnitStatHp_Values.MAX_HP;
                                var minDamage = 0;

                                //if (!e.UnitE(idx_to).IsMelee) powerDam_to /= 2;

                                if (powerDam_to > powerDam_from)
                                {
                                    max_limit = powerDam_to * 2;
                                    min_limit = powerDam_to / 3;

                                    if (min_limit >= powerDam_from)
                                    {
                                        minus_from = maxDamage;
                                        powerDam_to = minDamage;
                                    }
                                    else
                                    {
                                        minus_to = maxDamage * powerDam_from / max_limit;

                                        max_limit = powerDam_from * 2;
                                        minus_from = maxDamage * powerDam_to / max_limit;
                                    }
                                }
                                else
                                {
                                    max_limit = powerDam_from * 2;
                                    min_limit = powerDam_from / 3;

                                    if (min_limit >= powerDam_to)
                                    {
                                        minus_to = maxDamage;
                                        minus_from = minDamage;
                                    }
                                    else
                                    {
                                        minus_from = maxDamage * powerDam_to / max_limit;

                                        max_limit = powerDam_to * 2f;
                                        minus_to = maxDamage * powerDam_from / max_limit;
                                    }
                                }


                                if (_e.UnitEs(idx_from).IsMelee)
                                {
                                    if (_e.UnitEffectShield(idx_from).HaveProtection)
                                    {
                                        _e.UnitEffectShield(idx_from).Protection--;
                                    }
                                    else if (_e.UnitExtraTWTC(idx_from).Is(ToolWeaponTypes.Shield))
                                    {
                                        //_e.UnitExtraProtectionShieldTC(idx_from).BreakShield(1, _e.UnitExtraTWTC(idx_to));
                                    }
                                    else if (minus_from > 0)
                                    {
                                        //_e.UnitHpC(idx_from).AttackCellUnit(minus_from, CellUnitDamage_Values.HP_FOR_DEATH_AFTER_ATTACK);

                                        if (!_e.UnitHpC(idx_from).IsAlive)
                                        {
                                            //_e.UnitTC(idx_from).TrySetCooldownBeforeKilling(_e.ScoutHeroCooldownE(_e.UnitE(idx_from)).CooldownC, _e.Units(e.UnitE(idx_from)).AmountC, ScoutHeroCooldownValues.AfterKill(e.UnitTC(idx_from).Unit));
                                            //_e.UnitTC(idx_from).KillUnit(_e.UnitPlayerTC(idx_from), _e.WinnerC);


                                            _e.LastDiedUnitTC(idx_from).Unit = _e.UnitTC(idx_from).Unit;
                                            _e.LastDiedLevelTC(idx_from).Level = _e.UnitLevelTC(idx_from).Level;
                                            _e.LastDiedPlayerTC(idx_from).Player = _e.UnitPlayerTC(idx_from).Player;
                                        }
                                    }
                                }
                                else
                                {
                                    if (_e.UnitFrozenArrawC(idx_from).HaveEffect)
                                    {
                                        _e.UnitFrozenArrawC(idx_from).Shoots = 0;

                                        _e.UnitStunC(idx_to).Stun = 2;
                                    }
                                }

                                if (_e.UnitEffectShield(idx_to).HaveProtection)
                                {
                                    _e.UnitEffectShield(idx_to).Protection--;
                                }
                                else if (_e.UnitExtraTWTC(idx_to).Is(ToolWeaponTypes.Shield))
                                {
                                    //_e.UnitExtraProtectionShieldTC(idx_to).BreakShield(1, _e.UnitExtraTWTC(idx_to));
                                }
                                else if (minus_to > 0)
                                {
                                    var wasUnit = _e.UnitTC(idx_to).Unit;

                                    //_e.UnitHpC(idx_to).AttackCellUnit(minus_to, CellUnitDamage_Values.HP_FOR_DEATH_AFTER_ATTACK);

                                    if (!_e.UnitHpC(idx_to).IsAlive)
                                    {
                                        //_e.UnitTC(idx_to).TrySetCooldownBeforeKilling(_e.ScoutHeroCooldownE(_e.UnitE(idx_to)).CooldownC, _e.Units(e.UnitE(idx_to)).AmountC, ScoutHeroCooldownValues.AfterKill(e.UnitTC(idx_to).Unit));
                                        //_e.UnitTC(idx_to).KillUnit(_e.UnitPlayerTC(idx_to), _e.WinnerC);

                                        _e.LastDiedUnitTC(idx_to).Unit = _e.UnitTC(idx_to).Unit;
                                        _e.LastDiedLevelTC(idx_to).Level = _e.UnitLevelTC(idx_to).Level;
                                        _e.LastDiedPlayerTC(idx_to).Player = _e.UnitPlayerTC(idx_to).Player;
                                    }

                                    if (!_e.UnitTC(idx_to).HaveUnit)
                                    {
                                        if (wasUnit == UnitTypes.Camel)
                                        {
                                            //_e.InventorResourcesEs.Resource(ResourceTypes.Food, _e.UnitPlayerTC(idx_from).Player).ResourceC.Resources += ResourcesInInventorValues.AMOUNT_FOOD_AFTER_KILL_CAMEL;
                                        }

                                        if (_e.UnitTC(idx_from).HaveUnit)
                                        {
                                            if (_e.UnitEs(idx_from).IsMelee)
                                            {
                                                //_e.UnitEs(idx_from).Shift(idx_to, true, _e);
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        break;

                    case RpcMasterTypes.ConditionUnit:
                        //_ents.UnitE((byte)objects[_idx_cur++]).Condition_Master((ConditionUnitTypes)objects[_idx_cur++], sender, _ents);
                        break;

                    case RpcMasterTypes.SetUnit:
                        {
                            idx_0 = (byte)objects[_idx_cur++];
                            var unitT = (UnitTypes)objects[_idx_cur++];

                            if (_e.UnitEs(idx_0).ForPlayer(whoseMove).CanSetUnitHere)
                            {
                                _e.UnitTC(idx_0).Unit = unitT;
                                _e.UnitPlayerTC(idx_0).Player = whoseMove;
                                _e.UnitLevelTC(idx_0).Level = LevelTypes.First;
                                _e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                                _e.UnitIsRightArcherC(idx_0).IsRight = false;
                                _e.UnitHpC(idx_0).Health = CellUnitStatHp_Values.MAX_HP;
                                _e.UnitStepC(idx_0).Steps = CellUnitStatStep_Values.StandartForUnit(unitT);
                                _e.UnitWaterC(idx_0).Water = CellUnitStatWater_Values.MAX_WATER;
                                _e.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
                                _e.UnitExtraLevelTC(idx_0).Level = LevelTypes.None;
                                _e.UnitExtraProtectionShieldTC(idx_0).Protection = 0;
                                _e.UnitStunC(idx_0).Stun = 0;
                                _e.UnitEffectShield(idx_0).Protection = 0;
                                _e.UnitFrozenArrawC(idx_0).Shoots = 0;


                                if (unitT == UnitTypes.Pawn)
                                {
                                    _e.PlayerE(whoseMove).PeopleInCity -= 1;

                                    _e.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
                                    _e.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;
                                }
                                else
                                {
                                    _e.PlayerE(whoseMove).UnitsInfoE(unitT).HaveInInventor = false;

                                    _e.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
                                    _e.UnitMainTWLevelTC(idx_0).Level = LevelTypes.None;
                                }

                                _e.PlayerE(whoseMove).UnitsInfoE(unitT).UnitsInGame++;

                                _e.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);
                            }
                        }
                        break;

                    case RpcMasterTypes.GiveTakeToolWeapon:
                        {
                            idx_0 = (byte)objects[_idx_cur++];
                            var twT = (ToolWeaponTypes)objects[_idx_cur++];
                            var levelTW = (LevelTypes)objects[_idx_cur++];
                            if (twT == ToolWeaponTypes.Axe || twT == ToolWeaponTypes.BowCrossbow)
                            {
                                if (_e.UnitTC(idx_0).Is(UnitTypes.Pawn))
                                {
                                    if (_e.UnitStepC(idx_0).Steps >= CellUnitStatStep_Values.FOR_GIVE_TAKE_MAIN_TOOLWEAPON)
                                    {
                                        if (_e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Axe))
                                        {
                                            if (_e.UnitMainTWLevelTC(idx_0).Is(LevelTypes.First))
                                            {
                                                if (_e.PlayerE(whoseMove).LevelE(levelTW).ToolWeapons(twT).HaveAny)
                                                {
                                                    //_ents.SetFromInventor(twT, levT, whoseMove, _ents.InventorToolWeaponEs);
                                                    _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_MAIN_TOOLWEAPON;

                                                    _e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                                }
                                                else
                                                {
                                                    //if (_e.InventorResourcesEs.CanBuyTW(twT, levelTW, whoseMove, out var needs))
                                                    //{
                                                    //    _e.InventorResourcesEs.BuyTW(twT, levelTW, whoseMove);
                                                    //    //_ents.Set(twT, levT);

                                                    //    _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_MAIN_TOOLWEAPON;

                                                    //    _e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                                    //}
                                                    //else
                                                    //{
                                                    //    _e.RpcE.MistakeEconomyToGeneral(sender, needs);
                                                    //}
                                                }
                                            }
                                            else
                                            {
                                                //TakeToInventor(whoseMove, _e.InventorToolWeaponEs);
                                                _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_MAIN_TOOLWEAPON;
                                            }
                                        }
                                        else
                                        {
                                            //TakeToInventor(whoseMove, _e.InventorToolWeaponEs);
                                            _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_MAIN_TOOLWEAPON;

                                            _e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                        }
                                    }
                                    else _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                }
                            }
                            else
                            {
                                var ownUnit_0 = _e.UnitPlayerTC(idx_0).Player;

                                if (_e.UnitTC(idx_0).Is(UnitTypes.Pawn))
                                {
                                    if (_e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Axe))
                                    {
                                        if (_e.UnitStepC(idx_0).HaveAnySteps)
                                        {
                                            if (_e.UnitEs(idx_0).ExtraToolWeaponTC.HaveToolWeapon)
                                            {
                                                _e.PlayerE(ownUnit_0).LevelE(_e.UnitExtraLevelTC(idx_0).Level).ToolWeapons(_e.UnitExtraTWTC(idx_0).ToolWeapon).Amount++;
                                                _e.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;

                                                _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_EXTRA_TOOLWEAPON;

                                                _e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                            }


                                            else if (_e.PlayerE(ownUnit_0).LevelE(levelTW).ToolWeapons(twT).HaveAny)
                                            {
                                                _e.PlayerE(ownUnit_0).LevelE(levelTW).ToolWeapons(twT).Amount --;

                                                //_e.UnitExtraTWTC(idx_0).SetNew(twT, levelTW, _e.UnitEs(idx_0).ExtraTWLevelTC, _e.UnitEs(idx_0).ExtraTWShieldC);

                                                _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_EXTRA_TOOLWEAPON;

                                                _e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                            }

                                            else
                                            {
                                                //if (_e.InventorResourcesEs.CanBuyTW(twT, levelTW, ownUnit_0, out var needRes))
                                                //{
                                                //    _e.InventorResourcesEs.BuyTW(twT, levelTW, ownUnit_0);

                                                //    //_e.UnitExtraTWTC(idx_0).SetNew(twT, levelTW, _e.UnitEs(idx_0).ExtraTWLevelTC, _e.UnitEs(idx_0).ExtraTWShieldC);

                                                //    _e.UnitStepC(idx_0).Steps -= CellUnitStatStep_Values.FOR_GIVE_TAKE_EXTRA_TOOLWEAPON;

                                                //    _e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                                //}
                                                //else
                                                //{
                                                //    _e.RpcE.MistakeEconomyToGeneral(sender, needRes);
                                                //}
                                            }
                                        }
                                        else _e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                    }
                                }
                            }
                        }
                        break;

                    case RpcMasterTypes.GetHero:
                        //_e.Units((UnitTypes)objects[_idx_cur++], LevelTypes.First, whoseMove).AmountC.Add(1);
                        _e.PlayerE(whoseMove).HaveCenterHeroC = false;
                        break;

                    case RpcMasterTypes.UpgCenterUnits:
                        //var whoseMove = es.WhoseMove.Player;


                        //if (unit == UnitTypes.Scout)
                        //{
                        //    es.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Steps, unit, LevelTypes.First, whoseMove, UpgradeTypes.PickCenter).Have = true;
                        //    es.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Steps, unit, LevelTypes.Second, whoseMove, UpgradeTypes.PickCenter).Have = true;
                        //}
                        //else
                        //{
                        //    es.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Damage, unit, LevelTypes.First, whoseMove, UpgradeTypes.PickCenter).Have = true;
                        //    es.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Damage, unit, LevelTypes.Second, whoseMove, UpgradeTypes.PickCenter).Have = true;
                        //}

                        //es.AvailableCenterUpgradeEs.HaveUpgrade(whoseMove).Have = false;
                        //es.AvailableCenterUpgradeEs.HaveUnitUpgrade(unit, whoseMove).Have = false;

                        //es.RpcE.SoundToGeneral(sender, ClipTypes.PickUpgrade);
                        break;

                    case RpcMasterTypes.UpgCenterBuild:
                        //buildT = (BuildingTypes)objects[_idx_cur++];

                        //_e.AvailableCenterUpgradeEs.HaveUpgrade(whoseMove).Have = false;
                        //_e.HaveBuildingUpgrade(buildT, whoseMove, UpgradeTypes.PickCenter).Have = true;
                        //_e.AvailableCenterUpgradeEs.HaveBuildUpgrade(buildT, whoseMove).Have = false;

                        //_e.RpcE.SoundToGeneral(sender, ClipTypes.PickUpgrade);
                        break;

                    case RpcMasterTypes.UpgWater:
                        //var whoseMove = e.WhoseMove.Player;

                        //for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
                        //{
                        //    for (var level = LevelTypes.None + 1; level < LevelTypes.End; level++)
                        //    {
                        //        e.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Water, unit, level, whoseMove, UpgradeTypes.PickCenter).Have = true;
                        //    }
                        //}
                        //e.AvailableCenterUpgradeEs.HaveUpgrade(whoseMove).Have = false;
                        //e.AvailableCenterUpgradeEs.HaveWaterUpgrade(whoseMove).Have = false;

                        //e.RpcE.SoundToGeneral(sender, ClipTypes.PickUpgrade);
                        break;

                    default:
                        throw new Exception();
                }
            }

            else throw new Exception();

            SyncAllMaster();
        }

        [PunRPC]
        void GeneralRpc(object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;

            var obj = objects[_idx_cur++];

            if (obj is MistakeTypes mistakeT)
            {
                _e.MistakeE.MistakeTC.Mistake = mistakeT;
                _e.Sound(ClipTypes.Mistake).Action.Invoke();

                if (mistakeT == MistakeTypes.Economy)
                {
                    _e.MistakeEconomy(ResourceTypes.Food).Resources = 0;
                    _e.MistakeEconomy(ResourceTypes.Wood).Resources = 0;
                    _e.MistakeEconomy(ResourceTypes.Ore).Resources = 0;
                    _e.MistakeEconomy(ResourceTypes.Iron).Resources = 0;
                    _e.MistakeEconomy(ResourceTypes.Gold).Resources = 0;

                    var needRes = (float[])objects[_idx_cur++];

                    _e.MistakeEconomy(ResourceTypes.Food).Resources = needRes[0];
                    _e.MistakeEconomy(ResourceTypes.Wood).Resources = needRes[1];
                    _e.MistakeEconomy(ResourceTypes.Ore).Resources = needRes[2];
                    _e.MistakeEconomy(ResourceTypes.Iron).Resources = needRes[3];
                    _e.MistakeEconomy(ResourceTypes.Gold).Resources = needRes[4];
                }
            }
            else if (obj is RpcGeneralTypes rpcT)
            {
                switch (rpcT)
                {
                    case RpcGeneralTypes.None:
                        throw new Exception();

                    case RpcGeneralTypes.SoundEff:
                        _e.Sound((ClipTypes)objects[_idx_cur++]).Invoke();
                        break;

                    case RpcGeneralTypes.SoundUniqueAbility:
                        _e.Sound((AbilityTypes)objects[_idx_cur++]).Invoke();
                        break;

                    case RpcGeneralTypes.SoundRpcMaster:
                        //Sound((UniqueAbilityTypes)objects[_idx_cur++]).Invoke();
                        break;

                    case RpcGeneralTypes.ActiveMotion:
                        _e.MotionIsActive = true;
                        break;

                    default:
                        throw new Exception();
                }
            }
        }

        [PunRPC]
        void OtherRpc(object[] objects, PhotonMessageInfo infoFrom) => _e.RpcE.OtherRpc(objects, infoFrom);


        #region SyncData

        public static void SyncAllMaster()
        {
            var objs = new List<object>();


             for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                objs.Add(_e.UnitTC(idx_0).Unit);
                //objs.Add(_ents.CellEs(idx_0).UnitEs.MainE.LevelTC.Level);
                objs.Add(_e.UnitPlayerTC(idx_0).Player);

                objs.Add(_e.UnitHpC(idx_0).Health);
                objs.Add(_e.UnitStepC(idx_0).Steps);
                objs.Add(_e.UnitWaterC(idx_0).Water);

                objs.Add(_e.UnitConditionTC(idx_0).Condition);
                //foreach (var item in CellUnitEffectsEs.Keys) objs.Add(CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Have);


                objs.Add(_e.CellEs(idx_0).UnitEs.ExtraToolWeaponTC.ToolWeapon);
                objs.Add(_e.CellEs(idx_0).UnitEs.ExtraTWLevelTC.Level);
                objs.Add(_e.CellEs(idx_0).UnitEs.ExtraTWShieldC.Protection);

                objs.Add(_e.UnitStunC(idx_0).Stun);

                objs.Add(_e.UnitIsRightArcherC(idx_0).IsRight);

                //foreach (var item in _ents.CellEs(idx_0).UnitEs.CooldownKeys) objs.Add(_ents.CellEs(idx_0).UnitEs.Ability(item).CooldownC);





                //objs.Add(_ents.BuildingE(idx_0).Building);
                //objs.Add(_ents.BuildingE(idx_0).Owner);



                //foreach (var env in _ents.EnvironmentEs.Keys)
                //{
                //    objs.Add(_ents.EnvironmentEs.Environment(env, idx_0));
                //}




                objs.Add(_e.CellEs(idx_0).RiverEs.RiverE.RiverTC.River);
                //foreach (var item_0 in _e.CellEs(idx_0).RiverEs.Keys)
                //    objs.Add(_e.CellEs(idx_0).RiverEs.HaveRive(item_0).HaveRiver.Have);


                //foreach (var item_0 in _e.CellEs(idx_0).TrailEs.Keys)
                //    objs.Add(_e.CellEs(idx_0).TrailEs.Trail(item_0));

                //objs.Add(_ents.CellEs(idx_0).EffectEs.FireE.HaveFireC);
            }

            //objs.Add(_ents.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.First).CooldownC.Amount);
            //objs.Add(_ents.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.Second).CooldownC.Amount);
            //objs.Add(_ents.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.First).CooldownC.Amount);
            //objs.Add(_ents.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.Second).CooldownC.Amount);



            //foreach (var key in _ents.UnitStatUpgradesEs.Keys) objs.Add(_ents.UnitStatUpgradesEs.Upgrade(key).HaveUpgrade.Have);
            //foreach (var key in BuildingUpgradesEnt.Keys) objs.Add(BuildingUpgradesEnt.Upgrade<HaveUpgradeC>(key).Have);


            //foreach (var key in _ents.InventorResourcesEs.Keys) objs.Add(_ents.InventorResourcesEs.Resource(key).Resources);
            //foreach (var key in _ents.InventorUnitsEs.Keys) objs.Add(_ents.Units(key).Units.Amount);
            //foreach (var key in _ents.InventorToolWeaponEs.Keys) objs.Add(_ents.InventorToolWeaponEs.ToolWeapons(key).ToolWeaponsC.Amount);


            //foreach (var key in _ents.WhereUnitsEs.Keys) objs.Add(_ents.WhereUnitsEs.WhereUnit(key).HaveUnit.Have);
            //foreach (var key in _ents.WhereBuildingEs.Keys) objs.Add(_ents.WhereBuildingEs.HaveBuild(key).HaveBuilding.Have);
            //foreach (var key in _ents.WhereEnviromentEs.Keys) objs.Add(_ents.WhereEnviromentEs.Info(key).HaveEnv.Have);


            //foreach (var item in PickUpgC.HaveUpgrades) objs.Add(item.Value);
            //foreach (var item in UnitAvailPickUpgC.Available_0) objs.Add(item.Value);
            //foreach (var item in BuildAvailPickUpgC.Available) objs.Add(item.Value);
            //foreach (var item in WaterAvailPickUpgC.Available) objs.Add(item.Value);


            #region Other

            //objs.Add(_e.WhoseMove.Player);
            //objs.Add(_e.WinnerC.Player);
            //objs.Add(_e.IsStartedGameC.Is);
            //objs.Add(_ents.PeopleInCityE(PlayerTypes.Second).IsReadyC.IsReady);

            //objs.Add(_ents.MotionsC.Amount);

            //objs.Add(_e.CenterCloudIdxC);
            //foreach (var item in WindC.Directs) objs.Add(item.Value);
            //objs.Add(WindC.CurDirWind);

            #endregion


            var objects = new object[objs.Count];
            for (int i = 0; i < objects.Length; i++) objects[i] = objs[i];


            _e.RpcE.RPC(nameof(SyncAllOther), RpcTarget.Others, objects);

            _e.RpcE.RPC(nameof(UpdateDataAndView), RpcTarget.All, new object[] { });
        }

        [PunRPC]
        void SyncAllOther(object[] objects)
        {
            _idx_cur = 0;


            for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                //_ents.CellEs(idx_0).UnitEs.Main.UnitTC.Unit = (UnitTypes)objects[_idx_cur++];
                //_ents.CellEs(idx_0).UnitEs.Main.LevelC.Level = (LevelTypes)objects[_idx_cur++];
                //_ents.CellEs(idx_0).UnitEs.Main.OwnerC.Player = (PlayerTypes)objects[_idx_cur++];
                //_ents.CellEs(idx_0).UnitEs.Main.ConditionTC.Condition = (ConditionUnitTypes)objects[_idx_cur++];
                //_ents.CellEs(idx_0).UnitEs.Main.IsCorned.Is = (bool)objects[_idx_cur++];

                //_ents.CellEs(idx_0).UnitEs.StatEs.Hp.Health.Amount = (int)objects[_idx_cur++];
                //_ents.CellEs(idx_0).UnitEs.StatEs.Step.Steps.Amount = (int)objects[_idx_cur++];
                //_ents.CellEs(idx_0).UnitEs.StatEs.Water.Water.Amount = (int)objects[_idx_cur++];

               
                //foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Have = (bool)objects[_idx_cur++];


                //_ents.CellEs(idx_0).UnitEs.ToolWeapon.ToolWeaponTC.ToolWeapon = (ToolWeaponTypes)objects[_idx_cur++];
                //_ents.CellEs(idx_0).UnitEs.ToolWeapon.LevelTC.Level = (LevelTypes)objects[_idx_cur++];
                //_ents.CellEs(idx_0).UnitEs.ToolWeapon.Protection.Amount = (int)objects[_idx_cur++];


                //_ents.UnitE(idx_0).SyncRpc((int)objects[_idx_cur++]);

                

                //foreach (var item in _ents.CellEs(idx_0).UnitEs.CooldownKeys) _ents.CellEs(idx_0).UnitEs.Ability(item).Cooldown = (int)objects[_idx_cur++];



                //_ents.BuildingE(idx_0).Sync((int)objects[_idx_cur++], (BuildingTypes)objects[_idx_cur++], (PlayerTypes)objects[_idx_cur++]);

                //foreach (var item_0 in _ents.EnvironmentEs.Keys)
                //{
                //    _ents.EnvironmentEs.Environment(item_0, idx_0).Resources.Amount = (int)objects[_idx_cur++];
                //}

                //_e.CellEs(idx_0).RiverEs.RiverE.RiverTC.River = (RiverTypes)objects[_idx_cur++];
                //foreach (var dir in _e.CellEs(idx_0).RiverEs.Keys)
                //    _e.CellEs(idx_0).RiverEs.HaveRive(dir).HaveRiver.Have = (bool)objects[_idx_cur++];



                //foreach (var item_0 in _ents.CellEs(idx_0).TrailEs.Keys)
                //    _ents.TrailEs(idx_0).Trail(item_0).Sync((int)objects[_idx_cur++]);



                //_ents.CellEs(idx_0).EffectEs.FireE.SyncRpc((bool)objects[_idx_cur++]);
            }


            //_ents.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.First).SyncRpc((int)objects[_idx_cur++]);
            //_ents.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.Second).SyncRpc((int)objects[_idx_cur++]);
            //_ents.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.First).SyncRpc((int)objects[_idx_cur++]);
            //_ents.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.Second).SyncRpc((int)objects[_idx_cur++]);



            //foreach (var key in _ents.UnitStatUpgradesEs.Keys) _ents.UnitStatUpgradesEs.Upgrade(key).HaveUpgrade.Have = (bool)objects[_idx_cur++];
            //foreach (var key in BuildingUpgradesEnt.Keys) BuildingUpgradesEnt.Upgrade<HaveUpgradeC>(key).Have = (bool)objects[_idx_cur++];


            //foreach (var key in _ents.InventorResourcesEs.Keys) _ents.InventorResourcesEs.Resource(key).Set((int)objects[_idx_cur++]);
            //foreach (var key in _ents.InventorUnitsEs.Keys) _ents.Units(key).Sync((int)objects[_idx_cur++]);
            //foreach (var key in _ents.InventorToolWeaponEs.Keys) _ents.InventorToolWeaponEs.ToolWeapons(key).ToolWeaponsC.Amount = (int)objects[_idx_cur++];


            //foreach (var key in _ents.WhereUnitsEs.Keys) _ents.WhereUnitsEs.WhereUnit(key).HaveUnit.Have = (bool)objects[_idx_cur++];
            //foreach (var key in _ents.WhereBuildingEs.Keys) _ents.WhereBuildingEs.HaveBuild(key).HaveBuilding.Have = (bool)objects[_idx_cur++];
            //foreach (var key in _ents.WhereEnviromentEs.Keys) _ents.WhereEnviromentEs.Info(key).HaveEnv.Have = (bool)objects[_idx_cur++];


            //foreach (var item in PickUpgC.HaveUpgrades) PickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in UnitAvailPickUpgC.Available_0) UnitAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in BuildAvailPickUpgC.Available) BuildAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in WaterAvailPickUpgC.Available) WaterAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);


            #region Other

            //_e.WhoseMove.Player = (PlayerTypes)objects[_idx_cur++];
            //_e.WinnerC.Player = (PlayerTypes)objects[_idx_cur++];
            //_e.IsStartedGameC = (bool)objects[_idx_cur++];
            //_ents.ReadyE(_ents.WhoseMovePlayerTC.CurPlayerI).IsReadyC.IsReady = (bool)objects[_idx_cur++];


            //_ents.Motion.AmountMotionsC.Amount = (int)objects[_idx_cur++];

            //_e.CenterCloudIdxC.Set((byte)objects[_idx_cur++]);
            //foreach (var item in WindC.Directs) WindC.Sync(item.Key, (byte)objects[_idx_cur++]);
            //WindC.Sync((DirectTypes)objects[_idx_cur++]);

            #endregion
        }


        [PunRPC]
        void UpdateDataAndView(object[] objects)
        {
            _systems.Run(SystemDataTypes.RunAfterSyncRPC);
        }

        #endregion


        #region Serialize

        //public void Init()
        //{
        //    PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);
        //}

        //public static object DeserializeVector2Int(byte[] data)
        //{
        //    Vector2Int result = new Vector2Int();

        //    result.x = BitConverter.ToInt32(data, 0);
        //    result.y = BitConverter.ToInt32(data, 4);

        //    return result;

        //}
        //public static byte[] SerializeVector2Int(object obj)
        //{
        //    Vector2Int vector = (Vector2Int)obj;
        //    byte[] result = new byte[8];

        //    BitConverter.GetBytes(vector.x).CopyTo(result, 0);
        //    BitConverter.GetBytes(vector.y).CopyTo(result, 4);

        //    return result;
        //}

        #endregion
    }
}