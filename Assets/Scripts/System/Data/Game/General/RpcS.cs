﻿using Photon.Pun;
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
                        EntitiesMaster.Seed<IdxC>().Idx = (byte)objects[_idx_cur++];
                        EntitiesMaster.Seed<EnvironmetC>().Environment = (EnvironmentTypes)objects[_idx_cur++];
                        break;

                    case AbilityTypes.FireArcher:
                        EntitiesMaster.FireArcher<IdxFromToC>().From = (byte)objects[_idx_cur++];
                        EntitiesMaster.FireArcher<IdxFromToC>().To = (byte)objects[_idx_cur++];
                        break;

                    case AbilityTypes.GrowAdultForest:
                        EntitiesMaster.GrowAdultForest<IdxC>().Idx = (byte)objects[_idx_cur++];
                        break;

                    case AbilityTypes.StunElfemale:
                        EntitiesMaster.StunElfemale<IdxFromToC>().Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case AbilityTypes.ChangeDirectionWind:
                        EntitiesMaster.ChangeDirectionWind<IdxFromToC>().Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case AbilityTypes.ChangeCornerArcher:
                        IdxDoingMC.Set((byte)objects[_idx_cur++]);
                        break;

                    case AbilityTypes.FreezeDirectEnemy:
                        EntitiesMaster.FreezeDirectEnemy.IdxFromToC.Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case AbilityTypes.IceWall:
                        EntitiesMaster.IceWall.IdxC.Idx = (byte)objects[_idx_cur++];
                        break;

                    default: throw new Exception();
                }

                EntitiesMaster.UniqueAbilityC.Ability = uniqAbil;
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
                        EntitiesMaster.Build<IdxC>().Idx = (byte)objects[_idx_cur++];
                        EntitiesMaster.Build<BuildingTC>().Build = (BuildingTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.DestroyBuild:
                        EntitiesMaster.DestroyIdxC.Idx = (byte)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.Shift:
                        EntitiesMaster.Shift<IdxFromToC>().Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.Attack:
                        EntitiesMaster.Attack.From = (byte)objects[_idx_cur++];
                        EntitiesMaster.Attack.To = (byte)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.ConditionUnit:
                        EntitiesMaster.ConditionUnit<ConditionUnitC>().Condition = (ConditionUnitTypes)objects[_idx_cur++];
                        EntitiesMaster.ConditionUnit<IdxC>().Idx = (byte)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.CreateUnit:
                        EntitiesMaster.CreateUnit<UnitTC>().Unit = (UnitTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.MeltOre:
                        break;

                    case RpcMasterTypes.SetUnit:
                        EntitiesMaster.SetUnit<IdxC>().Idx = (byte)objects[_idx_cur++];
                        EntitiesMaster.SetUnit<UnitTC>().Unit = (UnitTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.BuyRes:
                        EntitiesMaster.BuyResources<ResourceTypeC>().Resource = (ResourceTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.ToNewUnit:
                        EntitiesMaster.ScoutOldNew<UnitTC>().Unit = (UnitTypes)objects[_idx_cur++];
                        EntitiesMaster.ScoutOldNew<IdxC>().Idx = (byte)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.CreateHeroFromTo:
                        EntitiesMaster.CreateHeroFromTo<UnitTC>().Unit = (UnitTypes)objects[_idx_cur++];
                        EntitiesMaster.CreateHeroFromTo<IdxFromToC>().Set((byte)objects[_idx_cur++], (byte)objects[_idx_cur++]);
                        break;

                    case RpcMasterTypes.UpgradeCellUnit:
                        EntitiesMaster.UpgradeUnit<IdxC>().Idx = (byte)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.GiveTakeToolWeapon:
                        EntitiesMaster.GiveTakeToolWeapon<ToolWeaponC>().ToolWeapon = (ToolWeaponTypes)objects[_idx_cur++];
                        EntitiesMaster.GiveTakeToolWeapon<LevelTC>().Level = (LevelTypes)objects[_idx_cur++];
                        EntitiesMaster.GiveTakeToolWeapon<IdxC>().Idx = (byte)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.GetHero:
                        EntitiesMaster.ForGetHero.Unit = (UnitTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.UpgCenterUnits:
                        EntitiesMaster.UpgradeCenterUnit<UnitTC>().Unit = (UnitTypes)objects[_idx_cur++];
                        break;

                    case RpcMasterTypes.UpgCenterBuild:
                        EntitiesMaster.Build<BuildingTC>().Build = (BuildingTypes)objects[_idx_cur++];
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

                    Entities.Sound(ClipTypes.Mistake).Sound.Invoke();
                    break;

                case RpcGeneralTypes.SoundEff:
                    Entities.Sound((ClipTypes)objects[_idx_cur++]).Sound.Invoke();
                    break;

                case RpcGeneralTypes.SoundUniqueAbility:
                    Entities.Sound((AbilityTypes)objects[_idx_cur++]).Sound.Invoke();
                    break;

                case RpcGeneralTypes.SoundRpcMaster:
                    //Sound((UniqueAbilityTypes)objects[_idx_cur++]).Invoke();
                    break;

                case RpcGeneralTypes.ActiveMotion:
                    Entities.Motion.IsActiveC.IsActive = true;
                    break;

                default:
                    throw new Exception();
            }
        }

        [PunRPC]
        void OtherRpc(object[] objects, PhotonMessageInfo infoFrom) => Entities.Rpc.OtherRpc(objects, infoFrom);


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

            objs.Add(Entities.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.First).Cooldown.Amount);
            objs.Add(Entities.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.Second).Cooldown.Amount);
            objs.Add(Entities.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.First).Cooldown.Amount);
            objs.Add(Entities.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.Second).Cooldown.Amount);



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

            objs.Add(Entities.WhoseMove.WhoseMove.Player);
            objs.Add(Entities.WinnerE.Winner.Player);
            objs.Add(Entities.GameInfo.IsStartedGameC.IsStartedGame);
            objs.Add(Entities.Ready(PlayerTypes.Second).IsReadyC.IsReady);

            objs.Add(Entities.Motion.AmountMotions.Amount);

            objs.Add(Entities.WindE.CenterCloud.Idx);
            //foreach (var item in WindC.Directs) objs.Add(item.Value);
            //objs.Add(WindC.CurDirWind);

            #endregion


            var objects = new object[objs.Count];
            for (int i = 0; i < objects.Length; i++) objects[i] = objs[i];


            Entities.Rpc.RPC(nameof(SyncAllOther), RpcTarget.Others, objects);

            Entities.Rpc.RPC(nameof(UpdateDataAndView), RpcTarget.All, new object[] { });
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


            Entities.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.First).Cooldown.Amount = (int)objects[_idx_cur++];
            Entities.ScoutHeroCooldownE(UnitTypes.Scout, PlayerTypes.Second).Cooldown.Amount = (int)objects[_idx_cur++];
            Entities.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.First).Cooldown.Amount = (int)objects[_idx_cur++];
            Entities.ScoutHeroCooldownE(UnitTypes.Elfemale, PlayerTypes.Second).Cooldown.Amount = (int)objects[_idx_cur++];



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

            Entities.WhoseMove.WhoseMove.Player = (PlayerTypes)objects[_idx_cur++];
            Entities.WinnerE.Winner.Player = (PlayerTypes)objects[_idx_cur++];
            Entities.GameInfo.IsStartedGameC.IsStartedGame = (bool)objects[_idx_cur++];
            Entities.Ready(Entities.WhoseMove.CurPlayerI).IsReadyC.IsReady = (bool)objects[_idx_cur++];


            Entities.Motion.AmountMotions.Amount = (int)objects[_idx_cur++];

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