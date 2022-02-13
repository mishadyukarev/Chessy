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
                switch (rpcT)
                {
                    case RpcMasterTypes.None:
                        throw new Exception();

                    case RpcMasterTypes.Ready:
                        _ents.Ready(sender.GetPlayer()).Ready_Master(sender, _ents);
                        break;

                    case RpcMasterTypes.Done:
                        if (_ents.WhoseMoveE.Done_Master(sender, _ents))
                        {
                            _systems.SystemsMaster.InvokeRun(SystemDataMasterTypes.UpdateMove);
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

                    case RpcMasterTypes.UpgradeCellUnit:
                        _ents.UnitEs((byte)objects[_idx_cur++]).UnitE.UpgradeUnit_Master(sender, _ents);
                        break;

                    case RpcMasterTypes.GiveTakeToolWeapon:
                        var idx_0 = (byte)objects[_idx_cur++];
                        var twT = (ToolWeaponTypes)objects[_idx_cur++];
                        var levT = (LevelTypes)objects[_idx_cur++];
                        if (twT == ToolWeaponTypes.Axe || twT == ToolWeaponTypes.BowCrossbow)
                        {
                            _ents.MainTWE(idx_0).GiveTake_Master(twT, levT, sender, _ents);
                        }
                        else
                        {
                            _ents.ExtraTWE(idx_0).GiveTakeTW_Master(twT, levT, sender, _ents);
                        }
                            
                        break;

                    case RpcMasterTypes.GetHero:
                        _ents.InventorUnitsEs.GetHero_Master((UnitTypes)objects[_idx_cur++], _ents);
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
            var rpcT = (RpcGeneralTypes)objects[_idx_cur++];

            switch (rpcT)
            {
                case RpcGeneralTypes.None:
                    throw new Exception();

                case RpcGeneralTypes.Mistake:
                    var mistakeT = (MistakeTypes)objects[_idx_cur++];
                    MistakeE.Mistake<MistakeC>().Mistake = mistakeT;
                    MistakeE.Mistake<TimerC>().Reset();

                    if (mistakeT == MistakeTypes.Economy)
                    {
                        MistakeE.Mistake(ResourceTypes.Food).Amount = default;
                        MistakeE.Mistake(ResourceTypes.Wood).Amount = default;
                        MistakeE.Mistake(ResourceTypes.Ore).Amount = default;
                        MistakeE.Mistake(ResourceTypes.Iron).Amount = default;
                        MistakeE.Mistake(ResourceTypes.Gold).Amount = default;

                        var needRes = (int[])objects[_idx_cur++];

                        MistakeE.Mistake(ResourceTypes.Food).Amount = needRes[0];
                        MistakeE.Mistake(ResourceTypes.Wood).Amount = needRes[1];
                        MistakeE.Mistake(ResourceTypes.Ore).Amount = needRes[2];
                        MistakeE.Mistake(ResourceTypes.Iron).Amount = needRes[3];
                        MistakeE.Mistake(ResourceTypes.Gold).Amount = needRes[4];
                    }

                    _ents.Sound(ClipTypes.Mistake).Sound.Invoke();
                    break;

                case RpcGeneralTypes.SoundEff:
                    _ents.Sound((ClipTypes)objects[_idx_cur++]).Sound.Invoke();
                    break;

                case RpcGeneralTypes.SoundUniqueAbility:
                    _ents.Sound((AbilityTypes)objects[_idx_cur++]).Sound.Invoke();
                    break;

                case RpcGeneralTypes.SoundRpcMaster:
                    //Sound((UniqueAbilityTypes)objects[_idx_cur++]).Invoke();
                    break;

                case RpcGeneralTypes.ActiveMotion:
                    _ents.Motion.IsActiveC.IsActive = true;
                    break;

                default:
                    throw new Exception();
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
                objs.Add(_ents.UnitE(idx_0).Unit);
                //objs.Add(_ents.CellEs(idx_0).UnitEs.MainE.LevelTC.Level);
                objs.Add(_ents.UnitE(idx_0).Owner);

                objs.Add(_ents.CellEs(idx_0).UnitEs.UnitE.Health);
                objs.Add(_ents.CellEs(idx_0).UnitEs.UnitE.Steps);
                objs.Add(_ents.CellEs(idx_0).UnitEs.UnitE.Water);

                objs.Add(_ents.CellEs(idx_0).UnitEs.UnitE.Condition);
                //foreach (var item in CellUnitEffectsEs.Keys) objs.Add(CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Have);


                objs.Add(_ents.CellEs(idx_0).UnitEs.ExtraToolWeaponE.ToolWeapon);
                objs.Add(_ents.CellEs(idx_0).UnitEs.ExtraToolWeaponE.LevelT);
                objs.Add(_ents.CellEs(idx_0).UnitEs.ExtraToolWeaponE.Protection);

                objs.Add(_ents.UnitE(idx_0).Stun);

                objs.Add(_ents.CellEs(idx_0).UnitEs.UnitE.IsRightArcher);

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

            objs.Add(_ents.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.First).Cooldown.Amount);
            objs.Add(_ents.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.Second).Cooldown.Amount);
            objs.Add(_ents.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.First).Cooldown.Amount);
            objs.Add(_ents.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.Second).Cooldown.Amount);



            foreach (var key in _ents.UnitStatUpgradesEs.Keys) objs.Add(_ents.UnitStatUpgradesEs.Upgrade(key).HaveUpgrade.Have);
            //foreach (var key in BuildingUpgradesEnt.Keys) objs.Add(BuildingUpgradesEnt.Upgrade<HaveUpgradeC>(key).Have);


            foreach (var key in _ents.InventorResourcesEs.Keys) objs.Add(_ents.InventorResourcesEs.Resource(key).Resources);
            foreach (var key in _ents.InventorUnitsEs.Keys) objs.Add(_ents.InventorUnitsEs.Units(key).Units.Amount);
            foreach (var key in _ents.InventorToolWeaponEs.Keys) objs.Add(_ents.InventorToolWeaponEs.ToolWeapons(key).ToolWeapons);


            //foreach (var key in _ents.WhereUnitsEs.Keys) objs.Add(_ents.WhereUnitsEs.WhereUnit(key).HaveUnit.Have);
            //foreach (var key in _ents.WhereBuildingEs.Keys) objs.Add(_ents.WhereBuildingEs.HaveBuild(key).HaveBuilding.Have);
            //foreach (var key in _ents.WhereEnviromentEs.Keys) objs.Add(_ents.WhereEnviromentEs.Info(key).HaveEnv.Have);


            //foreach (var item in PickUpgC.HaveUpgrades) objs.Add(item.Value);
            //foreach (var item in UnitAvailPickUpgC.Available_0) objs.Add(item.Value);
            //foreach (var item in BuildAvailPickUpgC.Available) objs.Add(item.Value);
            //foreach (var item in WaterAvailPickUpgC.Available) objs.Add(item.Value);


            #region Other

            objs.Add(_ents.WhoseMoveE.WhoseMove.Player);
            objs.Add(_ents.WinnerE.Winner.Player);
            objs.Add(_ents.GameInfoE.IsStartedGameC.IsStartedGame);
            objs.Add(_ents.Ready(PlayerTypes.Second).IsReadyC.IsReady);

            objs.Add(_ents.Motion.AmountMotionsC.Amount);

            objs.Add(_ents.WindCloudE.CenterCloud.Idx);
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



                foreach (var item_0 in _ents.CellEs(idx_0).TrailEs.Keys)
                    _ents.TrailEs(idx_0).Trail(item_0).Sync((int)objects[_idx_cur++]);



                _ents.CellEs(idx_0).EffectEs.FireE.SyncRpc((bool)objects[_idx_cur++]);
            }


            _ents.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.First).SyncRpc((int)objects[_idx_cur++]);
            _ents.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.Second).SyncRpc((int)objects[_idx_cur++]);
            _ents.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.First).SyncRpc((int)objects[_idx_cur++]);
            _ents.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.Second).SyncRpc((int)objects[_idx_cur++]);



            foreach (var key in _ents.UnitStatUpgradesEs.Keys) _ents.UnitStatUpgradesEs.Upgrade(key).HaveUpgrade.Have = (bool)objects[_idx_cur++];
            //foreach (var key in BuildingUpgradesEnt.Keys) BuildingUpgradesEnt.Upgrade<HaveUpgradeC>(key).Have = (bool)objects[_idx_cur++];


            foreach (var key in _ents.InventorResourcesEs.Keys) _ents.InventorResourcesEs.Resource(key).Set((int)objects[_idx_cur++]);
            foreach (var key in _ents.InventorUnitsEs.Keys) _ents.InventorUnitsEs.Units(key).Sync((int)objects[_idx_cur++]);
            foreach (var key in _ents.InventorToolWeaponEs.Keys) _ents.InventorToolWeaponEs.ToolWeapons(key).ToolWeapons = (int)objects[_idx_cur++];


            //foreach (var key in _ents.WhereUnitsEs.Keys) _ents.WhereUnitsEs.WhereUnit(key).HaveUnit.Have = (bool)objects[_idx_cur++];
            //foreach (var key in _ents.WhereBuildingEs.Keys) _ents.WhereBuildingEs.HaveBuild(key).HaveBuilding.Have = (bool)objects[_idx_cur++];
            //foreach (var key in _ents.WhereEnviromentEs.Keys) _ents.WhereEnviromentEs.Info(key).HaveEnv.Have = (bool)objects[_idx_cur++];


            //foreach (var item in PickUpgC.HaveUpgrades) PickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in UnitAvailPickUpgC.Available_0) UnitAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in BuildAvailPickUpgC.Available) BuildAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in WaterAvailPickUpgC.Available) WaterAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);


            #region Other

            _ents.WhoseMoveE.WhoseMove.Player = (PlayerTypes)objects[_idx_cur++];
            _ents.WinnerE.Winner.Player = (PlayerTypes)objects[_idx_cur++];
            _ents.GameInfoE.IsStartedGameC.IsStartedGame = (bool)objects[_idx_cur++];
            _ents.Ready(_ents.WhoseMoveE.CurPlayerI).IsReadyC.IsReady = (bool)objects[_idx_cur++];


            //_ents.Motion.AmountMotionsC.Amount = (int)objects[_idx_cur++];

            _ents.WindCloudE.CenterCloud.Idx = (byte)objects[_idx_cur++];
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