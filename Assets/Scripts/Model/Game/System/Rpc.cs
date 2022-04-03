using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Interface;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class Rpc : MonoBehaviour, IToggleScene
    {
        EntitiesModelGame _eMGame;
        SystemsModelGame _sMGame;
        EntitiesModelCommon _eMCommon;

        int _idx_cur;


        public static List<string> NamesMethods_S
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

        public Rpc GiveData(in SystemsModelGame sMGame, in EntitiesModelGame eMGame, in EntitiesModelCommon eMCommon)
        {
            _sMGame = sMGame;
            _eMGame = eMGame;
            _eMCommon = eMCommon;

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
            var whoseMove = _eMGame.WhoseMove.PlayerT;

            var curPlayerT = sender.GetPlayer();

            if (obj is byte cell_0)
            {
                var obj_1 = objects[_idx_cur++];

                if (obj_1 is ToolWeaponTypes twT)
                {
                    _sMGame.MasterSs.GiveTakeToolWeaponS_M.GiveTake(twT, (LevelTypes)objects[_idx_cur++], cell_0, sender);
                }
            }

            else if (obj is AbilityTypes abilityT)
            {
                switch (abilityT)
                {
                    case AbilityTypes.CircularAttack:
                        _sMGame.MasterSs.CurcularAttackKingS_M.Attack((byte)objects[_idx_cur++], abilityT, sender);
                        break;

                    case AbilityTypes.FirePawn:
                        _sMGame.MasterSs.FirePawnS_M.Fire((byte)objects[_idx_cur++], sender);
                        break;

                    case AbilityTypes.PutOutFirePawn:
                        _sMGame.MasterSs.PutOutFirePawnS_M.PutOut((byte)objects[_idx_cur++], sender);
                        break;

                    case AbilityTypes.Seed:
                        _sMGame.MasterSs.SeedPawnS_M.Seed(abilityT, sender, (byte)objects[_idx_cur++]);
                        break;

                    case AbilityTypes.SetFarm:
                        _sMGame.MasterSs.BuildFarmS_M.Build((byte)objects[_idx_cur++], sender);
                        break;

                    case AbilityTypes.DestroyBuilding:
                        _sMGame.MasterSs.DestroyBuildingS_M.Destroy((byte)objects[_idx_cur++], sender);
                        break;

                    case AbilityTypes.FireArcher:
                        _sMGame.MasterSs.FireArcherS_M.Fire((byte)objects[_idx_cur++], (byte)objects[_idx_cur++], sender);
                        break;

                    case AbilityTypes.GrowAdultForest:
                        _sMGame.MasterSs.GrowAdultForestS_M.Grow((byte)objects[_idx_cur++], abilityT, sender);
                        break;

                    case AbilityTypes.StunElfemale:
                        _sMGame.MasterSs.StunElfemaleS_M.Stun((byte)objects[_idx_cur++], (byte)objects[_idx_cur++], abilityT, sender);
                        break;

                    case AbilityTypes.ChangeCornerArcher:
                        _sMGame.MasterSs.ChangeCornerArcherS_M.Change((byte)objects[_idx_cur++], abilityT, sender);
                        break;

                    //Snowy
                    case AbilityTypes.ChangeDirectionWind:
                        _sMGame.MasterSs.ChangeDirectionWindS_M.Change((byte)objects[_idx_cur++], (byte)objects[_idx_cur++], abilityT, sender);
                        break;

                    case AbilityTypes.IncreaseWindSnowy:
                        _sMGame.MasterSs.IncreaseWindSnowyS_M.Execute(true, (byte)objects[_idx_cur++], abilityT, sender);
                        break;

                    case AbilityTypes.DecreaseWindSnowy:
                        _sMGame.MasterSs.IncreaseWindSnowyS_M.Execute(false, (byte)objects[_idx_cur++], abilityT, sender);
                        break;


                    case AbilityTypes.Resurrect:
                        {
                            var idx_from = (byte)objects[_idx_cur++];
                            var idx_to = (byte)objects[_idx_cur++];

                            if (!_eMGame.UnitTC(idx_to).HaveUnit)
                            {
                                if (!_eMGame.UnitAbilityE(idx_from).HaveCooldown(abilityT))
                                {
                                    if (_eMGame.UnitStepC(idx_from).Steps >= StepValues.RESURRECT)
                                    {
                                        _eMGame.UnitAbilityE(idx_from).Cooldown(abilityT) = AbilityCooldownValues.NeedAfterAbility(abilityT);
                                        _eMGame.UnitStepC(idx_from).Steps -= StepValues.RESURRECT;

                                        if (_eMGame.LastDiedE(idx_to).UnitTC.HaveUnit)
                                        {
                                            //e.UnitE(idx_to).SetNew((e.LastDiedUnitTC(idx_to).Unit, e.LastDiedLevelTC(idx_to).Level, e.LastDiedPlayerTC(idx_to).Player, ConditionUnitTypes.None, false), e);
                                            _eMGame.LastDiedUnitTC(idx_to).UnitT = UnitTypes.None;
                                        }
                                    }

                                    else
                                    {
                                        _eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                    }
                                }
                            }
                        }
                        break;

                    case AbilityTypes.SetTeleport:
                        {
                            cell_0 = (byte)objects[_idx_cur++];

                            if (!_eMGame.BuildingTC(cell_0).HaveBuilding)
                            {
                                if (!_eMGame.AdultForestC(cell_0).HaveAnyResources)
                                {
                                    _eMGame.YoungForestC(cell_0).Resources = 0;
                                    _eMGame.FertilizeC(cell_0).Resources = 0;

                                    if (_eMGame.UnitStepC(cell_0).Steps >= StepValues.SET_TELEPORT)
                                    {
                                        _eMGame.UnitStepC(cell_0).Steps -= StepValues.SET_TELEPORT;

                                        if (_eMGame.WhereTeleportC.Start > 0)
                                        {
                                            if (_eMGame.WhereTeleportC.End > 0)
                                            {
                                                _eMGame.BuildingTC(_eMGame.WhereTeleportC.Start).BuildingT = BuildingTypes.None;

                                                _eMGame.WhereTeleportC.Start = _eMGame.WhereTeleportC.End;

                                                _eMGame.WhereTeleportC.End = cell_0;
                                                _eMGame.UnitAbilityE(cell_0).Cooldown(abilityT) = AbilityCooldownValues.NeedAfterAbility(abilityT);
                                            }
                                            else
                                            {
                                                _eMGame.WhereTeleportC.End = cell_0;
                                                _eMGame.UnitAbilityE(cell_0).Cooldown(abilityT) = AbilityCooldownValues.NeedAfterAbility(abilityT);
                                            }
                                        }
                                        else
                                        {
                                            _eMGame.WhereTeleportC.Start = cell_0;
                                        }

                                        _eMGame.BuildingTC(cell_0).BuildingT = BuildingTypes.Teleport;
                                        _eMGame.BuildingLevelTC(cell_0).LevelT = LevelTypes.First;
                                        _eMGame.BuildingPlayerTC(cell_0).PlayerT = whoseMove;
                                        _eMGame.BuildingHpC(cell_0).Health = BuildingValues.MAX_HP;
                                    }
                                }
                            }
                        }
                        break;

                    case AbilityTypes.Teleport:
                        {
                            cell_0 = (byte)objects[_idx_cur++];

                            if (_eMGame.UnitStepC(cell_0).Steps >= StepValues.TELEPORT)
                            {
                                if (_eMGame.BuildingTC(cell_0).Is(BuildingTypes.Teleport))
                                {
                                    var idx_start = _eMGame.WhereTeleportC.Start;
                                    var idx_end = _eMGame.WhereTeleportC.End;

                                    if (_eMGame.WhereTeleportC.End > 0 && idx_start == cell_0)
                                    {
                                        if (!_eMGame.UnitTC(idx_end).HaveUnit)
                                        {
                                            _eMGame.UnitStepC(cell_0).Steps -= StepValues.TELEPORT;

                                            //Teleport(idx_end, ents);
                                        }
                                    }
                                    else if (_eMGame.WhereTeleportC.Start > 0 && idx_end == cell_0)
                                    {
                                        if (!_eMGame.UnitTC(idx_start).HaveUnit)
                                        {
                                            _eMGame.UnitStepC(cell_0).Steps -= StepValues.TELEPORT;

                                            //Teleport(idx_start, _e);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                _eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        break;

                    case AbilityTypes.InvokeSkeletons:
                        {
                            //    var cell_0 = CellEs.Idx;

                            //    if (ents.UnitStepC(cell_0).Have(CellUnitStatStepValues.NeedForAbility(ability)))
                            //    {
                            //        ents.UnitStepC(cell_0).Take(CellUnitStatStepValues.NeedForAbility(ability));

                            //        foreach (var idx_1 in ents.CellSpaceWorker.GetIdxsAround(cell_0))
                            //        {
                            //            if (!ents.UnitTC(cell_0).HaveUnit && !ents.MountainC(idx_1).HaveAny)
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
                _sMGame.MasterSs.BuyBuildingS_M.Buy(buildT, sender);
            }

            else if (obj is MarketBuyTypes marketBuy) _sMGame.MasterSs.BuyS_M.Buy(marketBuy, sender);

            else if (obj is RpcMasterTypes rpcT)
            {
                switch (rpcT)
                {
                    case RpcMasterTypes.Ready:
                        _sMGame.MasterSs.ReadyS_M.Ready(sender);
                        break;

                    case RpcMasterTypes.Done:
                        _sMGame.MasterSs.DonerS_M.TryDone(_eMCommon.GameModeTC, sender, curPlayerT);
                        break;

                    case RpcMasterTypes.Shift:
                        _sMGame.MasterSs.ShiftUnitS_M.TryShift((byte)objects[_idx_cur++], (byte)objects[_idx_cur++], sender);
                        break;

                    case RpcMasterTypes.Attack:
                        var cell_from = (byte)objects[_idx_cur++];
                        var cell_to = (byte)objects[_idx_cur++];
                        _sMGame.MasterSs.AttackUnit_M.Attack(cell_from, cell_to);
                        break;

                    case RpcMasterTypes.ConditionUnit:
                        _sMGame.MasterSs.SetConditionUnitS_M.Set((ConditionUnitTypes)objects[_idx_cur++], (byte)objects[_idx_cur++], sender);
                        break;

                    case RpcMasterTypes.SetUnit:
                        var cell = (byte)objects[_idx_cur++];
                        _sMGame.MasterSs.SetUnitS_M.Set((UnitTypes)objects[_idx_cur++], sender, cell);
                        break;

                    case RpcMasterTypes.GetHero:
                        _sMGame.MasterSs.GetHeroS_M.Get((UnitTypes)objects[_idx_cur++], sender);
                        break;

                    case RpcMasterTypes.Melt:
                        _sMGame.MasterSs.MeltS_M.Melt(sender);
                        break;

                    default: throw new Exception();
                }
            }

            else throw new Exception();


            _sMGame.MasterSs.GetDataCellsS_M.Run();
            _eMGame.NeedUpdateView = true;

            SyncAllMaster();
        }

        [PunRPC]
        void GeneralRpc(object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;

            var obj = objects[_idx_cur++];

            if (obj is MistakeTypes mistakeT)
            {
                _sMGame.SetMistakeS.Set(mistakeT, 0);

                _eMGame.SoundActionC(ClipTypes.WritePensil).Action.Invoke();

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
                    _eMGame.MistakeEconomy(ResourceTypes.Food).Resources = 0;
                    _eMGame.MistakeEconomy(ResourceTypes.Wood).Resources = 0;
                    _eMGame.MistakeEconomy(ResourceTypes.Ore).Resources = 0;
                    _eMGame.MistakeEconomy(ResourceTypes.Iron).Resources = 0;
                    _eMGame.MistakeEconomy(ResourceTypes.Gold).Resources = 0;

                    var needRes = (float[])objects[_idx_cur++];

                    _eMGame.MistakeEconomy(ResourceTypes.Food).Resources = needRes[0];
                    _eMGame.MistakeEconomy(ResourceTypes.Wood).Resources = needRes[1];
                    _eMGame.MistakeEconomy(ResourceTypes.Ore).Resources = needRes[2];
                    _eMGame.MistakeEconomy(ResourceTypes.Iron).Resources = needRes[3];
                    _eMGame.MistakeEconomy(ResourceTypes.Gold).Resources = needRes[4];
                }
            }
            else if (obj is RpcGeneralTypes rpcT)
            {
                switch (rpcT)
                {
                    case RpcGeneralTypes.None:
                        throw new Exception();

                    case RpcGeneralTypes.SoundEff:
                        _eMGame.SoundActionC((ClipTypes)objects[_idx_cur++]).Invoke();
                        break;

                    case RpcGeneralTypes.SoundUniqueAbility:
                        _eMGame.SoundActionC((AbilityTypes)objects[_idx_cur++]).Invoke();
                        break;

                    case RpcGeneralTypes.SoundRpcMaster:
                        //Sound((UniqueAbilityTypes)objects[_idx_cur++]).Invoke();
                        break;

                    case RpcGeneralTypes.ActiveMotion:
                        _eMGame.ZoneInfoC.IsActiveMotion = true;
                        break;

                    default:
                        throw new Exception();
                }
            }
        }

        [PunRPC]
        void OtherRpc(object[] objects, PhotonMessageInfo infoFrom) => _eMGame.RpcPoolEs.OtherRpc(objects, infoFrom);


        #region SyncData

        public void SyncAllMaster()
        {
            var objs = new List<object>();


            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                objs.Add(_eMGame.UnitTC(cell_0).UnitT);
                //objs.Add(_e.CellEs(cell_0).UnitEs.MainE.LevelTC.Level);
                objs.Add(_eMGame.UnitPlayerTC(cell_0).PlayerT);

                objs.Add(_eMGame.UnitHpC(cell_0).Health);
                objs.Add(_eMGame.UnitStepC(cell_0).Steps);
                objs.Add(_eMGame.UnitWaterC(cell_0).Water);

                objs.Add(_eMGame.UnitConditionTC(cell_0).Condition);
                //foreach (var item in CellUnitEffectsEs.Keys) objs.Add(CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, cell_0).Have);


                //objs.Add(_e.CellEs(cell_0).UnitEs.ExtraToolWeaponTC.ToolWeapon);
                //objs.Add(_e.CellEs(cell_0).UnitEs.ExtraTWLevelTC.Level);
                //objs.Add(_e.CellEs(cell_0).UnitEs.ExtraTWShieldC.Protection);

                objs.Add(_eMGame.UnitEffectStunC(cell_0).Stun);

                objs.Add(_eMGame.UnitIsRightArcherC(cell_0).IsRight);

                //foreach (var item in _e.CellEs(cell_0).UnitEs.CooldownKeys) objs.Add(_e.CellEs(cell_0).UnitEs.Ability(item).CooldownC);





                //objs.Add(_e.BuildingE(cell_0).Building);
                //objs.Add(_e.BuildingE(cell_0).Owner);



                //foreach (var env in _e.EnvironmentEs.Keys)
                //{
                //    objs.Add(_e.EnvironmentEs.Environment(env, cell_0));
                //}




                objs.Add(_eMGame.CellEs(cell_0).RiverEs.RiverTC.River);
                //foreach (var item_0 in _e.CellEs(cell_0).RiverEs.Keys)
                //    objs.Add(_e.CellEs(cell_0).RiverEs.HaveRive(item_0).HaveRiver.Have);


                //foreach (var item_0 in _e.CellEs(cell_0).TrailEs.Keys)
                //    objs.Add(_e.CellEs(cell_0).TrailEs.Trail(item_0));

                //objs.Add(_e.CellEs(cell_0).EffectEs.FireE.HaveFireC);
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


            _eMGame.RpcPoolEs.RPC(nameof(SyncAllOther), RpcTarget.Others, objects);

            _eMGame.RpcPoolEs.RPC(nameof(UpdateDataAndView), RpcTarget.All, new object[] { });
        }

        [PunRPC]
        void SyncAllOther(object[] objects)
        {
            _idx_cur = 0;


            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                //_e.CellEs(cell_0).UnitEs.Main.UnitTC.Unit = (UnitTypes)objects[_idx_cur++];
                //_e.CellEs(cell_0).UnitEs.Main.LevelC.Level = (LevelTypes)objects[_idx_cur++];
                //_e.CellEs(cell_0).UnitEs.Main.OwnerC.Player = (PlayerTypes)objects[_idx_cur++];
                //_e.CellEs(cell_0).UnitEs.Main.ConditionTC.Condition = (ConditionUnitTypes)objects[_idx_cur++];
                //_e.CellEs(cell_0).UnitEs.Main.IsCorned.Is = (bool)objects[_idx_cur++];

                //_e.CellEs(cell_0).UnitEs.StatEs.Hp.Health.Amount = (int)objects[_idx_cur++];
                //_e.CellEs(cell_0).UnitEs.StatEs.Step.Steps.Amount = (int)objects[_idx_cur++];
                //_e.CellEs(cell_0).UnitEs.StatEs.Water.Water.Amount = (int)objects[_idx_cur++];


                //foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, cell_0).Have = (bool)objects[_idx_cur++];


                //_e.CellEs(cell_0).UnitEs.ToolWeapon.ToolWeaponTC.ToolWeapon = (ToolWeaponTypes)objects[_idx_cur++];
                //_e.CellEs(cell_0).UnitEs.ToolWeapon.LevelTC.Level = (LevelTypes)objects[_idx_cur++];
                //_e.CellEs(cell_0).UnitEs.ToolWeapon.Protection.Amount = (int)objects[_idx_cur++];


                //_e.UnitE(cell_0).SyncRpc((int)objects[_idx_cur++]);



                //foreach (var item in _e.CellEs(cell_0).UnitEs.CooldownKeys) _e.CellEs(cell_0).UnitEs.Ability(item).Cooldown = (int)objects[_idx_cur++];



                //_e.BuildingE(cell_0).Sync((int)objects[_idx_cur++], (BuildingTypes)objects[_idx_cur++], (PlayerTypes)objects[_idx_cur++]);

                //foreach (var item_0 in _e.EnvironmentEs.Keys)
                //{
                //    _e.EnvironmentEs.Environment(item_0, cell_0).Resources.Amount = (int)objects[_idx_cur++];
                //}

                //_e.CellEs(cell_0).RiverEs.RiverTC.River = (RiverTypes)objects[_idx_cur++];
                //foreach (var dir in _e.CellEs(cell_0).RiverEs.Keys)
                //    _e.CellEs(cell_0).RiverEs.HaveRive(dir).HaveRiver.Have = (bool)objects[_idx_cur++];



                //foreach (var item_0 in _e.CellEs(cell_0).TrailEs.Keys)
                //    _e.TrailEs(cell_0).Trail(item_0).Sync((int)objects[_idx_cur++]);



                //_e.CellEs(cell_0).EffectEs.FireE.SyncRpc((bool)objects[_idx_cur++]);
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