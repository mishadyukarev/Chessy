using System;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class SystemDataMasterManager
    {
        public SystemDataMasterManager()
        {

            var systs = new Dictionary<MastDataSysTypes, Action>();

            var action =
                (Action)new UpdatorMS().Run
                + new UpdatorMS().Run
                + new ExtractBuildUpdMS().Run
                + new FireUpdMS().Run
                + new CloudUpdMS().Run

                + new ExtractUnitUpdMS().Run
                + new ResumeUnitUpdMS().Run
                + new HealingUnitUpdMS().Run
                + new HungryUpdMS().Run
                + new ThirstyUpdMS().Run;
            systs.Add(MastDataSysTypes.Update, action);

            action = new TruceMS().Run;
            systs.Add(MastDataSysTypes.Truce, action);



            var rpcSystems = new Dictionary<RpcMasterTypes, Action>();
            rpcSystems.Add(RpcMasterTypes.Build, (Action)new BuildMineMastSys().Run
                + new BuildFarmMS().Run
                + new BuildCityMS().Run);
            rpcSystems.Add(RpcMasterTypes.DestroyBuild, new DestroyMS().Run);
            rpcSystems.Add(RpcMasterTypes.Shift, new ShiftUnitMS().Run);
            rpcSystems.Add(RpcMasterTypes.Attack, new AttackMS().Run);
            rpcSystems.Add(RpcMasterTypes.ConditionUnit, new ConditionMS().Run);
            rpcSystems.Add(RpcMasterTypes.Ready, new ReadyMS().Run);
            rpcSystems.Add(RpcMasterTypes.Done, new DonerMS().Run);
            rpcSystems.Add(RpcMasterTypes.CreateUnit, new CreateUnitMastSys().Run);
            rpcSystems.Add(RpcMasterTypes.MeltOre, new MeltOreMS().Run);
            rpcSystems.Add(RpcMasterTypes.SetUnit, new SetterUnitMS().Run);
            rpcSystems.Add(RpcMasterTypes.BuyRes, new BuyResMastS().Run);
            rpcSystems.Add(RpcMasterTypes.UpgradeUnit, new UpgUnitMS().Run);
            rpcSystems.Add(RpcMasterTypes.ToNewUnit, new ScoutOldNewSys().Run);
            rpcSystems.Add(RpcMasterTypes.GiveTakeToolWeapon, new GiveTakeTWMasSys().Run);
            rpcSystems.Add(RpcMasterTypes.GetHero, new GetHeroMS().Run);
            rpcSystems.Add(RpcMasterTypes.FromToNewUnit, new FromToNewUnitMS().Run);
            rpcSystems.Add(RpcMasterTypes.UpgUnits, new PickUpgUnitsMS().Run);
            rpcSystems.Add(RpcMasterTypes.UpgBuilds, new PickUpgBuildsMS().Run);
            rpcSystems.Add(RpcMasterTypes.UpgWater, new WaterUpgMS().Run);


            var uniqSys = new Dictionary<UniqueAbilTypes, Action>();
            uniqSys.Add(UniqueAbilTypes.Seed, new SeedingMS().Run);
            uniqSys.Add(UniqueAbilTypes.StunElfemale, new StunElfemaleMS().Run);
            uniqSys.Add(UniqueAbilTypes.FirePawn, new FirePawnMS().Run);
            uniqSys.Add(UniqueAbilTypes.PutOutFirePawn, new PutOutFireMS().Run);
            uniqSys.Add(UniqueAbilTypes.FireArcher, new FireArcherMS().Run);
            uniqSys.Add(UniqueAbilTypes.GrowAdultForest, new GrowAdultForestMS().Run);
            uniqSys.Add(UniqueAbilTypes.CircularAttack, new CircularAttackKingMS().Run);
            uniqSys.Add(UniqueAbilTypes.BonusNear, new BonusNearUnitKingMS().Run);
            uniqSys.Add(UniqueAbilTypes.ChangeDirWind, new ChangeDirWindMS().Run);
            uniqSys.Add(UniqueAbilTypes.ChangeCornerArcher, new ChangeCornerArcherMS().Run);

            var list = new List<object>()
            {
                (Action)default,
                systs,
                rpcSystems,
                uniqSys
            };
            new DataMastSC(list);
        }
    }
}