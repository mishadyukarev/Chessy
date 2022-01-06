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
                .Add(new FireUpdMS())
                .Add(new CloudUpdMS())

                .Add(new ExtractUnitUpdMS())
                .Add(new ResumeUnitUpdMS())
                .Add(new HealingUnitUpdMS())
                .Add(new HungryUpdMS())
                .Add(new ThirstyUpdMS());

            var truceSystems = new EcsSystems(gameWorld)
                .Add(new TruceMS());

            var systs = new Dictionary<MastDataSysTypes, Action>();
            systs.Add(MastDataSysTypes.Update, updateMotion.Run);
            systs.Add(MastDataSysTypes.Truce, truceSystems.Run);



            var rpcSystems = new Dictionary<RpcMasterTypes, EcsSystems>();
            rpcSystems.Add(RpcMasterTypes.Build, new EcsSystems(gameWorld)
                .Add(new BuildMineMastSys())
                .Add(new BuildFarmMS())
                .Add(new BuildCityMS()));
            rpcSystems.Add(RpcMasterTypes.DestroyBuild, new EcsSystems(gameWorld).Add(new DestroyMS()));
            rpcSystems.Add(RpcMasterTypes.Shift, new EcsSystems(gameWorld).Add(new ShiftUnitMS()));
            rpcSystems.Add(RpcMasterTypes.Attack, new EcsSystems(gameWorld).Add(new AttackMS()));
            rpcSystems.Add(RpcMasterTypes.ConditionUnit, new EcsSystems(gameWorld).Add(new ConditionMS()));
            rpcSystems.Add(RpcMasterTypes.Ready, new EcsSystems(gameWorld).Add(new ReadyMS()));
            rpcSystems.Add(RpcMasterTypes.Done, new EcsSystems(gameWorld).Add(new DonerMS()));
            rpcSystems.Add(RpcMasterTypes.CreateUnit, new EcsSystems(gameWorld).Add(new CreateUnitMastSys()));
            rpcSystems.Add(RpcMasterTypes.MeltOre, new EcsSystems(gameWorld).Add(new MeltOreMS()));
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


            var uniqSys = new Dictionary<UniqueAbilTypes, EcsSystems>();
            uniqSys.Add(UniqueAbilTypes.Seed, new EcsSystems(gameWorld).Add(new SeedingMS()));
            uniqSys.Add(UniqueAbilTypes.StunElfemale, new EcsSystems(gameWorld).Add(new StunElfemaleMS()));
            uniqSys.Add(UniqueAbilTypes.FirePawn, new EcsSystems(gameWorld).Add(new FirePawnMS()));
            uniqSys.Add(UniqueAbilTypes.PutOutFirePawn, new EcsSystems(gameWorld).Add(new PutOutFireMS()));
            uniqSys.Add(UniqueAbilTypes.FireArcher, new EcsSystems(gameWorld).Add(new FireArcherMS()));
            uniqSys.Add(UniqueAbilTypes.GrowAdultForest, new EcsSystems(gameWorld).Add(new GrowAdultForestMS()));
            uniqSys.Add(UniqueAbilTypes.CircularAttack, new EcsSystems(gameWorld).Add(new CircularAttackKingMS()));
            uniqSys.Add(UniqueAbilTypes.BonusNear, new EcsSystems(gameWorld).Add(new BonusNearUnitKingMS()));
            uniqSys.Add(UniqueAbilTypes.ChangeDirWind, new EcsSystems(gameWorld).Add(new ChangeDirWindMS()));
            uniqSys.Add(UniqueAbilTypes.ChangeCornerArcher, new EcsSystems(gameWorld).Add(new ChangeCornerArcherMS()));

            var uniqSysAction = new Dictionary<UniqueAbilTypes, Action>();
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