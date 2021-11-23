using Leopotam.Ecs;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class DataMasSCreate
    {
        public DataMasSCreate(EcsSystems gameSysts)
        {
            var gameWorld = gameSysts.World;


            var runUpd = new EcsSystems(gameWorld);


            var updateMotion = new EcsSystems(gameWorld)
                .Add(new UpdatorMS())
                .Add(new ExtractBuildUpdMS())
                .Add(new FireUpdMasSys())
                .Add(new CloudUpdMS())
                .Add(new ThirstyUpdMS())
                .Add(new RelaxUpdMasSys())
                .Add(new HungryUpdMS());

            var truceSystems = new EcsSystems(gameWorld)
                .Add(new TruceMS());

            var systs = new Dictionary<MastDataSysTypes, Action>();
            systs.Add(MastDataSysTypes.Update, updateMotion.Run);
            systs.Add(MastDataSysTypes.Truce, truceSystems.Run);



            var rpcSystems = new Dictionary<RpcMasterTypes, EcsSystems>();
            rpcSystems.Add(RpcMasterTypes.Build, new EcsSystems(gameWorld)
                .Add(new BuildMineMastSys())
                .Add(new BuildFarmMastSys())
                .Add(new BuildCityMastSys()));
            rpcSystems.Add(RpcMasterTypes.DestroyBuild, new EcsSystems(gameWorld).Add(new DestroyMS()));
            rpcSystems.Add(RpcMasterTypes.Shift, new EcsSystems(gameWorld).Add(new ShiftUnitMS()));
            rpcSystems.Add(RpcMasterTypes.Attack, new EcsSystems(gameWorld).Add(new AttackMS()));
            rpcSystems.Add(RpcMasterTypes.ConditionUnit, new EcsSystems(gameWorld).Add(new ConditionMasterSystem()));
            rpcSystems.Add(RpcMasterTypes.Ready, new EcsSystems(gameWorld).Add(new ReadyMasterSystem()));
            rpcSystems.Add(RpcMasterTypes.Done, new EcsSystems(gameWorld).Add(new DonerMastSys()));
            rpcSystems.Add(RpcMasterTypes.CreateUnit, new EcsSystems(gameWorld).Add(new CreateUnitMastSys()));
            rpcSystems.Add(RpcMasterTypes.MeltOre, new EcsSystems(gameWorld).Add(new MeltOreMasterSystem()));
            rpcSystems.Add(RpcMasterTypes.SetUnit, new EcsSystems(gameWorld).Add(new SetterUnitMS()));
            rpcSystems.Add(RpcMasterTypes.BuyRes, new EcsSystems(gameWorld).Add(new BuyResMastS()));
            rpcSystems.Add(RpcMasterTypes.UpgradeUnit, new EcsSystems(gameWorld).Add(new UpgUnitMS()));
            rpcSystems.Add(RpcMasterTypes.ToNewUnit, new EcsSystems(gameWorld).Add(new ScoutOldNewSys()));
            rpcSystems.Add(RpcMasterTypes.GiveTakeToolWeapon, new EcsSystems(gameWorld).Add(new GiveTakeTWMasSys()));
            rpcSystems.Add(RpcMasterTypes.GetHero, new EcsSystems(gameWorld).Add(new GetHeroMS()));
            rpcSystems.Add(RpcMasterTypes.FromToNewUnit, new EcsSystems(gameWorld).Add(new FromToNewUnitMS()));
            rpcSystems.Add(RpcMasterTypes.UpgUnits, new EcsSystems(gameWorld).Add(new PickUpgUnitsMS()));
            rpcSystems.Add(RpcMasterTypes.UpgBuilds, new EcsSystems(gameWorld).Add(new PickUpgBuildsMS()));
            rpcSystems.Add(RpcMasterTypes.UpgWater, new EcsSystems(gameWorld).Add(new WaterUpgMS()));
            var rpcSystsAction = new Dictionary<RpcMasterTypes, Action>();
            foreach (var item_0 in rpcSystems) rpcSystsAction.Add(item_0.Key, item_0.Value.Run);


            var uniqSys = new Dictionary<UniqAbilTypes, EcsSystems>();
            uniqSys.Add(UniqAbilTypes.Seed, new EcsSystems(gameWorld).Add(new SeedingMS()));
            uniqSys.Add(UniqAbilTypes.StunElfemale, new EcsSystems(gameWorld).Add(new StunElfemaleMS()));
            uniqSys.Add(UniqAbilTypes.FirePawn, new EcsSystems(gameWorld).Add(new FirePawnMS()));
            uniqSys.Add(UniqAbilTypes.PutOutFirePawn, new EcsSystems(gameWorld).Add(new PutOutFireMS()));
            uniqSys.Add(UniqAbilTypes.FireArcher, new EcsSystems(gameWorld).Add(new FireArcherMS()));
            uniqSys.Add(UniqAbilTypes.GrowAdultForest, new EcsSystems(gameWorld).Add(new GrowAdultForestMS()));
            uniqSys.Add(UniqAbilTypes.CircularAttack, new EcsSystems(gameWorld).Add(new CircularAttackKingMastSys()));
            uniqSys.Add(UniqAbilTypes.BonusNear, new EcsSystems(gameWorld).Add(new BonusNearUnitKingMS()));
            uniqSys.Add(UniqAbilTypes.ChangeDirWind, new EcsSystems(gameWorld).Add(new ChangeDirWindMS()));
            uniqSys.Add(UniqAbilTypes.ChangeCornerArcher, new EcsSystems(gameWorld).Add(new ChangeCornerArcherMS()));

            var uniqSysAction = new Dictionary<UniqAbilTypes, Action>();
            foreach (var item_0 in uniqSys) uniqSysAction.Add(item_0.Key, item_0.Value.Run);


            var list = new List<object>()
            {
                (Action)runUpd.Run,
                systs,
                rpcSystsAction,
                uniqSysAction
            };
            new DataMastSC(list);


            gameSysts
                .Add(runUpd)
                .Add(truceSystems)
                .Add(updateMotion);

            foreach (var system in rpcSystems.Values) gameSysts.Add(system);
            foreach (var system in uniqSys.Values) gameSysts.Add(system);

        }
    }
}