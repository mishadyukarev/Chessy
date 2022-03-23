using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;
using Chessy.Game.EventsUI;
using Chessy.Game.System.Model;
using Chessy.Game.System.Model.Master;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Effect;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class Rpc : MonoBehaviour, IToggleScene
    {
        static EntitiesModelGame _e;
        static SystemsModelGame _sMGame;
        static EntitiesModelCommon _eMC;

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

        public Rpc GiveData(in SystemsModelGame sMM, in EntitiesModelGame ents, in EntitiesModelCommon eMC)
        {
            _sMGame = sMM;
            _e = ents;
            _eMC = eMC;

            return this;
        }

        public void ToggleScene(in SceneTypes newSceneT)
        {
            if (newSceneT != SceneTypes.Game) return;
            SyncAllMaster();
        }


        [PunRPC]
        void MasterRPC(object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;

            var sender = infoFrom.Sender;
            var obj = objects[_idx_cur++];
            var whoseMove = _e.WhoseMove.Player;

            if (obj is byte idx)
            {
                var obj_1 = objects[_idx_cur++];

                if (obj_1 is ToolWeaponTypes twT)
                {
                    _sMGame.GiveTakeToolWeaponS_M.GiveTake(twT, (LevelTypes)objects[_idx_cur++], idx, sender, _e);
                }
            }

            else if (obj is AbilityTypes abilityT)
            {
                switch (abilityT)
                {
                    case AbilityTypes.CircularAttack:
                        _sMGame.CurcularAttackKingS_M.Attack((byte)objects[_idx_cur++], abilityT, sender, _sMGame, _e);
                        break;

                    case AbilityTypes.FirePawn:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= StepValues.FIRE_PAWN)
                            {
                                if (_e.AdultForestC(idx_0).HaveAnyResources)
                                {
                                    _e.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                                    _e.HaveFire(idx_0) = true;
                                    _e.UnitStepC(idx_0).Steps -= StepValues.FIRE_PAWN;
                                }

                                else
                                {
                                    throw new Exception();
                                }
                            }

                            else
                            {
                                _e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.PutOutFirePawn:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= StepValues.PUT_OUT_FIRE_PAWN)
                            {
                                _e.HaveFire(idx_0) = false;

                                _e.UnitStepC(idx_0).Steps -= StepValues.PUT_OUT_FIRE_PAWN;
                            }

                            else
                            {
                                _e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.Seed:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= StepValues.SEED_PAWN)
                            {
                                if (_e.BuildingTC(idx_0).HaveBuilding && !_e.BuildingTC(idx_0).Is(BuildingTypes.Camp))
                                {
                                    _e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceSeed, sender);
                                }

                                else
                                {
                                    if (!_e.AdultForestC(idx_0).HaveAnyResources)
                                    {
                                        if (!_e.YoungForestC(idx_0).HaveAnyResources)
                                        {
                                            _e.RpcPoolEs.SoundToGeneral(sender, abilityT);

                                            _e.YoungForestC(idx_0).Resources = EnvironmentValues.MAX_RESOURCES;

                                            _e.UnitStepC(idx_0).Steps -= StepValues.SEED_PAWN;
                                        }

                                        else
                                        {
                                            _e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceSeed, sender);
                                        }
                                    }

                                    else
                                    {
                                        _e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
                                    }
                                }
                            }

                            else
                            {
                                _e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.SetFarm:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= StepValues.SET_FARM)
                            {
                                if (!_e.BuildingTC(idx_0).HaveBuilding || _e.BuildingTC(idx_0).Is(BuildingTypes.Camp))
                                {
                                    if (!_e.AdultForestC(idx_0).HaveAnyResources)
                                    {
                                        var needRes = new Dictionary<ResourceTypes, float>();
                                        var canBuild = true;

                                        for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                        {
                                            if (resT == ResourceTypes.Wood)
                                            {
                                                needRes.Add(resT, EconomyValues.WOOD_FOR_BUILDING_FARM);
                                            }
                                            else
                                            {
                                                needRes.Add(resT, 0);
                                            }

                                            if (needRes[resT] > _e.PlayerInfoE(whoseMove).ResourcesC(resT).Resources) canBuild = false;
                                        }

                                        if (canBuild)
                                        {
                                            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                            {
                                                _e.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= needRes[resT];
                                            }

                                            _e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Building);

                                            _e.YoungForestC(idx_0).Resources = 0;

                                            new BuildS(BuildingTypes.Farm, LevelTypes.First, whoseMove, BuildingValues.MAX_HP, idx_0, _e);

                                            _e.UnitStepC(idx_0).Steps -= StepValues.SET_FARM;
                                        }

                                        else
                                        {
                                            _e.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
                                        }
                                    }

                                    else
                                    {
                                        _e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
                                    }
                                }

                                else
                                {
                                    _e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceFarm, sender);
                                }
                            }

                            else
                            {
                                _e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.DestroyBuilding:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).HaveAnySteps)
                            {
                                _e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                                new DestroyBuildingS(1f, _e.UnitPlayerTC(idx_0).Player, idx_0, _e);

                                _e.UnitStepC(idx_0).Steps -= StepValues.DESTROY_BUILDING;
                            }

                            else
                            {
                                _e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.FireArcher:
                        {
                            var idx_from = (byte)objects[_idx_cur++];
                            var idx_to = (byte)objects[_idx_cur++];

                            if (_e.UnitEs(idx_from).ForArson.Contains(idx_to))
                            {
                                if (_e.UnitStepC(idx_from).Steps >= StepValues.ARCHER_FIRE)
                                {

                                    _e.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);

                                    _e.UnitStepC(idx_from).Steps -= StepValues.ARCHER_FIRE;
                                    _e.HaveFire(idx_to) = true;

                                }

                                else
                                {
                                    _e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                }
                            }
                        }
                        break;

                    case AbilityTypes.GrowAdultForest:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (!_e.UnitEs(idx_0).CoolDownC(abilityT).HaveCooldown)
                            {
                                if (_e.UnitStepC(idx_0).Steps >= StepValues.GROW_ADULT_FOREST)
                                {
                                    if (_e.YoungForestC(idx_0).HaveAnyResources)
                                    {
                                        _e.YoungForestC(idx_0).Resources = 0;

                                        _e.AdultForestC(idx_0).Resources = EnvironmentValues.MAX_RESOURCES;

                                        _e.UnitStepC(idx_0).Steps -= StepValues.GROW_ADULT_FOREST;

                                        _e.UnitEs(idx_0).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.AFTER_GROW_ADULT_FOREST;


                                        foreach (var idx_1 in _e.CellEs(idx_0).IdxsAround)
                                        {
                                            if (_e.YoungForestC(idx_1).HaveAnyResources)
                                            {
                                                _e.AdultForestC(idx_1).Resources = EnvironmentValues.MAX_RESOURCES;
                                            }
                                        }



                                        _e.RpcPoolEs.SoundToGeneral(sender, abilityT);


                                        foreach (var idxC_1 in _e.CellEs(idx_0).AroundCellIdxsC)
                                        {
                                            var idx_1 = idxC_1.Idx;

                                            if (_e.UnitTC(idx_1).HaveUnit)
                                            {
                                                if (_e.UnitPlayerTC(idx_1).Is(_e.UnitPlayerTC(idx_0).Player))
                                                {
                                                    //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have)
                                                    //{
                                                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have = true;
                                                    //}
                                                }
                                            }
                                        }

                                    }

                                    else _e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedOtherPlaceGrowAdultForest, sender);
                                }

                                else _e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }

                            else
                            {
                                _e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
                            }
                        }
                        break;

                    case AbilityTypes.StunElfemale:
                        {
                            var idx_from = (byte)objects[_idx_cur++];
                            var idx_to = (byte)objects[_idx_cur++];

                            if (!_e.UnitEs(idx_from).CoolDownC(abilityT).HaveCooldown)
                            {
                                if (_e.AdultForestC(idx_to).HaveAnyResources)
                                {
                                    if (_e.UnitStepC(idx_from).Steps >= StepValues.STUN_ELFEMALE)
                                    {
                                        if (!_e.UnitPlayerTC(idx_from).Is(_e.UnitPlayerTC(idx_to).Player))
                                        {
                                            _e.UnitEffectStunC(idx_to).Stun = StunValues.ELFEMALE;
                                            _e.UnitEs(idx_from).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);

                                            _e.UnitStepC(idx_from).Steps -= StepValues.STUN_ELFEMALE;

                                            _e.RpcPoolEs.SoundToGeneral(RpcTarget.All, abilityT);


                                            foreach (var idx_1 in _e.CellEs(idx_to).IdxsAround)
                                            {
                                                if (_e.AdultForestC(idx_1).HaveAnyResources)
                                                {
                                                    if (_e.UnitTC(idx_1).HaveUnit && _e.UnitPlayerTC(idx_1).Is(_e.UnitPlayerTC(idx_to).Player))
                                                    {
                                                        _e.UnitEffectStunC(idx_1).Stun = StunValues.ELFEMALE;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    else _e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                }
                            }

                            else _e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
                        }
                        break;

                    case AbilityTypes.ChangeCornerArcher:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= StepValues.Need(abilityT))
                            {
                                _e.UnitIsRightArcherC(idx_0).ToggleSide();

                                _e.UnitStepC(idx_0).Steps -= StepValues.CHANGE_CORNER_ARCHER;

                                _e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickArcher);
                            }

                            else
                            {
                                _e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    //Snowy
                    case AbilityTypes.ChangeDirectionWind:
                        new ChangeDirectionWindMS((byte)objects[_idx_cur++], (byte)objects[_idx_cur++], abilityT, sender, _e);
                        break;

                    case AbilityTypes.IncreaseWindSnowy:
                        _sMGame.IncreaseWindSnowyS_M.Execute(true, (byte)objects[_idx_cur++], abilityT, sender, _e);
                        break;

                    case AbilityTypes.DecreaseWindSnowy:
                        _sMGame.IncreaseWindSnowyS_M.Execute(false, (byte)objects[_idx_cur++], abilityT, sender, _e);
                        break;


                    case AbilityTypes.Resurrect:
                        {
                            var idx_from = (byte)objects[_idx_cur++];
                            var idx_to = (byte)objects[_idx_cur++];

                            if (!_e.UnitTC(idx_to).HaveUnit)
                            {
                                if (!_e.UnitEs(idx_from).CoolDownC(abilityT).HaveCooldown)
                                {
                                    if (_e.UnitStepC(idx_from).Steps >= StepValues.RESURRECT)
                                    {
                                        _e.UnitEs(idx_from).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);
                                        _e.UnitStepC(idx_from).Steps -= StepValues.RESURRECT;

                                        if (_e.LastDiedE(idx_to).UnitTC.HaveUnit)
                                        {
                                            //e.UnitE(idx_to).SetNew((e.LastDiedUnitTC(idx_to).Unit, e.LastDiedLevelTC(idx_to).Level, e.LastDiedPlayerTC(idx_to).Player, ConditionUnitTypes.None, false), e);
                                            _e.LastDiedUnitTC(idx_to).Unit = UnitTypes.None;
                                        }
                                    }

                                    else
                                    {
                                        _e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                    }
                                }
                            }
                        }
                        break;

                    case AbilityTypes.SetTeleport:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (!_e.BuildingTC(idx_0).HaveBuilding)
                            {
                                if (!_e.AdultForestC(idx_0).HaveAnyResources)
                                {
                                    _e.YoungForestC(idx_0).Resources = 0;
                                    _e.FertilizeC(idx_0).Resources = 0;

                                    if (_e.UnitStepC(idx_0).Steps >= StepValues.SET_TELEPORT)
                                    {
                                        _e.UnitStepC(idx_0).Steps -= StepValues.SET_TELEPORT;

                                        if (_e.WhereTeleportC.Start > 0)
                                        {
                                            if (_e.WhereTeleportC.End > 0)
                                            {
                                                _e.BuildingTC(_e.WhereTeleportC.Start).Building = BuildingTypes.None;

                                                _e.WhereTeleportC.Start = _e.WhereTeleportC.End;

                                                _e.WhereTeleportC.End = idx_0;
                                                _e.UnitEs(idx_0).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);
                                            }
                                            else
                                            {
                                                _e.WhereTeleportC.End = idx_0;
                                                _e.UnitEs(idx_0).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);
                                            }
                                        }
                                        else
                                        {
                                            _e.WhereTeleportC.Start = idx_0;
                                        }

                                        _e.BuildingTC(idx_0).Building = BuildingTypes.Teleport;
                                        _e.BuildingLevelTC(idx_0).Level = LevelTypes.First;
                                        _e.BuildingPlayerTC(idx_0).Player = whoseMove;
                                        _e.BuildHpC(idx_0).Health = BuildingValues.MAX_HP;
                                    }
                                }
                            }
                        }
                        break;

                    case AbilityTypes.Teleport:
                        {
                            var idx_0 = (byte)objects[_idx_cur++];

                            if (_e.UnitStepC(idx_0).Steps >= StepValues.TELEPORT)
                            {
                                if (_e.BuildingTC(idx_0).Is(BuildingTypes.Teleport))
                                {
                                    var idx_start = _e.WhereTeleportC.Start;
                                    var idx_end = _e.WhereTeleportC.End;

                                    if (_e.WhereTeleportC.End > 0 && idx_start == idx_0)
                                    {
                                        if (!_e.UnitTC(idx_end).HaveUnit)
                                        {
                                            _e.UnitStepC(idx_0).Steps -= StepValues.TELEPORT;

                                            //Teleport(idx_end, ents);
                                        }
                                    }
                                    else if (_e.WhereTeleportC.Start > 0 && idx_end == idx_0)
                                    {
                                        if (!_e.UnitTC(idx_start).HaveUnit)
                                        {
                                            _e.UnitStepC(idx_0).Steps -= StepValues.TELEPORT;

                                            //Teleport(idx_start, _e);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                _e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.InvokeSkeletons:
                        {
                            //    var idx_0 = CellEs.Idx;

                            //    if (ents.UnitStepC(idx_0).Have(CellUnitStatStepValues.NeedForAbility(ability)))
                            //    {
                            //        ents.UnitStepC(idx_0).Take(CellUnitStatStepValues.NeedForAbility(ability));

                            //        foreach (var idx_1 in ents.CellSpaceWorker.GetIdxsAround(idx_0))
                            //        {
                            //            if (!ents.UnitTC(idx_0).HaveUnit && !ents.MountainC(idx_1).HaveAny)
                            //            {
                            //                ents.UnitE(idx_1).SetNew((UnitTypes.Skeleton, LevelTypes.First, PlayerTC.Player, ConditionUnitTypes.None, false), ents);
                            //            }

                            //        }
                            //    }
                            //    else
                            //    {
                            //        ents.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            //    }
                            //_e.UnitE((byte)objects[_idx_cur++]).InvokeSkeletons_Master(ability, sender, _e);
                        }
                        break;

                    default: throw new Exception();
                }
            }

            else if (obj is BuildingTypes buildT)
            {
                _sMGame.BuyBuildingS_M.Buy(buildT, sender, _e);
            }

            else if (obj is MarketBuyTypes marketBuy) _sMGame.BuyS_M.Buy(marketBuy, sender, _e);

            else if (obj is RpcMasterTypes rpcT)
            {
                switch (rpcT)
                {
                    case RpcMasterTypes.None:
                        throw new Exception();

                    case RpcMasterTypes.Ready:
                        {
                            var playerSend = sender.GetPlayer();

                            _e.PlayerInfoE(playerSend).IsReadyC = !_e.PlayerInfoE(playerSend).IsReadyC;

                            if (_e.PlayerInfoE(PlayerTypes.First).IsReadyC
                                && _e.PlayerInfoE(PlayerTypes.Second).IsReadyC)
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
                        new DonerS_M(_eMC.GameModeTC, sender, _sMGame, _e);
                        break;

                    case RpcMasterTypes.Shift:
                        new ShiftUnitS_M((byte)objects[_idx_cur++], (byte)objects[_idx_cur++], sender, _e);
                        break;

                    case RpcMasterTypes.Attack:
                        _sMGame.AttackUnit_M.Attack((byte)objects[_idx_cur++], (byte)objects[_idx_cur++], _sMGame, _e);
                        break;

                    case RpcMasterTypes.ConditionUnit:
                        new SetConditionUnitS_M((byte)objects[_idx_cur++], (ConditionUnitTypes)objects[_idx_cur++], sender, _e);
                        break;

                    case RpcMasterTypes.SetUnit:
                        new SetUnitS_M((byte)objects[_idx_cur++], (UnitTypes)objects[_idx_cur++], sender, _e);
                        break;

                    case RpcMasterTypes.GetHero:
                        new GetHeroS_M((UnitTypes)objects[_idx_cur++], sender, _e);
                        break;

                    case RpcMasterTypes.Melt:
                        _sMGame.MeltS_M.Melt(sender, _e);
                        break;

                    default:
                        throw new Exception();
                }
            }

            else throw new Exception();



            new GetDataCells(_e);
            SyncAllMaster();
        }

        [PunRPC]
        void GeneralRpc(object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;

            var obj = objects[_idx_cur++];

            if (obj is MistakeTypes mistakeT)
            {
                _e.MistakeC.Set(mistakeT, 0);

                _e.Sound(ClipTypes.WritePensil).Action.Invoke();

                //if (mistakeT == MistakeTypes.NeedMoreSteps || mistakeT == MistakeTypes.MinSpeedWind 
                //    || mistakeT == MistakeTypes.MaxSpeedWind || mistakeT == MistakeTypes.NeedBuildingHouses
                //    || mistakeT == MistakeTypes.NeedMoreHp || mistakeT == MistakeTypes.NeedMorePeopleInCity)
                //{

                //}
                //else
                //{
                //    _e.Sound(ClipTypes.Mistake).Action.Invoke();
                //}



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
                        _e.ZoneInfoC.IsActiveMotion = true;
                        break;

                    default:
                        throw new Exception();
                }
            }
        }

        [PunRPC]
        void OtherRpc(object[] objects, PhotonMessageInfo infoFrom) => _e.RpcPoolEs.OtherRpc(objects, infoFrom);


        #region SyncData

        public static void SyncAllMaster()
        {
            var objs = new List<object>();


            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                objs.Add(_e.UnitTC(idx_0).Unit);
                //objs.Add(_e.CellEs(idx_0).UnitEs.MainE.LevelTC.Level);
                objs.Add(_e.UnitPlayerTC(idx_0).Player);

                objs.Add(_e.UnitHpC(idx_0).Health);
                objs.Add(_e.UnitStepC(idx_0).Steps);
                objs.Add(_e.UnitWaterC(idx_0).Water);

                objs.Add(_e.UnitConditionTC(idx_0).Condition);
                //foreach (var item in CellUnitEffectsEs.Keys) objs.Add(CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Have);


                //objs.Add(_e.CellEs(idx_0).UnitEs.ExtraToolWeaponTC.ToolWeapon);
                //objs.Add(_e.CellEs(idx_0).UnitEs.ExtraTWLevelTC.Level);
                //objs.Add(_e.CellEs(idx_0).UnitEs.ExtraTWShieldC.Protection);

                objs.Add(_e.UnitEffectStunC(idx_0).Stun);

                objs.Add(_e.UnitIsRightArcherC(idx_0).IsRight);

                //foreach (var item in _e.CellEs(idx_0).UnitEs.CooldownKeys) objs.Add(_e.CellEs(idx_0).UnitEs.Ability(item).CooldownC);





                //objs.Add(_e.BuildingE(idx_0).Building);
                //objs.Add(_e.BuildingE(idx_0).Owner);



                //foreach (var env in _e.EnvironmentEs.Keys)
                //{
                //    objs.Add(_e.EnvironmentEs.Environment(env, idx_0));
                //}




                objs.Add(_e.CellEs(idx_0).RiverEs.RiverTC.River);
                //foreach (var item_0 in _e.CellEs(idx_0).RiverEs.Keys)
                //    objs.Add(_e.CellEs(idx_0).RiverEs.HaveRive(item_0).HaveRiver.Have);


                //foreach (var item_0 in _e.CellEs(idx_0).TrailEs.Keys)
                //    objs.Add(_e.CellEs(idx_0).TrailEs.Trail(item_0));

                //objs.Add(_e.CellEs(idx_0).EffectEs.FireE.HaveFireC);
            }

            //objs.Add(_e.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.First).CooldownC.Amount);
            //objs.Add(_e.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.Second).CooldownC.Amount);
            //objs.Add(_e.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.First).CooldownC.Amount);
            //objs.Add(_e.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.Second).CooldownC.Amount);



            //foreach (var key in _e.UnitStatUpgradesEs.Keys) objs.Add(_e.UnitStatUpgradesEs.Upgrade(key).HaveUpgrade.Have);
            //foreach (var key in BuildingUpgradesEnt.Keys) objs.Add(BuildingUpgradesEnt.Upgrade<HaveUpgradeC>(key).Have);


            //foreach (var key in _e.InventorResourcesEs.Keys) objs.Add(_e.InventorResourcesEs.Resource(key).Resources);
            //foreach (var key in _e.InventorUnitsEs.Keys) objs.Add(_e.Units(key).Units.Amount);
            //foreach (var key in _e.InventorToolWeaponEs.Keys) objs.Add(_e.InventorToolWeaponEs.ToolWeapons(key).ToolWeaponsC.Amount);


            //foreach (var key in _e.WhereUnitsEs.Keys) objs.Add(_e.WhereUnitsEs.WhereUnit(key).HaveUnit.Have);
            //foreach (var key in _e.WhereBuildingEs.Keys) objs.Add(_e.WhereBuildingEs.HaveBuild(key).HaveBuilding.Have);
            //foreach (var key in _e.WhereEnviromentEs.Keys) objs.Add(_e.WhereEnviromentEs.Info(key).HaveEnv.Have);


            //foreach (var item in PickUpgC.HaveUpgrades) objs.Add(item.Value);
            //foreach (var item in UnitAvailPickUpgC.Available_0) objs.Add(item.Value);
            //foreach (var item in BuildAvailPickUpgC.Available) objs.Add(item.Value);
            //foreach (var item in WaterAvailPickUpgC.Available) objs.Add(item.Value);


            #region Other

            //objs.Add(_e.WhoseMove.Player);
            //objs.Add(_e.WinnerC.Player);
            //objs.Add(_e.IsStartedGameC.Is);
            //objs.Add(_e.PeopleInCityE(PlayerTypes.Second).IsReadyC.IsReady);

            //objs.Add(_e.MotionsC.Amount);

            //objs.Add(_e.CenterCloudIdxC);
            //foreach (var item in WindC.Directs) objs.Add(item.Value);
            //objs.Add(WindC.CurDirWind);

            #endregion


            var objects = new object[objs.Count];
            for (int i = 0; i < objects.Length; i++) objects[i] = objs[i];


            _e.RpcPoolEs.RPC(nameof(SyncAllOther), RpcTarget.Others, objects);

            _e.RpcPoolEs.RPC(nameof(UpdateDataAndView), RpcTarget.All, new object[] { });
        }

        [PunRPC]
        void SyncAllOther(object[] objects)
        {
            _idx_cur = 0;


            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                //_e.CellEs(idx_0).UnitEs.Main.UnitTC.Unit = (UnitTypes)objects[_idx_cur++];
                //_e.CellEs(idx_0).UnitEs.Main.LevelC.Level = (LevelTypes)objects[_idx_cur++];
                //_e.CellEs(idx_0).UnitEs.Main.OwnerC.Player = (PlayerTypes)objects[_idx_cur++];
                //_e.CellEs(idx_0).UnitEs.Main.ConditionTC.Condition = (ConditionUnitTypes)objects[_idx_cur++];
                //_e.CellEs(idx_0).UnitEs.Main.IsCorned.Is = (bool)objects[_idx_cur++];

                //_e.CellEs(idx_0).UnitEs.StatEs.Hp.Health.Amount = (int)objects[_idx_cur++];
                //_e.CellEs(idx_0).UnitEs.StatEs.Step.Steps.Amount = (int)objects[_idx_cur++];
                //_e.CellEs(idx_0).UnitEs.StatEs.Water.Water.Amount = (int)objects[_idx_cur++];


                //foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Have = (bool)objects[_idx_cur++];


                //_e.CellEs(idx_0).UnitEs.ToolWeapon.ToolWeaponTC.ToolWeapon = (ToolWeaponTypes)objects[_idx_cur++];
                //_e.CellEs(idx_0).UnitEs.ToolWeapon.LevelTC.Level = (LevelTypes)objects[_idx_cur++];
                //_e.CellEs(idx_0).UnitEs.ToolWeapon.Protection.Amount = (int)objects[_idx_cur++];


                //_e.UnitE(idx_0).SyncRpc((int)objects[_idx_cur++]);



                //foreach (var item in _e.CellEs(idx_0).UnitEs.CooldownKeys) _e.CellEs(idx_0).UnitEs.Ability(item).Cooldown = (int)objects[_idx_cur++];



                //_e.BuildingE(idx_0).Sync((int)objects[_idx_cur++], (BuildingTypes)objects[_idx_cur++], (PlayerTypes)objects[_idx_cur++]);

                //foreach (var item_0 in _e.EnvironmentEs.Keys)
                //{
                //    _e.EnvironmentEs.Environment(item_0, idx_0).Resources.Amount = (int)objects[_idx_cur++];
                //}

                //_e.CellEs(idx_0).RiverEs.RiverTC.River = (RiverTypes)objects[_idx_cur++];
                //foreach (var dir in _e.CellEs(idx_0).RiverEs.Keys)
                //    _e.CellEs(idx_0).RiverEs.HaveRive(dir).HaveRiver.Have = (bool)objects[_idx_cur++];



                //foreach (var item_0 in _e.CellEs(idx_0).TrailEs.Keys)
                //    _e.TrailEs(idx_0).Trail(item_0).Sync((int)objects[_idx_cur++]);



                //_e.CellEs(idx_0).EffectEs.FireE.SyncRpc((bool)objects[_idx_cur++]);
            }


            //_e.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.First).SyncRpc((int)objects[_idx_cur++]);
            //_e.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.Second).SyncRpc((int)objects[_idx_cur++]);
            //_e.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.First).SyncRpc((int)objects[_idx_cur++]);
            //_e.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.Second).SyncRpc((int)objects[_idx_cur++]);



            //foreach (var key in _e.UnitStatUpgradesEs.Keys) _e.UnitStatUpgradesEs.Upgrade(key).HaveUpgrade.Have = (bool)objects[_idx_cur++];
            //foreach (var key in BuildingUpgradesEnt.Keys) BuildingUpgradesEnt.Upgrade<HaveUpgradeC>(key).Have = (bool)objects[_idx_cur++];


            //foreach (var key in _e.InventorResourcesEs.Keys) _e.InventorResourcesEs.Resource(key).Set((int)objects[_idx_cur++]);
            //foreach (var key in _e.InventorUnitsEs.Keys) _e.Units(key).Sync((int)objects[_idx_cur++]);
            //foreach (var key in _e.InventorToolWeaponEs.Keys) _e.InventorToolWeaponEs.ToolWeapons(key).ToolWeaponsC.Amount = (int)objects[_idx_cur++];


            //foreach (var key in _e.WhereUnitsEs.Keys) _e.WhereUnitsEs.WhereUnit(key).HaveUnit.Have = (bool)objects[_idx_cur++];
            //foreach (var key in _e.WhereBuildingEs.Keys) _e.WhereBuildingEs.HaveBuild(key).HaveBuilding.Have = (bool)objects[_idx_cur++];
            //foreach (var key in _e.WhereEnviromentEs.Keys) _e.WhereEnviromentEs.Info(key).HaveEnv.Have = (bool)objects[_idx_cur++];


            //foreach (var item in PickUpgC.HaveUpgrades) PickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in UnitAvailPickUpgC.Available_0) UnitAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in BuildAvailPickUpgC.Available) BuildAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in WaterAvailPickUpgC.Available) WaterAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);


            #region Other

            //_e.WhoseMove.Player = (PlayerTypes)objects[_idx_cur++];
            //_e.WinnerC.Player = (PlayerTypes)objects[_idx_cur++];
            //_e.IsStartedGameC = (bool)objects[_idx_cur++];
            //_e.ReadyE(_e.WhoseMovePlayerTC.CurPlayerI).IsReadyC.IsReady = (bool)objects[_idx_cur++];


            //_e.Motion.AmountMotionsC.Amount = (int)objects[_idx_cur++];

            //_e.CenterCloudIdxC.Set((byte)objects[_idx_cur++]);
            //foreach (var item in WindC.Directs) WindC.Sync(item.Key, (byte)objects[_idx_cur++]);
            //WindC.Sync((DirectTypes)objects[_idx_cur++]);

            #endregion
        }


        [PunRPC]
        void UpdateDataAndView(object[] objects)
        {
            //_updateUI.Invoke();
            //_updateView.Invoke();
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