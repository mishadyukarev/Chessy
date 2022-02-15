using Game.Common;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public sealed class RpcS : MonoBehaviour
    {
        static Entities _ents;
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
            _ents = ents;
            _systems = systems;
            return this;
        }


        [PunRPC]
        void MasterRPC(object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;

            var sender = infoFrom.Sender;

            var obj = objects[_idx_cur++];

            var whoseMove = _ents.WhoseMovePlayerTC.Player;

            if (obj is AbilityTypes ability)
            {
                switch (ability)
                {
                    case AbilityTypes.None: throw new Exception();

                    case AbilityTypes.CircularAttack:
                        _ents.UnitEs((byte)objects[_idx_cur++]).Ability(ability).CircularAttack_Master(sender, _ents);
                        break;

                    case AbilityTypes.BonusNear:
                        _ents.UnitEs((byte)objects[_idx_cur++]).Ability(ability).BonusNear_Master(sender, _ents);
                        break;

                    case AbilityTypes.FirePawn:
                        _ents.UnitEs((byte)objects[_idx_cur++]).Ability(ability).FirePawn_Master(sender, _ents);
                        break;

                    case AbilityTypes.PutOutFirePawn:
                        _ents.UnitEs((byte)objects[_idx_cur++]).Ability(ability).PutOut_Master(sender, _ents);
                        break;

                    case AbilityTypes.Seed:
                        _ents.UnitEs((byte)objects[_idx_cur++]).Ability(ability).Seed_Master(sender, _ents);
                        break;

                    case AbilityTypes.SetFarm:
                        _ents.UnitEs((byte)objects[_idx_cur++]).Ability(ability).BuildFarm_Master(sender, _ents);
                        break;

                    //case AbilityTypes.Mine:
                    //    _ents.UnitEs((byte)objects[_idx_cur++]).Ability(ability).BuildMine_Master(sender, _ents);
                    //    break;

                    case AbilityTypes.SetCity:
                        _ents.UnitEs((byte)objects[_idx_cur++]).Ability(ability).BuildCity_Master(sender, _ents);
                        break;

                    case AbilityTypes.DestroyBuilding:
                        _ents.UnitEs((byte)objects[_idx_cur++]).Ability(ability).Destroy_Master(sender, _ents);
                        break;

                    case AbilityTypes.FireArcher:
                        _ents.UnitEs((byte)objects[_idx_cur++]).Ability(ability).FireArcher_Master((byte)objects[_idx_cur++], sender, _ents);
                        break;

                    case AbilityTypes.GrowAdultForest:
                        _ents.UnitEs((byte)objects[_idx_cur++]).Ability(ability).GrowElfemale_Master(sender, _ents);
                        break;

                    case AbilityTypes.StunElfemale:
                        _ents.UnitEs((byte)objects[_idx_cur++]).Ability(ability).StunElfemale_Master((byte)objects[_idx_cur++], sender, _ents);
                        break;

                    case AbilityTypes.ChangeDirectionWind:
                        _ents.UnitEs((byte)objects[_idx_cur++]).Ability(ability).ChangeDirectionWind_Master((byte)objects[_idx_cur++], sender, _ents);
                        break;

                    case AbilityTypes.ChangeCornerArcher:
                        _ents.UnitEs((byte)objects[_idx_cur++]).Ability(ability).ChangeCornerArcher_Master(sender, _ents);
                        break;

                    case AbilityTypes.IceWall:
                        _ents.UnitEs((byte)objects[_idx_cur++]).Ability(ability).SetIceWallSnowy_Master(_ents);
                        break;

                    case AbilityTypes.ActiveAroundBonusSnowy:
                        _ents.UnitEs((byte)objects[_idx_cur++]).Ability(ability).ActiveSnowyAround_Master(sender, _ents);
                        break;

                    case AbilityTypes.DirectWave:
                        _ents.UnitEs((byte)objects[_idx_cur++]).Ability(ability).DirectWaveSnowy_Master((byte)objects[_idx_cur++], sender, _ents);
                        break;

                    case AbilityTypes.Resurrect:
                        _ents.UnitEs((byte)objects[_idx_cur++]).Ability(ability).ResurrectUnit_Master(sender, (byte)objects[_idx_cur++], _ents);
                        break;

                    case AbilityTypes.SetTeleport:
                        _ents.UnitEs((byte)objects[_idx_cur++]).Ability(ability).SetTeleport_Master(_ents);
                        break;

                    case AbilityTypes.Teleport:
                        _ents.UnitE((byte)objects[_idx_cur++]).Teleport_Master(ability, sender, _ents);
                        break;

                    case AbilityTypes.InvokeSkeletons:
                        _ents.UnitE((byte)objects[_idx_cur++]).InvokeSkeletons_Master(ability, sender, _ents);
                        break;

                    default: throw new Exception();
                }
            }

            else if (obj is BuildingTypes buildT)
            {
                _ents.BuildingE((byte)objects[_idx_cur++]).Build_Master((byte)objects[_idx_cur++], buildT, sender, _ents);
            }

            else if (obj is MarketBuyTypes marketBuy)
            {
                _ents.InventorResourcesEs.TryBuyResourcesFromMarket_Master(marketBuy, sender, _ents);
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

                            _ents.ReadyE(playerSend).IsReadyC.IsReady = !_ents.ReadyE(playerSend).IsReadyC.IsReady;

                            if (_ents.ReadyE(PlayerTypes.First).IsReadyC.IsReady
                                && _ents.ReadyE(PlayerTypes.Second).IsReadyC.IsReady)
                            {
                                _ents.IsStartedGameC.Is = true;
                            }

                            else
                            {
                                _ents.IsStartedGameC.Is = false;
                            }
                        }       
                        break;

                    case RpcMasterTypes.Done:

                        _ents.RpcE.SoundToGeneral(sender, ClipTypes.ClickToTable);

                        if (PhotonNetwork.OfflineMode)
                        {
                            if (GameModeC.IsGameMode(GameModes.TrainingOff))
                            {
                                foreach (var idx in _ents.CellSpaceWorker.Idxs)
                                {
                                    _ents.UnitStunC(idx).Stun -= 2;
                                    //EntitiesPool.IceWalls[idx_0].Hp.Take(2);
                                }
                                _systems.SystemsMaster.InvokeRun(SystemDataMasterTypes.UpdateMove);
                                _ents.RpcE.ActiveMotionZoneToGen(sender);
                            }

                            else if (GameModeC.IsGameMode(GameModes.WithFriendOff))
                            {
                                foreach (var idx in _ents.CellSpaceWorker.Idxs)
                                {
                                    _ents.UnitStunC(idx).Stun -= 2;
                                    //EntitiesPool.IceWalls[idx_0].Hp.Take();
                                }

                                var curPlayer = _ents.WhoseMovePlayerTC.CurPlayerI;
                                var nextPlayer = _ents.WhoseMovePlayerTC.NextPlayerFrom(curPlayer);

                                if (nextPlayer == PlayerTypes.First)
                                {
                                    _systems.SystemsMaster.InvokeRun(SystemDataMasterTypes.UpdateMove);
                                    _ents.RpcE.ActiveMotionZoneToGen(sender);
                                }

                                _ents.WhoseMovePlayerTC.Player = nextPlayer;


                                curPlayer = _ents.WhoseMovePlayerTC.CurPlayerI;

                                //ViewDataSC.RotateAll.Invoke();

                                _ents.FriendIsActiveC.IsActive = true;
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
                        break;

                    case RpcMasterTypes.Shift:
                        _ents.UnitEs((byte)objects[_idx_cur++]).UnitE.Shift_Master((byte)objects[_idx_cur++], sender, _ents);
                        break;

                    case RpcMasterTypes.Attack:
                        _ents.UnitEs((byte)objects[_idx_cur++]).UnitE.Attack_Master((byte)objects[_idx_cur++], _ents);
                        break;

                    case RpcMasterTypes.ConditionUnit:
                        _ents.UnitE((byte)objects[_idx_cur++]).Condition_Master((ConditionUnitTypes)objects[_idx_cur++], sender, _ents);
                        break;

                    case RpcMasterTypes.SetUnit:
                        _ents.UnitEs((byte)objects[_idx_cur++]).UnitE.SetUnit_Master((UnitTypes)objects[_idx_cur++], sender, _ents);
                        break;

                    case RpcMasterTypes.GiveTakeToolWeapon:
                        idx_0 = (byte)objects[_idx_cur++];
                        var twT = (ToolWeaponTypes)objects[_idx_cur++];
                        var levelTW = (LevelTypes)objects[_idx_cur++];
                        if (twT == ToolWeaponTypes.Axe || twT == ToolWeaponTypes.BowCrossbow)
                        {
                            if (_ents.UnitTC(idx_0).Is(UnitTypes.Pawn))
                            {
                                if (_ents.UnitStepC(idx_0).Steps >= CellUnitStatStepValues.FOR_GIVE_TAKE_MAIN_TOOLWEAPON)
                                {
                                    if (_ents.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Axe))
                                    {
                                        if (_ents.UnitMainTWLevelTC(idx_0).Is(LevelTypes.First))
                                        {
                                            if (_ents.InventorToolWeaponEs.ToolWeapons(twT, levelTW, whoseMove).ToolWeaponsC.HaveAny)
                                            {
                                                //_ents.SetFromInventor(twT, levT, whoseMove, _ents.InventorToolWeaponEs);
                                                _ents.UnitStepC(idx_0).Take(CellUnitStatStepValues.FOR_GIVE_TAKE_MAIN_TOOLWEAPON);

                                                _ents.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                            }
                                            else
                                            {
                                                if (_ents.InventorResourcesEs.CanBuyTW(twT, levelTW, whoseMove, out var needs))
                                                {
                                                    _ents.InventorResourcesEs.BuyTW(twT, levelTW, whoseMove);
                                                    //_ents.Set(twT, levT);

                                                    _ents.UnitStepC(idx_0).Take(CellUnitStatStepValues.FOR_GIVE_TAKE_MAIN_TOOLWEAPON);

                                                    _ents.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                                }
                                                else
                                                {
                                                    _ents.RpcE.MistakeEconomyToGeneral(sender, needs);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //TakeToInventor(whoseMove, e.InventorToolWeaponEs);
                                            _ents.UnitStepC(idx_0).Take(CellUnitStatStepValues.FOR_GIVE_TAKE_MAIN_TOOLWEAPON);
                                        }
                                    }
                                    else
                                    {
                                        //TakeToInventor(whoseMove, e.InventorToolWeaponEs);
                                        _ents.UnitStepC(idx_0).Take(CellUnitStatStepValues.FOR_GIVE_TAKE_MAIN_TOOLWEAPON);

                                        _ents.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                    }
                                }
                                else _ents.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                            }
                        }
                        else
                        {
                            var ownUnit_0 = _ents.UnitPlayerTC(idx_0).Player;

                            if (_ents.UnitTC(idx_0).Is(UnitTypes.Pawn))
                            {
                                if (_ents.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Axe))
                                {
                                    if (_ents.UnitStepC(idx_0).HaveSteps)
                                    {
                                        if (_ents.ExtraTWE(idx_0).ToolWeaponTC.HaveToolWeapon)
                                        {
                                            _ents.InventorToolWeaponEs.ToolWeapons(_ents.ExtraTWE(idx_0).ToolWeaponTC.ToolWeapon, _ents.ExtraTWE(idx_0).LevelTC.Level, ownUnit_0).ToolWeaponsC.Add(1);
                                            _ents.UnitEs(idx_0).ExtraToolWeaponE.Reset();

                                            _ents.UnitStepC(idx_0).Take(CellUnitStatStepValues.FOR_GIVE_TAKE_EXTRA_TOOLWEAPON);

                                            _ents.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                        }


                                        else if (_ents.InventorToolWeaponEs.ToolWeapons(twT, levelTW, ownUnit_0).ToolWeaponsC.HaveAny)
                                        {
                                            _ents.InventorToolWeaponEs.ToolWeapons(twT, levelTW, ownUnit_0).ToolWeaponsC.Take(1);

                                            _ents.UnitEs(idx_0).ExtraToolWeaponE.SetNew(twT, levelTW);

                                            _ents.UnitStepC(idx_0).Take(CellUnitStatStepValues.FOR_GIVE_TAKE_EXTRA_TOOLWEAPON);

                                            _ents.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                        }

                                        else
                                        {
                                            if (_ents.InventorResourcesEs.CanBuyTW(twT, levelTW, ownUnit_0, out var needRes))
                                            {
                                                _ents.InventorResourcesEs.BuyTW(twT, levelTW, ownUnit_0);

                                                _ents.UnitEs(idx_0).ExtraToolWeaponE.SetNew(twT, levelTW);

                                                _ents.UnitStepC(idx_0).Take(CellUnitStatStepValues.FOR_GIVE_TAKE_EXTRA_TOOLWEAPON);

                                                _ents.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                            }
                                            else
                                            {
                                                _ents.RpcE.MistakeEconomyToGeneral(sender, needRes);
                                            }
                                        }
                                    }
                                    else _ents.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                                }
                            }
                        }
                            
                        break;

                    case RpcMasterTypes.GetHero:
                        _ents.Units((UnitTypes)objects[_idx_cur++], LevelTypes.First, whoseMove).AmountC.Add(1);
                        _ents.AvailableCenterHero(whoseMove).HaveCenterHero.Have = false;
                        break;

                    case RpcMasterTypes.UpgCenterUnits:
                        _ents.AvailableCenterUpgradeEs.UpgradeCenterUnit_Master((UnitTypes)objects[_idx_cur++], sender, _ents);
                        break;

                    case RpcMasterTypes.UpgCenterBuild:
                        _ents.BuildingUpgradeEs.UpgradeCenter_Master((BuildingTypes)objects[_idx_cur++], sender, _ents);
                        break;

                    case RpcMasterTypes.UpgWater:
                        _ents.UnitStatUpgradesEs.UpgradeCenterWater_Master(sender, _ents);
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
                _ents.MistakeC.Mistake = mistakeT;
                _ents.Sound(ClipTypes.Mistake).ActionC.Action.Invoke();

                if (mistakeT == MistakeTypes.Economy)
                {
                    _ents.MistakeEconomyE(ResourceTypes.Food).SetZero();
                    _ents.MistakeEconomyE(ResourceTypes.Wood).SetZero();
                    _ents.MistakeEconomyE(ResourceTypes.Ore).SetZero();
                    _ents.MistakeEconomyE(ResourceTypes.Iron).SetZero();
                    _ents.MistakeEconomyE(ResourceTypes.Gold).SetZero();

                    var needRes = (float[])objects[_idx_cur++];

                    _ents.MistakeEconomyE(ResourceTypes.Food).Set(needRes[0]);
                    _ents.MistakeEconomyE(ResourceTypes.Wood).Set(needRes[1]);
                    _ents.MistakeEconomyE(ResourceTypes.Ore).Set(needRes[2]);
                    _ents.MistakeEconomyE(ResourceTypes.Iron).Set(needRes[3]);
                    _ents.MistakeEconomyE(ResourceTypes.Gold).Set(needRes[4]);
                }
            }
            else if (obj is RpcGeneralTypes rpcT)
            {
                switch (rpcT)
                {
                    case RpcGeneralTypes.None:
                        throw new Exception();

                    case RpcGeneralTypes.SoundEff:
                        _ents.Sound((ClipTypes)objects[_idx_cur++]).ActionC.Invoke();
                        break;

                    case RpcGeneralTypes.SoundUniqueAbility:
                        _ents.Sound((AbilityTypes)objects[_idx_cur++]).ActionC.Invoke();
                        break;

                    case RpcGeneralTypes.SoundRpcMaster:
                        //Sound((UniqueAbilityTypes)objects[_idx_cur++]).Invoke();
                        break;

                    case RpcGeneralTypes.ActiveMotion:
                        _ents.MotionIsActiveC.IsActive = true;
                        break;

                    default:
                        throw new Exception();
                }
            }
        }

        [PunRPC]
        void OtherRpc(object[] objects, PhotonMessageInfo infoFrom) => _ents.RpcE.OtherRpc(objects, infoFrom);


        #region SyncData

        public static void SyncAllMaster()
        {
            var objs = new List<object>();


            foreach (byte idx_0 in _ents.CellSpaceWorker.Idxs)
            {
                objs.Add(_ents.UnitTC(idx_0).Unit);
                //objs.Add(_ents.CellEs(idx_0).UnitEs.MainE.LevelTC.Level);
                objs.Add(_ents.UnitPlayerTC(idx_0).Player);

                objs.Add(_ents.UnitHpC(idx_0).Health);
                objs.Add(_ents.UnitStepC(idx_0).Steps);
                objs.Add(_ents.UnitWaterC(idx_0).Water);

                objs.Add(_ents.UnitConditionTC(idx_0).Condition);
                //foreach (var item in CellUnitEffectsEs.Keys) objs.Add(CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Have);


                objs.Add(_ents.CellEs(idx_0).UnitEs.ExtraToolWeaponE.ToolWeaponTC.ToolWeapon);
                objs.Add(_ents.CellEs(idx_0).UnitEs.ExtraToolWeaponE.LevelTC.Level);
                objs.Add(_ents.CellEs(idx_0).UnitEs.ExtraToolWeaponE.ProtectionShieldC.Protection);

                objs.Add(_ents.UnitStunC(idx_0).Stun);

                objs.Add(_ents.UnitIsRightArcherC(idx_0).IsRight);

                foreach (var item in _ents.CellEs(idx_0).UnitEs.CooldownKeys) objs.Add(_ents.CellEs(idx_0).UnitEs.Ability(item).Cooldown);





                objs.Add(_ents.BuildingE(idx_0).Building);
                objs.Add(_ents.BuildingE(idx_0).Owner);



                //foreach (var env in _ents.EnvironmentEs.Keys)
                //{
                //    objs.Add(_ents.EnvironmentEs.Environment(env, idx_0));
                //}




                objs.Add(_ents.CellEs(idx_0).RiverEs.RiverE.RiverTC.River);
                foreach (var item_0 in _ents.CellEs(idx_0).RiverEs.Keys)
                    objs.Add(_ents.CellEs(idx_0).RiverEs.HaveRive(item_0).HaveRiver.Have);


                foreach (var item_0 in _ents.CellEs(idx_0).TrailEs.Keys)
                    objs.Add(_ents.CellEs(idx_0).TrailEs.Trail(item_0));

                objs.Add(_ents.CellEs(idx_0).EffectEs.FireE.HaveFireC.Have);
            }

            objs.Add(_ents.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.First).CooldownC.Amount);
            objs.Add(_ents.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.Second).CooldownC.Amount);
            objs.Add(_ents.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.First).CooldownC.Amount);
            objs.Add(_ents.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.Second).CooldownC.Amount);



            //foreach (var key in _ents.UnitStatUpgradesEs.Keys) objs.Add(_ents.UnitStatUpgradesEs.Upgrade(key).HaveUpgrade.Have);
            //foreach (var key in BuildingUpgradesEnt.Keys) objs.Add(BuildingUpgradesEnt.Upgrade<HaveUpgradeC>(key).Have);


            //foreach (var key in _ents.InventorResourcesEs.Keys) objs.Add(_ents.InventorResourcesEs.Resource(key).Resources);
            //foreach (var key in _ents.InventorUnitsEs.Keys) objs.Add(_ents.Units(key).Units.Amount);
            foreach (var key in _ents.InventorToolWeaponEs.Keys) objs.Add(_ents.InventorToolWeaponEs.ToolWeapons(key).ToolWeaponsC.Amount);


            //foreach (var key in _ents.WhereUnitsEs.Keys) objs.Add(_ents.WhereUnitsEs.WhereUnit(key).HaveUnit.Have);
            //foreach (var key in _ents.WhereBuildingEs.Keys) objs.Add(_ents.WhereBuildingEs.HaveBuild(key).HaveBuilding.Have);
            //foreach (var key in _ents.WhereEnviromentEs.Keys) objs.Add(_ents.WhereEnviromentEs.Info(key).HaveEnv.Have);


            //foreach (var item in PickUpgC.HaveUpgrades) objs.Add(item.Value);
            //foreach (var item in UnitAvailPickUpgC.Available_0) objs.Add(item.Value);
            //foreach (var item in BuildAvailPickUpgC.Available) objs.Add(item.Value);
            //foreach (var item in WaterAvailPickUpgC.Available) objs.Add(item.Value);


            #region Other

            objs.Add(_ents.WhoseMovePlayerTC.Player);
            objs.Add(_ents.WinnerC.Player);
            objs.Add(_ents.IsStartedGameC.Is);
            objs.Add(_ents.ReadyE(PlayerTypes.Second).IsReadyC.IsReady);

            objs.Add(_ents.MotionsC.Amount);

            objs.Add(_ents.CenterCloudIdxC.Idx);
            //foreach (var item in WindC.Directs) objs.Add(item.Value);
            //objs.Add(WindC.CurDirWind);

            #endregion


            var objects = new object[objs.Count];
            for (int i = 0; i < objects.Length; i++) objects[i] = objs[i];


            _ents.RpcE.RPC(nameof(SyncAllOther), RpcTarget.Others, objects);

            _ents.RpcE.RPC(nameof(UpdateDataAndView), RpcTarget.All, new object[] { });
        }

        [PunRPC]
        void SyncAllOther(object[] objects)
        {
            _idx_cur = 0;


            foreach (byte idx_0 in _ents.CellSpaceWorker.Idxs)
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

                

                foreach (var item in _ents.CellEs(idx_0).UnitEs.CooldownKeys) _ents.CellEs(idx_0).UnitEs.Ability(item).Cooldown = (int)objects[_idx_cur++];



                _ents.BuildingE(idx_0).Sync((int)objects[_idx_cur++], (BuildingTypes)objects[_idx_cur++], (PlayerTypes)objects[_idx_cur++]);

                //foreach (var item_0 in _ents.EnvironmentEs.Keys)
                //{
                //    _ents.EnvironmentEs.Environment(item_0, idx_0).Resources.Amount = (int)objects[_idx_cur++];
                //}

                _ents.CellEs(idx_0).RiverEs.RiverE.RiverTC.River = (RiverTypes)objects[_idx_cur++];
                foreach (var dir in _ents.CellEs(idx_0).RiverEs.Keys)
                    _ents.CellEs(idx_0).RiverEs.HaveRive(dir).HaveRiver.Have = (bool)objects[_idx_cur++];



                //foreach (var item_0 in _ents.CellEs(idx_0).TrailEs.Keys)
                //    _ents.TrailEs(idx_0).Trail(item_0).Sync((int)objects[_idx_cur++]);



                _ents.CellEs(idx_0).EffectEs.FireE.SyncRpc((bool)objects[_idx_cur++]);
            }


            //_ents.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.First).SyncRpc((int)objects[_idx_cur++]);
            //_ents.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.Second).SyncRpc((int)objects[_idx_cur++]);
            //_ents.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.First).SyncRpc((int)objects[_idx_cur++]);
            //_ents.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.Second).SyncRpc((int)objects[_idx_cur++]);



            //foreach (var key in _ents.UnitStatUpgradesEs.Keys) _ents.UnitStatUpgradesEs.Upgrade(key).HaveUpgrade.Have = (bool)objects[_idx_cur++];
            //foreach (var key in BuildingUpgradesEnt.Keys) BuildingUpgradesEnt.Upgrade<HaveUpgradeC>(key).Have = (bool)objects[_idx_cur++];


            //foreach (var key in _ents.InventorResourcesEs.Keys) _ents.InventorResourcesEs.Resource(key).Set((int)objects[_idx_cur++]);
            //foreach (var key in _ents.InventorUnitsEs.Keys) _ents.Units(key).Sync((int)objects[_idx_cur++]);
            foreach (var key in _ents.InventorToolWeaponEs.Keys) _ents.InventorToolWeaponEs.ToolWeapons(key).ToolWeaponsC.Amount = (int)objects[_idx_cur++];


            //foreach (var key in _ents.WhereUnitsEs.Keys) _ents.WhereUnitsEs.WhereUnit(key).HaveUnit.Have = (bool)objects[_idx_cur++];
            //foreach (var key in _ents.WhereBuildingEs.Keys) _ents.WhereBuildingEs.HaveBuild(key).HaveBuilding.Have = (bool)objects[_idx_cur++];
            //foreach (var key in _ents.WhereEnviromentEs.Keys) _ents.WhereEnviromentEs.Info(key).HaveEnv.Have = (bool)objects[_idx_cur++];


            //foreach (var item in PickUpgC.HaveUpgrades) PickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in UnitAvailPickUpgC.Available_0) UnitAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in BuildAvailPickUpgC.Available) BuildAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in WaterAvailPickUpgC.Available) WaterAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);


            #region Other

            _ents.WhoseMovePlayerTC.Player = (PlayerTypes)objects[_idx_cur++];
            _ents.WinnerC.Player = (PlayerTypes)objects[_idx_cur++];
            _ents.IsStartedGameC.Is = (bool)objects[_idx_cur++];
            _ents.ReadyE(_ents.WhoseMovePlayerTC.CurPlayerI).IsReadyC.IsReady = (bool)objects[_idx_cur++];


            //_ents.Motion.AmountMotionsC.Amount = (int)objects[_idx_cur++];

            _ents.CenterCloudIdxC.Set((byte)objects[_idx_cur++]);
            //foreach (var item in WindC.Directs) WindC.Sync(item.Key, (byte)objects[_idx_cur++]);
            //WindC.Sync((DirectTypes)objects[_idx_cur++]);

            #endregion
        }


        [PunRPC]
        void UpdateDataAndView(object[] objects)
        {
            _systems.Run(DataSTypes.RunAfterSyncRPC);
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