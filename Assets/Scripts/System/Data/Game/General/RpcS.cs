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

            var rpcT = (RpcMasterTypes)objects[_idx_cur++];

            InfoC.AddInfo(MGOTypes.Master, infoFrom);

            if (rpcT == RpcMasterTypes.UniqueAbility)
            {
                var uniqAbil = (AbilityTypes)objects[_idx_cur++];

                switch (uniqAbil)
                {
                    case AbilityTypes.None: throw new Exception();

                    case AbilityTypes.CircularAttack:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case AbilityTypes.BonusNear:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case AbilityTypes.FirePawn:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case AbilityTypes.PutOutFirePawn:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case AbilityTypes.Seed:
                        _ents.MasterEs.Seed<IdxC>().Idx = (byte)objects[_idx_cur++];
                        _ents.MasterEs.Seed<EnvironmetTC>().Environment = (EnvironmentTypes)objects[_idx_cur++];
                        break;

                    case AbilityTypes.FireArcher:
                        _ents.MasterEs.FireArcher<IdxFromToC>().From = (byte)objects[_idx_cur++];
                        _ents.MasterEs.FireArcher<IdxFromToC>().To = (byte)objects[_idx_cur++];
                        break;

                    case AbilityTypes.GrowAdultForest:
                        _ents.MasterEs.GrowAdultForest<IdxC>().Idx = (byte)objects[_idx_cur++];
                        break;

                    case AbilityTypes.StunElfemale:
                        _ents.MasterEs.StunElfemale<IdxFromToC>().Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case AbilityTypes.ChangeDirectionWind:
                        _ents.MasterEs.ChangeDirectionWind<IdxFromToC>().Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case AbilityTypes.ChangeCornerArcher:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case AbilityTypes.IceWall:
                        _ents.MasterEs.IceWall.IdxC.Idx = (byte)objects[_idx_cur++];
                        break;

                    default: throw new Exception();
                }

                _ents.MasterEs.UniqueAbilityC.Ability = uniqAbil;
                _systems.SystemsMaster.InvokeRun(uniqAbil);
            }

            else
            {
                switch (rpcT)
                {
                    case RpcMasterTypes.None:
                        throw new Exception();

                    case RpcMasterTypes.Ready:
                        break;

                    case RpcMasterTypes.Done:
                        break;

                    case RpcMasterTypes.Build:
                        _ents.MasterEs.Build<IdxC>().Idx = (byte)objects[_idx_cur++];
                        _ents.MasterEs.Build<BuildingTC>().Build = (BuildingTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.DestroyBuild:
                        _ents.MasterEs.DestroyIdxC.Idx = (byte)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.Shift:
                        _ents.MasterEs.Shift<IdxFromToC>().Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.Attack:
                        _ents.MasterEs.Attack.From = (byte)objects[_idx_cur++];
                        _ents.MasterEs.Attack.To = (byte)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.ConditionUnit:
                        _ents.MasterEs.ConditionUnit<ConditionUnitC>().Condition = (ConditionUnitTypes)objects[_idx_cur++];
                        _ents.MasterEs.ConditionUnit<IdxC>().Idx = (byte)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.CreateUnit:
                        _ents.MasterEs.CreateUnit<UnitTC>().Unit = (UnitTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.MeltOre:
                        break;

                    case RpcMasterTypes.SetUnit:
                        _ents.MasterEs.SetUnit<IdxC>().Idx = (byte)objects[_idx_cur++];
                        _ents.MasterEs.SetUnit<UnitTC>().Unit = (UnitTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.BuyRes:
                        _ents.MasterEs.BuyResources<ResourceTC>().Resource = (ResourceTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.ToNewUnit:
                        _ents.MasterEs.ScoutOldNew<UnitTC>().Unit = (UnitTypes)objects[_idx_cur++];
                        _ents.MasterEs.ScoutOldNew<IdxC>().Idx = (byte)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.CreateHeroFromTo:
                        _ents.MasterEs.CreateHeroFromTo<UnitTC>().Unit = (UnitTypes)objects[_idx_cur++];
                        _ents.MasterEs.CreateHeroFromTo<IdxFromToC>().Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.UpgradeCellUnit:
                        _ents.MasterEs.UpgradeUnit<IdxC>().Idx = (byte)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.GiveTakeToolWeapon:
                        _ents.MasterEs.GiveTakeToolWeapon<ToolWeaponTC>().ToolWeapon = (ToolWeaponTypes)objects[_idx_cur++];
                        _ents.MasterEs.GiveTakeToolWeapon<LevelTC>().Level = (LevelTypes)objects[_idx_cur++];
                        _ents.MasterEs.GiveTakeToolWeapon<IdxC>().Idx = (byte)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.GetHero:
                        _ents.MasterEs.ForGetHero.Unit = (UnitTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.UpgCenterUnits:
                        _ents.MasterEs.UpgradeCenterUnit<UnitTC>().Unit = (UnitTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.UpgCenterBuild:
                        _ents.MasterEs.Build<BuildingTC>().Build = (BuildingTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.UpgWater:
                        break;

                    default:
                        throw new Exception();
                }

                _systems.SystemsMaster.InvokeRun(rpcT);
            }

            SyncAllMaster();
        }

        [PunRPC]
        void GeneralRpc(object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;
            var rpcT = (RpcGeneralTypes)objects[_idx_cur++];

            InfoC.AddInfo(MGOTypes.General, infoFrom);

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
        void OtherRpc(object[] objects, PhotonMessageInfo infoFrom) => _ents.Rpc.OtherRpc(objects, infoFrom);


        #region SyncData

        public static void SyncAllMaster()
        {
            var objs = new List<object>();


            foreach (byte idx_0 in _ents.CellEs.Idxs)
            {
                objs.Add(_ents.CellEs.UnitEs.Main(idx_0).UnitTC.Unit);
                objs.Add(_ents.CellEs.UnitEs.Main(idx_0).LevelTC.Level);
                objs.Add(_ents.CellEs.UnitEs.Main(idx_0).OwnerC.Player);

                objs.Add(_ents.CellEs.UnitEs.StatEs.Hp(idx_0).Health.Amount);
                objs.Add(_ents.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Amount);
                objs.Add(_ents.CellEs.UnitEs.StatEs.Water(idx_0).Water.Amount);

                objs.Add(_ents.CellEs.UnitEs.Main(idx_0).ConditionTC.Condition);
                //foreach (var item in CellUnitEffectsEs.Keys) objs.Add(CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Have);


                objs.Add(_ents.CellEs.UnitEs.ToolWeapon(idx_0).ToolWeaponTC.ToolWeapon);
                objs.Add(_ents.CellEs.UnitEs.ToolWeapon(idx_0).LevelTC.Level);
                objs.Add(_ents.CellEs.UnitEs.ToolWeapon(idx_0).Protection.Amount);

                objs.Add(_ents.CellEs.UnitEs.Stun(idx_0).ForExitStun.Amount);

                objs.Add(_ents.CellEs.UnitEs.Main(idx_0).IsCorned.Is);

                foreach (var item in _ents.CellEs.UnitEs.CooldownKeys) objs.Add(_ents.CellEs.UnitEs.CooldownAbility(item, idx_0).Cooldown.Amount);





                objs.Add(_ents.CellEs.BuildEs.BuildingE(idx_0).BuildTC.Build);
                objs.Add(_ents.CellEs.BuildEs.BuildingE(idx_0).Owner.Player);



                //foreach (var env in _ents.CellEs.EnvironmentEs.Keys)
                //{
                //    objs.Add(_ents.CellEs.EnvironmentEs.Environment(env, idx_0));
                //}




                objs.Add(_ents.CellEs.RiverEs.River(idx_0).RiverTC.River);
                foreach (var item_0 in _ents.CellEs.RiverEs.Keys)
                    objs.Add(_ents.CellEs.RiverEs.HaveRive(item_0, idx_0).HaveRiver.Have);


                foreach (var item_0 in _ents.CellEs.TrailEs.Keys)
                    objs.Add(_ents.CellEs.TrailEs.Trail(item_0, idx_0));

                objs.Add(_ents.CellEs.FireEs.Fire(idx_0).Fire.Have);
            }

            objs.Add(_ents.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.First).Cooldown.Amount);
            objs.Add(_ents.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.Second).Cooldown.Amount);
            objs.Add(_ents.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.First).Cooldown.Amount);
            objs.Add(_ents.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.Second).Cooldown.Amount);



            foreach (var key in _ents.UnitStatUpgradesEs.Keys) objs.Add(_ents.UnitStatUpgradesEs.Upgrade(key).HaveUpgrade.Have);
            //foreach (var key in BuildingUpgradesEnt.Keys) objs.Add(BuildingUpgradesEnt.Upgrade<HaveUpgradeC>(key).Have);


            foreach (var key in _ents.InventorResourcesEs.Keys) objs.Add(_ents.InventorResourcesEs.Resource(key).Resources.Amount);
            foreach (var key in _ents.InventorUnitsEs.Keys) objs.Add(_ents.InventorUnitsEs.Units(key).Units.Amount);
            foreach (var key in _ents.InventorToolWeaponEs.Keys) objs.Add(_ents.InventorToolWeaponEs.ToolWeapons(key).ToolWeapons.Amount);


            foreach (var key in _ents.WhereUnitsEs.Keys) objs.Add(_ents.WhereUnitsEs.WhereUnit(key).HaveUnit.Have);
            foreach (var key in _ents.WhereBuildingEs.Keys) objs.Add(_ents.WhereBuildingEs.HaveBuild(key).HaveBuilding.Have);
            foreach (var key in _ents.WhereEnviromentEs.Keys) objs.Add(_ents.WhereEnviromentEs.Info(key).HaveEnv.Have);


            //foreach (var item in PickUpgC.HaveUpgrades) objs.Add(item.Value);
            //foreach (var item in UnitAvailPickUpgC.Available_0) objs.Add(item.Value);
            //foreach (var item in BuildAvailPickUpgC.Available) objs.Add(item.Value);
            //foreach (var item in WaterAvailPickUpgC.Available) objs.Add(item.Value);


            #region Other

            objs.Add(_ents.WhoseMove.WhoseMove.Player);
            objs.Add(_ents.WinnerE.Winner.Player);
            objs.Add(_ents.GameInfo.IsStartedGameC.IsStartedGame);
            objs.Add(_ents.Ready(PlayerTypes.Second).IsReadyC.IsReady);

            objs.Add(_ents.Motion.AmountMotions.Amount);

            objs.Add(_ents.WindE.CenterCloud.Idx);
            //foreach (var item in WindC.Directs) objs.Add(item.Value);
            //objs.Add(WindC.CurDirWind);

            #endregion


            var objects = new object[objs.Count];
            for (int i = 0; i < objects.Length; i++) objects[i] = objs[i];


            _ents.Rpc.RPC(nameof(SyncAllOther), RpcTarget.Others, objects);

            _ents.Rpc.RPC(nameof(UpdateDataAndView), RpcTarget.All, new object[] { });
        }

        [PunRPC]
        void SyncAllOther(object[] objects)
        {
            _idx_cur = 0;


            foreach (byte idx_0 in _ents.CellEs.Idxs)
            {
                //_ents.CellEs.UnitEs.Main(idx_0).UnitTC.Unit = (UnitTypes)objects[_idx_cur++];
                //_ents.CellEs.UnitEs.Main(idx_0).LevelC.Level = (LevelTypes)objects[_idx_cur++];
                //_ents.CellEs.UnitEs.Main(idx_0).OwnerC.Player = (PlayerTypes)objects[_idx_cur++];
                //_ents.CellEs.UnitEs.Main(idx_0).ConditionTC.Condition = (ConditionUnitTypes)objects[_idx_cur++];
                //_ents.CellEs.UnitEs.Main(idx_0).IsCorned.Is = (bool)objects[_idx_cur++];

                _ents.CellEs.UnitEs.StatEs.Hp(idx_0).Health.Amount = (int)objects[_idx_cur++];
                _ents.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Amount = (int)objects[_idx_cur++];
                _ents.CellEs.UnitEs.StatEs.Water(idx_0).Water.Amount = (int)objects[_idx_cur++];

               
                //foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Have = (bool)objects[_idx_cur++];


                //_ents.CellEs.UnitEs.ToolWeapon(idx_0).ToolWeaponTC.ToolWeapon = (ToolWeaponTypes)objects[_idx_cur++];
                //_ents.CellEs.UnitEs.ToolWeapon(idx_0).LevelTC.Level = (LevelTypes)objects[_idx_cur++];
                //_ents.CellEs.UnitEs.ToolWeapon(idx_0).Protection.Amount = (int)objects[_idx_cur++];


                _ents.CellEs.UnitEs.Stun(idx_0).SyncRpc((int)objects[_idx_cur++]);

                

                foreach (var item in _ents.CellEs.UnitEs.CooldownKeys) _ents.CellEs.UnitEs.CooldownAbility(item, idx_0).SyncRpc((int)objects[_idx_cur++]);



                _ents.CellEs.BuildEs.BuildingE(idx_0)
                    .Sync((int)objects[_idx_cur++], (BuildingTypes)objects[_idx_cur++], (PlayerTypes)objects[_idx_cur++]);

                //foreach (var item_0 in _ents.CellEs.EnvironmentEs.Keys)
                //{
                //    _ents.CellEs.EnvironmentEs.Environment(item_0, idx_0).Resources.Amount = (int)objects[_idx_cur++];
                //}

                _ents.CellEs.RiverEs.River(idx_0).RiverTC.River = (RiverTypes)objects[_idx_cur++];
                foreach (var dir in _ents.CellEs.RiverEs.Keys)
                    _ents.CellEs.RiverEs.HaveRive(dir, idx_0).HaveRiver.Have = (bool)objects[_idx_cur++];



                foreach (var item_0 in _ents.CellEs.TrailEs.Keys)
                    _ents.CellEs.TrailEs.Trail(item_0, idx_0).Health.Amount = (int)objects[_idx_cur++];



                _ents.CellEs.FireEs.Fire(idx_0).Fire.Have = (bool)objects[_idx_cur++];
            }


            _ents.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.First).Cooldown.Amount = (int)objects[_idx_cur++];
            _ents.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.Second).Cooldown.Amount = (int)objects[_idx_cur++];
            _ents.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.First).Cooldown.Amount = (int)objects[_idx_cur++];
            _ents.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.Second).Cooldown.Amount = (int)objects[_idx_cur++];



            foreach (var key in _ents.UnitStatUpgradesEs.Keys) _ents.UnitStatUpgradesEs.Upgrade(key).HaveUpgrade.Have = (bool)objects[_idx_cur++];
            //foreach (var key in BuildingUpgradesEnt.Keys) BuildingUpgradesEnt.Upgrade<HaveUpgradeC>(key).Have = (bool)objects[_idx_cur++];


            foreach (var key in _ents.InventorResourcesEs.Keys) _ents.InventorResourcesEs.Resource(key).Resources.Amount = (int)objects[_idx_cur++];
            foreach (var key in _ents.InventorUnitsEs.Keys) _ents.InventorUnitsEs.Units(key).Sync((int)objects[_idx_cur++]);
            foreach (var key in _ents.InventorToolWeaponEs.Keys) _ents.InventorToolWeaponEs.ToolWeapons(key).ToolWeapons.Amount = (int)objects[_idx_cur++];


            foreach (var key in _ents.WhereUnitsEs.Keys) _ents.WhereUnitsEs.WhereUnit(key).HaveUnit.Have = (bool)objects[_idx_cur++];
            foreach (var key in _ents.WhereBuildingEs.Keys) _ents.WhereBuildingEs.HaveBuild(key).HaveBuilding.Have = (bool)objects[_idx_cur++];
            foreach (var key in _ents.WhereEnviromentEs.Keys) _ents.WhereEnviromentEs.Info(key).HaveEnv.Have = (bool)objects[_idx_cur++];


            //foreach (var item in PickUpgC.HaveUpgrades) PickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in UnitAvailPickUpgC.Available_0) UnitAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in BuildAvailPickUpgC.Available) BuildAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in WaterAvailPickUpgC.Available) WaterAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);


            #region Other

            _ents.WhoseMove.WhoseMove.Player = (PlayerTypes)objects[_idx_cur++];
            _ents.WinnerE.Winner.Player = (PlayerTypes)objects[_idx_cur++];
            _ents.GameInfo.IsStartedGameC.IsStartedGame = (bool)objects[_idx_cur++];
            _ents.Ready(_ents.WhoseMove.CurPlayerI).IsReadyC.IsReady = (bool)objects[_idx_cur++];


            _ents.Motion.AmountMotions.Amount = (int)objects[_idx_cur++];

            _ents.WindE.CenterCloud.Idx = (byte)objects[_idx_cur++];
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