using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Game.Game.CellUnitEs;
using static Game.Game.CellBuildE;
using static Game.Game.EntityCellCloudPool;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellFireEs;
using static Game.Game.CellEs;
using static Game.Game.EntityCellRiverPool;
using static Game.Game.CellTrailEs;
using static Game.Game.EntitySound;
using static Game.Game.CellUnitTWE;

namespace Game.Game
{
    public sealed class RpcS : MonoBehaviour
    {
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


        [PunRPC]
        void MasterRPC(object[] objects, PhotonMessageInfo infoFrom)
        {
            _idx_cur = 0;

            var rpcT = (RpcMasterTypes)objects[_idx_cur++];

            InfoC.AddInfo(MGOTypes.Master, infoFrom);

            if (rpcT == RpcMasterTypes.UniqAbil)
            {
                var uniqAbil = (UniqueAbilityTypes)objects[_idx_cur++];

                switch (uniqAbil)
                {
                    case UniqueAbilityTypes.None: throw new Exception();

                    case UniqueAbilityTypes.CircularAttack:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case UniqueAbilityTypes.BonusNear:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case UniqueAbilityTypes.FirePawn:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case UniqueAbilityTypes.PutOutFirePawn:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case UniqueAbilityTypes.Seed:
                        EntityMPool.Seed<IdxC>().Idx = (byte)objects[_idx_cur++];
                        EntityMPool.Seed<EnvironmetC>().Environment = (EnvironmentTypes)objects[_idx_cur++];
                        break;

                    case UniqueAbilityTypes.FireArcher:
                        EntityMPool.FireArcher<IdxFromToC>().From = (byte)objects[_idx_cur++];
                        EntityMPool.FireArcher<IdxFromToC>().To = (byte)objects[_idx_cur++];
                        break;

                    case UniqueAbilityTypes.GrowAdultForest:
                        EntityMPool.GrowAdultForest<IdxC>().Idx = (byte)objects[_idx_cur++];
                        break;

                    case UniqueAbilityTypes.StunElfemale:
                        FromToDoingMC.Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case UniqueAbilityTypes.ChangeDirectionWind:
                        FromToDoingMC.Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case UniqueAbilityTypes.ChangeCornerArcher:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    default: throw new Exception();
                }

                UniqueAbilityMC.Set(uniqAbil);
                SystemDataMasterManager.InvokeRun(uniqAbil);
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
                        EntityMPool.Build<IdxC>().Idx = (byte)objects[_idx_cur++];
                        EntityMPool.Build<BuildingTC>().Build = (BuildingTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.DestroyBuild:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.Shift:
                        EntityMPool.Shift<IdxFromToC>().Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.Attack:
                        EntityMPool.Attack<IdxFromToC>().From = (byte)objects[_idx_cur++];
                        EntityMPool.Attack<IdxFromToC>().To = (byte)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.ConditionUnit:
                        EntityMPool.ConditionUnit<ConditionUnitC>().Condition = (ConditionUnitTypes)objects[_idx_cur++];
                        EntityMPool.ConditionUnit<IdxC>().Idx = (byte)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.CreateUnit:
                        EntityMPool.CreateUnit<UnitTC>().Unit = (UnitTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.MeltOre:
                        break;

                    case RpcMasterTypes.SetUnit:
                        EntityMPool.SetUnit<IdxC>().Idx = (byte)objects[_idx_cur++];
                        EntityMPool.SetUnit<UnitTC>().Unit = (UnitTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.BuyRes:
                        EntityMPool.BuyResources<ResourceTypeC>().Resource = (ResTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.ToNewUnit:
                        UnitDoingMC.Set((UnitTypes)objects[_idx_cur++]);
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.CreateHeroFromTo:
                        EntityMPool.CreateHeroFromTo<UnitTC>().Unit = (UnitTypes)objects[_idx_cur++];
                        EntityMPool.CreateHeroFromTo<IdxFromToC>().Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.UpgradeCellUnit:
                        EntityMPool.UpgradeUnit<IdxC>().Idx = (byte)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.GiveTakeToolWeapon:
                        EntityMPool.GiveTakeToolWeapon<ToolWeaponC>().ToolWeapon = (ToolWeaponTypes)objects[_idx_cur++];
                        EntityMPool.GiveTakeToolWeapon<LevelTC>().Level = (LevelTypes)objects[_idx_cur++];
                        EntityMPool.GiveTakeToolWeapon<IdxC>().Idx = (byte)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.GetHero:
                        EntityMPool.GetHero<UnitTC>().Unit = (UnitTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.UpgUnits:
                        UnitDoingMC.Set((UnitTypes)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.UpgCenterBuild:
                        EntityMPool.Build<BuildingTC>().Build = (BuildingTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.UpgWater:
                        break;

                    default:
                        throw new Exception();
                }

                SystemDataMasterManager.InvokeRun(rpcT);
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
                    //var mistakeType = (MistakeTypes)objects[_idx_cur++];
                    //EntMistakeC.Mistake<MistakeC>().Mistake = mistakeType;
                    //EntMistakeC.Mistake<TimerC>().Reset();

                    //if (mistakeType == MistakeTypes.Economy)
                    //{
                    //    EntMistakeC.Mistake<NeedResourcesC>(ResTypes.Food).Resources = default;
                    //    EntMistakeC.Mistake<NeedResourcesC>(ResTypes.Wood).Resources = default;
                    //    EntMistakeC.Mistake<NeedResourcesC>(ResTypes.Ore).Resources = default;
                    //    EntMistakeC.Mistake<NeedResourcesC>(ResTypes.Iron).Resources = default;
                    //    EntMistakeC.Mistake<NeedResourcesC>(ResTypes.Gold).Resources = default;

                    //    var needRes = (int[])objects[_idx_cur++];

                    //    EntMistakeC.Mistake<NeedResourcesC>(ResTypes.Food).Resources = needRes[0];
                    //    EntMistakeC.Mistake<NeedResourcesC>(ResTypes.Wood).Resources = needRes[1];
                    //    EntMistakeC.Mistake<NeedResourcesC>(ResTypes.Ore).Resources = needRes[2];
                    //    EntMistakeC.Mistake<NeedResourcesC>(ResTypes.Iron).Resources = needRes[3];
                    //    EntMistakeC.Mistake<NeedResourcesC>(ResTypes.Gold).Resources = needRes[4];
                    //}

                    Sound<ActionC>(ClipTypes.Mistake).Invoke();
                    break;

                case RpcGeneralTypes.SoundEff:
                    Sound<ActionC>((ClipTypes)objects[_idx_cur++]).Invoke();
                    break;

                case RpcGeneralTypes.SoundUniq:
                    Sound<ActionC>((UniqueAbilityTypes)objects[_idx_cur++]).Invoke();
                    break;

                case RpcGeneralTypes.ActiveMotion:
                    EntityPool.MotionZone<IsActiveC>().IsActive = true;
                    break;

                default:
                    throw new Exception();
            }
        }

        [PunRPC]
        void OtherRpc(object[] objects, PhotonMessageInfo infoFrom) => EntityPool.Rpc<RpcC>().OtherRpc(objects, infoFrom);


        #region SyncData

        public static void SyncAllMaster()
        {
            var objs = new List<object>();


            foreach (byte idx_0 in Idxs)
            {
                objs.Add(Unit<UnitTC>(idx_0).Unit);
                objs.Add(Unit<LevelTC>(idx_0).Level);
                objs.Add(Unit<PlayerTC>(idx_0).Player);

                objs.Add(CellUnitHpEs.Hp<AmountC>(idx_0).Amount);
                objs.Add(CellUnitStepEs.Steps<AmountC>(idx_0).Amount);
                objs.Add(CellUnitWaterEs.Water<AmountC>(idx_0).Amount);

                objs.Add(Unit<ConditionUnitC>(idx_0).Condition);
                foreach (var item in KeysStat) objs.Add(Unit<HaveEffectC>(item, idx_0).Have);


                objs.Add(UnitTW<ToolWeaponC>(idx_0).ToolWeapon);
                objs.Add(UnitTW<LevelTC>(idx_0).Level);
                objs.Add(UnitTW<ProtectionC>(idx_0).Protection);

                objs.Add(CellUnitStunEs.StepsForExitStun<AmountC>(idx_0).Amount);

                objs.Add(Unit<IsCornedArcherC>(idx_0).IsCornered);

                foreach (var item in CellUnitAbilityUniqueEs.Keys) objs.Add(CellUnitAbilityUniqueEs.Cooldown<CooldownC>(item, idx_0).Cooldown);





                objs.Add(Build<BuildingTC>(idx_0).Build);
                objs.Add(Build<PlayerTC>(idx_0).Player);



                foreach (var env in Enviroments)
                {
                    objs.Add(Environment<HaveEnvironmentC>(env, idx_0));
                    objs.Add(Environment<AmountC>(env, idx_0));
                }




                objs.Add(River<RiverC>(idx_0).River);
                foreach (var item_0 in River<RiverC>(idx_0).DirectsDict)
                    objs.Add(item_0.Value);


                foreach (var item_0 in Trail<TrailCellEC>(idx_0).Health)
                    objs.Add(item_0.Value);


                objs.Add(Cloud<HaveEffectC>(idx_0).Have);


                objs.Add(Fire<HaveEffectC>(idx_0).Have);



            }

            objs.Add(EntityPool.ScoutHeroCooldown<CooldownC>(UnitTypes.Scout, PlayerTypes.First));
            objs.Add(EntityPool.ScoutHeroCooldown<CooldownC>(UnitTypes.Scout, PlayerTypes.Second));
            objs.Add(EntityPool.ScoutHeroCooldown<CooldownC>(UnitTypes.Elfemale, PlayerTypes.First));
            objs.Add(EntityPool.ScoutHeroCooldown<CooldownC>(UnitTypes.Elfemale, PlayerTypes.Second));



            foreach (var key in StatUnitsUpgradesE.Keys) objs.Add(StatUnitsUpgradesE.Upgrade<HaveUpgradeC>(key).Have);
            //foreach (var key in BuildingUpgradesEnt.Keys) objs.Add(BuildingUpgradesEnt.Upgrade<HaveUpgradeC>(key).Have);


            foreach (var key in InventorResourcesE.Keys) objs.Add(InventorResourcesE.Resource<AmountC>(key).Amount);
            foreach (var key in EntInventorUnits.Keys) objs.Add(EntInventorUnits.Units<AmountC>(key).Amount);
            foreach (var key in InventorToolWeaponE.Keys) objs.Add(InventorToolWeaponE.ToolWeapons<AmountC>(key).Amount);


            foreach (var key in EntWhereUnits.Keys) objs.Add(EntWhereUnits.HaveUnit<HaveUnitC>(key).Have);
            foreach (var key in WhereBuildsE.Keys) objs.Add(WhereBuildsE.HaveBuild<HaveBuildingC>(key).Have);
            foreach (var key in EntWhereEnviroments.Keys) objs.Add(EntWhereEnviroments.HaveEnv<HaveEnvC>(key).Have);


            //foreach (var item in PickUpgC.HaveUpgrades) objs.Add(item.Value);
            //foreach (var item in UnitAvailPickUpgC.Available_0) objs.Add(item.Value);
            //foreach (var item in BuildAvailPickUpgC.Available) objs.Add(item.Value);
            //foreach (var item in WaterAvailPickUpgC.Available) objs.Add(item.Value);


            #region Other

            objs.Add(WhoseMoveE.WhoseMove<PlayerTC>().Player);
            objs.Add(EntityPool.Winner<PlayerTC>().Player);
            objs.Add(EntityPool.GameInfo<IsStartedGameC>().IsStartedGame);
            objs.Add(EntityPool.Ready<IsReadyC>(PlayerTypes.Second).IsReady);

            objs.Add(EntityPool.GameInfo<AmountMotionsC>().Amount);

            objs.Add(CenterCloudEnt.CenterCloud<IdxC>().Idx);
            //foreach (var item in WindC.Directs) objs.Add(item.Value);
            //objs.Add(WindC.CurDirWind);

            #endregion


            var objects = new object[objs.Count];
            for (int i = 0; i < objects.Length; i++) objects[i] = objs[i];


            EntityPool.Rpc<RpcC>().RPC(nameof(SyncAllOther), RpcTarget.Others, objects);

            EntityPool.Rpc<RpcC>().RPC(nameof(UpdateDataAndView), RpcTarget.All, new object[] { });
        }

        [PunRPC]
        void SyncAllOther(object[] objects)
        {
            _idx_cur = 0;


            foreach (byte idx_0 in Idxs)
            {
                Unit<UnitCellEC>(idx_0).Sync((UnitTypes)objects[_idx_cur++], (LevelTypes)objects[_idx_cur++], (PlayerTypes)objects[_idx_cur++], (int)objects[_idx_cur++], (int)objects[_idx_cur++], (int)objects[_idx_cur++]);

                Unit<ConditionUnitC>(idx_0).Condition = (ConditionUnitTypes)objects[_idx_cur++];
                foreach (var item in KeysStat) Unit<HaveEffectC>(item, idx_0).Have = (bool)objects[_idx_cur++];

                UnitTW<UnitTWCellEC>(idx_0).Sync((ToolWeaponTypes)objects[_idx_cur++], (LevelTypes)objects[_idx_cur++], (int)objects[_idx_cur++]);


                CellUnitStunEs.StepsForExitStun<AmountC>(idx_0).Amount = (int)objects[_idx_cur++];

                Unit<IsCornedArcherC>(idx_0).IsCornered = (bool)objects[_idx_cur++];

                foreach (var item in CellUnitAbilityUniqueEs.Keys) CellUnitAbilityUniqueEs.Cooldown<CooldownC>(item, idx_0).Cooldown = (int)objects[_idx_cur++];



                Build<BuildingTC>(idx_0).Build = (BuildingTypes)objects[_idx_cur++];
                Build<PlayerTC>(idx_0).Player = (PlayerTypes)objects[_idx_cur++];


                foreach (var item_0 in Enviroments)
                {
                    Environment<HaveEnvironmentC>(item_0, idx_0).Have = (bool)objects[_idx_cur++];
                    Environment<AmountC>(item_0, idx_0).Amount = (int)objects[_idx_cur++];
                }

                ref var river_0 = ref River<RiverC>(idx_0);
                river_0.Sync((RiverTypes)objects[_idx_cur++]);
                foreach (var item_0 in river_0.DirectsDict)
                    river_0.Sync(item_0.Key, (bool)objects[_idx_cur++]);



                ref var trail_0 = ref Trail<TrailCellEC>(idx_0);
                foreach (var item_0 in trail_0.Health)
                    trail_0.SyncTrail(item_0.Key, (int)objects[_idx_cur++]);



                Cloud<HaveEffectC>(idx_0).Sync((bool)objects[_idx_cur++]);
                Fire<HaveEffectC>(idx_0).Have = (bool)objects[_idx_cur++];
            }


            EntityPool.ScoutHeroCooldown<CooldownC>(UnitTypes.Scout, PlayerTypes.First).Cooldown = (int)objects[_idx_cur++];
            EntityPool.ScoutHeroCooldown<CooldownC>(UnitTypes.Scout, PlayerTypes.Second).Cooldown = (int)objects[_idx_cur++];
            EntityPool.ScoutHeroCooldown<CooldownC>(UnitTypes.Elfemale, PlayerTypes.First).Cooldown = (int)objects[_idx_cur++];
            EntityPool.ScoutHeroCooldown<CooldownC>(UnitTypes.Elfemale, PlayerTypes.Second).Cooldown = (int)objects[_idx_cur++];



            foreach (var key in StatUnitsUpgradesE.Keys) StatUnitsUpgradesE.Upgrade<HaveUpgradeC>(key).Have = (bool)objects[_idx_cur++];
            //foreach (var key in BuildingUpgradesEnt.Keys) BuildingUpgradesEnt.Upgrade<HaveUpgradeC>(key).Have = (bool)objects[_idx_cur++];


            foreach (var key in InventorResourcesE.Keys) InventorResourcesE.Resource<AmountC>(key).Amount = (int)objects[_idx_cur++];
            foreach (var key in EntInventorUnits.Keys) EntInventorUnits.Units<AmountC>(key).Amount = (int)objects[_idx_cur++];
            foreach (var key in InventorToolWeaponE.Keys) InventorToolWeaponE.ToolWeapons<AmountC>(key).Amount = (int)objects[_idx_cur++];


            foreach (var key in EntWhereUnits.Keys) EntWhereUnits.HaveUnit<HaveUnitC>(key).Have = (bool)objects[_idx_cur++];
            foreach (var key in WhereBuildsE.Keys) WhereBuildsE.HaveBuild<HaveBuildingC>(key).Have = (bool)objects[_idx_cur++];
            foreach (var key in EntWhereEnviroments.Keys) EntWhereEnviroments.HaveEnv<HaveEnvC>(key).Have = (bool)objects[_idx_cur++];


            //foreach (var item in PickUpgC.HaveUpgrades) PickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in UnitAvailPickUpgC.Available_0) UnitAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in BuildAvailPickUpgC.Available) BuildAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in WaterAvailPickUpgC.Available) WaterAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);


            #region Other

            WhoseMoveE.WhoseMove<PlayerTC>().Player = (PlayerTypes)objects[_idx_cur++];
            EntityPool.Winner<PlayerTC>().Player = (PlayerTypes)objects[_idx_cur++];
            EntityPool.GameInfo<IsStartedGameC>().IsStartedGame = (bool)objects[_idx_cur++];
            EntityPool.Ready<IsReadyC>(WhoseMoveE.CurPlayerI).IsReady = (bool)objects[_idx_cur++];


            EntityPool.GameInfo<AmountMotionsC>().Amount = (int)objects[_idx_cur++];

            CenterCloudEnt.CenterCloud<IdxC>().Idx = (byte)objects[_idx_cur++];
            //foreach (var item in WindC.Directs) WindC.Sync(item.Key, (byte)objects[_idx_cur++]);
            //WindC.Sync((DirectTypes)objects[_idx_cur++]);

            #endregion
        }


        [PunRPC]
        void UpdateDataAndView(object[] objects)
        {
            SystemDataManager.Run(DataSTypes.RunAfterSyncRPC);
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