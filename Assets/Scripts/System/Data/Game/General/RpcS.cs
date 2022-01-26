using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellEs;
using static Game.Game.CellTrailEs;
using static Game.Game.SoundE;

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

            if (rpcT == RpcMasterTypes.UniqueAbility)
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
                        EntityMPool.StunElfemale<IdxFromToC>().Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case UniqueAbilityTypes.ChangeDirectionWind:
                        EntityMPool.ChangeDirectionWind<IdxFromToC>().Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case UniqueAbilityTypes.ChangeCornerArcher:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case UniqueAbilityTypes.FreezeDirectEnemy:
                        FreezeDirectEnemyME.IdxFromToC.Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case UniqueAbilityTypes.IceWall:
                        IceWallME.IdxC.Idx = (byte)objects[_idx_cur++];
                        break;

                    default: throw new Exception();
                }

                EntityMPool.UniqueAbilityC.Ability = uniqAbil;
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
                        EntityMPool.DestroyIdxC.Idx = (byte)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.Shift:
                        EntityMPool.Shift<IdxFromToC>().Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.Attack:
                        EntityMPool.Attack.From = (byte)objects[_idx_cur++];
                        EntityMPool.Attack.To = (byte)objects[_idx_cur++];
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
                        EntityMPool.BuyResources<ResourceTypeC>().Resource = (ResourceTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.ToNewUnit:
                        EntityMPool.ScoutOldNew<UnitTC>().Unit = (UnitTypes)objects[_idx_cur++];
                        EntityMPool.ScoutOldNew<IdxC>().Idx = (byte)objects[_idx_cur++];
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
                        EntityMPool.ForGetHero.Unit = (UnitTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.UpgCenterUnits:
                        EntityMPool.UpgradeCenterUnit<UnitTC>().Unit = (UnitTypes)objects[_idx_cur++];
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
                    var mistakeT = (MistakeTypes)objects[_idx_cur++];
                    MistakeE.Mistake<MistakeC>().Mistake = mistakeT;
                    MistakeE.Mistake<TimerC>().Reset();

                    if (mistakeT == MistakeTypes.Economy)
                    {
                        MistakeE.Mistake<AmountC>(ResourceTypes.Food).Amount = default;
                        MistakeE.Mistake<AmountC>(ResourceTypes.Wood).Amount = default;
                        MistakeE.Mistake<AmountC>(ResourceTypes.Ore).Amount = default;
                        MistakeE.Mistake<AmountC>(ResourceTypes.Iron).Amount = default;
                        MistakeE.Mistake<AmountC>(ResourceTypes.Gold).Amount = default;

                        var needRes = (int[])objects[_idx_cur++];

                        MistakeE.Mistake<AmountC>(ResourceTypes.Food).Amount = needRes[0];
                        MistakeE.Mistake<AmountC>(ResourceTypes.Wood).Amount = needRes[1];
                        MistakeE.Mistake<AmountC>(ResourceTypes.Ore).Amount = needRes[2];
                        MistakeE.Mistake<AmountC>(ResourceTypes.Iron).Amount = needRes[3];
                        MistakeE.Mistake<AmountC>(ResourceTypes.Gold).Amount = needRes[4];
                    }

                    Sound(ClipTypes.Mistake).Invoke();
                    break;

                case RpcGeneralTypes.SoundEff:
                    Sound((ClipTypes)objects[_idx_cur++]).Invoke();
                    break;

                case RpcGeneralTypes.SoundUniqueAbility:
                    Sound((UniqueAbilityTypes)objects[_idx_cur++]).Invoke();
                    break;

                case RpcGeneralTypes.SoundRpcMaster:
                    //Sound((UniqueAbilityTypes)objects[_idx_cur++]).Invoke();
                    break;

                case RpcGeneralTypes.ActiveMotion:
                    EntityPool.MotionZone<IsActiveC>().IsActive = true;
                    break;

                default:
                    throw new Exception();
            }
        }

        [PunRPC]
        void OtherRpc(object[] objects, PhotonMessageInfo infoFrom) => EntityPool.Rpc.OtherRpc(objects, infoFrom);


        #region SyncData

        public static void SyncAllMaster()
        {
            var objs = new List<object>();


            foreach (byte idx_0 in Idxs)
            {
                objs.Add(CellUnitEs.Else(idx_0).UnitC.Unit);
                objs.Add(CellUnitEs.Else(idx_0).LevelC.Level);
                objs.Add(CellUnitEs.Else(idx_0).OwnerC.Player);

                objs.Add(CellUnitEs.Hp(idx_0).AmountC.Amount);
                objs.Add(CellUnitEs.Step(idx_0).AmountC.Amount);
                objs.Add(CellUnitEs.Water(idx_0).AmountC.Amount);

                objs.Add(CellUnitEs.Else(idx_0).ConditionC.Condition);
                //foreach (var item in CellUnitEffectsEs.Keys) objs.Add(CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Have);


                objs.Add(CellUnitEs.ToolWeapon(idx_0).ToolWeaponC.ToolWeapon);
                objs.Add(CellUnitEs.ToolWeapon(idx_0).LevelC.Level);
                objs.Add(CellUnitEs.ToolWeapon(idx_0).Protection.Amount);

                objs.Add(CellUnitEs.Stun(idx_0).ForExitStun.Amount);

                objs.Add(CellUnitEs.Else(idx_0).CornedC.IsCornered);

                foreach (var item in CellUnitEs.CooldownKeys) objs.Add(CellUnitEs.CooldownUnique(item, idx_0).Cooldown.Amount);





                objs.Add(CellBuildEs.Build(idx_0).BuildTC.Build);
                objs.Add(CellBuildEs.Build(idx_0).PlayerTC.Player);



                foreach (var env in CellEnvironmentEs.KeysEnvironment)
                {
                    objs.Add(Environment(env, idx_0));
                }




                objs.Add(CellRiverEs.River(idx_0).RiverTC.River);
                foreach (var item_0 in CellRiverEs.Keys)
                    objs.Add(CellRiverEs.HaveRive(item_0, idx_0).HaveRiver.Have);


                foreach (var item_0 in CellTrailEs.Keys)
                    objs.Add(Trail(item_0, idx_0));

                objs.Add(CellFireEs.Fire(idx_0).Fire.Have);
            }

            objs.Add(EntityPool.ScoutHeroCooldown(UnitTypes.Scout, PlayerTypes.First).Amount);
            objs.Add(EntityPool.ScoutHeroCooldown(UnitTypes.Scout, PlayerTypes.Second).Amount);
            objs.Add(EntityPool.ScoutHeroCooldown(UnitTypes.Elfemale, PlayerTypes.First).Amount);
            objs.Add(EntityPool.ScoutHeroCooldown(UnitTypes.Elfemale, PlayerTypes.Second).Amount);



            foreach (var key in StatUnitsUpgradesE.Keys) objs.Add(StatUnitsUpgradesE.Upgrade<HaveUpgradeC>(key).Have);
            //foreach (var key in BuildingUpgradesEnt.Keys) objs.Add(BuildingUpgradesEnt.Upgrade<HaveUpgradeC>(key).Have);


            foreach (var key in InventorResourcesE.Keys) objs.Add(InventorResourcesE.Resource(key).Amount);
            foreach (var key in InventorUnitsE.Keys) objs.Add(InventorUnitsE.Units(key).Amount);
            foreach (var key in InventorToolWeaponE.Keys) objs.Add(InventorToolWeaponE.ToolWeapons<AmountC>(key).Amount);


            foreach (var key in WhereUnitsE.Keys) objs.Add(WhereUnitsE.HaveUnit(key).Have);
            foreach (var key in WhereBuildsE.Keys) objs.Add(WhereBuildsE.HaveBuild<HaveBuildingC>(key).Have);
            foreach (var key in EntWhereEnviroments.Keys) objs.Add(EntWhereEnviroments.HaveEnv(key).Have);


            //foreach (var item in PickUpgC.HaveUpgrades) objs.Add(item.Value);
            //foreach (var item in UnitAvailPickUpgC.Available_0) objs.Add(item.Value);
            //foreach (var item in BuildAvailPickUpgC.Available) objs.Add(item.Value);
            //foreach (var item in WaterAvailPickUpgC.Available) objs.Add(item.Value);


            #region Other

            objs.Add(Entities.WhoseMoveE.WhoseMove.Player);
            objs.Add(Entities.WinnerE.Winner.Player);
            objs.Add(EntityPool.GameInfo<IsStartedGameC>().IsStartedGame);
            objs.Add(EntityPool.Ready<IsReadyC>(PlayerTypes.Second).IsReady);

            objs.Add(EntityPool.GameInfo<AmountMotionsC>().Amount);

            objs.Add(Entities.WindE.CenterCloud.Idx);
            //foreach (var item in WindC.Directs) objs.Add(item.Value);
            //objs.Add(WindC.CurDirWind);

            #endregion


            var objects = new object[objs.Count];
            for (int i = 0; i < objects.Length; i++) objects[i] = objs[i];


            EntityPool.Rpc.RPC(nameof(SyncAllOther), RpcTarget.Others, objects);

            EntityPool.Rpc.RPC(nameof(UpdateDataAndView), RpcTarget.All, new object[] { });
        }

        [PunRPC]
        void SyncAllOther(object[] objects)
        {
            _idx_cur = 0;


            foreach (byte idx_0 in Idxs)
            {
                CellUnitEs.Else(idx_0).UnitC.Unit = (UnitTypes)objects[_idx_cur++];
                CellUnitEs.Else(idx_0).LevelC.Level = (LevelTypes)objects[_idx_cur++];
                CellUnitEs.Else(idx_0).OwnerC.Player = (PlayerTypes)objects[_idx_cur++];
                CellUnitEs.Hp(idx_0).AmountC.Amount = (int)objects[_idx_cur++];
                CellUnitEs.Step(idx_0).AmountC.Amount = (int)objects[_idx_cur++];
                CellUnitEs.Water(idx_0).AmountC.Amount = (int)objects[_idx_cur++];

                CellUnitEs.Else(idx_0).ConditionC.Condition = (ConditionUnitTypes)objects[_idx_cur++];
                //foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_0).Have = (bool)objects[_idx_cur++];


                CellUnitEs.ToolWeapon(idx_0).ToolWeaponC.ToolWeapon = (ToolWeaponTypes)objects[_idx_cur++];
                CellUnitEs.ToolWeapon(idx_0).LevelC.Level = (LevelTypes)objects[_idx_cur++];
                CellUnitEs.ToolWeapon(idx_0).Protection.Amount = (int)objects[_idx_cur++];


                CellUnitEs.Stun(idx_0).ForExitStun.Amount = (int)objects[_idx_cur++];

                CellUnitEs.Else(idx_0).CornedC.IsCornered = (bool)objects[_idx_cur++];

                foreach (var item in CellUnitEs.CooldownKeys) CellUnitEs.CooldownUnique(item, idx_0).Cooldown.Amount = (int)objects[_idx_cur++];



                CellBuildEs.Build(idx_0).BuildTC.Build = (BuildingTypes)objects[_idx_cur++];
                CellBuildEs.Build(idx_0).PlayerTC.Player = (PlayerTypes)objects[_idx_cur++];


                foreach (var item_0 in CellEnvironmentEs.KeysEnvironment)
                {
                    Environment(item_0, idx_0).Resources.Amount = (int)objects[_idx_cur++];
                }

                CellRiverEs.River(idx_0).RiverTC.River = (RiverTypes)objects[_idx_cur++];
                foreach (var dir in CellRiverEs.Keys)
                    CellRiverEs.HaveRive(dir, idx_0).HaveRiver.Have = (bool)objects[_idx_cur++];



                foreach (var item_0 in CellTrailEs.Keys)
                    Trail(item_0, idx_0).Health.Amount = (int)objects[_idx_cur++];



                CellFireEs.Fire(idx_0).Fire.Have = (bool)objects[_idx_cur++];
            }


            EntityPool.ScoutHeroCooldown(UnitTypes.Scout, PlayerTypes.First).Amount = (int)objects[_idx_cur++];
            EntityPool.ScoutHeroCooldown(UnitTypes.Scout, PlayerTypes.Second).Amount = (int)objects[_idx_cur++];
            EntityPool.ScoutHeroCooldown(UnitTypes.Elfemale, PlayerTypes.First).Amount = (int)objects[_idx_cur++];
            EntityPool.ScoutHeroCooldown(UnitTypes.Elfemale, PlayerTypes.Second).Amount = (int)objects[_idx_cur++];



            foreach (var key in StatUnitsUpgradesE.Keys) StatUnitsUpgradesE.Upgrade<HaveUpgradeC>(key).Have = (bool)objects[_idx_cur++];
            //foreach (var key in BuildingUpgradesEnt.Keys) BuildingUpgradesEnt.Upgrade<HaveUpgradeC>(key).Have = (bool)objects[_idx_cur++];


            foreach (var key in InventorResourcesE.Keys) InventorResourcesE.Resource(key).Amount = (int)objects[_idx_cur++];
            foreach (var key in InventorUnitsE.Keys) InventorUnitsE.Units(key).Amount = (int)objects[_idx_cur++];
            foreach (var key in InventorToolWeaponE.Keys) InventorToolWeaponE.ToolWeapons<AmountC>(key).Amount = (int)objects[_idx_cur++];


            foreach (var key in WhereUnitsE.Keys) WhereUnitsE.HaveUnit(key).Have = (bool)objects[_idx_cur++];
            foreach (var key in WhereBuildsE.Keys) WhereBuildsE.HaveBuild<HaveBuildingC>(key).Have = (bool)objects[_idx_cur++];
            foreach (var key in EntWhereEnviroments.Keys) EntWhereEnviroments.HaveEnv(key).Have = (bool)objects[_idx_cur++];


            //foreach (var item in PickUpgC.HaveUpgrades) PickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in UnitAvailPickUpgC.Available_0) UnitAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in BuildAvailPickUpgC.Available) BuildAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);
            //foreach (var item in WaterAvailPickUpgC.Available) WaterAvailPickUpgC.Sync(item.Key, (bool)objects[_idx_cur++]);


            #region Other

            Entities.WhoseMoveE.WhoseMove.Player = (PlayerTypes)objects[_idx_cur++];
            Entities.WinnerE.Winner.Player = (PlayerTypes)objects[_idx_cur++];
            EntityPool.GameInfo<IsStartedGameC>().IsStartedGame = (bool)objects[_idx_cur++];
            EntityPool.Ready<IsReadyC>(Entities.WhoseMoveE.CurPlayerI).IsReady = (bool)objects[_idx_cur++];


            EntityPool.GameInfo<AmountMotionsC>().Amount = (int)objects[_idx_cur++];

            Entities.WindE.CenterCloud.Idx = (byte)objects[_idx_cur++];
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